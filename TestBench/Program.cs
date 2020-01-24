using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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



			using var dsfmt = new dSfmtPrimitiveState();
			dSfmtPrimitive.dsfmt_chk_init_by_array(dsfmt, new uint[] {1, 2, 3, 4}, 4);

			dSfmtPrimitive.dsfmt_gen_rand_all(dsfmt);


			Console.WriteLine(dsfmt.status[0].u32[0]);
			Console.WriteLine(dsfmt.status[0].u32[1]);

		}


		private static void Foo(Hoge obj)
		{

		}

		private static double ProbOneCanon(double p, int n) => Pow(1 - p, (Pow(n, 2) + n) / 2);
	}
}