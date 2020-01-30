using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using System.Text;
using static CSfmt.Float.FloatDefination;

namespace CSfmt.Float
{
	public static unsafe class dSfmtPrimitive
	{
		public static string dsfmt_get_idstring()
		{
			return DSFMT_IDSTR;
		}

		public static int dsfmt_get_min_array_size()
		{
			return DSFMT_N64;
		}


		static void do_recursion(ref FloatW128 r, ref FloatW128 a, ref FloatW128 b, ref FloatW128 u)
		{

			Vector128<int> v, w, x, y, z;


			x = a.si;
			//z = _mm_slli_epi64(x, DSFMT_SL1);
			z = Sse2.ShiftLeftLogical(x.AsInt64(),  DSFMT_SL1).AsInt32();

			//y = _mm_shuffle_epi32(u->si, SSE2_SHUFF);
			y = Sse2.Shuffle(u.si, SSE2_SHUFF);

			//z = _mm_xor_si128(z, b->si);
			z = Sse2.Xor(z, b.si);

			//y = _mm_xor_si128(y, z);
			y = Sse2.Xor(y, z);

			//v = _mm_srli_epi64(y, DSFMT_SR);
			v = Sse2.ShiftRightLogical(y.AsUInt64(), DSFMT_SR).AsInt32();

			//w = _mm_and_si128(y, sse2_param_mask.i128);
			w = Sse2.And(y, sse2_param_mask.i128);

			//v = _mm_xor_si128(v, x);
			v = Sse2.Xor(v, x);

			//v = _mm_xor_si128(v, w);
			v = Sse2.Xor(v, w);

			//r->si = v;
			r.si = v;

			//u->si = y;
			u.si = y;
		}


		public static void dsfmt_gen_rand_all(dSfmtPrimitiveState dsfmt)
		{
			int i;
			FloatW128 lung=new FloatW128();

			lung = dsfmt.status[DSFMT_N];


			do_recursion(ref dsfmt.status[0], ref dsfmt.status[0],
				ref dsfmt.status[DSFMT_POS1], ref lung);


			for (i = 1; i < DSFMT_N - DSFMT_POS1; i++)
			{
				do_recursion(ref dsfmt.status[i], ref dsfmt.status[i],
					ref dsfmt.status[i + DSFMT_POS1], ref lung);
			}

			for (; i < DSFMT_N; i++)
			{
				do_recursion(ref dsfmt.status[i], ref dsfmt.status[i],
					ref dsfmt.status[i + DSFMT_POS1 - DSFMT_N], ref lung);
			}


			dsfmt.status[DSFMT_N] = lung;
		}

		public static void initial_mask(dSfmtPrimitiveState dsfmt)
		{
			int i;
			ulong* psfmt;

			psfmt = &dsfmt.status[0].u[0];
			for (i = 0; i < DSFMT_N * 2; i++)
			{
				psfmt[i] = (psfmt[i] & DSFMT_LOW_MASK) | DSFMT_HIGH_CONST;
			}
		}



		static void period_certification(dSfmtPrimitiveState dsfmt)
		{
			ulong* pcv = stackalloc ulong[2];
			pcv[0] = DSFMT_PCV1;
			pcv[1] = DSFMT_PCV2;

			;
			ulong* tmp = stackalloc ulong[2];

			ulong inner;
			int i;

			tmp[0] = (dsfmt.status[DSFMT_N].u[0] ^ DSFMT_FIX1);
			tmp[1] = (dsfmt.status[DSFMT_N].u[1] ^ DSFMT_FIX2);

			inner = tmp[0] & pcv[0];
			inner ^= tmp[1] & pcv[1];
			for (i = 32; i > 0; i >>= 1)
			{
				inner ^= inner >> i;
			}

			inner &= 1;
			/* check OK */
			if (inner == 1)
			{
				return;
			}

			dsfmt.status[DSFMT_N].u[1] ^= 1;
		}

		public static int idxof(int i)
		{
			return i;
		}

		
		private static void dsfmt_chk_init_gen_rand(dSfmtPrimitiveState dsfmt, uint seed, int mexp)
		{
			int i;
			uint* psfmt;

			/* make sure caller program is compiled with the same MEXP */
			psfmt = &dsfmt.status[0].u32[0];

			psfmt[idxof(0)] = seed;
			for (i = 1; i < (DSFMT_N + 1) * 4; i++)
			{
				psfmt[idxof(i)] = 1812433253U * (psfmt[idxof(i - 1)] ^ (psfmt[idxof(i - 1)] >> 30)) + (uint) i;
			}
			initial_mask(dsfmt);



			period_certification(dsfmt);
			dsfmt.idx = DSFMT_N64;
		}


