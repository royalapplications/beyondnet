import SwiftUI
import BeyondDotNETSampleKit

struct GuidView: View {
    @State
    private var guidString = Self.newGuidString()
    
    var body: some View {
        VStack {
            Text("Here's a new .NET System.Guid:")
            
            Text(guidString)
                .textSelection(.enabled)
                .bold()
                .padding(.bottom)
            
            Button {
                updateGuidString()
            } label: {
                Image(systemName: "arrow.clockwise.circle.fill")
                Text("Generate New")
            }.buttonStyle(.borderedProminent)
        }
    }
}

private extension GuidView {
    func updateGuidString() {
        guidString = Self.newGuidString()
    }
    
    static func newGuidString() -> String {
        // Generate a new System.Guid
        guard let guid = try? System.Guid.newGuid() else {
            fatalError("Error while generating new System.Guid")
        }
        
        // Convert the System.Guid to a System.String
        guard let guidStringDN = try? guid.toString() else {
            fatalError("Error while converting System.Guid to System.String")
        }
        
        // Convert the System.String to a Swift String
        let guidString = guidStringDN.string()
        
        return guidString
    }
}

struct GuidView_Previews: PreviewProvider {
    static var previews: some View {
        GuidView()
    }
}
