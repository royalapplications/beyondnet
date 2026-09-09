package com.example.beyondnetsampleandroid

import androidx.test.ext.junit.runners.AndroidJUnit4
import com.example.beyondnetsampleandroid.dn.*
import com.example.beyondnetsampleandroid.dn.System

import org.junit.Assert.*
import org.junit.Test
import org.junit.runner.RunWith

@RunWith(AndroidJUnit4::class)
class NamespaceTests {
    @Test
    fun testTypeIdentity() {
        assertEquals(System_Guid::class.java, System.Guid::class.java)
        assertEquals(System_Int32::class.java, System.Int32::class.java)
        assertEquals(System_String::class.java, System.String::class.java)
        assertEquals(Beyond_NET_Sample_NiceLevels::class.java, Beyond.NET.Sample.NiceLevels::class.java)
        assertEquals(Beyond_NET_Sample_IAnimal::class.java, Beyond.NET.Sample.IAnimal::class.java)
        assertEquals(Beyond_NET_Sample_IAnimal_DNInterface::class.java, Beyond.NET.Sample.IAnimal_DNInterface::class.java)
        assertEquals(System_Action::class.java, System.Action::class.java)
        assertEquals(Beyond_NET_Sample_Source_NestedTypeTests_MyNestedType::class.java, Beyond.NET.Sample.Source.NestedTypeTests_MyNestedType::class.java)
    }

    @Test
    fun testConstructorsAndStaticMembers() {
        val version: System_Version = System.Version(1, 2, 3)
        assertEquals(1, version.major)
        assertEquals(2, version.minor)
        assertEquals(3, version.build)

        val guid: System_Guid = System.Guid.newGuid()
        assertFalse(guid.equals(System.Guid.empty))

        assertEquals(Beyond_NET_Sample_NiceLevels.VERY_NICE, Beyond.NET.Sample.NiceLevels.VERY_NICE)
        assertTrue(Beyond.NET.Sample.Source.NestedTypeTests_MyNestedType.prettyLonelyAroundHere)
    }
}
