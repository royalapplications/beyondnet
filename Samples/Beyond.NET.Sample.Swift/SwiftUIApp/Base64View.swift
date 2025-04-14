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
        outputString = Self.base64String(from: inputString)
    }

    static func base64String(from inputString: String) -> String {
        let inputStringDN = inputString.dotNETString()

        // Get the .NET UTF8 Text Encoding
        guard let utf8Encoding = try? System.Text.Encoding.uTF8 else {
            fatalError("Failed to get System.Text.Encoding.UTF8")
        }

        // Convert the input string to a .NET byte array
        guard let inputStringBytes = try? utf8Encoding.getBytes(inputStringDN) else {
            fatalError("Failed to get bytes of input string")
        }

        // Convert the .NET byte array to a Base64 string
        guard let base64OutputStringDN = try? System.Convert.toBase64String(inputStringBytes) else {
            fatalError("Failed to get Base64 String")
        }

        // Convert the .NET System.String to a Swift string
        let base64OutputString = base64OutputStringDN.string()

        return base64OutputString
    }
}

struct Base64View_Previews: PreviewProvider {
    static var previews: some View {
        Base64View()
    }
}
