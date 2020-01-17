using System;
using System.Collections.Generic;
using ChainingAssertion;
using CSfmt;
using Xunit;

namespace CSfmtTest
{
	public unsafe class AlignedMemoryChunkTest
	{

		public static IEnumerable<object[]> CorrectAlign()
		{
			var ret = 2;
			for (int i = 1; i <= 7; i++)
			{
				yield return new object[] {(uint)ret};
				ret *= 2;
			}
		}


		[Theory] 
		[MemberData(nameof(CorrectAlign))]
		public void AllocateTest(int alignment)
		{
			var target = new AlignedMemoryChunk(1024, alignment);
			var ptr = (ulong) target.DangerousGetHandle();
			var ret = (int) (ptr % (ulong)alignment);
			ret.Is(0);
		}

		[Fact]
		public void InvalidAlignmentSizeTest()
		{
			Assert.Throws<ArgumentException>(() => new AlignedMemoryChunk(10, 1));
			Assert.Throws<ArgumentException>(() => new AlignedMemoryChunk(10, 3));
			Assert.Throws<ArgumentException>(() => new AlignedMemoryChunk(10, 101));
		}

		[Fact]
		public void CloseTest()
		{
			var chunk = new AlignedMemoryChunk(1024, 16);

			chunk.Dispose();
			chunk.IsClosed.IsTrue();
			chunk.IsInvalid.IsFalse();

			Assert.Throws<ObjectDisposedException>(() => chunk.GetChunkHead());
			Assert.Throws<ObjectDisposedException>(() => chunk.Alignment);
			Assert.Throws<ObjectDisposedException>(() => chunk.Size);
//			Assert.Throws<ObjectDisposedException>(() => chunk.DangerousGetHandle());

		}

	}
}
