package com.example.beyondnetsampleandroid.dn

import com.sun.jna.*
import com.sun.jna.ptr.*

// MARK: - Manually curated

// MARK: - BEGIN System.Object
/// Supports all classes in the .NET class hierarchy and provides low-level services to derived classes. This is the ultimate base class of all .NET classes; it is the root of the type hierarchy.
open class System_Object /* System.Object */(handle: Pointer): DNObject(handle) {
    companion object {
        /// Determines whether the specified object instances are considered equal.
        /// - Parameter objA: The first object to compare.
        /// - Parameter objB: The second object to compare.
        /// - Returns: true if the objects are considered equal; otherwise, false. If both objA and objB are null, the method returns true.
        public fun equals(objA: System_Object? /* System.Object */, objB: System_Object? /* System.Object */) : Boolean /* System.Boolean */ {
            val objAC = objA.getHandleOrNullPointer()
            val objBC = objB.getHandleOrNullPointer()


            val __exceptionC = PointerByReference()

            val __returnValueC = BeyondDotNETSampleNative.System_Object_Equals_1(objAC, objBC, __exceptionC)

            if (__exceptionC.value !== Pointer.NULL) {
                // TODO
                // throw System_Exception(__exceptionC).toKException()
                throw Exception("TODO: Convert System.Exception to Kotlin Exception")
            }

            return __returnValueC

        }
        /// Determines whether the specified System.Object instances are the same instance.
        /// - Parameter objA: The first object to compare.
        /// - Parameter objB: The second object  to compare.
        /// - Returns: true if objA is the same instance as objB or if both are null; otherwise, false.
        public fun referenceEquals(objA: System_Object? /* System.Object */, objB: System_Object? /* System.Object */) : Boolean /* System.Boolean */ {
            val objAC = objA.getHandleOrNullPointer()
            val objBC = objB.getHandleOrNullPointer()


            val __exceptionC = PointerByReference()

            val __returnValueC = BeyondDotNETSampleNative.System_Object_ReferenceEquals(objAC, objBC, __exceptionC)

            if (__exceptionC.value !== Pointer.NULL) {
                // TODO
                // throw System_Exception(__exceptionC).toKException()
                throw Exception("TODO: Convert System.Exception to Kotlin Exception")
            }

            return __returnValueC

        }
        /// Initializes a new instance of the System.Object class.
        // TODO: Constructor
        // TODO: TypeOf
    }

    /// Determines whether the specified object is equal to the current object.
    /// - Parameter obj: The object to compare with the current object.
    /// - Returns: true if the specified object  is equal to the current object; otherwise, false.
    public fun equals(obj: System_Object? /* System.Object */) : Boolean /* System.Boolean */ {
        val objC = obj.getHandleOrNullPointer()


        val __exceptionC = PointerByReference()

        val __returnValueC = BeyondDotNETSampleNative.System_Object_Equals(this.__handle, objC, __exceptionC)

        if (__exceptionC.value !== Pointer.NULL) {
            // TODO
            // throw System_Exception(__exceptionC).toKException()
            throw Exception("TODO: Convert System.Exception to Kotlin Exception")
        }

        return __returnValueC

    }
    /// Serves as the default hash function.
    /// - Returns: A hash code for the current object.
    public fun getHashCode() : Int /* System.Int32 */ {


        val __exceptionC = PointerByReference()

        val __returnValueC = BeyondDotNETSampleNative.System_Object_GetHashCode(this.__handle, __exceptionC)

        if (__exceptionC.value !== Pointer.NULL) {
            // TODO
            // throw System_Exception(__exceptionC).toKException()
            throw Exception("TODO: Convert System.Exception to Kotlin Exception")
        }

        return __returnValueC

    }
    // TODO: Destructor

}


// MARK: - END System.Object

