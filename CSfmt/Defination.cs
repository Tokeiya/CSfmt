using System.Runtime.Intrinsics;

namespace CSfmt
{
	public static class Defination
	{
		public const int SFMT_MEXP = 19937;

		public const int SFMT_POS1 = 122;
		public const int SFMT_SL1 = 18;
		public const int SFMT_SL2 = 1;
		public const int SFMT_SR1 = 11;
		public const int SFMT_SR2 = 1;
		public const uint SFMT_MSK1 = 0xdfffffefU;
		public const uint SFMT_MSK2 = 0xddfecb7fU;
		public const uint SFMT_MSK3 = 0xbffaffffU;
		public const uint SFMT_MSK4 = 0xbffffff6U;
		public const uint SFMT_PARITY1 = 0x00000001U;
		public const uint SFMT_PARITY2 = 0x00000000U;
		public const uint SFMT_PARITY3 = 0x00000000U;
		public const uint SFMT_PARITY4 = 0x13c9e684U;

		public const string SFMT_IDSTR = "SFMT-19937:122-18-1-11-1:dfffffef-ddfecb7f-bffaffff-bffffff6";

		public const int SFMT_N = SFMT_MEXP / 128 + 1;
		public const int SFMT_N32 = SFMT_N * 4;
		public const int SFMT_N64 = SFMT_N * 2;

		public static readonly int[] SFMT_ALTI_SL1 = {SFMT_SL1, SFMT_SL1, SFMT_SL1, SFMT_SL1};
		public static readonly int[] SFMT_ALTI_SR1 = {SFMT_SR1, SFMT_SR1, SFMT_SR1, SFMT_SR1};
		public static readonly uint[] SFMT_ALTI_MSK = {SFMT_MSK1, SFMT_MSK2, SFMT_MSK3, SFMT_MSK4};
		public static readonly uint[] SFMT_ALTI_MSK64 = {SFMT_MSK2, SFMT_MSK1, SFMT_MSK4, SFMT_MSK3};
		public static readonly int[] SFMT_ALTI_SL2_PERM = {1, 2, 3, 23, 5, 6, 7, 0, 9, 10, 11, 4, 13, 14, 15, 8};
		public static readonly int[] SFMT_ALTI_SL2_PERM64 = {1, 2, 3, 4, 5, 6, 7, 31, 9, 10, 11, 12, 13, 14, 15, 0};
		private static readonly int[] SFMT_ALTI_SR2_PERM = {7, 0, 1, 2, 11, 4, 5, 6, 15, 8, 9, 10, 17, 12, 13, 14};
		public static readonly int[] SFMT_ALTI_SR2_PERM64 = {15, 0, 1, 2, 3, 4, 5, 6, 17, 8, 9, 10, 11, 12, 13, 14};

		public static readonly Vector128<int> sse2_param_mask_128i;

		static Defination()
		{
			unchecked
			{
				sse2_param_mask_128i =
					Vector128.Create((int) SFMT_MSK1, (int) SFMT_MSK2, (int) SFMT_MSK3, (int) SFMT_MSK4);
			}
		}
	}
}