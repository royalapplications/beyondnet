import SwiftUI

struct MainView: View {
    var body: some View {
        TabView {
            GuidView()
                .tabItem {
                    Label("Guid", systemImage: "lanyardcard.fill")
                }
        }
        .padding()
    }
}

struct ContentView_Previews: PreviewProvider {
    static var previews: some View {
        MainView()
    }
}
