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
        guard let guid = try? System.Guid.newGuid() else {
            fatalError("Error while generating new System.Guid")
        }
        
        guard let guidString = try? guid.toString()?.string() else {
            fatalError("Error while converting System.Guid to String")
        }
        
        return guidString
    }
}

struct GuidView_Previews: PreviewProvider {
    static var previews: some View {
        GuidView()
    }
}
