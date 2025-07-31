package com.example.beyondnetsampleandroid

import android.os.Bundle
import android.util.Log
import androidx.activity.compose.*
import androidx.activity.*
import androidx.compose.foundation.layout.*
import androidx.compose.material3.*
import androidx.compose.runtime.*
import androidx.compose.ui.*
import androidx.compose.ui.tooling.preview.*

import java.util.UUID

import kotlin.time.*

import com.example.beyondnetsampleandroid.dn.*
import com.example.beyondnetsampleandroid.ui.theme.BeyondNETSampleAndroidTheme

class MainActivity : ComponentActivity() {
    private val LOG_TAG = "Beyond.NET.Sample.Android"

    private var _guidStr: String = ""
    private var guidText = mutableStateOf(_guidStr)

    private val numberOfIDs = 10_000

    private var _guidTime = Duration.ZERO
    private var guidTime = mutableStateOf(_guidTime)

    private var _uuidTime = Duration.ZERO
    private var uuidTime = mutableStateOf(_uuidTime)

    private var timer: System_Threading_Timer? = null
    private var _numberOfTimesTimerFired = 0
    private var numberOfTimesTimerFired = mutableIntStateOf(_numberOfTimesTimerFired)

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        // Should improve debugging failures while loading native libraries
        System.setProperty("jna.debug_load", "true")
        System.setProperty("jna.debug_jna_load", "true")

        enableEdgeToEdge()

        val timerCallback = System_Threading_TimerCallback { _ ->
            numberOfTimesTimerFired.intValue++
        }

        timer = System_Threading_Timer(
            timerCallback,
            null,
            0,
            250
        )

        updateGuid()

        val emptyGuid = System_Guid.empty
        require(emptyGuid.`is`(System_Guid.typeOf))

        val emptyGuidWithCtor = System_Guid()
        require(emptyGuid.dnToString().toKString() == emptyGuidWithCtor.dnToString().toKString())

        require(System_Object.equals(emptyGuid, emptyGuidWithCtor))
        require(System_Object.equals(System_Guid.typeOf, emptyGuid.getType()))

        val emptyStringDN = System_String.empty

        var exceptionString = "ERROR: We should run into an exception here"

        try {
            System_Guid.parse(emptyStringDN)
        } catch (e: Exception) {
            Log.d(LOG_TAG, "We caught an expected exception in System_Guid.parse: $e")

            exceptionString = e.toString()
        }

        val johnDoe = Beyond_NET_Sample_Person(
            "John".toDotNETString(),
            "Doe".toDotNETString(),
            50
        )

        val johnDoeName = johnDoe.fullName.toKString()
        val johnDoeAge = johnDoe.age
        johnDoe.niceLevel = Beyond_NET_Sample_NiceLevels.LITTLEBITNICE
        val johnDoeNiceLevel = johnDoe.niceLevel
        val welcomeMessage = johnDoe.getWelcomeMessage().toKString()

        require(johnDoeNiceLevel == Beyond_NET_Sample_NiceLevels.LITTLEBITNICE)

        setContent {
            BeyondNETSampleAndroidTheme {
                Scaffold(modifier = Modifier.fillMaxSize()) { innerPadding ->
                    Column(modifier = Modifier.padding(innerPadding)) {
                        Text("Hello, ${johnDoeName}! You're $johnDoeAge years old.")
                        Text(welcomeMessage)

                        Text("Timer fired ${numberOfTimesTimerFired.intValue} times.")

                        Text("Here's a new System.Guid for you: ${guidText.value}")

                        Button(onClick = { updateGuid() }) {
                            Text("New Guid")
                        }

                        Button(onClick = { measureGuidPerformances() }) {
                            Text("Measure Guid Performance")
                        }

                        Text("It took ${durationToString(guidTime.value)} to create $numberOfIDs System.Guids")
                        Text("It took ${durationToString(uuidTime.value)} to create $numberOfIDs Java UUIDs")

                        Text("Did we catch a .NET exception?")
                        Text(exceptionString)
                    }
                }
            }
        }
    }

    private fun measureGuidPerformances() {
        guidTime.value = measureGuidPerformance(numberOfIDs)
        uuidTime.value = measureUUIDPerformance(numberOfIDs)
    }

    private fun measureGuidPerformance(numberOfIDs: Int): Duration {
        System.gc()
        System_GC.collect()

        val t = measureTime {
            for (i in 0..numberOfIDs)
                makeGuidString()
        }

        System.gc()
        System_GC.collect()

        return t
    }

    private fun measureUUIDPerformance(numberOfIDs: Int): Duration {
        System.gc()

        val t = measureTime {
            for (i in 0..numberOfIDs)
                makeUUIDString()
        }

        System.gc()

        return t
    }

    private fun durationToString(duration: Duration): String {
        return duration.toString(DurationUnit.SECONDS, 3)
    }

    private fun makeGuidString(): String {
        val guid = System_Guid.newGuid()
        val guidStrDN = guid.dnToString()
        val guidStr = guidStrDN.toKString()

        return guidStr
    }

    private fun makeUUIDString(): String {
        val uuid = UUID.randomUUID()
        val uuidStr = uuid.toString()

        return uuidStr
    }

    private fun updateGuid() {
        guidText.value = makeGuidString()
    }
}

@Preview(showBackground = true)
@Composable
fun MainActivityPreview() {
    BeyondNETSampleAndroidTheme {
        MainActivity()
    }
}
