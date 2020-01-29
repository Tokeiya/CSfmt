using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static CSfmt.Float.FloatDefination;


//struct DSFMT_T
//{
//	w128_t status[DSFMT_N + 1];
//	int idx;
//};


namespace CSfmt.Float
{
	public unsafe class dSfmtPrimitiveState:IDisposable
	{
		private readonly AlignedMemoryChunk _chunk;

		public readonly FloatW128* status;
		public int idx;


		public dSfmtPrimitiveState()
		{
			_chunk = new AlignedMemoryChunk(sizeof(ulong) * (DSFMT_N + 1) * 2, 16);
			//_chunk = new AlignedMemoryChunk(3104, 16);
			status = (FloatW128*) _chunk.AlignedHead;
			idx = 0;
		}



		public void  Dispose()
		{
			_chunk.Dispose();
		}
	}
}
