using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using static CSfmt.Integer.IntegerDefinition;

namespace CSfmt.Integer
{
	public static unsafe class SfmtPrimitive
	{
		public const string IdString = Idstr;

		public const int MinArraySize32 = N32;

		public const int MinArraySize64 = N64;

		private static readonly uint[] Parity =
		{
			Parity1, Parity2,
			Parity3, Parity4
		};

		private static readonly IntegerW128 Sse2ParamMask;


		static SfmtPrimitive()
		{
			// ReSharper disable once UseObjectOrCollectionInitializer
			Sse2ParamMask = new IntegerW128();

			Sse2ParamMask.u[0] = Msk1;
			Sse2ParamMask.u[1] = Msk2;
			Sse2ParamMask.u[2] = Msk3;
			Sse2ParamMask.u[3] = Msk4;
		}

		private static uint Func1(uint x)
		{
			return (x ^ (x >> 27)) * (uint) 1664525UL;
		}

		private static uint Func2(uint x)
		{
			return (x ^ (x >> 27)) * (uint) 1566083941UL;
		}


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static Vector128<int> Recursion(Vector128<int> a, Vector128<int> b, Vector128<int> c,
			Vector128<int> d)
		{
			var y = Sse2.ShiftRightLogical(b, Sr1);
			var z = Sse2.ShiftRightLogical128BitLane(c, Sr2);
			var v = Sse2.ShiftLeftLogical(d, Sl1);
			z = Sse2.Xor(z, a);
			z = Sse2.Xor(z, v);
			var x = Sse2.ShiftLeftLogical128BitLane(a, Sl2);
			y = Sse2.And(y, Sse2ParamMask.si);
			z = Sse2.Xor(z, x);
			return Sse2.Xor(z, y);
		}

		private static void GenRandArray(SfmtPrimitiveState sfmt, IntegerW128* array, int size)
		{
			int i, j;
			Vector128<int> r1, r2;
			var pstate = sfmt.State;

			r1 = pstate[N - 2].si;
			r2 = pstate[N - 1].si;
			for (i = 0; i < N - Pos1; i++)
			{
				array[i].si = Recursion(pstate[i].si,
					pstate[i + Pos1].si, r1, r2);
				r1 = r2;
				r2 = array[i].si;
			}

			for (; i < N; i++)
			{
				array[i].si = Recursion(pstate[i].si,
					array[i + Pos1 - N].si, r1, r2);
				r1 = r2;
				r2 = array[i].si;
			}

			for (; i < size - N; i++)
			{
				array[i].si = Recursion(array[i - N].si,
					array[i + Pos1 - N].si, r1, r2);
				r1 = r2;
				r2 = array[i].si;
			}

			for (j = 0; j < 2 * N - size; j++) pstate[j] = array[j + size - N];
			for (; i < size; i++, j++)
			{
				array[i].si = Recursion(array[i - N].si,
					array[i + Pos1 - N].si, r1, r2);
				r1 = r2;
				r2 = array[i].si;
				pstate[j] = array[i];
			}
		}

		public static void FillArray64(SfmtPrimitiveState sfmt, AlignedArray<ulong> array, int size)
		{
			if (sfmt.Index != N32) throw new ArgumentException($"{nameof(sfmt)} internal state error.");
			if (size % 2 != 0)
				throw new ArgumentOutOfRangeException($"{nameof(size)} requires to be a multiple of four.");

			if (size <= N64)
				throw new ArgumentOutOfRangeException($"{nameof(size)} requires to be at least {N64}");


			GenRandArray(sfmt, (IntegerW128*) array.StatusUncheckedPointer, size / 2);
			sfmt.Index = N32;
		}


		public static void FillArray32(SfmtPrimitiveState sfmt, AlignedArray<uint> array, int size)
		{
			if (sfmt.Index != N32) throw new ArgumentException($"{nameof(sfmt)} internal state error.");
			if (size % 4 != 0)
				throw new ArgumentOutOfRangeException($"{nameof(size)} requires to be a multiple of four.");

			if (size <= N32)
				throw new ArgumentOutOfRangeException($"{nameof(size)} requires to be at least {N32}");

			GenRandArray(sfmt, (IntegerW128*) array.StatusUncheckedPointer, size / 4);
		}

		private static void PeriodCertification(SfmtPrimitiveState sfmt)
		{
			uint inner = 0;
			int i, j;
			var psfmt32 = &sfmt.State[0].u[0];

			for (i = 0; i < 4; i++) inner ^= psfmt32[i] & Parity[i];
			for (i = 16; i > 0; i >>= 1) inner ^= inner >> i;
			inner &= 1;
			/* check OK */
			if (inner == 1) return;
			/* check NG, and modification */
			for (i = 0; i < 4; i++)
			{
				uint work = 1;
				for (j = 0; j < 32; j++)
				{
					if ((work & Parity[i]) != 0)
					{
						psfmt32[i] ^= work;
						return;
					}

					work <<= 1;
				}
			}
		}


