package main

/*
#include "Generated_C.h"
*/
import "C"

import (
	"runtime"
)

type System_Exception struct {
	ptr C.System_Exception_t
}

func Wrap_System_Exception(ptr C.System_Exception_t) *System_Exception {
	if ptr == nil {
		return nil
	}

	inst := &System_Exception{
		ptr: ptr,
	}

	runtime.SetFinalizer(inst, System_Exception_Destroy)

	return inst
}

func System_Exception_Destroy(self *System_Exception) {
	self.Destroy()
}

func (self *System_Exception) Destroy() {
	if self == nil {
		return
	}

	runtime.SetFinalizer(self, nil)

	C.System_Exception_Destroy(self.ptr)
}

func (self *System_Exception) ToString() *System_String {
	defer runtime.KeepAlive(self)

	strDNPtr := C.System_Exception_ToString(self.ptr, nil)

	return Wrap_System_String(strDNPtr)
}

func (self *System_Exception) Error() string {
	defer runtime.KeepAlive(self)

	strDN := self.ToString()
	defer strDN.Destroy()

	str := strDN.ToGoString()

	return str
}
