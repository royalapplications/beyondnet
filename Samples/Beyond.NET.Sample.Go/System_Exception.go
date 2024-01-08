package main

/*
#include "Generated_C.h"
*/
import "C"

type System_Exception struct {
	ptr C.System_Exception_t
}

func Wrap_System_Exception(ptr C.System_Exception_t) *System_Exception {
	if ptr == nil {
		return nil
	}

	inst := System_Exception{
		ptr: ptr,
	}

	return &inst
}

func (self *System_Exception) Destroy() {
	if self == nil {
		return
	}

	C.System_Exception_Destroy(self.ptr)
}

func (self *System_Exception) ToString() *System_String {
	strDNPtr := C.System_Exception_ToString(self.ptr, nil)

	return Wrap_System_String(strDNPtr)
}

func (e *System_Exception) Error() string {
	strDN := e.ToString()
	defer strDN.Destroy()

	str := strDN.ToGoString()

	return str
}
