using System;

namespace SeedCreate
{
	public struct Option<T>
	{
		private readonly T _value;

		public Option(T value)
			: this()
		{
			_value = value;
			HasValue = true;
		}

		public bool HasValue { get; }

		public T Value
		{
			get
			{
				if (HasValue) return _value;
				throw new InvalidOperationException("Get values by nil object.");
			}
		}

		public bool TryGetValue(out T value)
		{
			if (HasValue)
			{
				value = _value;
				return true;
			}
			value = default;
			return false;
		}


		public Option<TResult> Select<TResult>(Func<T, TResult> selector)
		{
			if (selector == null) throw new ArgumentNullException(nameof(selector));
			return HasValue ? new Option<TResult>(selector(_value)) : new Option<TResult>();
		}

		public Option<TResult> SelectMany<TResult>(Func<T, Option<TResult>> selector)
		{
			if (selector == null) throw new ArgumentNullException(nameof(selector));
			return HasValue ? selector(_value) : new Option<TResult>();
		}

		public Option<TProjected> SelectMany<TSelected, TProjected>(Func<T, Option<TSelected>> selector,
			Func<T, TSelected, TProjected> projector)
		{
			if (selector == null) throw new ArgumentNullException(nameof(selector));
			if (projector == null) throw new ArgumentNullException(nameof(projector));

			var selected = HasValue ? selector(_value) : new Option<TSelected>();

			return selected.HasValue ? new Option<TProjected>(projector(_value, selected.Value)) : new Option<TProjected>();
		}

		public T TryGetValueOrDefault()
		{
			return _value;
		}
	}
}