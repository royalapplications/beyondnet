package com.example.beyondnetsampleandroid

import android.os.Bundle
import androidx.activity.ComponentActivity
import androidx.activity.compose.setContent
import androidx.activity.enableEdgeToEdge
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.padding
import androidx.compose.material3.Scaffold
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.tooling.preview.Preview

import com.example.beyondnetsampleandroid.ui.theme.BeyondNETSampleAndroidTheme
import com.example.beyondnetsampleandroid.dn.BeyondDotNETSampleNative
import com.example.beyondnetsampleandroid.dn.value
import com.sun.jna.Native
import com.sun.jna.Pointer
import com.sun.jna.ptr.PointerByReference

class MainActivity : ComponentActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        // Should improve debugging failures while loading native libraries
        System.setProperty("jna.debug_load", "true")
        System.setProperty("jna.debug_jna_load", "true")

        val ex = PointerByReference()

        val guid = BeyondDotNETSampleNative.System_Guid_NewGuid(ex)
        require(ex.value == Pointer.NULL)
        require(guid.value != Pointer.NULL)

        val guidStrDN = BeyondDotNETSampleNative.System_Guid_ToString(guid, ex)
        require(ex.value == Pointer.NULL)
        require(guidStrDN.value != Pointer.NULL)

        val guidStrC = BeyondDotNETSampleNative.DNStringToC(guidStrDN)
        require(guidStrC.value != Pointer.NULL)

        val guidStr = guidStrC.getString(0)

        val guidStrCPtrVal = Pointer.nativeValue(guidStrC)
        Native.free(guidStrCPtrVal)

        BeyondDotNETSampleNative.System_Guid_Destroy(guid)

        enableEdgeToEdge()

        setContent {
            BeyondNETSampleAndroidTheme {
                Scaffold(modifier = Modifier.fillMaxSize()) { innerPadding ->
                    Text(
                        text = "New Guid: $guidStr",
                        modifier = Modifier.padding(innerPadding)
                    )
                }
            }
        }
    }
}

@Preview(showBackground = true)
@Composable
fun MainActivityPreview() {
    BeyondNETSampleAndroidTheme {
        MainActivity()
    }
}