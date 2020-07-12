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



	internal unsafe class Program
	{
		private static void Main()
		{
			using var array = new AlignedArray<uint>(1024, 16);

			using var sfmt = new SfmtPrimitiveState();
			SfmtPrimitive.InitGenRand(sfmt,1234);

			SfmtPrimitive.FillArray32(sfmt, array, 1024);

			Console.WriteLine(array[0]);







		}

	}
}