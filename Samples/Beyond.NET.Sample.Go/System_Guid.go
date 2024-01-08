package main

/*
#include "Generated_C.h"
*/
import "C"

type System_Guid struct {
	ptr C.System_Guid_t
}

func Wrap_System_Guid(ptr C.System_Guid_t) *System_Guid {
	if ptr == nil {
		return nil
	}

	inst := System_Guid{
		ptr: ptr,
	}

	return &inst
}

func System_Guid_NewGuid(exception *System_Exception) *System_Guid {
	var exPtr C.System_Exception_t
	ptr := C.System_Guid_NewGuid(&exPtr)

	if exPtr != nil {
		exception = Wrap_System_Exception(exPtr)
	}

	return Wrap_System_Guid(ptr)
}

func System_Guid_Parse(str *System_String) (*System_Guid, error) {
	var exPtr C.System_Exception_t
	ptr := C.System_Guid_Parse(str.ptr, &exPtr)

	if exPtr != nil {
		ex := Wrap_System_Exception(exPtr)

		return nil, ex
	}

	return Wrap_System_Guid(ptr), nil
}

func (self *System_Guid) Destroy() {
	if self == nil {
		return
	}

	C.System_Guid_Destroy(self.ptr)
}

func (self *System_Guid) ToString() *System_String {
	strDNPtr := C.System_Guid_ToString(self.ptr, nil)

	return Wrap_System_String(strDNPtr)
}
