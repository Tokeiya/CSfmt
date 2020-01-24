using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;

namespace CSfmt.Float
{
	[StructLayout(LayoutKind.Explicit)]
	public unsafe struct FloatW128
	{
		[FieldOffset(0)] public Vector128<int> si;
		[FieldOffset(0)] public Vector128<double> sd;
		[FieldOffset(0)] public fixed ulong u[2];
		[FieldOffset(0)] public fixed uint u32[4];
		[FieldOffset(0)] public fixed double d[2];
	}

	[StructLayout(LayoutKind.Explicit)]
	public unsafe struct X128I_T
	{
		[FieldOffset(0)] public fixed ulong u[2];
		[FieldOffset(0)] public Vector128<int> i128;
	}

	[StructLayout(LayoutKind.Explicit)]
	public unsafe struct X128D_T
	{

		[FieldOffset(0)] public fixed double d[2];
		[FieldOffset(0)] private Vector128<double> d128;
	}

}