/// Controls the system garbage collector, a service that automatically reclaims unused memory.
open class System_GC /* System.GC */(handle: Pointer): System_Object(handle) {
    companion object {
        /// Informs the runtime of a large allocation of unmanaged memory that should be taken into account when scheduling garbage collection.
        /// - Parameter bytesAllocated: The incremental amount of unmanaged memory that has been allocated.
        /// - Throws: System.ArgumentOutOfRangeException: bytesAllocated is less than or equal to 0.  -or-  On a 32-bit computer, bytesAllocated is larger than System.Int32.MaxValue.
        public fun addMemoryPressure(bytesAllocated: Long /* System.Int64 */) {


            val __exceptionC = PointerByReference()

            BeyondDotNETSampleNative.System_GC_AddMemoryPressure(bytesAllocated, __exceptionC)

            if (__exceptionC.value !== Pointer.NULL) {
                // TODO
                // throw System_Exception(__exceptionC).toKException()
                throw Exception("TODO: Convert System.Exception to Kotlin Exception")
            }


        }
        /// Informs the runtime that unmanaged memory has been released and no longer needs to be taken into account when scheduling garbage collection.
        /// - Parameter bytesAllocated: The amount of unmanaged memory that has been released.
        /// - Throws: System.ArgumentOutOfRangeException: bytesAllocated is less than or equal to 0.  -or-  On a 32-bit computer, bytesAllocated is larger than System.Int32.MaxValue.
        public fun removeMemoryPressure(bytesAllocated: Long /* System.Int64 */) {


            val __exceptionC = PointerByReference()

            BeyondDotNETSampleNative.System_GC_RemoveMemoryPressure(bytesAllocated, __exceptionC)

            if (__exceptionC.value !== Pointer.NULL) {
                // TODO
                // throw System_Exception(__exceptionC).toKException()
                throw Exception("TODO: Convert System.Exception to Kotlin Exception")
            }


        }
        /// Returns the current generation number of the specified object.
        /// - Parameter obj: The object that generation information is retrieved for.
        /// - Returns: The current generation number of obj, or System.Int32.MaxValue.
        public fun getGeneration(obj: System_Object /* System.Object */) : Int /* System.Int32 */ {
            val objC = obj.getHandleOrNullPointer()


            val __exceptionC = PointerByReference()

            val __returnValueC = BeyondDotNETSampleNative.System_GC_GetGeneration(objC, __exceptionC)

            if (__exceptionC.value !== Pointer.NULL) {
                // TODO
                // throw System_Exception(__exceptionC).toKException()
                throw Exception("TODO: Convert System.Exception to Kotlin Exception")
            }

            return __returnValueC

        }
        /// Forces an immediate garbage collection from generation 0 through a specified generation.
        /// - Parameter generation: The number of the oldest generation to be garbage collected.
        /// - Throws: System.ArgumentOutOfRangeException: generation is not valid.
        public fun collect(generation: Int /* System.Int32 */) {


            val __exceptionC = PointerByReference()

            BeyondDotNETSampleNative.System_GC_Collect(generation, __exceptionC)

            if (__exceptionC.value !== Pointer.NULL) {
                // TODO
                // throw System_Exception(__exceptionC).toKException()
                throw Exception("TODO: Convert System.Exception to Kotlin Exception")
            }


        }
        /// Forces an immediate garbage collection of all generations.
        public fun collect() {


            val __exceptionC = PointerByReference()

            BeyondDotNETSampleNative.System_GC_Collect_1(__exceptionC)

            if (__exceptionC.value !== Pointer.NULL) {
                // TODO
                // throw System_Exception(__exceptionC).toKException()
                throw Exception("TODO: Convert System.Exception to Kotlin Exception")
            }


        }
        /// Forces a garbage collection from generation 0 through a specified generation, at a time specified by a System.GCCollectionMode value.
        /// - Parameter generation: The number of the oldest generation to be garbage collected.
        /// - Parameter mode: An enumeration value that specifies whether the garbage collection is forced (System.GCCollectionMode.Default or System.GCCollectionMode.Forced) or optimized (System.GCCollectionMode.Optimized).
        /// - Throws: System.ArgumentOutOfRangeException: generation is not valid.  -or-  mode is not one of the System.GCCollectionMode values.
        public fun collect(generation: Int /* System.Int32 */, mode: System_GCCollectionMode /* System.GCCollectionMode */) {
            val modeC = mode.rawValue


            val __exceptionC = PointerByReference()

            BeyondDotNETSampleNative.System_GC_Collect_2(generation, modeC, __exceptionC)

            if (__exceptionC.value !== Pointer.NULL) {
                // TODO
                // throw System_Exception(__exceptionC).toKException()
                throw Exception("TODO: Convert System.Exception to Kotlin Exception")
            }


        }
        /// Forces a garbage collection from generation 0 through a specified generation, at a time specified by a System.GCCollectionMode value, with a value specifying whether the collection should be blocking.
        /// - Parameter generation: The number of the oldest generation to be garbage collected.
        /// - Parameter mode: An enumeration value that specifies whether the garbage collection is forced (System.GCCollectionMode.Default or System.GCCollectionMode.Forced) or optimized (System.GCCollectionMode.Optimized).
        /// - Parameter blocking: true to perform a blocking garbage collection; false to perform a background garbage collection where possible.
        /// - Throws: System.ArgumentOutOfRangeException: generation is not valid.  -or-  mode is not one of the System.GCCollectionMode values.
        public fun collect(generation: Int /* System.Int32 */, mode: System_GCCollectionMode /* System.GCCollectionMode */, blocking: Boolean /* System.Boolean */) {
            val modeC = mode.rawValue


            val __exceptionC = PointerByReference()

            BeyondDotNETSampleNative.System_GC_Collect_3(generation, modeC, blocking, __exceptionC)

            if (__exceptionC.value !== Pointer.NULL) {
                // TODO
                // throw System_Exception(__exceptionC).toKException()
                throw Exception("TODO: Convert System.Exception to Kotlin Exception")
            }


        }
        /// Forces a garbage collection from generation 0 through a specified generation, at a time specified by a System.GCCollectionMode value, with values that specify whether the collection should be blocking and compacting.
        /// - Parameter generation: The number of the oldest generation to be garbage collected.
        /// - Parameter mode: An enumeration value that specifies whether the garbage collection is forced (System.GCCollectionMode.Default or System.GCCollectionMode.Forced) or optimized (System.GCCollectionMode.Optimized).
        /// - Parameter blocking: true to perform a blocking garbage collection; false to perform a background garbage collection where possible.
        /// - Parameter compacting: true to compact the small object heap; false to sweep only.
        public fun collect(generation: Int /* System.Int32 */, mode: System_GCCollectionMode /* System.GCCollectionMode */, blocking: Boolean /* System.Boolean */, compacting: Boolean /* System.Boolean */) {
            val modeC = mode.rawValue


            val __exceptionC = PointerByReference()

            BeyondDotNETSampleNative.System_GC_Collect_4(generation, modeC, blocking, compacting, __exceptionC)

            if (__exceptionC.value !== Pointer.NULL) {
                // TODO
                // throw System_Exception(__exceptionC).toKException()
                throw Exception("TODO: Convert System.Exception to Kotlin Exception")
            }


        }
        /// Returns the number of times garbage collection has occurred for the specified generation of objects.
        /// - Parameter generation: The generation of objects for which the garbage collection count is to be determined.
        /// - Throws: System.ArgumentOutOfRangeException: generation is less than 0.
        /// - Returns: The number of times garbage collection has occurred for the specified generation since the process was started.
        public fun collectionCount(generation: Int /* System.Int32 */) : Int /* System.Int32 */ {


            val __exceptionC = PointerByReference()

            val __returnValueC = BeyondDotNETSampleNative.System_GC_CollectionCount(generation, __exceptionC)

            if (__exceptionC.value !== Pointer.NULL) {
                // TODO
                // throw System_Exception(__exceptionC).toKException()
                throw Exception("TODO: Convert System.Exception to Kotlin Exception")
            }

            return __returnValueC

        }
        /// References the specified object, which makes it ineligible for garbage collection from the start of the current routine to the point where this method is called.
        /// - Parameter obj: The object to reference.
        public fun keepAlive(obj: System_Object? /* System.Object */) {
            val objC = obj.getHandleOrNullPointer()


            val __exceptionC = PointerByReference()

            BeyondDotNETSampleNative.System_GC_KeepAlive(objC, __exceptionC)

            if (__exceptionC.value !== Pointer.NULL) {
                // TODO
                // throw System_Exception(__exceptionC).toKException()
                throw Exception("TODO: Convert System.Exception to Kotlin Exception")
            }


        }
        /// Suspends the current thread until the thread that is processing the queue of finalizers has emptied that queue.
        public fun waitForPendingFinalizers() {


            val __exceptionC = PointerByReference()

            BeyondDotNETSampleNative.System_GC_WaitForPendingFinalizers(__exceptionC)

            if (__exceptionC.value !== Pointer.NULL) {
                // TODO
                // throw System_Exception(__exceptionC).toKException()
                throw Exception("TODO: Convert System.Exception to Kotlin Exception")
            }


        }
        /// Requests that the common language runtime not call the finalizer for the specified object.
        /// - Parameter obj: The object whose finalizer must not be executed.
        /// - Throws: System.ArgumentNullException: obj is null.
        public fun suppressFinalize(obj: System_Object /* System.Object */) {
            val objC = obj.getHandleOrNullPointer()


            val __exceptionC = PointerByReference()

            BeyondDotNETSampleNative.System_GC_SuppressFinalize(objC, __exceptionC)

            if (__exceptionC.value !== Pointer.NULL) {
                // TODO
                // throw System_Exception(__exceptionC).toKException()
                throw Exception("TODO: Convert System.Exception to Kotlin Exception")
            }


        }
        /// Requests that the system call the finalizer for the specified object for which System.GC.SuppressFinalize(System.Object) has previously been called.
        /// - Parameter obj: The object that a finalizer must be called for.
        /// - Throws: System.ArgumentNullException: obj is null.
        public fun reRegisterForFinalize(obj: System_Object /* System.Object */) {
            val objC = obj.getHandleOrNullPointer()


            val __exceptionC = PointerByReference()

            BeyondDotNETSampleNative.System_GC_ReRegisterForFinalize(objC, __exceptionC)

            if (__exceptionC.value !== Pointer.NULL) {
                // TODO
                // throw System_Exception(__exceptionC).toKException()
                throw Exception("TODO: Convert System.Exception to Kotlin Exception")
            }


        }
        /// Retrieves the heap size excluding fragmentation. For example if the total GC heap size is 100mb and fragmentation, ie, space taken up by free objects, takes up 40mb, this API would report 60mb. A parameter indicates whether this method can wait a short interval before returning, to allow the system to collect garbage and finalize objects.
        /// - Parameter forceFullCollection: true to indicate that this method can wait for garbage collection to occur before returning; otherwise, false.
        /// - Returns: The heap size, in bytes, excluding fragmentation.
        public fun getTotalMemory(forceFullCollection: Boolean /* System.Boolean */) : Long /* System.Int64 */ {


            val __exceptionC = PointerByReference()

            val __returnValueC = BeyondDotNETSampleNative.System_GC_GetTotalMemory(forceFullCollection, __exceptionC)

            if (__exceptionC.value !== Pointer.NULL) {
                // TODO
                // throw System_Exception(__exceptionC).toKException()
                throw Exception("TODO: Convert System.Exception to Kotlin Exception")
            }

            return __returnValueC

        }
        /// Gets the total number of bytes allocated to the current thread since the beginning of its lifetime.
        /// - Returns: The total number of bytes allocated to the current thread since the beginning of its lifetime.
        public fun getAllocatedBytesForCurrentThread() : Long /* System.Int64 */ {


            val __exceptionC = PointerByReference()

            val __returnValueC = BeyondDotNETSampleNative.System_GC_GetAllocatedBytesForCurrentThread(__exceptionC)

            if (__exceptionC.value !== Pointer.NULL) {
                // TODO
                // throw System_Exception(__exceptionC).toKException()
                throw Exception("TODO: Convert System.Exception to Kotlin Exception")
            }

            return __returnValueC

        }
        /// Gets a count of the bytes allocated over the lifetime of the process. The returned value does not include any native allocations.
        /// - Parameter precise: If true, gather a precise number; otherwise, gather an approximate count. Gathering a precise value entails a significant performance penalty.
        /// - Returns: The total number of bytes allocated over the lifetime of the process.
        public fun getTotalAllocatedBytes(precise: Boolean /* System.Boolean */) : Long /* System.Int64 */ {


            val __exceptionC = PointerByReference()

            val __returnValueC = BeyondDotNETSampleNative.System_GC_GetTotalAllocatedBytes(precise, __exceptionC)

            if (__exceptionC.value !== Pointer.NULL) {
                // TODO
                // throw System_Exception(__exceptionC).toKException()
                throw Exception("TODO: Convert System.Exception to Kotlin Exception")
            }

            return __returnValueC

        }
        /// Specifies that a garbage collection notification should be raised when conditions favor full garbage collection and when the collection has been completed.
        /// - Parameter maxGenerationThreshold: A number between 1 and 99 that specifies when the notification should be raised based on the objects allocated in generation 2.
        /// - Parameter largeObjectHeapThreshold: A number between 1 and 99 that specifies when the notification should be raised based on objects allocated in the large object heap.
        /// - Throws: System.ArgumentOutOfRangeException: maxGenerationThreshold or largeObjectHeapThreshold is not between 1 and 99.
        /// - Throws: System.InvalidOperationException: This member is not available when concurrent garbage collection is enabled. See the <gcConcurrent> runtime setting for information about how to disable concurrent garbage collection.
        public fun registerForFullGCNotification(maxGenerationThreshold: Int /* System.Int32 */, largeObjectHeapThreshold: Int /* System.Int32 */) {


            val __exceptionC = PointerByReference()

            BeyondDotNETSampleNative.System_GC_RegisterForFullGCNotification(maxGenerationThreshold, largeObjectHeapThreshold, __exceptionC)

            if (__exceptionC.value !== Pointer.NULL) {
                // TODO
                // throw System_Exception(__exceptionC).toKException()
                throw Exception("TODO: Convert System.Exception to Kotlin Exception")
            }


        }
        /// Cancels the registration of a garbage collection notification.
        /// - Throws: System.InvalidOperationException: This member is not available when concurrent garbage collection is enabled. See the <gcConcurrent> runtime setting for information about how to disable concurrent garbage collection.
        public fun cancelFullGCNotification() {


            val __exceptionC = PointerByReference()

            BeyondDotNETSampleNative.System_GC_CancelFullGCNotification(__exceptionC)

            if (__exceptionC.value !== Pointer.NULL) {
                // TODO
                // throw System_Exception(__exceptionC).toKException()
                throw Exception("TODO: Convert System.Exception to Kotlin Exception")
            }


        }
        /// Returns the status of a registered notification for determining whether a full, blocking garbage collection by the common language runtime is imminent.
        /// - Returns: The status of the registered garbage collection notification.
        public fun waitForFullGCApproach() : System_GCNotificationStatus /* System.GCNotificationStatus */ {


            val __exceptionC = PointerByReference()

            val __returnValueC = BeyondDotNETSampleNative.System_GC_WaitForFullGCApproach(__exceptionC)

            val __returnValue = System_GCNotificationStatus.entries.first { it.rawValue == __returnValueC }

            if (__exceptionC.value !== Pointer.NULL) {
                // TODO
                // throw System_Exception(__exceptionC).toKException()
                throw Exception("TODO: Convert System.Exception to Kotlin Exception")
            }

            return __returnValue

        }
        /// Returns, in a specified time-out period, the status of a registered notification for determining whether a full, blocking garbage collection by the common language runtime is imminent.
        /// - Parameter millisecondsTimeout: The length of time to wait before a notification status can be obtained. Specify -1 to wait indefinitely.
        /// - Throws: System.ArgumentOutOfRangeException: millisecondsTimeout must be either non-negative or less than or equal to System.Int32.MaxValue or -1.
        /// - Returns: The status of the registered garbage collection notification.
        public fun waitForFullGCApproach(millisecondsTimeout: Int /* System.Int32 */) : System_GCNotificationStatus /* System.GCNotificationStatus */ {


            val __exceptionC = PointerByReference()

            val __returnValueC = BeyondDotNETSampleNative.System_GC_WaitForFullGCApproach_1(millisecondsTimeout, __exceptionC)

            val __returnValue = System_GCNotificationStatus.entries.first { it.rawValue == __returnValueC }

            if (__exceptionC.value !== Pointer.NULL) {
                // TODO
                // throw System_Exception(__exceptionC).toKException()
                throw Exception("TODO: Convert System.Exception to Kotlin Exception")
            }

            return __returnValue

        }
        /// Returns the status of a registered notification for determining whether a full, blocking garbage collection by the common language runtime has completed.
        /// - Returns: The status of the registered garbage collection notification.
        public fun waitForFullGCComplete() : System_GCNotificationStatus /* System.GCNotificationStatus */ {


            val __exceptionC = PointerByReference()

            val __returnValueC = BeyondDotNETSampleNative.System_GC_WaitForFullGCComplete(__exceptionC)

            val __returnValue = System_GCNotificationStatus.entries.first { it.rawValue == __returnValueC }

            if (__exceptionC.value !== Pointer.NULL) {
                // TODO
                // throw System_Exception(__exceptionC).toKException()
                throw Exception("TODO: Convert System.Exception to Kotlin Exception")
            }

            return __returnValue

        }
        /// Returns, in a specified time-out period, the status of a registered notification for determining whether a full, blocking garbage collection by common language the runtime has completed.
        /// - Parameter millisecondsTimeout: The length of time to wait before a notification status can be obtained. Specify -1 to wait indefinitely.
        /// - Throws: System.InvalidOperationException: millisecondsTimeout must be either non-negative or less than or equal to System.Int32.MaxValue or -1.
        /// - Returns: The status of the registered garbage collection notification.
        public fun waitForFullGCComplete(millisecondsTimeout: Int /* System.Int32 */) : System_GCNotificationStatus /* System.GCNotificationStatus */ {


            val __exceptionC = PointerByReference()

            val __returnValueC = BeyondDotNETSampleNative.System_GC_WaitForFullGCComplete_1(millisecondsTimeout, __exceptionC)

            val __returnValue = System_GCNotificationStatus.entries.first { it.rawValue == __returnValueC }

            if (__exceptionC.value !== Pointer.NULL) {
                // TODO
                // throw System_Exception(__exceptionC).toKException()
                throw Exception("TODO: Convert System.Exception to Kotlin Exception")
            }

            return __returnValue

        }
        /// Attempts to disallow garbage collection during the execution of a critical path if a specified amount of memory is available.
        /// - Parameter totalSize: The amount of memory in bytes to allocate without triggering a garbage collection. It must be less than or equal to the size of an ephemeral segment. For information on the size of an ephemeral segment, see the "Ephemeral generations and segments" section in the Fundamentals of Garbage Collection article.
        /// - Throws: System.ArgumentOutOfRangeException: totalSize exceeds the ephemeral segment size.
        /// - Throws: System.InvalidOperationException: The process is already in no GC region latency mode.
        /// - Returns: true if the runtime was able to commit the required amount of memory and the garbage collector is able to enter no GC region latency mode; otherwise, false.
        public fun tryStartNoGCRegion(totalSize: Long /* System.Int64 */) : Boolean /* System.Boolean */ {


            val __exceptionC = PointerByReference()

            val __returnValueC = BeyondDotNETSampleNative.System_GC_TryStartNoGCRegion(totalSize, __exceptionC)

            if (__exceptionC.value !== Pointer.NULL) {
                // TODO
                // throw System_Exception(__exceptionC).toKException()
                throw Exception("TODO: Convert System.Exception to Kotlin Exception")
            }

            return __returnValueC

        }
        /// Attempts to disallow garbage collection during the execution of a critical path if a specified amount of memory is available for the large object heap and the small object heap.
        /// - Parameter totalSize: The amount of memory in bytes to allocate without triggering a garbage collection. totalSize -lohSize must be less than or equal to the size of an ephemeral segment. For information on the size of an ephemeral segment, see the "Ephemeral generations and segments" section in the Fundamentals of Garbage Collection article.
        /// - Parameter lohSize: The number of bytes in totalSize to use for large object heap (LOH) allocations.
        /// - Throws: System.ArgumentOutOfRangeException: totalSize - lohSize exceeds the ephemeral segment size.
        /// - Throws: System.InvalidOperationException: The process is already in no GC region latency mode.
        /// - Returns: true if the runtime was able to commit the required amount of memory and the garbage collector is able to enter no GC region latency mode; otherwise, false.
        public fun tryStartNoGCRegion(totalSize: Long /* System.Int64 */, lohSize: Long /* System.Int64 */) : Boolean /* System.Boolean */ {


            val __exceptionC = PointerByReference()

            val __returnValueC = BeyondDotNETSampleNative.System_GC_TryStartNoGCRegion_1(totalSize, lohSize, __exceptionC)

            if (__exceptionC.value !== Pointer.NULL) {
                // TODO
                // throw System_Exception(__exceptionC).toKException()
                throw Exception("TODO: Convert System.Exception to Kotlin Exception")
            }

            return __returnValueC

        }
        /// Attempts to disallow garbage collection during the execution of a critical path if a specified amount of memory is available, and controls whether the garbage collector does a full blocking garbage collection if not enough memory is initially available.
        /// - Parameter totalSize: The amount of memory in bytes to allocate without triggering a garbage collection. It must be less than or equal to the size of an ephemeral segment. For information on the size of an ephemeral segment, see the "Ephemeral generations and segments" section in the Fundamentals of Garbage Collection article.
        /// - Parameter disallowFullBlockingGC: true to omit a full blocking garbage collection if the garbage collector is initially unable to allocate totalSize bytes; otherwise, false.
        /// - Throws: System.ArgumentOutOfRangeException: totalSize exceeds the ephemeral segment size.
        /// - Throws: System.InvalidOperationException: The process is already in no GC region latency mode.
        /// - Returns: true if the runtime was able to commit the required amount of memory and the garbage collector is able to enter no GC region latency mode; otherwise, false.
        public fun tryStartNoGCRegion(totalSize: Long /* System.Int64 */, disallowFullBlockingGC: Boolean /* System.Boolean */) : Boolean /* System.Boolean */ {


            val __exceptionC = PointerByReference()

            val __returnValueC = BeyondDotNETSampleNative.System_GC_TryStartNoGCRegion_2(totalSize, disallowFullBlockingGC, __exceptionC)

            if (__exceptionC.value !== Pointer.NULL) {
                // TODO
                // throw System_Exception(__exceptionC).toKException()
                throw Exception("TODO: Convert System.Exception to Kotlin Exception")
            }

            return __returnValueC

        }
        /// Attempts to disallow garbage collection during the execution of a critical path if a specified amount of memory is available for the large object heap and the small object heap, and controls whether the garbage collector does a full blocking garbage collection if not enough memory is initially available.
        /// - Parameter totalSize: The amount of memory in bytes to allocate without triggering a garbage collection. totalSize -lohSize must be less than or equal to the size of an ephemeral segment. For information on the size of an ephemeral segment, see the "Ephemeral generations and segments" section in the Fundamentals of Garbage Collection article.
        /// - Parameter lohSize: The number of bytes in totalSize to use for large object heap (LOH) allocations.
        /// - Parameter disallowFullBlockingGC: true to omit a full blocking garbage collection if the garbage collector is initially unable to allocate the specified memory on the small object heap (SOH) and LOH; otherwise, false.
        /// - Throws: System.ArgumentOutOfRangeException: totalSize - lohSize exceeds the ephemeral segment size.
        /// - Throws: System.InvalidOperationException: The process is already in no GC region latency mode.
        /// - Returns: true if the runtime was able to commit the required amount of memory and the garbage collector is able to enter no GC region latency mode; otherwise, false.
        public fun tryStartNoGCRegion(totalSize: Long /* System.Int64 */, lohSize: Long /* System.Int64 */, disallowFullBlockingGC: Boolean /* System.Boolean */) : Boolean /* System.Boolean */ {


            val __exceptionC = PointerByReference()

            val __returnValueC = BeyondDotNETSampleNative.System_GC_TryStartNoGCRegion_3(totalSize, lohSize, disallowFullBlockingGC, __exceptionC)

            if (__exceptionC.value !== Pointer.NULL) {
                // TODO
                // throw System_Exception(__exceptionC).toKException()
                throw Exception("TODO: Convert System.Exception to Kotlin Exception")
            }

            return __returnValueC

        }
        /// Ends the no GC region latency mode.
        /// - Throws: System.InvalidOperationException: The garbage collector is not in no GC region latency mode.  -or-  The no GC region latency mode was ended previously because a garbage collection was induced.  -or-  A memory allocation exceeded the amount specified in the call to the System.GC.TryStartNoGCRegion(System.Int64) method.
        public fun endNoGCRegion() {


            val __exceptionC = PointerByReference()

            BeyondDotNETSampleNative.System_GC_EndNoGCRegion(__exceptionC)

            if (__exceptionC.value !== Pointer.NULL) {
                // TODO
                // throw System_Exception(__exceptionC).toKException()
                throw Exception("TODO: Convert System.Exception to Kotlin Exception")
            }


        }
        /// Instructs the Garbage Collector to reconfigure itself by detecting the various memory limits on the system.
        /// - Throws: System.InvalidOperationException: The hard limit is too low. This can happen if the heap hard limit that the refresh will set, either because of new AppData settings or implied by the container memory-limit changes, is lower than what's already committed.-or-The hard limit is invalid. This can happen, for example, with negative heap hard-limit percentages.
        public fun refreshMemoryLimit() {


            val __exceptionC = PointerByReference()

            BeyondDotNETSampleNative.System_GC_RefreshMemoryLimit(__exceptionC)

            if (__exceptionC.value !== Pointer.NULL) {
                // TODO
                // throw System_Exception(__exceptionC).toKException()
                throw Exception("TODO: Convert System.Exception to Kotlin Exception")
            }


        }
        /// Gets the maximum number of generations that the system currently supports.
        /// - Returns: A value that ranges from zero to the maximum number of supported generations.
        // TODO: Get Only Property

        // TODO: TypeOf

    }

