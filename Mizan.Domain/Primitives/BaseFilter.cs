namespace Mizan.Domain.Primitives
{
    public abstract record BaseFilter
    {
        public int? _take;
        public int? Take
        {
            get
            {
                if (_take.HasValue is false || _take < 0) return 0;
                return _take;
            }
            set
            {
                _take = value;
            }
        }

        public int? _skip;
        public int? Skip
        {
            get
            {
                if (_skip.HasValue is false || _skip < 0) return 0;
                return _skip;
            }
            set
            {
                _skip = value;
            }
        }
    }
}