		public static void dsfmt_init_gen_rand(dSfmtPrimitiveState dsfmt, uint seed) =>
			dsfmt_chk_init_gen_rand(dsfmt, seed, 19937);


		public static void dsfmt_chk_init_by_array(dSfmtPrimitiveState dsfmt, uint[] init_key, int key_length)
		{
			unchecked
			{


				static int idxof(int i)
				{
					return i;
				}

				static void memset(void* s, byte value, int size)
				{
					var ptr = (byte*)s;

					for (var i = 0; i < size; i++) ptr[i] = value;
				}

				static uint ini_func1(uint x)
				{
					return (x ^ (x >> 27)) * 1664525U;
				}

				static uint ini_func2(uint x)
				{
					return (x ^ (x >> 27)) * 1566083941U;
				}




				int i, j, count;
				uint r;
				uint* psfmt32;
				int lag;
				int mid;
				int size = (DSFMT_N + 1) * 4;   /* pulmonary */

				/* make sure caller program is compiled with the same MEXP */
				//if (mexp != dsfmt_mexp)
				//{
				//	fprintf(stderr, "DSFMT_MEXP doesn't match with dSFMT.c\n");
				//	exit(1);
				//}


				if (size >= 623)
				{
					lag = 11;
				}
				else if (size >= 68)
				{
					lag = 7;
				}
				else if (size >= 39)
				{
					lag = 5;
				}
				else
				{
					lag = 3;
				}
				mid = (size - lag) / 2;

				psfmt32 = &dsfmt.status[0].u32[0];
				memset(dsfmt.status, 0x8b, sizeof(FloatW128) * (DSFMT_N + 1));
				if (key_length + 1 > size)
				{
					count = key_length + 1;
				}
				else
				{
					count = size;
				}
				r = ini_func1(psfmt32[idxof(0)] ^ psfmt32[idxof(mid % size)]
				                                ^ psfmt32[idxof((size - 1) % size)]);
				psfmt32[idxof(mid % size)] += r;
				r +=(uint) key_length;
				psfmt32[idxof((mid + lag) % size)] += r;
				psfmt32[idxof(0)] = r;
				count--;
				for (i = 1, j = 0; (j < count) && (j < key_length); j++)
				{
					r = ini_func1(psfmt32[idxof(i)]
					              ^ psfmt32[idxof((i + mid) % size)]
					              ^ psfmt32[idxof((i + size - 1) % size)]);
					psfmt32[idxof((i + mid) % size)] += r;
					r += (uint)(init_key[j] + i);
					psfmt32[idxof((i + mid + lag) % size)] += r;
					psfmt32[idxof(i)] = r;
					i = (i + 1) % size;
				}
				for (; j < count; j++)
				{
					r = ini_func1(psfmt32[idxof(i)]
					              ^ psfmt32[idxof((i + mid) % size)]
					              ^ psfmt32[idxof((i + size - 1) % size)]);
					psfmt32[idxof((i + mid) % size)] += r;
					r +=(uint) i;
					psfmt32[idxof((i + mid + lag) % size)] += r;
					psfmt32[idxof(i)] = r;
					i = (i + 1) % size;
				}
				for (j = 0; j < size; j++)
				{
					r = ini_func2(psfmt32[idxof(i)]
					              + psfmt32[idxof((i + mid) % size)]
					              + psfmt32[idxof((i + size - 1) % size)]);
					psfmt32[idxof((i + mid) % size)] ^= r;
					r -=(uint) i;
					psfmt32[idxof((i + mid + lag) % size)] ^= r;
					psfmt32[idxof(i)] = r;
					i = (i + 1) % size;
				}
				initial_mask(dsfmt);
				period_certification(dsfmt);
				dsfmt.idx = DSFMT_N64;
			}
		}

		private static void gen_rand_array_c1o2(dSfmtPrimitiveState dsfmt, FloatW128* array,
			int size)
		{
			int i, j;
			FloatW128 lung;

			lung = dsfmt.status[DSFMT_N];
			do_recursion(ref array[0], ref dsfmt.status[0], ref dsfmt.status[DSFMT_POS1],
				ref lung);
			for (i = 1; i < DSFMT_N - DSFMT_POS1; i++)
			{
				do_recursion(ref array[i], ref dsfmt.status[i],
					ref dsfmt.status[i + DSFMT_POS1], ref lung);
			}
			for (; i < DSFMT_N; i++)
			{
				do_recursion(ref array[i], ref dsfmt.status[i],
					ref array[i + DSFMT_POS1 - DSFMT_N], ref lung);
			}
			for (; i < size - DSFMT_N; i++)
			{
				do_recursion(ref array[i], ref array[i - DSFMT_N],
					ref array[i + DSFMT_POS1 - DSFMT_N], ref lung);
			}
			for (j = 0; j < 2 * DSFMT_N - size; j++)
			{
				dsfmt.status[j] = array[j + size - DSFMT_N];
			}
			for (; i < size; i++, j++)
			{
				do_recursion(ref array[i], ref array[i - DSFMT_N],
					ref array[i + DSFMT_POS1 - DSFMT_N], ref lung);
				dsfmt.status[j] = array[i];
			}
			dsfmt.status[DSFMT_N] = lung;
		}
		
