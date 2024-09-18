import XCTest
import BeyondDotNETSampleKit

final class SystemSecurityCryptographyTests: XCTestCase {
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testAES() throws {
        let unencryptedData = "Hello 🌎!"
        
        let encryptResult = try encrypt(data: unencryptedData)
        let encryptedData = encryptResult.encryptedData
        let iv = encryptResult.iv
        let key = encryptResult.key
        
        XCTAssertNotEqual(encryptedData, "")
        
        let decryptedData = try decrypt(data: encryptedData,
                                        iv: iv,
                                        key: key)
        
        XCTAssertEqual(decryptedData, unencryptedData)
    }
}

fileprivate extension SystemSecurityCryptographyTests {
    func encrypt(data: String) throws -> (encryptedData: String, 
                                          iv: DNArray<System.Byte>,
                                          key: DNArray<System.Byte>) {
        let aes = try System.Security.Cryptography.Aes.create()
        
        try aes.generateIV()
        try aes.generateKey()
        
        let iv = try aes.iV
        let key = try aes.key
        
        let encryptor = try aes.createEncryptor()
        let memoryStream = try System.IO.MemoryStream()
        
        let cryptoStream = try System.Security.Cryptography.CryptoStream(memoryStream,
                                                                         encryptor,
                                                                         .write)
        
        let streamWriter = try System.IO.StreamWriter(cryptoStream)
        
        try streamWriter.write(data.dotNETString())
        
        try cryptoStream.flush()
        
        try streamWriter.dispose()
        try cryptoStream.dispose()
        try memoryStream.dispose()
        try aes.dispose()
        
        let encryptedData = try memoryStream.toArray()
        
        let base64EncryptedDataDN = try System.Convert.toBase64String(encryptedData)
        let base64EncryptedData = base64EncryptedDataDN.string()
        
        return (encryptedData: base64EncryptedData,
                iv: iv,
                key: key)
    }
    
    func decrypt(data: String,
                 iv: DNArray<System.Byte>,
                 key: DNArray<System.Byte>) throws -> String {
        let buffer = try System.Convert.fromBase64String(data.dotNETString())
        
        let aes = try System.Security.Cryptography.Aes.create()
        try aes.iV_set(iv)
        try aes.key_set(key)
        
        let decryptor = try aes.createDecryptor()
        let memoryStream = try System.IO.MemoryStream(buffer)
        
        let cryptoStream = try System.Security.Cryptography.CryptoStream(memoryStream,
                                                                         decryptor,
                                                                         .read)
        
        let streamReader = try System.IO.StreamReader(cryptoStream)
        
        try cryptoStream.flush()
        
        let decryptedDataDN = try streamReader.readToEnd()
        let decryptedData = decryptedDataDN.string()
        
        try cryptoStream.dispose()
        try memoryStream.dispose()
        try aes.dispose()
        try streamReader.dispose()
        
        return decryptedData
    }
}
