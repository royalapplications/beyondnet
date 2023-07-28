import SwiftUI
import BeyondDotNETSampleKit

struct ExceptionView: View {
    private var error: DNError
    
    private var localizedDescription: String {
        // The localizedDescription will contain the System.Exception.Message value
        error.localizedDescription
    }
    
    private var stackTrace: String {
        // You can get the stack trace of the underlying System.Exception object using this convenience API
        error.stackTrace() ?? ""
    }
    
    private var exceptionToString: String {
        // You also get access to the underlying System.Exception object
        (try? error.exception.toString()?.string()) ?? ""
    }
    
    init() {
        do {
            // Try to get a System.Type by name which certainly does not exist
            // This will throw an Exception which gets wrapped in a Swift Error (DNError to be specific)
            _ = try System_Type.getType("This Type Does Not Exist".dotNETString(), true)
            
            fatalError("System.Type.GetType with a non-existent type should throw but did not")
        } catch let dnError as DNError {
            self.error = dnError
        } catch {
            // Exceptions thrown from .NET are always returned as DNError so we should never end up here
            fatalError("The thrown error was not of type DNError")
        }
    }
    
    var body: some View {
        ScrollView {
            VStack {
                VStack {
                    Text("Localized Description:")
                        .bold()
                    Text(localizedDescription)
                        .textSelection(.enabled)
                }.padding(.bottom)
                
                VStack {
                    Text("Stack Trace:")
                        .bold()
                    Text(stackTrace)
                        .textSelection(.enabled)
                }.padding(.bottom)
                
                VStack {
                    Text("System.Exception.ToString:")
                        .bold()
                    Text(exceptionToString)
                        .textSelection(.enabled)
                }
            }
        }
    }
}

struct ExceptionView_Previews: PreviewProvider {
    static var previews: some View {
        ExceptionView()
    }
}
