package main

/*
#include "Generated_C.h"
*/
import "C"

import (
	"runtime"
)

type System_DateTime struct {
	ptr C.System_DateTime_t
}

func Wrap_System_DateTime(ptr C.System_DateTime_t) *System_DateTime {
	if ptr == nil {
		return nil
	}

	inst := &System_DateTime{
		ptr: ptr,
	}

	runtime.SetFinalizer(inst, System_DateTime_Destroy)

	return inst
}

func System_DateTime_Destroy(self *System_DateTime) {
	self.Destroy()
}

func (self *System_DateTime) Destroy() {
	if self == nil {
		return
	}

	runtime.SetFinalizer(self, nil)

	C.System_DateTime_Destroy(self.ptr)
}

func System_DateTime_Now() *System_DateTime {
	ptr := C.System_DateTime_Now_Get(nil)

	return Wrap_System_DateTime(ptr)
}

func (self *System_DateTime) ToString() *System_String {
	defer runtime.KeepAlive(self)

	strDNPtr := C.System_DateTime_ToString(self.ptr, nil)

	return Wrap_System_String(strDNPtr)
}
