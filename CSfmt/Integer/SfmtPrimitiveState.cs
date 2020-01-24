using System;

namespace CSfmt.Integer
{
	public unsafe class SfmtPrimitiveState : IDisposable
	{
		private readonly AlignedMemoryChunk _chunk;

		public readonly IntegerW128* State;
		public int Index;

		public SfmtPrimitiveState()
		{
			_chunk = new AlignedMemoryChunk(sizeof(ulong) * IntegerDefinition.N64, 16);
			State = (IntegerW128*) _chunk.AlignedHead;
			Index = 0;
		}

		public void Dispose()
		{
			_chunk.Dispose();
		}

		public void Copy(SfmtPrimitiveState source)
		{
			for (var i = 0; i < IntegerDefinition.N64; i++) State->u64[i] = source.State->u64[i];
		}
	}
}