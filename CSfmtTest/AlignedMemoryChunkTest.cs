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
			for (var i = 1; i <= 7; i++)
			{
				yield return new object[] {(uint) ret};
				ret *= 2;
			}
		}


		[Theory]
		[MemberData(nameof(CorrectAlign))]
		public void AllocateTest(int alignment)
		{
			var target = new AlignedMemoryChunk(1024, alignment);
			var ptr = (ulong) target.AlignedHead;
			var ret = (int) (ptr % (ulong) alignment);
			ret.Is(0);
		}

		[Fact]
		public void AsSpanTest()
		{
			using var chunk = new AlignedMemoryChunk(1024, 16);

			{
				var span = chunk.AsSpan<uint>(256);

				for (var i = 0; i < 256; i++) span[i] = (uint) i;
			}


			{
				var span = chunk.AsSpan<uint>(256);
				var roSpan = chunk.AsReadOnlySpan<uint>(256);
				var ptr = (uint*) chunk.AlignedHead;

				for (var i = 0; i < span.Length; i++)
				{
					span[i].Is((uint) i);
					roSpan[i].Is((uint) i);
					ptr[i].Is((uint) i);
				}
			}
		}

		[Fact]
		public void CloseTest()
		{
			var chunk = new AlignedMemoryChunk(1024, 16);

			chunk.Dispose();
			chunk.IsClosed.IsTrue();

			Assert.Throws<ObjectDisposedException>(() => chunk.GetChunkHead);
			Assert.Throws<ObjectDisposedException>(() => chunk.Alignment);
			Assert.Throws<ObjectDisposedException>(() => chunk.Size);
		}

		[Fact]
		public void GetSizeTest()
		{
			using var chunk = new AlignedMemoryChunk(1024, 16);

			chunk.Alignment.Is(16);
			chunk.Size.Is(1024);
		}

		[Fact]
		public void InvalidAlignmentSizeTest()
		{
			Assert.Throws<ArgumentException>(() => new AlignedMemoryChunk(10, 1));
			Assert.Throws<ArgumentException>(() => new AlignedMemoryChunk(10, 3));
			Assert.Throws<ArgumentException>(() => new AlignedMemoryChunk(10, 101));
		}
	}
}