import SwiftUI
import BeyondDotNETSampleKit

struct TimerView: View {
    @StateObject
    private var timer = TimerModel(interval: 1)
    
    var body: some View {
        VStack {
            Text("Every second, a new System.Guid is generated using a System.Threading.Timer.")
            
            ScrollView {
                Text(timer.guidsString)
                    .font(.footnote)
                    .textSelection(.enabled)
            }
        }.padding()
    }
}

private extension TimerView {
    class TimerModel: ObservableObject {
        @Published
        var guidsString = ""
        
        private var timer: System.Threading.Timer?
        
        init(interval: TimeInterval) {
            startTimer(interval: interval)
        }
        
        deinit {
            stopTimer()
        }
        
        private func startTimer(interval: TimeInterval) {
            let dueTimeMS: Int32 = 0 // Immediately
            let periodMS: Int32 = .init(interval * 1_000)
            
            let closure: System.Threading.TimerCallback.ClosureType = { [weak self] _ in
                self?.addNewGuid()
            }
            
            guard let timer = try? System.Threading.Timer(.init(closure),
                                                          nil,
                                                          dueTimeMS,
                                                          periodMS) else {
                fatalError("Failed to create System.Threading.Timer")
            }
            
            self.timer = timer
        }
        
        private func stopTimer() {
            try? self.timer?.dispose()
            self.timer = nil
        }
        
        private func addNewGuid() {
            guard let newGuid = try? System.Guid.newGuid() else {
                fatalError("Failed to create new System.Guid")
            }
            
            addGuid(newGuid)
        }
        
        private func addGuid(_ newGuid: System.Guid) {
            guard let guidString = try? newGuid.toString().string() else {
                fatalError("Failed to convert System.Guid to string")
            }
            
            DispatchQueue.main.async { [weak self] in
                self?.guidsString += "\(guidString)\n"
            }
        }
    }
}

struct TimerView_Previews: PreviewProvider {
    static var previews: some View {
        TimerView()
    }
}