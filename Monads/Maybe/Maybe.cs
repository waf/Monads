using System;

namespace Monads
{
    class Maybe<T>
    {
        public readonly static Maybe<T> Nothing = new Maybe<T>();
        public T Value { get; private set; }
        public bool HasValue { get; private set; }

        Maybe()
        {
            HasValue = false;
        }

        public Maybe(T value)
        {
            Value = value;
            HasValue = true;
        }

        public override string ToString()
        {
            return HasValue ? Value.ToString() : "Nothing";
        }

        public static implicit operator Maybe<T>(T t)
        {
            return new Maybe<T>(t);
        }
    }

    static class MaybeMonad
    {
        // Bind
        public static Maybe<U> SelectMany<T, U>(this Maybe<T> m, Func<T, Maybe<U>> k)
        {
            if (!m.HasValue)
                return Maybe<U>.Nothing;
            return k(m.Value);
        }

        /// <summary>
        /// The following code:
        ///   from m0 in m
        ///   from n0 in n
        ///   select m0 + n0
        /// would be translated in:
        ///   m.SelectMany(m0 => n, (m, n0) => m0 + n0);
        /// we want to make it instead invoke SelectMany twice
        /// </summary>
        public static Maybe<V> SelectMany<T, U, V>(this Maybe<T> m, Func<T, Maybe<U>> k, Func<T, U, V> s)
        {
            return m.SelectMany(t => k(t)
                .SelectMany(u => new Maybe<V>(s(t, u))));
        }
    }
}
