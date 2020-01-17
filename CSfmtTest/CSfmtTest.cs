using ChainingAssertion;
using CSfmt;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using static CSfmt.Defination;

namespace CSfmtTest
{
	public unsafe class CSfmtTest
	{

		[Fact]
		public void InitGenRandTest()
		{
			using var sfmt=new sfmt_t();

			CSfmt.SfmtNative.sfmt_init_gen_rand(sfmt, 1234);

			var expected = File.ReadLines("./Data/expected.txt").Skip(1).Select(x => x.Split('\t'))
				.Select(x => (index: int.Parse(x[0]), value: uint.Parse(x[1]))).ToArray();

			var actual = new List<(int index, uint value)>();

			var span = new Span<uint>(sfmt.state, Defination.SFMT_N32);

			span.Length.Is(expected.Length + 1);

			for (int i = 1; i < span.Length; i++)
			{
				span[i].Is(expected[i - 1].value);
			}
		}

		[Fact]
		public void SfmtFillArray32Test()
		{
			const int size = 1024;

			using var chunk_buffer = new AlignedMemoryChunk(sizeof(uint) * size, 16);


			var sfmt = new sfmt_t();

			uint* buffer = (uint*)chunk_buffer.DangerousGetHandle();


			SfmtNative.sfmt_init_gen_rand(sfmt, 1234);
			SfmtNative.sfmt_fill_array32(sfmt, buffer, size);

			var expected = File.ReadLines("./Data/AfterStateFill32.txt").Select(x => uint.Parse(x)).ToArray();

			var actual = new Span<uint>(sfmt.state, SFMT_N32);
			actual.Length.Is(expected.Length);

			for (int i = 0; i < actual.Length; i++)
			{
				actual[i].Is(expected[i]);
			}


			var actualA = new Span<uint>(buffer, size).ToArray();

			SfmtNative.sfmt_fill_array32(sfmt, buffer, size);
			var actualB = new Span<uint>(buffer, size).ToArray();

			var actualM = actualA.Concat(actualB).ToArray();

			expected = File.ReadLines("./Data/init1234Fill32.txt").Select(x => uint.Parse(x)).ToArray();

			actualM.Length.Is(expected.Length);

			for (int i = 0; i < actualM.Length; i++)
			{
				actualM[i].Is(expected[i]);
			}
		}

		[Fact]
		public void SfmtFillArray64Test()
		{
			const int size = 1024;

			using var chunk_buffer = new AlignedMemoryChunk(sizeof(ulong) * size, 16);

			var sfmt =new sfmt_t();

			ulong* buffer = (ulong*)chunk_buffer.DangerousGetHandle();

			SfmtNative.sfmt_init_gen_rand(sfmt, 1234);
			SfmtNative.sfmt_fill_array64(sfmt, buffer, size);

			{
				var expected = File.ReadLines("./Data/AfterStateFill64.txt").Select(x => uint.Parse(x)).ToArray();
				var actual = new Span<uint>(sfmt.state, SFMT_N32);
				actual.Length.Is(expected.Length);

				for (var i = 0; i < actual.Length; i++)
				{
					actual[i].Is(expected[i]);
				}
			}

			{
				var actA = new Span<ulong>(buffer, size).ToArray();
				SfmtNative.sfmt_fill_array64(sfmt, buffer, size);

				var actB = new Span<ulong>(buffer, size).ToArray();

				var act = actA.Concat(actB).ToArray();

				var expected = File.ReadLines("./Data/Init1234Fill64.txt").Select(x => ulong.Parse(x)).ToArray();

				act.Length.Is(expected.Length);

				for (int i = 0; i < act.Length; i++)
				{
					act[i].Is(expected[i]);
				}
			}
		}

		[Fact]
		public unsafe void SfmtInitArrayTest()
		{
			const int size = 1024;

			using var chunk_array = new AlignedMemoryChunk(sizeof(uint) * size, 16);

			uint* key = stackalloc uint[] { 0x1234, 0x5678, 0x9abc, 0xdef0 };



			var sfmt = new sfmt_t();

			uint* array = (uint*)chunk_array.DangerousGetHandle();

			SfmtNative.sfmt_init_by_array(sfmt, key, 4);

			{
				var expected = File.ReadLines("./Data/initArrayFirstState.txt").Select(x => uint.Parse(x)).ToArray();

				var actual = new Span<uint>(sfmt.state, SFMT_N32);
				actual.Length.Is(expected.Length);

				for (int i = 0; i < expected.Length; i++)
				{
					actual[i].Is(expected[i]);
				}

			}

			SfmtNative.sfmt_fill_array32(sfmt, array, 1024);

			{
				var expected = File.ReadLines("./Data/initArrayNextState.txt").Select(x => uint.Parse(x)).ToArray();
				var actual = new Span<uint>(sfmt.state, SFMT_N32);
				actual.Length.Is(expected.Length);

				for (int i = 0; i < expected.Length; i++)
				{
					actual[i].Is(expected[i]);
				}
			}

			{
				var expected = File.ReadLines("./Data/initArrayRnd32.txt").Select(x => uint.Parse(x)).ToArray();
				var fst = new Span<uint>(array, 1024).ToArray();

				SfmtNative.sfmt_fill_array32(sfmt, array, 1024);

				var second = new Span<uint>(array, 1024).ToArray();

				var actual = fst.Concat(second).ToArray();

				actual.Length.Is(expected.Length);

				for (int i = 0; i < actual.Length; i++)
				{
					actual[i].Is(expected[i]);
				}

			}


		}




	}
}
