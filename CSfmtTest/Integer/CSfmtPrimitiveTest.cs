using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ChainingAssertion;
using CSfmt;
using CSfmt.Integer;
using Xunit;
using static CSfmt.Integer.IntegerDefinition;
using static CSfmt.Integer.SfmtPrimitive;

namespace CSfmtTest.Integer
{
	public unsafe class CSfmtPrimitiveTest
	{
		private static void Equal(SfmtPrimitiveState? expected, SfmtPrimitiveState? actual)
		{
			if (expected is null && actual is null) Assert.True(true);
			if (expected is null || actual is null) Assert.True(false);

			actual!.Index.Is(expected!.Index);

			var eSpan = new ReadOnlySpan<ulong>(expected.State, N64);
			var aSpan = new ReadOnlySpan<ulong>(actual.State, N64);

			for (var i = 0; i < N64; i++) aSpan[i].Is(eSpan[i]);
		}

		private static void DeepCopy(SfmtPrimitiveState source, SfmtPrimitiveState destination)
		{
			var scr = new ReadOnlySpan<ulong>(source.State, N64);
			var dest = new Span<ulong>(destination.State, N64);

			for (var i = 0; i < N64; i++) dest[i] = scr[i];

			destination.Index = source.Index;
		}


		[Fact]
		public void InitGenRandTest()
		{
			using var sfmt = new SfmtPrimitiveState();

			InitGenRand(sfmt, 1234);

			var expected = File.ReadLines("./Data/expected.txt").Skip(1).Select(x => x.Split('\t'))
				.Select(x => (index: int.Parse(x[0]), value: uint.Parse(x[1]))).ToArray();

			var actual = new List<(int index, uint value)>();

			var span = new Span<uint>(sfmt.State, N32);

			span.Length.Is(expected.Length + 1);

			for (var i = 1; i < span.Length; i++) span[i].Is(expected[i - 1].value);
		}

		[Fact]
		public void SfmtFillArray32ErrorTest()
		{
			const int size = 1024;

			using var sfmt = new SfmtPrimitiveState();
			InitGenRand(sfmt, 1234);


			var buffer = new AlignedArray<uint>(size, 16);


			GenRandUint32(sfmt);
			using var expected = new SfmtPrimitiveState();
			DeepCopy(sfmt, expected);


			Assert.Throws<ArgumentException>(() => FillArray32(sfmt, buffer, size));
			Equal(sfmt, expected);


			InitGenRand(sfmt, 1234);
			DeepCopy(sfmt, expected);
			Assert.Throws<ArgumentOutOfRangeException>(() => FillArray32(sfmt, buffer, 1023));
			Equal(sfmt, expected);

			Assert.Throws<ArgumentOutOfRangeException>(() => FillArray32(sfmt, buffer, 620));
			Equal(sfmt, expected);
		}

		[Fact]
		public void SfmtFillArray32Test()
		{
			const int size = 1024;


			using var sfmt = new SfmtPrimitiveState();

			var buffer = new AlignedArray<uint>(size, 16);


			InitGenRand(sfmt, 1234);
			FillArray32(sfmt, buffer, size);

			var expected = File.ReadLines("./Data/AfterStateFill32.txt").Select(x => uint.Parse(x)).ToArray();

			var actual = new Span<uint>(sfmt.State, N32);
			actual.Length.Is(expected.Length);

			for (var i = 0; i < actual.Length; i++) actual[i].Is(expected[i]);


			var actualA = buffer.ToArray();

			FillArray32(sfmt, buffer, size);
			var actualB = buffer.ToArray();

			var actualM = actualA.Concat(actualB).ToArray();

			expected = File.ReadLines("./Data/init1234Fill32.txt").Select(x => uint.Parse(x)).ToArray();

			actualM.Length.Is(expected.Length);

			for (var i = 0; i < actualM.Length; i++) actualM[i].Is(expected[i]);
		}


