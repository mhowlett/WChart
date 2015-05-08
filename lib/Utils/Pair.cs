namespace WChart
{
    public struct Pair<T, U>
    {
        public Pair(T first, U second)
        {
            _first = first;
            _second = second;
        }

        public T First
        {
            get
            {
                return _first;
            }
            set
            {
                _first = value;
            }
        }
        private T _first;

        public U Second
        {
            get
            {
                return _second;
            }
            set
            {
                _second = value;
            }
        }
        private U _second;
    }
}
