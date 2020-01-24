using System;

namespace SeedCreate
{
	public static class Option
	{
		public static Option<T> Some<T>(T value)
		{
			return new Option<T>(value);
		}

		public static Option<T> None<T>()
		{
			return new Option<T>();
		}

		public static Option<T> ToOption<T>(this T value, Func<T, bool> predicate)
		{
			if (predicate == null) throw new ArgumentNullException(nameof(predicate));
			return predicate(value) ? new Option<T>(value) : new Option<T>();
		}

		public static Option<T> ToOption<T>(this T value) where T : class
		{
			return ToOption(value, x => x != null);
		}

		public static Option<T> ToOption<T>(this T? value) where T : struct
		{
			return value.HasValue ? new Option<T>(value.Value) : new Option<T>();
		}
	}
}