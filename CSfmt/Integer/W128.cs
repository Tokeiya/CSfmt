using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;

namespace CSfmt.Integer
{
	[StructLayout(LayoutKind.Explicit)]
	public unsafe struct IntegerW128
	{
		[FieldOffset(0)] public fixed uint u[4];
		[FieldOffset(0)] public fixed ulong u64[2];
		[FieldOffset(0)] public Vector128<int> si;
	}
}