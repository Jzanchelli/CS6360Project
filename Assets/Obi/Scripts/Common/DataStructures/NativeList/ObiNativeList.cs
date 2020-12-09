using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace Obi
{
    public unsafe abstract class ObiNativeList<T> : IDisposable, ISerializationCallbackReceiver where T : struct 
    {
        public T[] serializedContents;
        protected void* m_AlignedPtr = null;

        protected int m_Stride;
        protected int m_Capacity;
        protected int m_Count;

#if ENABLE_UNITY_COLLECTIONS_CHECKS
        protected AtomicSafetyHandle m_SafetyHandle;
#endif
        protected bool m_AsNativeArray = false;

        protected ComputeBuffer m_ComputeBuffer;

        public int count
        {
            set 
            {
                if (value != m_Count)
                {
                    // we should not use ResizeUninitialized as it would destroy all current data.
                    // we first ensure we can hold the previous count, and then set the new one.
                    EnsureCapacity(m_Count);
                    m_Count = Mathf.Min(m_Capacity,value);
                }
            }
            get { return m_Count; }
        }

        public int capacity
        {
            set
            {
                if (value != m_Capacity)
                    ChangeCapacity(value);
            }
            get { return m_Capacity; }
        }

        public bool isCreated
        {
            get { return m_AlignedPtr != null; }
        }

        public T this[int index]
        {
            get
            {
                return UnsafeUtility.ReadArrayElementWithStride<T>(m_AlignedPtr, index, m_Stride);
            }
            set
            {
                UnsafeUtility.WriteArrayElementWithStride<T>(m_AlignedPtr, index, m_Stride, value);

                if (m_ComputeBuffer != null)
                    m_ComputeBuffer.SetData(AsNativeArray<T>(), index, index, 1);
            }
        }

        public ObiNativeList(int capacity = 8, int alignment = 16)
        {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
            m_SafetyHandle = AtomicSafetyHandle.Create();
#endif

            ChangeCapacity(capacity);
        }

        ~ObiNativeList()
        {
            Dispose(false);
        }

        protected void Dispose(bool disposing)
        {
            if (isCreated)
            {

#if ENABLE_UNITY_COLLECTIONS_CHECKS
                // dispose of atomic safety handle:
                if (m_AsNativeArray)
                {
                    AtomicSafetyHandle.CheckDeallocateAndThrow(m_SafetyHandle);
                    AtomicSafetyHandle.Release(m_SafetyHandle);
                    m_AsNativeArray = false;
                }
#endif

                // dispose of compuse buffer representation:
                if (m_ComputeBuffer != null)
                {
                    m_ComputeBuffer.Dispose();
                }

                // free unmanaged memory buffer:
                UnsafeUtility.Free(m_AlignedPtr,Allocator.Persistent);
                m_AlignedPtr = null;
            }
        }

        public void Dispose() 
        {
            Dispose(true);
        }

        public void OnBeforeSerialize()
        {

            if (isCreated)
            {
                // create a new managed array to serialize the data:
                serializedContents = new T[m_Count];
                    
                // pin the managed array and get its address:
                ulong serializedContentsHandle;
                var serializedContentsAddress = UnsafeUtility.PinGCArrayAndGetDataAddress(serializedContents, out serializedContentsHandle);

                // copy data over to the managed array:
                m_Stride = UnsafeUtility.SizeOf<T>();
                UnsafeUtility.MemCpy(serializedContentsAddress, m_AlignedPtr, m_Count * m_Stride);
                    
                // unpin the managed array:
                UnsafeUtility.ReleaseGCObject(serializedContentsHandle);
            }
        }

        public void OnAfterDeserialize()
        {
            if (serializedContents != null)
            {
                unsafe
                {
                    // resize to receive the serialized data:
                    ResizeUninitialized(serializedContents.Length);

                    // pin the managed array and get its address:
                    ulong serializedContentsHandle;
                    var serializedContentsAddress = UnsafeUtility.PinGCArrayAndGetDataAddress(serializedContents, out serializedContentsHandle);

                    // copy data from to the managed array:
                    m_Stride = UnsafeUtility.SizeOf<T>();
                    UnsafeUtility.MemCpy(m_AlignedPtr, serializedContentsAddress, m_Count* m_Stride);

                    // unpin the managed array:
                    UnsafeUtility.ReleaseGCObject(serializedContentsHandle);
                }
            }
        }

        // Reinterprets the data in the list as a native array.
        public NativeArray<U> AsNativeArray<U>() where U : struct
        {
            unsafe
            {
                NativeArray<U> array = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<U>(m_AlignedPtr, m_Count, Allocator.None);

#if ENABLE_UNITY_COLLECTIONS_CHECKS
                NativeArrayUnsafeUtility.SetAtomicSafetyHandle(ref array, m_SafetyHandle);
                m_AsNativeArray = true;
#endif
                return array;
            }
        }

        // Reinterprets the data in the list as a compute buffer. Note: This also calls AsNativeArray() internally, to be able to pass the raw pointer to the compute buffer 
        public ComputeBuffer AsComputeBuffer<U>() where U : struct
        {
            if (m_ComputeBuffer != null)
            {
                m_ComputeBuffer.Dispose();
            }

            m_ComputeBuffer = new ComputeBuffer(m_Count, m_Stride);
            m_ComputeBuffer.SetData(AsNativeArray<U>());
            return m_ComputeBuffer;
        }

        protected void ChangeCapacity(int newCapacity, int byteAlignment = 16)
        {
            // allocate a new buffer:
            m_Stride = UnsafeUtility.SizeOf<T>();
            var newAlignedPtr = UnsafeUtility.Malloc(newCapacity * m_Stride, 16, Allocator.Persistent); 

            // if there was a previous allocation:
            if (isCreated)
            {
                // copy contents from previous memory region
                unsafe
                {
                    UnsafeUtility.MemCpy(newAlignedPtr, m_AlignedPtr, Mathf.Min(newCapacity,m_Capacity) * m_Stride);
                }

                // free previous memory region
                UnsafeUtility.Free(m_AlignedPtr, Allocator.Persistent);
            }

            // get hold of new pointers/capacity.
            m_AlignedPtr = newAlignedPtr;
            m_Capacity = newCapacity;
        }

        public void CopyFrom(ObiNativeList<T> other)
        {
            if (other == null)
                throw new ArgumentNullException();

            if (m_Count < other.m_Count)
                throw new ArgumentException();

            unsafe
            {
                UnsafeUtility.MemCpy(m_AlignedPtr, other.m_AlignedPtr, other.count * m_Stride);
            }
        }

        public void Clear()
        {
            m_Count = 0;
        }

        public void Add(T item)
        {
            EnsureCapacity(m_Count + 1);
            this[m_Count++] = item;
        }


        public void AddRange(IEnumerable<T> enumerable)
        {
            ICollection<T> collection = enumerable as ICollection<T>;
            if (collection != null)
            {
                if (collection.Count > 0)
                {
                    EnsureCapacity(m_Count + collection.Count);
                }
            }

            using (IEnumerator<T> enumerator = collection.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    Add(enumerator.Current);
                }
            }
        }

        public void RemoveRange(int index, int count)
        {
            if (index < 0 || count < 0)
                throw new ArgumentOutOfRangeException();

            if (index + count > m_Count)
                throw new ArgumentException();

            for (int i = index; i < m_Count - count; ++i)
                this[i] = this[i + count];

            m_Count -= count;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= count)
                throw new ArgumentOutOfRangeException();

            for (int i = index; i < m_Count-1; ++i)
                this[i] = this[i + 1];
         
            m_Count--;
        }

        /**
         * Ensures a minimal capacity of count elements, then sets the new count. Useful when passing the backing array to C++
         * for being filled with new data.
         */
        public bool ResizeUninitialized(int newCount)
        {
            newCount = Mathf.Max(0, newCount);
            bool realloc = EnsureCapacity(newCount);
            m_Count = newCount;
            return realloc;
        }

        public bool ResizeInitialized(int newCount, T value = default(T))
        {
            bool initialize = newCount >= m_Capacity || !isCreated;

            bool realloc = ResizeUninitialized(newCount);

            if (initialize)
            {
                for (int i = m_Count; i < m_Capacity; ++i)
                    this[i] = value;
            }

            return realloc;
        }

        protected bool EnsureCapacity(int min)
        {
            if (min >= m_Capacity || !isCreated)
            {
                ChangeCapacity(min * 2);
                return true;
            }
            return false;
        }

        public void WipeToZero()
        {
            unsafe
            {
                UnsafeUtility.MemClear(m_AlignedPtr, count * m_Stride);
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append('[');

            for (int t = 0; t < m_Count; t++)
            {
                sb.Append(this[t].ToString());

                if (t < (m_Count - 1)) sb.Append(',');

            }
            sb.Append(']');
            return sb.ToString();
        }

        public IntPtr GetIntPtr()
        {
            return new IntPtr(m_AlignedPtr);
        }

        public void Swap(int index1, int index2)
        {
            // check to avoid out of bounds access:
            if (index1 >= 0 && index1 < count && index2 >= 0 && index2 < count)
            {
                var aux = this[index1];
                this[index1] = this[index2];
                this[index2] = aux;
            }
        }
    }
}