    // TODO: Destructor

}


// MARK: - END System.GC

class System_Exception(handle: Pointer): DNObject(handle) {
    override fun destroy() {
        BeyondDotNETSampleNative.System_Exception_Destroy(getHandleOrNullPointer())
    }

    fun dn_toString(): System_String {
        val exceptionRef = PointerByReference()

        val returnValueHandle = BeyondDotNETSampleNative.System_Exception_ToString(getHandleOrNullPointer(), exceptionRef)

        val exceptionHandle = exceptionRef.value

        if (exceptionHandle !== Pointer.NULL) {
            throw System_Exception(exceptionHandle).toKException()
        }

        val returnValue = System_String(returnValueHandle)

        return returnValue
    }
}

class System_String(handle: Pointer): DNObject(handle) {
    companion object {
        val empty: System_String
            get() {
                val handle = BeyondDotNETSampleNative.System_String_Empty_Get()
                val returnValue = System_String(handle)

                return returnValue
            }

        /// Compares two specified System.String objects using the specified rules, and returns an integer that indicates their relative position in the sort order.
        /// - Parameter strA: The first string to compare.
        /// - Parameter strB: The second string to compare.
        /// - Parameter comparisonType: One of the enumeration values that specifies the rules to use in the comparison.
        /// - Throws: System.ArgumentException: comparisonType is not a System.StringComparison value.
        /// - Throws: System.NotSupportedException: System.StringComparison is not supported.
        /// - Returns: A 32-bit signed integer that indicates the lexical relationship between the two comparands.   Value Condition Less than zerostrA precedes strB in the sort order. ZerostrA is in the same position as strB in the sort order. Greater than zerostrA follows strB in the sort order.
        public fun compare(strA: System_String? /* System.String */, strB: System_String? /* System.String */, comparisonType: System_StringComparison /* System.StringComparison */) : Int /* System.Int32 */ {
            val strAC = strA.getHandleOrNullPointer()
            val strBC = strB.getHandleOrNullPointer()
            val comparisonTypeC = comparisonType.rawValue


            val __exceptionC = PointerByReference()

            val __returnValueC = BeyondDotNETSampleNative.System_String_Compare_2(strAC, strBC, comparisonTypeC, __exceptionC)

            if (__exceptionC.value !== Pointer.NULL) {
                // TODO
                // throw System_Exception(__exceptionC).toKException()
                throw Exception("TODO: Convert System.Exception to Kotlin Exception")
            }

            return __returnValueC

        }
    }

