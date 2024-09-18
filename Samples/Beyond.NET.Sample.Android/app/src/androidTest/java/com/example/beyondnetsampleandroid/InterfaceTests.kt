package com.example.beyondnetsampleandroid

import androidx.test.ext.junit.runners.AndroidJUnit4

import org.junit.Test
import org.junit.runner.RunWith

import org.junit.Assert.*

import com.example.beyondnetsampleandroid.dn.*

interface IHaveDifferentNullability {
    fun nullableReturnType(): String?
    fun nonNullableParameter(str: String)
}

class ImplWithDifferentNullability: IHaveDifferentNullability {
    // Overriding a nullable parameter with a non-nullable one DOES work
    override fun nullableReturnType(): String {
        return ""
    }

    // TODO: Overriding a non-nullable parameter with a nullable one does NOT work
//    override fun nonNullableParameter(str: String?) {
//
//    }

    override fun nonNullableParameter(str: String) {

    }
}

@RunWith(AndroidJUnit4::class)
class InterfaceTests {
    @Test
    fun testInterfaces() {
        // TODO
    }
}