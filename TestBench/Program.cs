using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using CSfmt;
using CSfmt.Float;
using static System.Math;

namespace TestBench
{

	public class Hoge
	{
		public int Value { get; set; }
	}

	public class Piyo : Hoge
	{

	}



	internal unsafe class Program
	{
		private static void Main()
		{
			var dsfmt = new dSfmtPrimitiveState();
			using var ary = new AlignedArray<double>(1024, 16);

			dSfmtPrimitive.dsfmt_chk_init_by_array(dsfmt, new uint[] {1, 2, 3, 4}, 4);

			dSfmtPrimitive.dsfmt_fill_array_close1_open2(dsfmt, ary, 1024);


			Console.WriteLine(ary[0].ToString("G17"));

			dsfmt.Dispose();
		}



		private static double ProbOneCanon(double p, int n) => Pow(1 - p, (Pow(n, 2) + n) / 2);
	}
}