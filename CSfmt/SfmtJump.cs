using System;
using System.Runtime.Intrinsics.X86;
using static CSfmt.Defination;

namespace CSfmt
{
	public static unsafe class SfmtJump
	{
		private static void rshift128(w128_t* output, w128_t* input, int shift)
		{
			ulong th, tl, oh, ol;

			th = ((ulong) input->u[3] << 32) | input->u[2];
			tl = ((ulong) input->u[1] << 32) | input->u[0];

			oh = th >> (shift * 8);
			ol = tl >> (shift * 8);
			ol |= th << (64 - shift * 8);
			output->u[1] = (uint) (ol >> 32);
			output->u[0] = (uint) ol;
			output->u[3] = (uint) (oh >> 32);
			output->u[2] = (uint) oh;
		}

		private static void lshift128(w128_t* output, w128_t* input, int shift)
		{
			ulong th, tl, oh, ol;

			th = ((ulong) input->u[3] << 32) | input->u[2];
			tl = ((ulong) input->u[1] << 32) | input->u[0];

			oh = th << (shift * 8);
			ol = tl << (shift * 8);
			oh |= tl >> (64 - shift * 8);
			output->u[1] = (uint) (ol >> 32);
			output->u[0] = (uint) ol;
			output->u[3] = (uint) (oh >> 32);
			output->u[2] = (uint) oh;
		}

		private static void do_recursion(w128_t* r, w128_t* a, w128_t* b,
			w128_t* c, w128_t* d)
		{
			w128_t x;
			w128_t y;

			lshift128(&x, a, SFMT_SL2);
			rshift128(&y, c, SFMT_SR2);
			r->u[0] = a->u[0] ^ x.u[0] ^ ((b->u[0] >> SFMT_SR1) & SFMT_MSK1)
			          ^ y.u[0] ^ (d->u[0] << SFMT_SL1);
			r->u[1] = a->u[1] ^ x.u[1] ^ ((b->u[1] >> SFMT_SR1) & SFMT_MSK2)
			          ^ y.u[1] ^ (d->u[1] << SFMT_SL1);
			r->u[2] = a->u[2] ^ x.u[2] ^ ((b->u[2] >> SFMT_SR1) & SFMT_MSK3)
			          ^ y.u[2] ^ (d->u[2] << SFMT_SL1);
			r->u[3] = a->u[3] ^ x.u[3] ^ ((b->u[3] >> SFMT_SR1) & SFMT_MSK4)
			          ^ y.u[3] ^ (d->u[3] << SFMT_SL1);
		}

		private static void add(sfmt_t dest, sfmt_t src)
		{
			var dp = dest.idx / 4;
			var sp = src.idx / 4;
			var diff = (sp - dp + SFMT_N) % SFMT_N;
			int p;
			int i;
			for (i = 0; i < SFMT_N - diff; i++)
			{
				p = i + diff;
				dest.state[i].si
					= Sse2.Xor(dest.state[i].si, src.state[p].si);
			}

			for (; i < SFMT_N; i++)
			{
				p = i + diff - SFMT_N;
				dest.state[i].si
					= Sse2.Xor(dest.state[i].si, src.state[p].si);
			}
		}

		private static void next_state(sfmt_t sfmt)
		{
			var idx = sfmt.idx / 4 % SFMT_N;
			w128_t* r1;
			w128_t* r2;
			var pstate = sfmt.state;

			r1 = &pstate[(idx + SFMT_N - 2) % SFMT_N];
			r2 = &pstate[(idx + SFMT_N - 1) % SFMT_N];
			do_recursion(&pstate[idx],
				&pstate[idx],
				&pstate[(idx + SFMT_POS1) % SFMT_N],
				r1,
				r2);
			r1 = r2;
			r2 = &pstate[idx];
			sfmt.idx = sfmt.idx + 4;
		}

		public static void SFMT_jump(sfmt_t sfmt, byte* jump_string)
		{
			const byte a = 0x61;
			const byte f = 0x66;
			const byte c0 = 0x30;
			const byte cnull = 0x00;

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

				throw new ArgumentException(nameof(jump_string));
			}

			static int tolower(int b)
			{
				if (b >= 0x41 && b <= 0x5a) return b + 0x20;

				return b;
			}


			using var work = new sfmt_t();


			var index = sfmt.idx;
			int bits;
			memset(work.state, 0, sizeof(ulong) * SFMT_N64);
			sfmt.idx = SFMT_N32;

			for (var i = 0; jump_string[i] != cnull; i++)
			{
				bits = jump_string[i];
				check(bits);
				bits = tolower(bits);
				if (bits >= a && bits <= f)
					bits = bits - a + 10;
				else
					bits = bits - c0;
				bits = bits & 0x0f;
				for (var j = 0; j < 4; j++)
				{
					if ((bits & 1) != 0) add(work, sfmt);
					next_state(sfmt);
					bits = bits >> 1;
				}
			}

			//* sfmt = work;
			sfmt.Copy(work);

			sfmt.idx = index;
		}
	}
}