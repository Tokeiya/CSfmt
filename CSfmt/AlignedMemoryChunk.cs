using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace CSfmt
{
	public sealed class AlignedMemoryChunk : CriticalFinalizerObject, IDisposable
	{
		private readonly IntPtr _aligned;
		private readonly int _alignment;
		private readonly IntPtr _head;
		private readonly int _size;

		public AlignedMemoryChunk(int size, int alignment)
		{
			if (size <= 0) throw new ArgumentOutOfRangeException(nameof(size));
			Check(alignment, nameof(alignment));

			_head = Marshal.AllocCoTaskMem(size + alignment);


			var tmp = (ulong) _head;

			for (;;)
			{
				if (tmp % (ulong) alignment == 0) break;
				tmp++;
			}

			_aligned = (IntPtr) tmp;


			_size = size;
			_alignment = alignment;
		}


		public IntPtr GetChunkHead
		{
			get
			{
				Check();
				return _head;
			}
		}

		public IntPtr AlignedHead
		{
			get
			{
				Check();
				return _aligned;
			}
		}

		public int Size
		{
			get
			{
				Check();
				return _size;
			}
		}

		public int Alignment
		{
			get
			{
				Check();
				return _alignment;
			}
		}

		public bool IsClosed { get; private set; }


		public void Dispose()
		{
			Dispose(true);
		}


		private void Check()
		{
			if (IsClosed) throw new ObjectDisposedException("AlignedMemoryChunk");
		}

		private void Check(int value, string paramName)
		{
			if (value < 0) throw new ArgumentOutOfRangeException(paramName);


			var tmp = value;

			if (tmp == 1)
				throw new ArgumentException("The alignment value, which must be an integer power of 2.", paramName);

			while (tmp != 1)
			{
				if (tmp % 2 != 0)
					throw new ArgumentException("The alignment value, which must be an integer power of 2.", paramName);
				tmp /= 2;
			}
		}

		public unsafe Span<T> AsSpan<T>(int length) where T : unmanaged
		{
			Check();
			return new Span<T>((void*) _aligned, length);
		}

		public unsafe ReadOnlySpan<T> AsReadOnlySpan<T>(int length) where T : unmanaged
		{
			Check();
			return new ReadOnlySpan<T>((void*) _aligned, length);
		}


		private void Dispose(bool disposing)
		{
			if (IsClosed) return;

			IsClosed = true;
			Marshal.FreeCoTaskMem(_head);

			if (disposing) GC.SuppressFinalize(this);
		}

		public void Close()
		{
			Dispose(true);
		}

		~AlignedMemoryChunk()
		{
			Dispose(false);
		}
	}
}