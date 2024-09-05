package com.example.beyondnetsampleandroid

import androidx.test.ext.junit.runners.AndroidJUnit4
import com.example.beyondnetsampleandroid.dn.*
import org.junit.Assert.*
import org.junit.Test
import org.junit.runner.RunWith

@RunWith(AndroidJUnit4::class)
class SystemVersionTests {
    @Test
    fun testSystemVersionFromComponents() {

        val major = 1
        val minor = 2
        val build = 3
        val revision = 4

        val versionString = "$major.$minor.$build.$revision"

        val version = System_Version(major,
            minor,
            build,
            revision)

        val systemVersionType = System_Version.typeOf()
        val versionFromComponentsType = version.getType()
        assertTrue(version.`is`(systemVersionType))
        assertTrue(systemVersionType == versionFromComponentsType)
        assertTrue(System_Object.equals(systemVersionType, versionFromComponentsType))

        val majorRet = version.major_get()
        assertEquals(major, majorRet)

        val minorRet = version.minor_get()
        assertEquals(minor, minorRet)

        val buildRet = version.build_get()
        assertEquals(build, buildRet)

        val revisionRet = version.revision_get()
        assertEquals(revision, revisionRet)

        assertEquals(versionString, version.dnToString().toKString())
    }
}