using ChainingAssertion;
using CSfmt;
using CSfmt.Float;
using System;
using System.Collections.Generic;
using System.Reflection;
using CSfmtTest.FloatData;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;
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

			var actual32 = new ReadOnlySpan<uint>(dSfmt.status, FloatDefination.DSFMT_N32);

			AreEqual(Init0Status.Expected, actual32);

			var actual64 = new ReadOnlySpan<ulong>(dSfmt.status, DSFMT_N64);
			AreEqual(Init0Status.ExpectedUlong, actual64);
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
		public void GenRandAllTestInit0()
		{
			using var dsfmt = new dSfmtPrimitiveState();
			dSfmtPrimitive.dsfmt_init_gen_rand(dsfmt, 0);

			dSfmtPrimitive.dsfmt_gen_rand_all(dsfmt);

			AreEqual(GenRandAll.FirstState,
				new ReadOnlySpan<ulong>(dsfmt.status->u, DSFMT_N64));



		}


		[Fact]
		public void FillArrayClo1Ope2Test()
		{
			var dsfmt = new dSfmtPrimitiveState();
			dSfmtPrimitive.dsfmt_chk_init_by_array(dsfmt, new uint[] { 1, 2, 3, 4 }, 4);
			var actual = new AlignedArray<double>(1024, 16);

			dSfmtPrimitive.dsfmt_fill_array_close1_open2(dsfmt, actual);


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


		[Fact]
		public void dsfmt_fill_array_open_closeTest()
		{
			var dsfmt = new dSfmtPrimitiveState();
			dSfmtPrimitive.dsfmt_chk_init_by_array(dsfmt, new uint[] { 1, 2, 3, 4 }, 4);
			var actual = new AlignedArray<double>(1024, 16);

			dSfmtPrimitive.dsfmt_fill_array_open_close(dsfmt, actual);


			actual.Count.Is(OpenClose.Expected.Count);

			for (var i = 0; i < actual.Count; i++)
			{
				actual[i].Is(OpenClose.Expected[i]);
			}


			var ptr = dsfmt.status->u32;
			DSFMT_N32.Is(OpenClose.ExpectedAfterStatus.Count);

			var status = new ReadOnlySpan<uint>(ptr, DSFMT_N32);

			for (int i = 0; i < status.Length; i++)
			{
				status[i].Is(OpenClose.ExpectedAfterStatus[i]);
			}
		}

		[Fact]
		public void dsfmt_fill_array_close_openTest()
		{
			var dsfmt = new dSfmtPrimitiveState();
			dSfmtPrimitive.dsfmt_chk_init_by_array(dsfmt, new uint[] { 1, 2, 3, 4 }, 4);
			var actual = new AlignedArray<double>(1024, 16);

			dSfmtPrimitive.dsfmt_fill_array_close_open(dsfmt, actual);


			actual.Count.Is(OpenClose.Expected.Count);

			for (var i = 0; i < actual.Count; i++)
			{
				actual[i].Is(CloseOpen.Expected[i]);
			}


			var ptr = dsfmt.status->u32;
			DSFMT_N32.Is(CloseOpen.ExpectedAfterStatus.Count);

			var status = new ReadOnlySpan<uint>(ptr, DSFMT_N32);

			for (int i = 0; i < status.Length; i++)
			{
				status[i].Is(CloseOpen.ExpectedAfterStatus[i]);
			}

		}

		[Fact]
		public void dsfmt_fill_array_open_openTest()
		{
			var dsfmt = new dSfmtPrimitiveState();
			dSfmtPrimitive.dsfmt_chk_init_by_array(dsfmt, new uint[] { 1, 2, 3, 4 }, 4);
			var actual = new AlignedArray<double>(1024, 16);

			dSfmtPrimitive.dsfmt_fill_array_open_open(dsfmt, actual);


			actual.Count.Is(OpenClose.Expected.Count);

			for (var i = 0; i < actual.Count; i++)
			{
				actual[i].Is(OpenOpen.Expected[i]);
			}


			var ptr = dsfmt.status->u32;
			DSFMT_N32.Is(OpenOpen.ExpectedAfterStatus.Count);

			var status = new ReadOnlySpan<uint>(ptr, DSFMT_N32);

			for (int i = 0; i < status.Length; i++)
			{
				status[i].Is(OpenOpen.ExpectedAfterStatus[i]);
			}

		}

		[Fact]
		public void dsfmt_get_idStringTest()
		{
			dSfmtPrimitive.dsfmt_get_idstring()
				.Is("dSFMT2-19937:117-19:ffafffffffb3f-ffdfffc90fffd");
		}

		[Fact]
		public void dsfmt_get_min_array_sizeTest()
		{
			dSfmtPrimitive.dsfmt_get_min_array_size().Is(382);
		}


		private static void AssertExpectedAfterState(dSfmtPrimitiveState actual, IReadOnlyList<ulong> expected)
		{
			expected.Count.Is(DSFMT_N64);
			ulong* ptr = &actual.status->u[0];

			for (int i = 0; i < DSFMT_N64; i++)
			{
				ptr[i].Is(expected[i]);
			}

		}

		private static void AssertOutput(IReadOnlyList<double> actual, IReadOnlyList<double> expected)
		{
			actual.Count.Is(expected.Count);


			for (int i = 0; i < expected.Count; i++)
			{
				actual[i].Is(expected[i], i.ToString());
			}

		}

		[Fact]
		public void DsfmtGenrandClose1Open2Test()
		{
			using var dsfmt = new dSfmtPrimitiveState();
			dSfmtPrimitive.dsfmt_init_gen_rand(dsfmt, 0);

			var actual=new double[1024];

			for (int i = 0; i < actual.Length; i++)
			{
				actual[i] = dSfmtPrimitive.dsfmt_genrand_close1_open2(dsfmt);
			}

			AssertOutput(actual, GenRandClose1Open2.Expected);
			AssertExpectedAfterState(dsfmt, GenRandClose1Open2.ExpectedAfterState);
		}

		[Fact]
		public void GenrandCloseOpenTest()
		{
			using var dsfmt = new dSfmtPrimitiveState();
			dSfmtPrimitive.dsfmt_init_gen_rand(dsfmt, 0);

			var actual = new double[1024];

			for (int i = 0; i < actual.Length; i++)
			{
				actual[i] = dSfmtPrimitive.dsfmt_genrand_close_open(dsfmt);
			}

			AssertOutput(actual, GenRandCloseOpen.Expected);
			AssertExpectedAfterState(dsfmt, GenRandCloseOpen.ExpectedAfterState);
		}

		[Fact]
		public void GenrandOpenCloseTest()
		{
			using var dsfmt = new dSfmtPrimitiveState();
			dSfmtPrimitive.dsfmt_init_gen_rand(dsfmt, 0);

			var actual = new double[1024];

			for (int i = 0; i < actual.Length; i++)
			{
				actual[i] = dSfmtPrimitive.dsfmt_genrand_open_close(dsfmt);
			}

			AssertOutput(actual, GenRandOpenClose.Expected);
			AssertExpectedAfterState(dsfmt, GenRandOpenClose.ExpectedAfterState);

		}

		[Fact]
		public void GenrandOpenOpenTest()
		{
			using var dsfmt = new dSfmtPrimitiveState();
			dSfmtPrimitive.dsfmt_init_gen_rand(dsfmt, 0);

			var actual = new double[1024];

			for (int i = 0; i < actual.Length; i++)
			{
				actual[i] = dSfmtPrimitive.dsfmt_genrand_open_open(dsfmt);
			}

			AssertOutput(actual, GenrandOpenOpen.Expected);
			AssertExpectedAfterState(dsfmt, GenrandOpenOpen.ExpectedAfterState);

		}


		[Fact]
		public void dsfmt_genrand_uint32Test()
		{
			using var dsfmt = new dSfmtPrimitiveState();
			dSfmtPrimitive.dsfmt_init_gen_rand(dsfmt, 114514);

			var actual = new uint[1024];
			for (var i = 0; i < actual.Length; i++)
			{
				actual[i] = dSfmtPrimitive.dsfmt_genrand_uint32(dsfmt);
			}

			actual.Length.Is(GenUint32.Expected.Count);

			for (int i = 0; i < actual.Length; i++)
			{
				actual[i].Is(GenUint32.Expected[i]);
			}

		}



	}
}
