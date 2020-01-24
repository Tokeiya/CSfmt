using System;
using System.Collections;
using System.Collections.Generic;

namespace CSfmt
{
	public sealed unsafe class AlignedArray<T> : IDisposable, IReadOnlyList<T> where T : unmanaged
	{
		private readonly AlignedMemoryChunk _chunk;

		public AlignedArray(int length, int alignment)
		{
			if (length <= 0) throw new ArgumentOutOfRangeException(nameof(length));

			IsMultipleOf2(alignment, nameof(alignment));

			_chunk = new AlignedMemoryChunk(sizeof(T) * length, alignment);
			StatusUncheckedPointer = (T*) _chunk.AlignedHead;

			Count = length;
			Alignment = alignment;
		}

		internal T* StatusUncheckedPointer { get; private set; }

		public int Alignment { get; }

		public void Dispose()
		{
			_chunk.Dispose();
			StatusUncheckedPointer = null;
		}

		public IEnumerator<T> GetEnumerator()
		{
			DisposeCheck();
			return GetEnumeratorImpl();
		}


		public int Count { get; }

		public T this[int index]
		{
			get
			{
				if ((uint) index > Count) throw new IndexOutOfRangeException();

				DisposeCheck();

				return GetStatusUncheckedSpan()[index];
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		private void DisposeCheck()
		{
			if (StatusUncheckedPointer == null) throw new ObjectDisposedException(nameof(AlignedArray<T>));
		}

		private static void IsMultipleOf2(int value, string paramName)
		{
			if (value <= 1) throw new ArgumentOutOfRangeException(paramName);

			var tmp = value;

			while (tmp != 1)
			{
				if (tmp % 2 != 0) throw new ArgumentOutOfRangeException(paramName);
				tmp >>= 2;
			}
		}

		public ReadOnlySpan<T> GetStatusUncheckedSpan()
		{
			return new ReadOnlySpan<T>(StatusUncheckedPointer, Count);
		}

		private IEnumerator<T> GetEnumeratorImpl()
		{
			for (var i = 0; i < Count; i++) yield return this[i];
		}
	}
}