    override fun destroy() {
        BeyondDotNETSampleNative.System_String_Destroy(getHandleOrNullPointer())
    }
}

class System_Guid(handle: Pointer): System_Object(handle) {
    companion object {
        operator fun invoke(): System_Guid {
            val exceptionRef = PointerByReference()

            val returnValueHandle = BeyondDotNETSampleNative.System_Guid_Create_7(
                exceptionRef
            )

            val exceptionHandle = exceptionRef.value

            if (exceptionHandle !== Pointer.NULL) {
                throw System_Exception(exceptionHandle).toKException()
            }

            val returnValue = System_Guid(returnValueHandle)

            return returnValue
        }

        operator fun invoke(
            string: System_String
        ): System_Guid {
            val exceptionRef = PointerByReference()

            val returnValueHandle = BeyondDotNETSampleNative.System_Guid_Create_6(
                string.getHandleOrNullPointer(),
                exceptionRef
            )

            val exceptionHandle = exceptionRef.value

            if (exceptionHandle !== Pointer.NULL) {
                throw System_Exception(exceptionHandle).toKException()
            }

            val returnValue = System_Guid(returnValueHandle)

            return returnValue
        }

        val empty: System_Guid
            get() {
                val returnValueHandle = BeyondDotNETSampleNative.System_Guid_Empty_Get()
                val returnValue = System_Guid(returnValueHandle)

                return returnValue
            }

        /// Initializes a new instance of the System.Guid structure.
        /// - Returns: A new GUID object.
        public fun newGuid() : System_Guid /* System.Guid */ {


            val __exceptionC = PointerByReference()

            val __returnValueC = BeyondDotNETSampleNative.System_Guid_NewGuid(__exceptionC)

            val __returnValue = System_Guid(__returnValueC)

            if (__exceptionC.value !== Pointer.NULL) {
                // TODO
                // throw System_Exception(__exceptionC).toKException()
                throw Exception("TODO: Convert System.Exception to Kotlin Exception")
            }

            return __returnValue

        }

        /// Converts the string representation of a GUID to the equivalent System.Guid structure.
        /// - Parameter input: The string to convert.
        /// - Throws: System.ArgumentNullException: input is null.
        /// - Throws: System.FormatException: input is not in a recognized format.
        /// - Returns: A structure that contains the value that was parsed.
        public fun parse(input: System_String /* System.String */) : System_Guid /* System.Guid */ {
            val inputC = input.getHandleOrNullPointer()


            val __exceptionC = PointerByReference()

            val __returnValueC = BeyondDotNETSampleNative.System_Guid_Parse(inputC, __exceptionC)

            val __returnValue = System_Guid(__returnValueC)

            if (__exceptionC.value !== Pointer.NULL) {
                // TODO
                // throw System_Exception(__exceptionC).toKException()
                throw Exception("TODO: Convert System.Exception to Kotlin Exception")
            }

            return __returnValue

        }
    }

