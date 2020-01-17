using System;
using System.IO;
using System.Linq;
using ChainingAssertion;
using CSfmt;
using Xunit;
using Xunit.Abstractions;
using static CSfmt.Defination;

namespace CSfmtTest
{
	public unsafe class SfmtJumpTest
	{
		private readonly ITestOutputHelper _output;

		public SfmtJumpTest(ITestOutputHelper output) => _output = output;

		[Fact]
		public void JumpTest()
		{
			byte* str = stackalloc byte[1];
			str[0] = 0x32;

			var sfmt = new sfmt_t();

			SfmtNative.sfmt_init_gen_rand(sfmt, 1234);
			SfmtJump.SFMT_jump(sfmt, str);

			var expected = File.ReadLines("./Data/jump2.txt").Select(x => uint.Parse(x)).ToArray();
			expected.Length.Is(SFMT_N32);

			var actual = new ReadOnlySpan<uint>(sfmt.state, SFMT_N32).ToArray();

			for (var i=0; i < SFMT_N32; i++)
			{
				actual[i].Is(expected[i]);
			}




		}

	}
}
