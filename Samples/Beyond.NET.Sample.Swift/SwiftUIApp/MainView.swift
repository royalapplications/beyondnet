import SwiftUI

struct MainView: View {
    var body: some View {
        TabView {
            GuidView()
                .tabItem {
                    Label("Guid", systemImage: "lanyardcard.fill")
                }
            ClockView()
                .tabItem {
                    Label("Clock", systemImage: "clock.fill")
                }
            Base64View()
                .tabItem {
                    Label("Base64", systemImage: "number.circle.fill")
                }
            ExceptionView()
                .tabItem {
                    Label("Exception", systemImage: "exclamationmark.bubble.fill")
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
