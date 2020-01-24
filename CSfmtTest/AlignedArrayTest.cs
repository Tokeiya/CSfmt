using System;
using System.Collections;
using System.Reflection;
using ChainingAssertion;
using CSfmt;
using CSfmt.Integer;
using Xunit;
using Xunit.Abstractions;

namespace CSfmtTest
{
	public abstract class AlignedArrayTest<T> where T : unmanaged
	{
		protected AlignedArrayTest(ITestOutputHelper output)
		{
			_output = output;
		}

		private readonly ITestOutputHelper _output;


		private unsafe AlignedArray<T> Generate(Func<int, T>? pred = null, int length = SfmtPrimitive.MinArraySize64,
			int alignment = 16)
		{
			if (pred is null) pred = ValueSetter;

			var ret = new AlignedArray<T>(length, alignment);
			for (var i = 0; i < length; i++) ret.StatusUncheckedPointer[i] = pred(i);

			return ret;
		}

		protected abstract void AreEqual(T actual, int expected);

		protected abstract void AreEqual(object? actual, int expected);

		protected abstract T ValueSetter(int i);

		[Fact]
		public unsafe void GetAttributesTest()
		{
			using var actual = new AlignedArray<T>(SfmtPrimitive.MinArraySize64, 16);
			actual.Count.Is(SfmtPrimitive.MinArraySize64);
			actual.Alignment.Is(16);

			var tmp = (ulong) actual.StatusUncheckedPointer;
			(tmp % 16UL).Is(0UL);
		}

		[Fact]
		public void GetEnumeratorTest()
		{
			using var actual = Generate();

			var cnt = 0;

			using (var genEnum = actual.GetEnumerator())
			{
				while (genEnum.MoveNext()) AreEqual((object?) genEnum.Current, cnt++);
			}

			var objEnum = ((IEnumerable) actual).GetEnumerator();

			cnt = 0;
			while (objEnum.MoveNext()) AreEqual(objEnum.Current, cnt++);

			using var disGenEnum = actual.GetEnumerator();
			var disObjEnum = ((IEnumerable) actual).GetEnumerator();


			disGenEnum.MoveNext();
			disObjEnum.MoveNext();

			actual.Dispose();

			Assert.Throws<ObjectDisposedException>(() => actual.GetEnumerator());
			Assert.Throws<ObjectDisposedException>(() => ((IEnumerable) actual).GetEnumerator());

			Assert.Throws<ObjectDisposedException>(() => disGenEnum.MoveNext());
			Assert.Throws<ObjectDisposedException>(() => disObjEnum.MoveNext());
		}


		[Fact]
		public void IndexerTest()
		{
			using var actual = Generate();

			for (var i = 0; i < actual.Count; i++) AreEqual(actual[i], i);

			Assert.Throws<IndexOutOfRangeException>(() => actual[-1]);
			Assert.Throws<IndexOutOfRangeException>(() => actual[SfmtPrimitive.MinArraySize64]);

			actual.Dispose();


			Assert.Throws<ObjectDisposedException>(() => actual[10]);
		}


		[Fact]
		public void InitTest()
		{
			var actual = new AlignedArray<T>(SfmtPrimitive.MinArraySize64, 16);
			actual.Dispose();

			actual = new AlignedArray<T>(SfmtPrimitive.MinArraySize64 + 2, 16);


			Assert.Throws<ArgumentOutOfRangeException>(() => new AlignedArray<ulong>(SfmtPrimitive.MinArraySize64, 15));
			Assert.Throws<ArgumentOutOfRangeException>(() => new AlignedArray<ulong>(SfmtPrimitive.MinArraySize64, 17));
		}

		[Fact]
		public unsafe void PointerTest()
		{
			using var actual = Generate();

			for (var i = 0; i < actual.Count; i++) actual.StatusUncheckedPointer[i] = ValueSetter(i + 100);

			for (var i = 0; i < actual.Count; i++) AreEqual(actual.StatusUncheckedPointer[i], i + 100);
		}


		[Fact]
		public void StatusUncheckedSpanTest()
		{
			using var actual = Generate();

			var span = actual.GetStatusUncheckedSpan();

			for (var i = 0; i < span.Length; i++) AreEqual(span[i], i);

			Assert.Throws<IndexOutOfRangeException>(() => actual.GetStatusUncheckedSpan()[-1]);
			Assert.Throws<IndexOutOfRangeException>(() =>
				actual.GetStatusUncheckedSpan()[SfmtPrimitive.MinArraySize64]);
		}
	}

	public class UInt64ArrayTest : AlignedArrayTest<ulong>
	{
		public UInt64ArrayTest(ITestOutputHelper output) : base(output)
		{
		}


		protected override void AreEqual(ulong actual, int expected)
		{
			actual.Is((ulong) expected);
		}


		protected override void AreEqual(object? actual, int expected)
		{
			actual.Is((ulong) expected);
		}

		protected override ulong ValueSetter(int i)
		{
			return (ulong) i;
		}

	}

	public class Int64ArrayTest:AlignedArrayTest<long>
	{
		public Int64ArrayTest(ITestOutputHelper output) : base(output)
		{
		}

		protected override void AreEqual(long actual, int expected) => actual.Is(expected);

		protected override void AreEqual(object? actual, int expected) => actual.Is((long)expected);

		protected override long ValueSetter(int i) => i;

	}

	public class UInt32ArrayTest:AlignedArrayTest<uint>
	{
		public UInt32ArrayTest(ITestOutputHelper output) : base(output)
		{
		}

		protected override void AreEqual(uint actual, int expected) => actual.Is((uint) expected);

		protected override void AreEqual(object? actual, int expected) => actual.Is((uint) expected);

		protected override uint ValueSetter(int i) => (uint)i;
	}

	public class Int32ArrayTest : AlignedArrayTest<int>
	{
		public Int32ArrayTest(ITestOutputHelper output) : base(output)
		{
		}

		protected override void AreEqual(int actual, int expected) => actual.Is(expected);

		protected override void AreEqual(object? actual, int expected) => actual.Is(expected);

		protected override int ValueSetter(int i) => i;
	}

	public class DoubleArrayTest : AlignedArrayTest<double>
	{
		public DoubleArrayTest(ITestOutputHelper output) : base(output)
		{
		}

		protected override void AreEqual(double actual, int expected) => actual.Is(expected);

		protected override void AreEqual(object? actual, int expected) => actual.Is((double) expected);

		protected override double ValueSetter(int i) => i;
	}

}