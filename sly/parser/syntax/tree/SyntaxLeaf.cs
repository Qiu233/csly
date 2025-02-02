using sly.lexer;

namespace sly.parser.syntax.tree
{
    public class SyntaxLeaf<IN> : ISyntaxNode<IN> where IN : struct
    {
        public SyntaxLeaf(Token<IN> token, bool discarded)
        {
            Token = token;
            Discarded = discarded;
        }
        
        

        public Token<IN> Token { get;  }
        public bool Discarded { get; }
        public string Name => Token.TokenID.ToString();
        
        public bool HasByPassNodes { get; set; } = false;
        
        public string Dump(string tab)
        {
            return $"{tab}+ {Token.TokenID} : {Token.Value} @{Token.PositionInTokenFlow}";
        }
    }
}