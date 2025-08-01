package com.example.beyondnetsampleandroid

import androidx.test.ext.junit.runners.AndroidJUnit4
import com.example.beyondnetsampleandroid.dn.*

import org.junit.Assert.*
import org.junit.*
import org.junit.runner.*

@RunWith(AndroidJUnit4::class)
class IndexerTests {
    // NOTE: This was transpiled from Swift
    @Test
    fun testGetIndexedPropertyWith3Parameters() {
        val indexerTests = Beyond_NET_Sample_IndexerTests()

        val aString = "A String"
        val aStringDN = aString.toDotNETString()
        val aNumber = 123456
        val aGuid = System_Guid.newGuid()

        val arrayRet = indexerTests.item(aStringDN, aNumber, aGuid)
        val arrayLength = arrayRet.length
        assertEquals(3, arrayLength)

        val item1RetAsString = try {
            arrayRet.getValue(0)?.castAs(System_String)?.toKString()
        } catch (_: Exception) {
            fail("System.Array.getValue(0) should not throw")
            return
        }
        assertEquals(aString, item1RetAsString)

        val item2RetAsInt32 = try {
            arrayRet.getValue(1)?.castToInt()
        } catch (_: Exception) {
            fail("System.Array.getValue(1) should not throw")
            return
        }
        assertEquals(aNumber, item2RetAsInt32)

        val item3Ret = try {
            arrayRet.getValue(2)?.castTo(System_Guid)
        } catch (_: Exception) {
            fail("System.Array.getValue(2) should not throw")
            return
        }
        assertTrue(aGuid == item3Ret)
    }

    // NOTE: This was transpiled from Swift
    @Test
    fun testSetIndexedPropertyWith3Parameters() {
        val indexerTests = Beyond_NET_Sample_IndexerTests()

        val aString = "A String"
        val aStringDN = aString.toDotNETString()
        val aNumber = 123456
        val aGuid = System_Guid.newGuid()

        val array = DNArray(System_Object, 3)
        array.setValue(aStringDN, 0)
        array.setValue(aNumber.toDotNETObject(), 1)
        array.setValue(aGuid, 2)

        indexerTests.item_set(aStringDN, aNumber, aGuid, array)

        val arrayRet = indexerTests.storedValue
        val arrayLength = arrayRet.length
        assertEquals(3, arrayLength)

        val item1RetAsString = try {
            arrayRet.getValue(0)?.castTo(System_String)?.toKString()
        } catch (_: Exception) {
            fail("System.Array.getValue(0) should not throw")
            return
        }
        assertEquals(aString, item1RetAsString)

        val storedString = indexerTests.storedString.toKString()
        assertEquals(aString, storedString)

        val item2RetAsInt32 = try {
            arrayRet.getValue(1)?.castToInt()
        } catch (e: Exception) {
            fail("System.Array.getValue(1) should not throw")
            return
        }
        assertEquals(aNumber, item2RetAsInt32)

        val storedNumber = indexerTests.storedNumber
        assertEquals(aNumber, storedNumber)

        val item3Ret = try {
            arrayRet.getValue(2)?.castTo(System_Guid)
        } catch (e: Exception) {
            fail("System.Array.getValue(2) should not throw")
            return
        }
        assertTrue(aGuid == item3Ret)

        val storedGuid = indexerTests.storedGuid
        assertTrue(aGuid == storedGuid)
    }
}
