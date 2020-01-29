using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using CSfmt;
using CSfmt.Float;
using CSfmt.Integer;
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

			Console.WriteLine(sizeof(ulong) * (FloatDefination.DSFMT_N + 1) * 2);
		}



		private static double ProbOneCanon(double p, int n) => Pow(1 - p, (Pow(n, 2) + n) / 2);
	}
}