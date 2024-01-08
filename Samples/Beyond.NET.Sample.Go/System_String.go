package main

/*
#include "Generated_C.h"
*/
import "C"
import "unsafe"

type System_String struct {
	ptr C.System_String_t
}

func Wrap_System_String(ptr C.System_String_t) *System_String {
	if ptr == nil {
		return nil
	}

	inst := System_String{
		ptr: ptr,
	}

	return &inst
}

func System_String_FromGoString(str string) *System_String {
	strC := C.CString(str)
	defer C.free(unsafe.Pointer(strC))

	strDNPtr := C.DNStringFromC(strC)

	return Wrap_System_String(strDNPtr)
}

func System_String_Empty() *System_String {
	ptr := C.System_String_Empty_Get()

	return Wrap_System_String(ptr)
}

func (self *System_String) Destroy() {
	if self == nil {
		return
	}

	C.System_String_Destroy(self.ptr)
}

func (self *System_String) ToGoString() string {
	strC := C.DNStringToC(self.ptr)
	defer C.free(unsafe.Pointer(strC))

	return C.GoString(strC)
}