    override fun destroy() {
        BeyondDotNETSampleNative.System_Guid_Destroy(getHandleOrNullPointer())
    }

    fun dn_toString(): System_String {
        val exceptionRef = PointerByReference()

        val returnValueHandle = BeyondDotNETSampleNative.System_Guid_ToString(getHandleOrNullPointer(), exceptionRef)

        val exceptionHandle = exceptionRef.value

        if (exceptionHandle !== Pointer.NULL) {
            throw System_Exception(exceptionHandle).toKException()
        }

        val returnValue = System_String(returnValueHandle)

        return returnValue
    }
}



class Beyond_NET_Sample_Person(handle: Pointer): DNObject(handle) {
    companion object {
        // this method invocation looks like constructor invocation
        operator fun invoke(
            firstName: System_String,
            lastName: System_String,
            age: Int
        ): Beyond_NET_Sample_Person {
            val exceptionRef = PointerByReference()

            val returnValueHandle = BeyondDotNETSampleNative.Beyond_NET_Sample_Person_Create(
                firstName.getHandleOrNullPointer(),
                lastName.getHandleOrNullPointer(),
                age,
                exceptionRef
            )

            val exceptionHandle = exceptionRef.value

            if (exceptionHandle !== Pointer.NULL) {
                throw System_Exception(exceptionHandle).toKException()
            }

//            if (returnValueHandle == Pointer.NULL) {
//                return null
//            }

            val returnValue = Beyond_NET_Sample_Person(returnValueHandle)

            return returnValue
        }
    }

