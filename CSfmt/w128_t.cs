using System;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using static CSfmt.Defination;


namespace CSfmt
{
	[StructLayout(LayoutKind.Explicit)]
	public unsafe struct w128_t
	{
		[FieldOffset(0)] public fixed uint u[4];
		[FieldOffset(0)] public fixed ulong u64[2];
		[FieldOffset(0)] public Vector128<int> si;
	}



	public unsafe class sfmt_t:IDisposable
	{
		private readonly AlignedMemoryChunk _chunk;
		public int idx;

		public readonly w128_t* state;

		public sfmt_t()
		{
			_chunk = new AlignedMemoryChunk(sizeof(ulong) * SFMT_N64, 16);
			state = (w128_t*) _chunk.DangerousGetHandle();
			idx = 0;
		}

		public void Copy(sfmt_t source)
		{
			for (var i = 0; i < SFMT_N64; i++)
			{
				state->u64[i] = source.state->u64[i];
			}
		}

		public void Dispose() => _chunk.Dispose();
	}
}