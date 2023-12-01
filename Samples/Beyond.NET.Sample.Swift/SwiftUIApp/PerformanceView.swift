import SwiftUI
import BeyondDotNETSampleKit

struct PerformanceView: View {
    var body: some View {
        VStack {
            Button {
                testIterationPerformance()
            } label: {
                Text("Test Iteration Performance")
            }.buttonStyle(.borderedProminent)
        }
    }
}

private extension PerformanceView {
    func testIterationPerformance() {
        let array = createArray()
        
//        Thread.sleep(forTimeInterval: 2)
        
        iterate(array)
    }
    
    func createArray() -> System.Array {
        do {
            let systemObjectType = System.Object.typeOf
            let count: Int32 = 1_000_000
            
            var values = [System.Object]()
            
            let systemArray = try System_Array.createInstance(systemObjectType,
                                                              count)
            
            for idx in 0..<count {
                let obj = try System.Object()
                
                values.append(obj)
                try systemArray.setValue(obj, idx)
            }
            
            return systemArray
        } catch {
            fatalError("Error: \(error.localizedDescription)")
        }
    }
    
    func iterate(_ systemArray: System.Array) {
        for obj in systemArray {
            // Aka PleaseDontOptimizeThisOut
            let _ = obj
        }
    }
}

struct PerformanceView_Previews: PreviewProvider {
    static var previews: some View {
        PerformanceView()
    }
}
