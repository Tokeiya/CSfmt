using System;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using static CSfmt.Defination;

namespace CSfmt
{
	public static unsafe class SfmtNative
	{
		private static readonly uint[] parity =
		{
			SFMT_PARITY1, SFMT_PARITY2,
			SFMT_PARITY3, SFMT_PARITY4
		};

		private static readonly w128_t sse2_param_mask;


		static SfmtNative()
		{
			sse2_param_mask = new w128_t();

			sse2_param_mask.u[0] = SFMT_MSK1;
			sse2_param_mask.u[1] = SFMT_MSK2;
			sse2_param_mask.u[2] = SFMT_MSK3;
			sse2_param_mask.u[3] = SFMT_MSK4;
		}

		private static uint Func1(uint x)=> (x ^ (x >> 27)) * (uint) 1664525UL;

		private static uint Func2(uint x)=>(x ^ (x >> 27)) * (uint) 1566083941UL;


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static Vector128<int> mm_recursion(Vector128<int> a, Vector128<int> b, Vector128<int> c,
			Vector128<int> d)
		{
			Vector128<int> v, x, y, z;
			y = Sse2.ShiftRightLogical(b, SFMT_SR1);
			z = Sse2.ShiftRightLogical128BitLane(c, SFMT_SR2);
			v = Sse2.ShiftLeftLogical(d, SFMT_SL1);
			z = Sse2.Xor(z, a);
			z = Sse2.Xor(z, v);
			x = Sse2.ShiftLeftLogical128BitLane(a, SFMT_SL2);
			y = Sse2.And(y, sse2_param_mask.si);
			z = Sse2.Xor(z, x);
			return Sse2.Xor(z, y);
		}

		private static void gen_rand_array(sfmt_t sfmt, w128_t* array, int size)
		{
			int i, j;
			Vector128<int> r1, r2;
			var pstate = sfmt.state;

			r1 = pstate[SFMT_N - 2].si;
			r2 = pstate[SFMT_N - 1].si;
			for (i = 0; i < SFMT_N - SFMT_POS1; i++)
			{
				array[i].si = mm_recursion(pstate[i].si,
					pstate[i + SFMT_POS1].si, r1, r2);
				r1 = r2;
				r2 = array[i].si;
			}

			for (; i < SFMT_N; i++)
			{
				array[i].si = mm_recursion(pstate[i].si,
					array[i + SFMT_POS1 - SFMT_N].si, r1, r2);
				r1 = r2;
				r2 = array[i].si;
			}

			for (; i < size - SFMT_N; i++)
			{
				array[i].si = mm_recursion(array[i - SFMT_N].si,
					array[i + SFMT_POS1 - SFMT_N].si, r1, r2);
				r1 = r2;
				r2 = array[i].si;
			}

			for (j = 0; j < 2 * SFMT_N - size; j++) pstate[j] = array[j + size - SFMT_N];
			for (; i < size; i++, j++)
			{
				array[i].si = mm_recursion(array[i - SFMT_N].si,
					array[i + SFMT_POS1 - SFMT_N].si, r1, r2);
				r1 = r2;
				r2 = array[i].si;
				pstate[j] = array[i];
			}
		}

		public static void sfmt_fill_array64(sfmt_t sfmt, ulong* array, int size)
		{
			if (sfmt.idx != SFMT_N32) throw new ArgumentException($"{nameof(sfmt)} internal state error.");
			if (size % 2 != 0)
				throw new ArgumentOutOfRangeException($"{nameof(size)} requires to be a multiple of four.");

			if (size <= SFMT_N64)
				throw new ArgumentOutOfRangeException($"{nameof(size)} requires to be at least {SFMT_N64}");


			gen_rand_array(sfmt, (w128_t*) array, size / 2);
			sfmt.idx = SFMT_N32;
		}


		public static void sfmt_fill_array32(sfmt_t sfmt, uint* array, int size)
		{
			if (sfmt.idx != SFMT_N32) throw new ArgumentException($"{nameof(sfmt)} internal state error.");
			if (size % 4 != 0)
				throw new ArgumentOutOfRangeException($"{nameof(size)} requires to be a multiple of four.");

			if (size <= SFMT_N32)
				throw new ArgumentOutOfRangeException($"{nameof(size)} requires to be at least {SFMT_N32}");

			gen_rand_array(sfmt, (w128_t*) array, size / 4);
		}

