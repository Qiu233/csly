using System.Collections.Generic;
using sly.parser.syntax.tree;

namespace sly.parser
{
    public class SyntaxParseResult<IN> where IN : struct
    {
        public ISyntaxNode<IN> Root { get; set; }

        public bool IsError { get; set; }

        public bool IsOk => !IsError;

        public List<UnexpectedTokenSyntaxError<IN>> Errors { get; set; } = new List<UnexpectedTokenSyntaxError<IN>>();

        public int EndingPosition { get; set; }

        public bool IsEnded { get; set; }
        
        public List<IN> Expecting {get; set;}

        public void AddExpecting(IN expected)
        {
            if (Expecting == null)
            {
                Expecting = new List<IN>();
            }
            Expecting.Add(expected);
        }
        
        public void AddExpectings(IEnumerable<IN> expected)
        {
            if (expected == null)
            {
                return;
            }
            if (Expecting == null)
            {
                Expecting = new List<IN>();
            }
            Expecting.AddRange(expected);
        }

        public bool HasByPassNodes { get; set; } = false;
        public bool UsesOperations { get; set; }
    }
}