		private static void gen_rand_array_c0o1(dSfmtPrimitiveState dsfmt, FloatW128* array,
			int size)
		{

			static void convert_c0o1(ref FloatW128 w)
			{
				w.sd = Sse2.Add(w.sd, FloatDefination.sse2_double_m_one.d128);
			}

			int i, j;
			FloatW128 lung;

			lung = dsfmt.status[DSFMT_N];
			do_recursion(ref array[0], ref dsfmt.status[0], ref dsfmt.status[DSFMT_POS1],
				ref lung);

			for (i = 1; i < DSFMT_N - DSFMT_POS1; i++)
			{
				do_recursion(ref array[i], ref dsfmt.status[i],
					ref dsfmt.status[i + DSFMT_POS1], ref lung);
			}
			for (; i < DSFMT_N; i++)
			{
				do_recursion(ref array[i], ref dsfmt.status[i],
					ref array[i + DSFMT_POS1 - DSFMT_N], ref lung);
			}
			for (; i < size - DSFMT_N; i++)
			{
				do_recursion(ref array[i], ref array[i - DSFMT_N],
					ref array[i + DSFMT_POS1 - DSFMT_N], ref lung);
				convert_c0o1(ref array[i - DSFMT_N]);
			}
			for (j = 0; j < 2 * DSFMT_N - size; j++)
			{
				dsfmt.status[j] = array[j + size - DSFMT_N];
			}
			for (; i < size; i++, j++)
			{
				do_recursion(ref array[i], ref array[i - DSFMT_N],
					ref array[i + DSFMT_POS1 - DSFMT_N], ref lung);
				dsfmt.status[j] = array[i];
				convert_c0o1(ref array[i - DSFMT_N]);
			}
			for (i = size - DSFMT_N; i < size; i++)
			{
				convert_c0o1(ref array[i]);
			}
			dsfmt.status[DSFMT_N] = lung;
		}



		private static void gen_rand_array_o0o1(dSfmtPrimitiveState dsfmt,  FloatW128* array, int size)
		{
			static void convert_o0o1(ref FloatW128 w)
			{
				w.si = Sse2.Or(w.si, sse2_int_one.i128);
				w.sd = Sse2.Add(w.sd, sse2_double_m_one.d128);
			}

			int i, j;
			FloatW128 lung;

			lung = dsfmt.status[DSFMT_N];
			do_recursion(ref array[0], ref dsfmt.status[0], ref dsfmt.status[DSFMT_POS1],
				ref lung);


			for (i = 1; i < DSFMT_N - DSFMT_POS1; i++)
			{
				do_recursion(ref array[i], ref dsfmt.status[i],
					ref dsfmt.status[i + DSFMT_POS1], ref lung);
			}

			for (; i < DSFMT_N; i++)
			{
				do_recursion(ref array[i], ref dsfmt.status[i],
					ref array[i + DSFMT_POS1 - DSFMT_N], ref lung);
			}

			for (; i < size - DSFMT_N; i++)
			{
				do_recursion(ref array[i], ref array[i - DSFMT_N],
					ref array[i + DSFMT_POS1 - DSFMT_N], ref lung);
				convert_o0o1(ref array[i - DSFMT_N]);
			}

			for (j = 0; j < 2 * DSFMT_N - size; j++)
			{
				dsfmt.status[j] = array[j + size - DSFMT_N];
			}

			for (; i < size; i++, j++)
			{
				do_recursion(ref array[i], ref array[i - DSFMT_N],
					ref array[i + DSFMT_POS1 - DSFMT_N], ref lung);
				dsfmt.status[j] = array[i];
				convert_o0o1(ref array[i - DSFMT_N]);
			}

			for (i = size - DSFMT_N; i < size; i++)
			{
				convert_o0o1(ref array[i]);
			}

			dsfmt.status[DSFMT_N] = lung;
		}


