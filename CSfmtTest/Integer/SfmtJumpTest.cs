using System;
using System.IO;
using System.Linq;
using ChainingAssertion;
using CSfmt.Integer;
using Xunit;
using static CSfmt.Integer.IntegerDefinition;

namespace CSfmtTest.Integer
{
	public unsafe class SfmtJumpTest
	{
		[Fact]
		public void JumpTest()
		{
			var sfmt = new SfmtPrimitiveState();

			SfmtPrimitive.InitGenRand(sfmt, 1234);
			SfmtJump.Jump(sfmt, "2");

			var expected = File.ReadLines("./Data/jump2.txt").Select(x => uint.Parse(x)).ToArray();
			expected.Length.Is(N32);

			var actual = new ReadOnlySpan<uint>(sfmt.State, N32).ToArray();

			for (var i = 0; i < N32; i++) actual[i].Is(expected[i]);
		}
	}
}