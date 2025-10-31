#!/usr/bin/env pwsh
#Requires -PSEdition Core -Version 7.4
Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'
$PSNativeCommandUseErrorActionPreference = $true
$PSStyle.OutputRendering = 'ANSI'

if (-not $IsMacOS) {
    throw "This script only supports macOS"
}
if (-not "${env:ANDROID_HOME}") {
    throw "ANDROID_HOME environment variable must be set"
}

$adb = Join-Path $env:ANDROID_HOME 'platform-tools/adb' -Resolve
$emulator = Join-Path $env:ANDROID_HOME 'emulator/emulator' -Resolve
$avd = $env:ANDROID_AVD
if (-not "$avd") {
    $avd = 'Pixel_9_Pro'
    Write-Warning "ANDROID_AVD environment variable not set, falling back to '${avd}'"
}

$port = 5566 # [System.Random]::Shared.Next(5555, 5587)
Write-Host "Using ${emulator} with ${avd}"
Write-Host "Using ${adb} with port ${port}"

[scriptblock]$start_emulator = { param($emulator, $avd, $port)
    & $emulator -avd $avd -port $port -no-audio -no-boot-anim -no-window -no-snapshot-save -gpu off
}
$job = Start-Job -Name 'run-emulator' -WorkingDirectory $PSScriptRoot -ScriptBlock $start_emulator -ArgumentList @($emulator, $avd, $port)

try {
    # give the emulator a couple of seconds to boot up
    Wait-Job $job -Timeout 2

    $finished_booting = $false

    for ($try = 1; $try -le 20; $try++) {
        Write-Host "Checking emulator boot status (attempt #${try})"

        $status = & {
            $PSNativeCommandUseErrorActionPreference = $false
            & $adb -s "emulator-${port}" shell getprop sys.boot_completed
        }

        $status = "$status".Trim()
        Write-Host "Emulator status: ${status}"

        if ($status -eq "1") {
            $finished_booting = $true
            break
        }

        Start-Sleep -Seconds 2
    }

    if (-not $finished_booting) {
        throw "Checking emulator status timed out"
    }

    Start-Sleep -Seconds 2

    # unlock screen
    & {
        $PSNativeCommandUseErrorActionPreference = $false
        & $adb -s "emulator-${port}" shell 'input keyevent 82'
    }

    # run tests
    & $PSScriptRoot/gradlew connectedAndroidTest --warning-mode all

    # shut emulator down gracefully
    & $adb -s "emulator-${port}" emu kill
}
catch {
    # if there was a failure, it's possible that the `emulator` running in the background
    # encountered an issue -- print its output before terminating
    Receive-Job $job -Keep

    throw "See emulator output above"
}
finally {
    # force kill emulator process
    Remove-Job $job -Force
}
