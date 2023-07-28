import SwiftUI
import BeyondDotNETSampleKit

struct Base64View: View {
    @State
    private var inputString = ""
    
    @State
    private var outputString = ""
    
    var body: some View {
        VStack {
            HStack {
                Text("Input:")
                TextField("Input", text: $inputString)
                    .textFieldStyle(.roundedBorder)
                    .onChange(of: inputString) { _ in
                        updateBase64OutputString()
                    }
            }
            
            HStack {
                Text("Output:")
                TextField("Output", text: $outputString)
                    .textFieldStyle(.roundedBorder)
            }
        }
        .padding()
    }
}

private extension Base64View {
    func updateBase64OutputString() {
        let inputStringDN = inputString.dotNETString()
        
        guard let utf8Encoding = try? System.Text.Encoding.uTF8 else {
            fatalError("Failed to get System.Text.Encoding.UTF8")
        }
        
        guard let inputStringBytes = try? utf8Encoding.getBytes(inputStringDN) else {
            fatalError("Failed to get bytes of input string")
        }
        
        guard let base64OutputStringDN = try? System.Convert.toBase64String(inputStringBytes) else {
            fatalError("Failed to get Base64 String")
        }
        
        let base64OutputString = base64OutputStringDN.string()
        
        outputString = base64OutputString
    }
}

struct Base64View_Previews: PreviewProvider {
    static var previews: some View {
        Base64View()
    }
}