		static void gen_rand_array_o0c1(dSfmtPrimitiveState dsfmt, FloatW128* array,
			int size)
		{

			static void convert_o0c1(ref FloatW128 w)
			{
				w.sd = Sse2.Subtract(sse2_double_two.d128, w.sd);
			}

			int i, j;
			FloatW128 lung;

			lung = dsfmt.status[DSFMT_N];
			do_recursion(ref array[0], ref dsfmt.status[0], ref dsfmt.status[DSFMT_POS1],
				ref lung);
			for (i = 1; i < DSFMT_N - DSFMT_POS1; i++)
			{
				do_recursion(ref array[i], ref dsfmt.status[i],
					ref dsfmt.status[i + DSFMT_POS1], ref lung);
			}
			for (; i < DSFMT_N; i++)
			{
				do_recursion(ref array[i], ref dsfmt.status[i],
					ref array[i + DSFMT_POS1 - DSFMT_N], ref lung);
			}
			for (; i < size - DSFMT_N; i++)
			{
				do_recursion(ref array[i], ref array[i - DSFMT_N],
					ref array[i + DSFMT_POS1 - DSFMT_N], ref lung);
				convert_o0c1(ref array[i - DSFMT_N]);
			}
			for (j = 0; j < 2 * DSFMT_N - size; j++)
			{
				dsfmt.status[j] = array[j + size - DSFMT_N];
			}
			for (; i < size; i++, j++)
			{
				do_recursion(ref array[i], ref array[i - DSFMT_N],
					ref array[i + DSFMT_POS1 - DSFMT_N], ref lung);
				dsfmt.status[j] = array[i];
				convert_o0c1(ref array[i - DSFMT_N]);
			}
			for (i = size - DSFMT_N; i < size; i++)
			{
				convert_o0c1(ref array[i]);
			}
			dsfmt.status[DSFMT_N] = lung;
		}

		public static void dsfmt_fill_array_close1_open2(dSfmtPrimitiveState dsfmt, AlignedArray<double> array)
		{
			Trace.Assert(array.Count % 2 == 0);
			Trace.Assert(array.Count >= DSFMT_N64);

			gen_rand_array_c1o2(dsfmt, (FloatW128*)array.StatusUncheckedPointer, array.Count / 2);
		}

		public static void dsfmt_fill_array_open_close(dSfmtPrimitiveState dsfmt, AlignedArray<double> array)
		{
			Trace.Assert(array.Count % 2 == 0);
			Trace.Assert(array.Count >= DSFMT_N64);


			gen_rand_array_o0c1(dsfmt, (FloatW128*) array.StatusUncheckedPointer, array.Count / 2);
		}

		public static void dsfmt_fill_array_close_open(dSfmtPrimitiveState dsfmt, AlignedArray<double> array)
		{
			Trace.Assert(array.Count % 2 == 0);
			Trace.Assert(array.Count >= DSFMT_N64);
			gen_rand_array_c0o1(dsfmt, (FloatW128*) array.StatusUncheckedPointer, array.Count / 2);
		}

		public static void dsfmt_fill_array_open_open(dSfmtPrimitiveState dsfmt, AlignedArray<double> array)
		{
			Trace.Assert(array.Count % 2 == 0);
			Trace.Assert(array.Count >= DSFMT_N64);
			gen_rand_array_o0o1(dsfmt, (FloatW128*)array.StatusUncheckedPointer, array.Count / 2);
		}

		public static double dsfmt_genrand_close1_open2(dSfmtPrimitiveState dsfmt)
		{
			double r;
			double* psfmt64 = &dsfmt.status[0].d[0];


			if (dsfmt.idx >= DSFMT_N64)
			{
				dsfmt_gen_rand_all(dsfmt);
				dsfmt.idx = 0;
			}

			r = psfmt64[dsfmt.idx++];
			return r;
		}

		public static double dsfmt_genrand_close_open(dSfmtPrimitiveState dsfmt)
			=> dsfmt_genrand_close1_open2(dsfmt) - 1.0;

		public static double dsfmt_genrand_open_close(dSfmtPrimitiveState dsfmt)
			=> 2.0 - dsfmt_genrand_close1_open2(dsfmt);

		[StructLayout(LayoutKind.Explicit)]
		private struct  RImpl
		{
			[FieldOffset(0)]public double d;
			[FieldOffset(0)] public ulong u;
		}


		public static double dsfmt_genrand_open_open(dSfmtPrimitiveState dsfmt)
		{
			double* dsfmt64 = &dsfmt.status[0].d[0];

			RImpl r = new RImpl();

			if (dsfmt.idx >= DSFMT_N64)
			{
				dsfmt_gen_rand_all(dsfmt);
				dsfmt.idx = 0;
			}
			r.d = dsfmt64[dsfmt.idx++];
			r.u |= 1;
			return r.d - 1.0;

		}

		public static uint dsfmt_genrand_uint32(dSfmtPrimitiveState dsfmt)
		{
			uint r;
			ulong* psfmt64 = &dsfmt.status[0].u[0];

			if (dsfmt.idx >= DSFMT_N64)
			{
				dsfmt_gen_rand_all(dsfmt);
				dsfmt.idx = 0;
			}

			r = (uint) (psfmt64[dsfmt.idx++] & 0xffffffffU);
			return r;
		}



	}


}
