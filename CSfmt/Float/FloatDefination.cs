using System;
using System.Collections.Generic;
using System.Text;

namespace CSfmt.Float
{
	public static class FloatDefination
	{
		public const int DSFMT_MEXP = 19937;
		public const int DSFMT_N = (DSFMT_MEXP - 128) / 104 + 1;
		public const int DSFMT_N32 = DSFMT_N * 4;
		public const int DSFMT_N64 = DSFMT_N * 2;

		public const int DSFMT_POS1 = 117;
		public const int DSFMT_SL1 = 19;
		public const ulong DSFMT_MSK1 = 0x000ffafffffffb3fUL;
		public const ulong DSFMT_MSK2 = 0x000ffdfffc90fffdUL;
		public const uint DSFMT_MSK32_1 = 0x000ffaffU;
		public const uint DSFMT_MSK32_2 = 0xfffffb3fU;
		public const uint DSFMT_MSK32_3 = 0x000ffdffU;
		public const uint DSFMT_MSK32_4 = 0xfc90fffdU;
		public const ulong DSFMT_FIX1 = 0x90014964b32f4329UL;
		public const ulong DSFMT_FIX2 = 0x3b8d12ac548a7c7aUL;
		public const ulong DSFMT_PCV1 = 0x3d84e1ac0dc82880UL;
		public const ulong DSFMT_PCV2 = 0x0000000000000001UL;
		public const string DSFMT_IDSTR = "dSFMT2-19937:117-19:ffafffffffb3f-ffdfffc90fffd";

		public static readonly int[] ALTI_SL1 = {3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3};
		public static readonly int[] ALTI_SL1_PERM = {2, 3, 4, 5, 6, 7, 30, 30, 10, 11, 12, 13, 14, 15, 0, 1};
		public static readonly uint[] ALTI_SL1_MSK = {0xffffffffU, 0xfff80000U, 0xffffffffU, 0xfff80000U};

		public static readonly uint[] ALTI_MSK = {DSFMT_MSK32_1, DSFMT_MSK32_2, DSFMT_MSK32_3, DSFMT_MSK32_4};

		public const int SSE2_SHUFF = 0x1b;
		public const int DSFMT_SR = 12;


		public const ulong DSFMT_LOW_MASK = 0x000FFFFFFFFFFFFFUL;
		public const ulong DSFMT_HIGH_CONST = 0x3FF0000000000000UL;


		public static readonly X128I_T sse2_param_mask;

		public const int dsfmt_mexp = DSFMT_MEXP;

		static unsafe FloatDefination()
		{
			
			sse2_param_mask = new X128I_T();
			sse2_param_mask.u[0] = DSFMT_MSK1;
			sse2_param_mask.u[1] = DSFMT_MSK2;
		}
	}
}
