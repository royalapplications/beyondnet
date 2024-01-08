package main

/*
#cgo CFLAGS: -g -Wall
#cgo LDFLAGS: -L. -lBeyondDotNETSampleKit
*/
import "C"
import "fmt"

func main() {
	testSystemDateTime()

	testNewGuid()
	testSuccessfulGuidParsing()
	testGuidParsingError()
}

// System.DateTime Tests
func testSystemDateTime() {
	dateTime := System_DateTime_Now()
	defer dateTime.Destroy()

	dateTimeStrDN := dateTime.ToString()
	defer dateTimeStrDN.Destroy()

	fmt.Println("It's", dateTimeStrDN.ToGoString())
}

// System.Guid Tests
func testNewGuid() {
	guid := System_Guid_NewGuid(nil)
	defer guid.Destroy()

	guidStrDN := guid.ToString()
	defer guidStrDN.Destroy()

	fmt.Println("Here's a new System.Guid:", guidStrDN.ToGoString())
}

func testSuccessfulGuidParsing() {
	guid := System_Guid_NewGuid(nil)
	defer guid.Destroy()

	guidStrDN := guid.ToString()
	defer guidStrDN.Destroy()

	parsedGuid, err := System_Guid_Parse(guidStrDN)
	defer parsedGuid.Destroy()

	if err != nil {
		panic("System.Guid.Parse raised an exception")
	}

	if parsedGuid == nil {
		panic("System.Guid.Parse returned nil")
	}
}

func testGuidParsingError() {
	guidStr := "abc 123"
	guidStrDN := System_String_FromGoString(guidStr)
	defer guidStrDN.Destroy()

	parsedGuid, err := System_Guid_Parse(guidStrDN)
	defer parsedGuid.Destroy()

	if err == nil {
		panic("System.Guid.Parse did not raise an exception although it should")
	}

	if parsedGuid != nil {
		panic("System.Guid.Parse did not return nil although it should")
	}

	errStr := err.Error()
	fmt.Println("Expected exception raised by System.Guid.Parse:", errStr)
}
