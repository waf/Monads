using System;

namespace Monads
{
    class Either<L, R>
    {
        public L Left { get; private set; }
        public bool HasLeft { get; set; }
        public R Right { get; private set; }
        public bool HasRight { get; set; }

        public Either()
        {
            this.HasRight = false;
            this.HasLeft = false;
        }

        public Either<L, R> WithLeft(L left)
        {
            this.Left = left;
            this.HasLeft = true;
            return this;
        }

        public Either<L, R> WithRight(R right)
        {
            this.Right = right;
            this.HasRight = true;
            return this;
        }

        public static Either<L, R> FromLeft(L left)
        {
            return new Either<L, R>().WithLeft(left);
        }

        public static Either<L, R> FromRight(R right)
        {
            return new Either<L, R>().WithRight(right);
        }

        public override string ToString()
        {
            return this.HasRight ? this.Right.ToString() : this.Left.ToString();
        }
    }

    static class EitherMonad
    {
        // Bind
        public static Either<L,U> SelectMany<L, R, U>(this Either<L, R> m, Func<R, Either<L,U>> k)
        {
            if (m.HasLeft)
                return new Either<L, U>().WithLeft(m.Left);
            return k(m.Right);
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
        public static Either<L,V> SelectMany<L, R, U, V>(this Either<L, R> m, Func<R, Either<L,U>> k, Func<R, U, V> s)
        {
            return m.SelectMany(r => k(r).SelectMany(u => new Either<L, V>().WithRight(s(r, u))));
        }
    }
}
