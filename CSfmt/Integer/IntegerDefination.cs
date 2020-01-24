using System.Runtime.Intrinsics;

namespace CSfmt.Integer
{
	public static class IntegerDefinition
	{
		public const int Mexp = 19937;

		public const int Pos1 = 122;
		public const int Sl1 = 18;
		public const int Sl2 = 1;
		public const int Sr1 = 11;
		public const int Sr2 = 1;
		public const uint Msk1 = 0xdfffffefU;
		public const uint Msk2 = 0xddfecb7fU;
		public const uint Msk3 = 0xbffaffffU;
		public const uint Msk4 = 0xbffffff6U;
		public const uint Parity1 = 0x00000001U;
		public const uint Parity2 = 0x00000000U;
		public const uint Parity3 = 0x00000000U;
		public const uint Parity4 = 0x13c9e684U;

		public const string Idstr = "SFMT-19937:122-18-1-11-1:dfffffef-ddfecb7f-bffaffff-bffffff6";

		public const int N = Mexp / 128 + 1;
		public const int N32 = N * 4;
		public const int N64 = N * 2;

		public static readonly int[] AltiSl1 = {Sl1, Sl1, Sl1, Sl1};
		public static readonly int[] AltiSr1 = {Sr1, Sr1, Sr1, Sr1};
		public static readonly uint[] AltiMsk = {Msk1, Msk2, Msk3, Msk4};
		public static readonly uint[] AltiMsk64 = {Msk2, Msk1, Msk4, Msk3};
		public static readonly int[] AltiSl2Perm = {1, 2, 3, 23, 5, 6, 7, 0, 9, 10, 11, 4, 13, 14, 15, 8};
		public static readonly int[] AltiSl2Perm64 = {1, 2, 3, 4, 5, 6, 7, 31, 9, 10, 11, 12, 13, 14, 15, 0};
		// ReSharper disable once UnusedMember.Local
#pragma warning disable IDE0052 // 読み取られていないプライベート メンバーを削除
		private static readonly int[] AltiSr2Perm = {7, 0, 1, 2, 11, 4, 5, 6, 15, 8, 9, 10, 17, 12, 13, 14};
#pragma warning restore IDE0052 // 読み取られていないプライベート メンバーを削除
		public static readonly int[] AltiSr2Perm64 = {15, 0, 1, 2, 3, 4, 5, 6, 17, 8, 9, 10, 11, 12, 13, 14};

		public static readonly Vector128<int> Sse2ParamMask128I;

		static IntegerDefinition()
		{
			unchecked
			{
				Sse2ParamMask128I =
					Vector128.Create((int) Msk1, (int) Msk2, (int) Msk3, (int) Msk4);
			}
		}
	}
}