		[Fact]
		public void SfmtFillArray64ErrorTest()
		{
			const int size = 1024;

			var buffer = new AlignedArray<ulong>(size, 16);

			using var actual = new SfmtPrimitiveState();
			InitGenRand(actual, 1234);
			GenRandUint64(actual);

			using var expected = new SfmtPrimitiveState();
			DeepCopy(actual, expected);


			Assert.Throws<ArgumentException>(() => FillArray64(actual, buffer, size));
			Equal(expected, actual);

			InitGenRand(actual, 1234);
			DeepCopy(actual, expected);

			Assert.Throws<ArgumentOutOfRangeException>(() => FillArray64(actual, buffer, 1023));
			Assert.Throws<ArgumentOutOfRangeException>(() => FillArray64(actual, buffer, 310));

			Equal(expected, actual);
		}


		[Fact]
		public void SfmtFillArray64Test()
		{
			const int size = 1024;

			var sfmt = new SfmtPrimitiveState();

			var buffer = new AlignedArray<ulong>(size, 16);


			InitGenRand(sfmt, 1234);
			FillArray64(sfmt, buffer, size);

			{
				var expected = File.ReadLines("./Data/AfterStateFill64.txt").Select(x => uint.Parse(x)).ToArray();
				var actual = new Span<uint>(sfmt.State, N32);
				actual.Length.Is(expected.Length);

				for (var i = 0; i < actual.Length; i++) actual[i].Is(expected[i]);
			}

			{
				var actA = buffer.ToArray();
				FillArray64(sfmt, buffer, size);

				var actB = buffer.ToArray();

				var act = actA.Concat(actB).ToArray();

				var expected = File.ReadLines("./Data/Init1234Fill64.txt").Select(x => ulong.Parse(x)).ToArray();

				act.Length.Is(expected.Length);

				for (var i = 0; i < act.Length; i++) act[i].Is(expected[i]);
			}
		}


		[Fact]
		public void SfmtGetAttributesTest()
		{
			using var sfmt = new SfmtPrimitiveState();
			InitGenRand(sfmt, 1234);

			IdString.Is("SFMT-19937:122-18-1-11-1:dfffffef-ddfecb7f-bffaffff-bffffff6");
			MinArraySize32.Is(624);
			MinArraySize64.Is(312);
		}

		[Fact]
		public void SfmtInitArrayLongKey()
		{
			const int size = 1024;
			var key = Enumerable.Range(0, 32).Select(x => (uint) (x + 42)).ToArray();

			using var sfmt = new SfmtPrimitiveState();

			InitByArray(sfmt, key);

			var expected = File.ReadLines("./Data/LongKey.txt").Select(x => ulong.Parse(x)).ToArray();

			var p = new AlignedArray<ulong>(size, 16);


			FillArray64(sfmt, p, size);

			for (var i = 0; i < size; i++) p[i].Is(expected[i]);
		}

		[Fact]
		public void SfmtInitArrayTest()
		{
			const int size = 1024;

			ReadOnlySpan<uint> key = stackalloc uint[] {0x1234, 0x5678, 0x9abc, 0xdef0};


			var sfmt = new SfmtPrimitiveState();

			var array = new AlignedArray<uint>(size, 16);

			InitByArray(sfmt, key);

			{
				var expected = File.ReadLines("./Data/initArrayFirstState.txt").Select(x => uint.Parse(x)).ToArray();

				var actual = new Span<uint>(sfmt.State, N32);
				actual.Length.Is(expected.Length);

				for (var i = 0; i < expected.Length; i++) actual[i].Is(expected[i]);
			}

			FillArray32(sfmt, array, 1024);

			{
				var expected = File.ReadLines("./Data/initArrayNextState.txt").Select(x => uint.Parse(x)).ToArray();
				var actual = new Span<uint>(sfmt.State, N32);
				actual.Length.Is(expected.Length);

				for (var i = 0; i < expected.Length; i++) actual[i].Is(expected[i]);
			}

			{
				var expected = File.ReadLines("./Data/initArrayRnd32.txt").Select(x => uint.Parse(x)).ToArray();
				var fst = array.ToArray();

				FillArray32(sfmt, array, 1024);

				var second = array.ToArray();

				var actual = fst.Concat(second).ToArray();

				actual.Length.Is(expected.Length);

				for (var i = 0; i < actual.Length; i++) actual[i].Is(expected[i]);
			}
		}
	}
}