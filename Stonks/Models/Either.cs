namespace Stonks.Models
{
    public sealed class Either<TLeft, TRight>
    {
        private readonly TLeft _left;
        private readonly TRight _right;
        private readonly bool _isLeft;

        public Either(TLeft left)
        {
            _left = left;
            _isLeft = true;
        }

        public Either(TRight right)
        {
            _right = right;
            _isLeft = false;
        }

        public T Match<T>(Func<TLeft, T> left, Func<TRight, T> right)
        {
            return _isLeft ? left(_left) : right(_right);
        }

        public static Either<TLeft, TRight> Create(TLeft left)
        {
            return new Either<TLeft, TRight>(left);
        }

        public static Either<TLeft, TRight> Create(TRight right)
        {
            return new Either<TLeft, TRight>(right);
        }
    }
}