		public static void InitGenRand(SfmtPrimitiveState sfmt, uint seed)
		{
			unchecked
			{
				int i;

				var psfmt32 = &sfmt.State[0].u[0];

				psfmt32[0] = seed;
				for (i = 1; i < N32; i++)
					psfmt32[i] = (uint) (1812433253U * (psfmt32[i - 1] ^ (psfmt32[i - 1] >> 30)) + i);

				sfmt.Index = N32;
				PeriodCertification(sfmt);
			}
		}


		public static uint GenRandUint32(SfmtPrimitiveState sfmt)
		{
			var psfmt32 = &sfmt.State[0].u[0];

			if (sfmt.Index >= N32)
			{
				GenRandAll(sfmt);
				sfmt.Index = 0;
			}

			var r = psfmt32[sfmt.Index++];
			return r;
		}

		public static ulong GenRandUint64(SfmtPrimitiveState sfmt)
		{
			var psfmt64 = &sfmt.State[0].u64[0];
			Trace.Assert(sfmt.Index % 2 == 0);

			if (sfmt.Index >= N32)
			{
				GenRandAll(sfmt);
				sfmt.Index = 0;
			}

			var r = psfmt64[sfmt.Index / 2];
			sfmt.Index += 2;
			return r;
		}

		public static void GenRandAll(SfmtPrimitiveState sfmt)
		{
			int i;
			var pstate = sfmt.State;

			var r1 = pstate[N - 2].si;
			var r2 = pstate[N - 1].si;
			for (i = 0; i < N - Pos1; i++)
			{
				pstate[i].si = Recursion(pstate[i].si,
					pstate[i + Pos1].si, r1, r2);
				r1 = r2;
				r2 = pstate[i].si;
			}

			for (; i < N; i++)
			{
				pstate[i].si = Recursion(pstate[i].si,
					pstate[i + Pos1 - N].si,
					r1, r2);
				r1 = r2;
				r2 = pstate[i].si;
			}
		}

		public static void InitByArray(SfmtPrimitiveState sfmt, ReadOnlySpan<uint> initKey)
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
				const int size = N * 4;
				var psfmt32 = &sfmt.State[0].u[0];

				const int lag = 11;

				const int mid = (size - lag) / 2;

				memset(sfmt.State, 0x8b, sizeof(ulong) * N64);


				if (initKey.Length + 1 > N32)
					count = initKey.Length + 1;
				else
					count = N32;

				var r = Func1(psfmt32[idxof(0)] ^ psfmt32[idxof(mid)]
				                                ^ psfmt32[idxof(N32 - 1)]);
				psfmt32[idxof(mid)] += r;
				r += (uint) initKey.Length;
				psfmt32[idxof(mid + lag)] += r;
				psfmt32[idxof(0)] = r;

				count--;
				for (i = 1, j = 0; j < count && j < initKey.Length; j++)
				{
					r = Func1(psfmt32[idxof(i)] ^ psfmt32[idxof((i + mid) % N32)]
					                            ^ psfmt32[idxof((i + N32 - 1) % N32)]);
					psfmt32[idxof((i + mid) % N32)] += r;
					r += (uint) (initKey[j] + i);
					psfmt32[idxof((i + mid + lag) % N32)] += r;
					psfmt32[idxof(i)] = r;
					i = (i + 1) % N32;
				}

				for (; j < count; j++)
				{
					r = Func1(psfmt32[idxof(i)] ^ psfmt32[idxof((i + mid) % N32)]
					                            ^ psfmt32[idxof((i + N32 - 1) % N32)]);
					psfmt32[idxof((i + mid) % N32)] += r;
					r += (uint) i;
					psfmt32[idxof((i + mid + lag) % N32)] += r;
					psfmt32[idxof(i)] = r;
					i = (i + 1) % N32;
				}

				for (j = 0; j < N32; j++)
				{
					r = Func2(psfmt32[idxof(i)] + psfmt32[idxof((i + mid) % N32)]
					                            + psfmt32[idxof((i + N32 - 1) % N32)]);
					psfmt32[idxof((i + mid) % N32)] ^= r;
					r -= (uint) i;
					psfmt32[idxof((i + mid + lag) % N32)] ^= r;
					psfmt32[idxof(i)] = r;
					i = (i + 1) % N32;
				}

				sfmt.Index = N32;
				PeriodCertification(sfmt);
			}
		}
	}
}