		private static void period_certification(sfmt_t sfmt)
		{
			uint inner = 0;
			int i, j;
			uint work;
			var psfmt32 = &sfmt.state[0].u[0];

			for (i = 0; i < 4; i++) inner ^= psfmt32[i] & parity[i];
			for (i = 16; i > 0; i >>= 1) inner ^= inner >> i;
			inner &= 1;
			/* check OK */
			if (inner == 1) return;
			/* check NG, and modification */
			for (i = 0; i < 4; i++)
			{
				work = 1;
				for (j = 0; j < 32; j++)
				{
					if ((work & parity[i]) != 0)
					{
						psfmt32[i] ^= work;
						return;
					}

					work = work << 1;
				}
			}
		}


		public static void sfmt_init_gen_rand(sfmt_t sfmt, uint seed)
		{
			unchecked
			{
				int i;

				var psfmt32 = &sfmt.state[0].u[0];

				psfmt32[0] = seed;
				for (i = 1; i < SFMT_N32; i++)
					psfmt32[i] = (uint) (1812433253U * (psfmt32[i - 1] ^ (psfmt32[i - 1] >> 30)) + i);

				sfmt.idx = SFMT_N32;
				period_certification(sfmt);
			}
		}


		public static void sfmt_init_by_array(sfmt_t sfmt, uint* init_key, int key_length)
		{
			unchecked
			{
				static int idxof(int i)
				{
					return i;
				}

				static void memset(void* s, byte value, int size)
				{
					var ptr = (byte*) s;

					for (var i = 0; i < size; i++) ptr[i] = value;
				}


				int i, j, count;
				uint r;
				int lag;
				int mid;
				var size = SFMT_N * 4;
				var psfmt32 = &sfmt.state[0].u[0];

				if (size >= 623)
					lag = 11;
				else if (size >= 68)
					lag = 7;
				else if (size >= 39)
					lag = 5;
				else
					lag = 3;

				mid = (size - lag) / 2;

				memset(sfmt.state, 0x8b, sizeof(ulong) * SFMT_N64);


				if (key_length + 1 > SFMT_N32)
					count = key_length + 1;
				else
					count = SFMT_N32;

				r = Func1(psfmt32[idxof(0)] ^ psfmt32[idxof(mid)]
				                            ^ psfmt32[idxof(SFMT_N32 - 1)]);
				psfmt32[idxof(mid)] += r;
				r += (uint) key_length;
				psfmt32[idxof(mid + lag)] += r;
				psfmt32[idxof(0)] = r;

				count--;
				for (i = 1, j = 0; j < count && j < key_length; j++)
				{
					r = Func1(psfmt32[idxof(i)] ^ psfmt32[idxof((i + mid) % SFMT_N32)]
					                            ^ psfmt32[idxof((i + SFMT_N32 - 1) % SFMT_N32)]);
					psfmt32[idxof((i + mid) % SFMT_N32)] += r;
					r += (uint) (init_key[j] + i);
					psfmt32[idxof((i + mid + lag) % SFMT_N32)] += r;
					psfmt32[idxof(i)] = r;
					i = (i + 1) % SFMT_N32;
				}

				for (; j < count; j++)
				{
					r = Func1(psfmt32[idxof(i)] ^ psfmt32[idxof((i + mid) % SFMT_N32)]
					                            ^ psfmt32[idxof((i + SFMT_N32 - 1) % SFMT_N32)]);
					psfmt32[idxof((i + mid) % SFMT_N32)] += r;
					r += (uint) i;
					psfmt32[idxof((i + mid + lag) % SFMT_N32)] += r;
					psfmt32[idxof(i)] = r;
					i = (i + 1) % SFMT_N32;
				}

				for (j = 0; j < SFMT_N32; j++)
				{
					r = Func2(psfmt32[idxof(i)] + psfmt32[idxof((i + mid) % SFMT_N32)]
					                            + psfmt32[idxof((i + SFMT_N32 - 1) % SFMT_N32)]);
					psfmt32[idxof((i + mid) % SFMT_N32)] ^= r;
					r -= (uint) i;
					psfmt32[idxof((i + mid + lag) % SFMT_N32)] ^= r;
					psfmt32[idxof(i)] = r;
					i = (i + 1) % SFMT_N32;
				}

				sfmt.idx = SFMT_N32;
				period_certification(sfmt);
			}
		}
	}
}