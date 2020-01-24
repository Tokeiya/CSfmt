using System;
using System.Runtime.Intrinsics.X86;
using System.Text;
using static CSfmt.Integer.IntegerDefinition;

namespace CSfmt.Integer
{
	public static unsafe class SfmtJump
	{
		private static void RightShift128(IntegerW128* output, IntegerW128* input, int shift)
		{
			var th = ((ulong) input->u[3] << 32) | input->u[2];
			var tl = ((ulong) input->u[1] << 32) | input->u[0];

			var oh = th >> (shift * 8);
			var ol = tl >> (shift * 8);
			ol |= th << (64 - shift * 8);
			output->u[1] = (uint) (ol >> 32);
			output->u[0] = (uint) ol;
			output->u[3] = (uint) (oh >> 32);
			output->u[2] = (uint) oh;
		}

		private static void LeftShift128(IntegerW128* output, IntegerW128* input, int shift)
		{
			var th = ((ulong) input->u[3] << 32) | input->u[2];
			var tl = ((ulong) input->u[1] << 32) | input->u[0];

			var oh = th << (shift * 8);
			var ol = tl << (shift * 8);
			oh |= tl >> (64 - shift * 8);
			output->u[1] = (uint) (ol >> 32);
			output->u[0] = (uint) ol;
			output->u[3] = (uint) (oh >> 32);
			output->u[2] = (uint) oh;
		}

		private static void DoRecursion(IntegerW128* r, IntegerW128* a, IntegerW128* b,
			IntegerW128* c, IntegerW128* d)
		{
			IntegerW128 x;
			IntegerW128 y;

			LeftShift128(&x, a, Sl2);
			RightShift128(&y, c, Sr2);
			r->u[0] = a->u[0] ^ x.u[0] ^ ((b->u[0] >> Sr1) & Msk1)
			          ^ y.u[0] ^ (d->u[0] << Sl1);
			r->u[1] = a->u[1] ^ x.u[1] ^ ((b->u[1] >> Sr1) & Msk2)
			          ^ y.u[1] ^ (d->u[1] << Sl1);
			r->u[2] = a->u[2] ^ x.u[2] ^ ((b->u[2] >> Sr1) & Msk3)
			          ^ y.u[2] ^ (d->u[2] << Sl1);
			r->u[3] = a->u[3] ^ x.u[3] ^ ((b->u[3] >> Sr1) & Msk4)
			          ^ y.u[3] ^ (d->u[3] << Sl1);
		}

		private static void Add(SfmtPrimitiveState dest, SfmtPrimitiveState src)
		{
			var dp = dest.Index / 4;
			var sp = src.Index / 4;
			var diff = (sp - dp + N) % N;
			int p;
			int i;
			for (i = 0; i < N - diff; i++)
			{
				p = i + diff;
				dest.State[i].si
					= Sse2.Xor(dest.State[i].si, src.State[p].si);
			}

			for (; i < N; i++)
			{
				p = i + diff - N;
				dest.State[i].si
					= Sse2.Xor(dest.State[i].si, src.State[p].si);
			}
		}

		private static void NextState(SfmtPrimitiveState sfmt)
		{
			var idx = sfmt.Index / 4 % N;
			var pstate = sfmt.State;

			var r1 = &pstate[(idx + N - 2) % N];
			var r2 = &pstate[(idx + N - 1) % N];
			DoRecursion(&pstate[idx],
				&pstate[idx],
				&pstate[(idx + Pos1) % N],
				r1,
				r2);
			r1 = r2;
			r2 = &pstate[idx];
			sfmt.Index += 4;
		}

		public static void Jump(SfmtPrimitiveState sfmt, string jumpString)
		{
			const byte a = 0x61;
			const byte f = 0x66;
			const byte c0 = 0x30;

			var data = Encoding.ASCII.GetBytes(jumpString);

			static void memset(void* s, byte value, int size)
			{
				var ptr = (byte*) s;

				for (var i = 0; i < size; i++) ptr[i] = value;
			}

			static void check(int target)
			{
				if (target >= 0x30 && target <= 0x39)
					return;
				if (target >= 0x41 && target <= 0x46)
					return;
				if (target >= 0x61 && target <= 0x66) return;

				throw new ArgumentException(nameof(jumpString));
			}

			static int toLower(int b)
			{
				if (b >= 0x41 && b <= 0x5a) return b + 0x20;

				return b;
			}


			using var work = new SfmtPrimitiveState();


			var index = sfmt.Index;
			memset(work.State, 0, sizeof(ulong) * N64);
			sfmt.Index = N32;

			foreach (var elem in data)
			{
				int bits = elem;
				check(bits);
				bits = toLower(bits);
				if (bits >= a && bits <= f)
					bits = bits - a + 10;
				else
					bits -= c0;
				bits &= 0x0f;
				for (var j = 0; j < 4; j++)
				{
					if ((bits & 1) != 0) Add(work, sfmt);
					NextState(sfmt);
					bits >>= 1;
				}
			}

			sfmt.Copy(work);

			sfmt.Index = index;
		}
	}
}