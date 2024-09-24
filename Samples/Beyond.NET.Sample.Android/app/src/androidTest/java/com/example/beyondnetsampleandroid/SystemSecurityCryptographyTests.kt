// NOTE: This requires OpenSSL libraries (.so files).
// Right now, libcrypto.so and libssl.so included in this repo are sourced from https://github.com/Sharm/Prebuilt-OpenSSL-Android which is ancient.
// TODO: Find a better way to include OpenSSL

package com.example.beyondnetsampleandroid

import androidx.test.ext.junit.runners.AndroidJUnit4

import org.junit.Test
import org.junit.runner.RunWith

import org.junit.Assert.*

import com.example.beyondnetsampleandroid.dn.*

@RunWith(AndroidJUnit4::class)
class SystemSecurityCryptographyTests {
    @Test
    fun testAES() {
        val unencryptedData = "Hello 🌎!"

        val encryptResult = encrypt(unencryptedData)
        val encryptedData = encryptResult.encryptedData
        val iv = encryptResult.iv
        val key = encryptResult.key

        assertNotEquals(encryptedData, "")

        val decryptedData = decrypt(encryptedData, iv, key)

        assertEquals(decryptedData, unencryptedData)
    }

    class EncryptRet(val encryptedData: String, val iv: DNArray<System_Byte>, val key: DNArray<System_Byte>)

    private fun encrypt(data: String): EncryptRet {
        val aes = System_Security_Cryptography_Aes.create()

        aes.generateIV()
        aes.generateKey()

        val iv = aes.iV_get()
        val key = aes.key_get()

        val encryptor = aes.createEncryptor()
        val memoryStream = System_IO_MemoryStream()

        val cryptoStream = System_Security_Cryptography_CryptoStream(memoryStream, encryptor, System_Security_Cryptography_CryptoStreamMode.WRITE)
        val streamWriter = System_IO_StreamWriter(cryptoStream)

        streamWriter.write(data.toDotNETString())
        cryptoStream.flush()

        streamWriter.dispose()
        cryptoStream.dispose()
        memoryStream.dispose()
        aes.dispose()

        val encryptedData = memoryStream.toArray()

        val base64EncryptedDataDN = System_Convert.toBase64String(encryptedData)
        val base64EncryptedData = base64EncryptedDataDN.toKString()

        return EncryptRet(base64EncryptedData, iv, key)
    }

    private fun decrypt(data: String, iv: DNArray<System_Byte>, key: DNArray<System_Byte>): String {
        val buffer = System_Convert.fromBase64String(data.toDotNETString())

        val aes = System_Security_Cryptography_Aes.create()
        aes.iV_set(iv)
        aes.key_set(key)

        val decryptor = aes.createDecryptor()
        val memoryStream = System_IO_MemoryStream(buffer)

        val cryptoStream = System_Security_Cryptography_CryptoStream(memoryStream, decryptor, System_Security_Cryptography_CryptoStreamMode.READ)
        val streamReader = System_IO_StreamReader(cryptoStream)

        cryptoStream.flush()

        val decryptedDataDN = streamReader.readToEnd()
        val decryptedData = decryptedDataDN.toKString()

        cryptoStream.dispose()
        memoryStream.dispose()
        aes.dispose()
        streamReader.dispose()

        return decryptedData
    }
}