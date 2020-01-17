using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.InteropServices;

namespace CSfmt
{

	public class AlignedMemoryChunk : SafeHandleZeroOrMinusOneIsInvalid
	{
		private readonly IntPtr _head;
		private readonly int _size;
		private readonly int _alignment;


		private void Check()
		{
			if (IsInvalid) throw new InvalidOperationException("Invalid handle.");
			if (IsClosed) throw new ObjectDisposedException("AlignedMemoryChunk");
		}

		private void Check(int value, string paramName)
		{
			if (value < 0) throw new ArgumentOutOfRangeException(paramName);


			var tmp = value;

			if (tmp == 1) throw new ArgumentException("The alignment value, which must be an integer power of 2.", paramName);

			while (tmp != 1)
			{
				if (tmp % 2 != 0)
					throw new ArgumentException("The alignment value, which must be an integer power of 2.", paramName);
				tmp /= 2;
			}
		}

		public AlignedMemoryChunk(int size, int alignment) : base(true)
		{
			if (size <= 0) throw new ArgumentOutOfRangeException(nameof(size));
			Check(alignment, nameof(alignment));

			_head = Marshal.AllocCoTaskMem(size + alignment);


			var tmp = (ulong)_head;

			for (; ; )
			{
				if (tmp % (ulong)alignment == 0) break;
				tmp++;
			}

			SetHandle((IntPtr)tmp);
			_size = size;
			_alignment = alignment;
		}


		public IntPtr GetChunkHead()
		{
			Check();
			return _head;
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

		protected override bool ReleaseHandle()
		{
			try
			{
				Marshal.FreeCoTaskMem(_head);
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
	}

}