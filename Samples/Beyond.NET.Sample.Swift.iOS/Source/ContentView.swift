import SwiftUI

struct ContentView: View {
    var body: some View {
        VStack {
            Text("Here's a new .NET System.Guid:")
            Text(newGuidString())
        }
        .padding()
    }
    
    func newGuidString() -> String {
        guard let guid = try? System.Guid.newGuid() else {
            fatalError("Error while generating new System.Guid")
        }
        
        guard let guidString = try? guid.toString()?.string() else {
            fatalError("Error while converting System.Guid to String")
        }
        
        return guidString
    }
}

struct ContentView_Previews: PreviewProvider {
    static var previews: some View {
        ContentView()
    }
}