    override fun destroy() {
        BeyondDotNETSampleNative.Beyond_NET_Sample_Person_Destroy(getHandleOrNullPointer())
    }

    val fullName: System_String
        get() {
            val exceptionRef = PointerByReference()

            val returnValueHandle = BeyondDotNETSampleNative.Beyond_NET_Sample_Person_FullName_Get(
                getHandleOrNullPointer(),
                exceptionRef
            )

            val exceptionHandle = exceptionRef.value

            if (exceptionHandle !== Pointer.NULL) {
                throw System_Exception(exceptionHandle).toKException()
            }

            val returnValue = System_String(returnValueHandle)

            return returnValue
        }

    val welcomeMessage: System_String
        get() {
            val exceptionRef = PointerByReference()

            val returnValueHandle = BeyondDotNETSampleNative.Beyond_NET_Sample_Person_GetWelcomeMessage(
                getHandleOrNullPointer(),
                exceptionRef
            )

            val exceptionHandle = exceptionRef.value

            if (exceptionHandle !== Pointer.NULL) {
                throw System_Exception(exceptionHandle).toKException()
            }

            val returnValue = System_String(returnValueHandle)

            return returnValue
        }

    var age: Int
        get() {
            val exceptionRef = PointerByReference()

            val result = BeyondDotNETSampleNative.Beyond_NET_Sample_Person_Age_Get(getHandleOrNullPointer(), exceptionRef)

            val exceptionHandle = exceptionRef.value

            if (exceptionHandle !== Pointer.NULL) {
                throw System_Exception(exceptionHandle).toKException()
            }

            return result
        }
        set(value) {
            val exceptionRef = PointerByReference()

            BeyondDotNETSampleNative.Beyond_NET_Sample_Person_Age_Set(getHandleOrNullPointer(), value, exceptionRef)

            val exceptionHandle = exceptionRef.value

            if (exceptionHandle !== Pointer.NULL) {
                throw System_Exception(exceptionHandle).toKException()
            }
        }

