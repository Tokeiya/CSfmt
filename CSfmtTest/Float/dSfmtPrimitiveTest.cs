using ChainingAssertion;
using CSfmt;
using CSfmt.Float;
using System;
using Xunit;
using Xunit.Abstractions;
using static CSfmt.Float.FloatDefination;

namespace CSfmtTest.Float
{
	public unsafe class dSfmtPrimitiveTest
	{
		private readonly ITestOutputHelper _output;
		public dSfmtPrimitiveTest(ITestOutputHelper output) => _output = output;

		static void AreEqual<T>(T[] expected, ReadOnlySpan<T> actual)
		{
			actual.Length.Is(expected.Length);


			for (var i = 0; i < expected.Length; i++)
			{
				actual[i].Is(expected[i]);
			}

		}

		[Fact]
		public void InitGenRandTest()
		{
			using var dSfmt = new dSfmtPrimitiveState();
			dSfmtPrimitive.dsfmt_init_gen_rand(dSfmt, 0);

			var actual = new ReadOnlySpan<uint>(dSfmt.status, FloatDefination.DSFMT_N32);

			AreEqual(Init0Status.Expected, actual);

		}

		[Fact]
		public void InitByArrayTest()
		{
			using var dsfmt = new dSfmtPrimitiveState();
			dSfmtPrimitive.dsfmt_chk_init_by_array(dsfmt, new uint[] { 1, 2, 3, 4 }, 4);

			var actual = new ReadOnlySpan<uint>(dsfmt.status[0].u32, DSFMT_N32);

			actual.Length.Is(InitByArrayStatus.Expected.Count);

			for (int i = 0; i < actual.Length; i++)
			{
				actual[i].Is(InitByArrayStatus.Expected[i]);
			}

		}

		[Fact]
		public void GenRandAllTest()
		{
			using var dsfmt = new dSfmtPrimitiveState();
			dSfmtPrimitive.dsfmt_chk_init_by_array(dsfmt, new uint[] { 1, 2, 3, 4 }, 4);


			var actual = new ReadOnlySpan<uint>(dsfmt.status[0].u32, DSFMT_N32);

			actual.Length.Is(InitByArrayStatus.Expected.Count);

			for (int i = 0; i < actual.Length; i++)
			{
				actual[i].Is(InitByArrayStatus.Expected[i]);
			}



			dSfmtPrimitive.dsfmt_gen_rand_all(dsfmt);

			actual = new ReadOnlySpan<uint>(dsfmt.status[0].u32, DSFMT_N32);

			actual.Length.Is(GenRandAll.Expected.Count);

			for (int i = 0; i < actual.Length; i++)
			{
				actual[i].Is(GenRandAll.Expected[i]);
			}

		}

		[Fact]
		public void FillArrayClo1Ope2Test()
		{
			var dsfmt = new dSfmtPrimitiveState();
			dSfmtPrimitive.dsfmt_chk_init_by_array(dsfmt, new uint[] { 1, 2, 3, 4 }, 4);
			var actual = new AlignedArray<double>(1024, 16);

			dSfmtPrimitive.dsfmt_fill_array_close1_open2(dsfmt, actual, 1024);


			actual.Count.Is(FillArrayClo1Ope2.Expected.Count);

			for (var i = 0; i < actual.Count; i++)
			{
				actual[i].Is(FillArrayClo1Ope2.Expected[i]);
			}


			var ptr = dsfmt.status->u32;
			DSFMT_N32.Is(FillArrayClo1Ope2.ExpectedAfterState.Count);

			var status = new ReadOnlySpan<uint>(ptr, DSFMT_N32);

			for (int i = 0; i < status.Length; i++)
			{
				status[i].Is(FillArrayClo1Ope2.ExpectedAfterState[i]);
			}
		}

	}
}
