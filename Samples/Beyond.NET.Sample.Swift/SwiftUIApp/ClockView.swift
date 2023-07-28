import SwiftUI
import BeyondDotNETSampleKit

struct ClockView: View {
    @State
    private var dateTimeString = Self.currentDateTimeString()
    
    private static var dateFormatter: DateFormatter = {
        let formatter = DateFormatter()
        formatter.dateStyle = .medium
        formatter.timeStyle = .medium
        
        return formatter
    }()
    
    private let timer = Timer.publish(every: 1,
                                      on: .main,
                                      in: .common).autoconnect()
    
    var body: some View {
        VStack {
            Text("The current date and time is…")
                .padding(.bottom)
            
            Text(dateTimeString)
                .textSelection(.enabled)
                .bold()
                .onReceive(timer) { _ in
                    updateDateTimeString()
                }
        }
    }
}

private extension ClockView {
    func updateDateTimeString() {
        dateTimeString = Self.currentDateTimeString()
    }
    
    static func currentDateTimeString() -> String {
        // Get the current System.DateTime
        guard let nowDN = try? System.DateTime.now else {
            fatalError("Error while getting System.DateTime.now")
        }
        
        // Convert the System.DateTime to a Swift Date
        guard let now = try? nowDN.swiftDate() else {
            fatalError("Error while converting System.DateTime to Swift Date")
        }
        
        let dateString = dateFormatter.string(from: now)
        
        return dateString
    }
}

struct ClockView_Previews: PreviewProvider {
    static var previews: some View {
        ClockView()
    }
}
