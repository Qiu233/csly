using System;
using System.Linq;
using simpleExpressionParser;
using sly.parser.generator;

namespace Issue244
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ParserBuilder<SimpleExpressionToken, Result>();

            var result = builder.BuildParser(new Issue244Parser(), ParserType.EBNF_LL_RECURSIVE_DESCENT, $"{typeof(Issue244Parser).Name}_expressions");
            if (!result.IsOk)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine(error.Message);
                }
                throw new Exception(result.Errors.First().ToString());
            }
                

            var r = result.Result.Parse(" 2 + 2 + 2");
            double doubleValue = r.Result;
            Console.WriteLine(doubleValue);
        }
    }
}