    var niceLevel: Beyond_NET_Sample_NiceLevels
        get() {
            val exceptionRef = PointerByReference()

            val result = BeyondDotNETSampleNative.Beyond_NET_Sample_Person_NiceLevel_Get(getHandleOrNullPointer(), exceptionRef)

            val exceptionHandle = exceptionRef.value

            if (exceptionHandle !== Pointer.NULL) {
                throw System_Exception(exceptionHandle).toKException()
            }

            val resultEnum = Beyond_NET_Sample_NiceLevels.entries.first { it.rawValue == result }

            return resultEnum
        }
        set(value) {
            val exceptionRef = PointerByReference()

            BeyondDotNETSampleNative.Beyond_NET_Sample_Person_NiceLevel_Set(getHandleOrNullPointer(), value.rawValue, exceptionRef)

            val exceptionHandle = exceptionRef.value

            if (exceptionHandle !== Pointer.NULL) {
                throw System_Exception(exceptionHandle).toKException()
            }
        }
}

// Extensions
fun System_String.toKString(): String {
    val cString = BeyondDotNETSampleNative.DNStringToC(getHandleOrNullPointer())
    val string = cString.getString(0)

    val ptrVal = Pointer.nativeValue(cString)
    Native.free(ptrVal)

    return string
}

fun String.toDotNETString(): System_String {
    val dnStringHandle = BeyondDotNETSampleNative.DNStringFromC(this)
    val dnString = System_String(dnStringHandle)

    return dnString
}

fun System_Exception.toKException(): Exception {
    val exStrDN = dn_toString() // Should be message instead of toString
    val exStr = exStrDN.toKString()
    val ex = Exception(exStr)

    return ex
}