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

    func createArray() -> DNArray<System.Object> {
        do {
            let count: Int32 = 1_000_000

            var values = [System.Object]()

            let systemObjectArray = try DNArray<System.Object>(length: count)

            for idx in 0..<count {
                let obj = try System.Object()

                values.append(obj)
                systemObjectArray[idx] = obj
            }

            return systemObjectArray
        } catch {
            fatalError("Error: \(error.localizedDescription)")
        }
    }

    func iterate(_ systemObjectArray: DNArray<System.Object>) {
        for obj in systemObjectArray {
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
