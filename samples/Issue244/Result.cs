namespace Issue244
{
    public class Result
    {
        public double Value { get; set; }

        public int StartColumn { get; set; }

        public int EndColumn { get; set; }

        public int Column => EndColumn - StartColumn;
        
        public static implicit operator double(Result result) => result.Value;
        public static explicit operator Result(double dbl) => new Result()
        {
            Value = dbl
        };

    }
}