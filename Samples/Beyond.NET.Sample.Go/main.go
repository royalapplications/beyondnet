package main

/*
#cgo CFLAGS: -g -Wall
#cgo LDFLAGS: -L. -lBeyondDotNETSampleKit
*/
import "C"

import (
	"fmt"
	"runtime"
	"time"
)

func main() {
	testSystemDateTime()

	testNewGuid(5)
	testSuccessfulGuidParsing()
	testGuidParsingError()

	// Ensure that finializers run
	runtime.GC()
	time.Sleep(1 * time.Second)
}

// System.DateTime Tests
func testSystemDateTime() {
	dateTime := System_DateTime_Now()
	dateTimeStrDN := dateTime.ToString()

	fmt.Println("It's", dateTimeStrDN.ToGoString())
}

// System.Guid Tests
func testNewGuid(numberOfGuids int) {
	for i := 0; i < numberOfGuids; i++ {
		guid := System_Guid_NewGuid(nil)
		guidStrDN := guid.ToString()

		fmt.Println("Here's a new System.Guid:", guidStrDN.ToGoString())
	}
}

func testSuccessfulGuidParsing() {
	guid := System_Guid_NewGuid(nil)
	guidStrDN := guid.ToString()
	parsedGuid, err := System_Guid_Parse(guidStrDN)

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
	parsedGuid, err := System_Guid_Parse(guidStrDN)

	if err == nil {
		panic("System.Guid.Parse did not raise an exception although it should")
	}

	if parsedGuid != nil {
		panic("System.Guid.Parse did not return nil although it should")
	}

	errStr := err.Error()
	fmt.Println("Expected exception raised by System.Guid.Parse:", errStr)
}
