using expressionparser;
using simpleExpressionParser;
using sly.lexer;
using sly.parser.generator;

namespace Issue244
{
 public class Issue244Parser
    {
        [Operation((int) SimpleExpressionToken.PLUS, Affix.InFix, Associativity.Right, 10)]
        [Operation("MINUS", Affix.InFix, Associativity.Left, 10)]
        public Result BinaryTermExpression(Result left, Token<SimpleExpressionToken> operation, Result right)
        {
            Result result = null;
            switch (operation.TokenID)
            {
                case SimpleExpressionToken.PLUS:
                {
                    result = new Result()
                    {
                        Value = left.Value + right.Value, 
                        StartColumn = left.StartColumn,
                        EndColumn = right.EndColumn
                    }
                    ;
                    break;
                }
                case SimpleExpressionToken.MINUS:
                {
                    result = new Result()
                    {
                        Value = left.Value - right.Value,
                        StartColumn = left.StartColumn,
                        EndColumn = right.EndColumn
                    };
                    break;
                }
            }

            return result;
        }


        [Operation((int) SimpleExpressionToken.TIMES, Affix.InFix, Associativity.Right, 50)]
        [Operation("DIVIDE", Affix.InFix, Associativity.Left, 50)]
        public Result BinaryFactorExpression(Result left, Token<SimpleExpressionToken> operation, Result right)
        {
            Result result = null;
            switch (operation.TokenID)
            {
                case SimpleExpressionToken.TIMES:
                {
                    result = new Result()
                    {
                        Value = left.Value * right.Value,
                        StartColumn = left.StartColumn,
                        EndColumn = right.EndColumn
                    };
                    break;
                }
                case SimpleExpressionToken.DIVIDE:
                {
                    result = new Result()
                    {
                        Value = left.Value / right.Value,
                        StartColumn = left.StartColumn,
                        EndColumn = right.EndColumn
                    };
                    break;
                }
            }

            return result;
        }


        [Operation((int) SimpleExpressionToken.MINUS, Affix.PreFix, Associativity.Right, 100)]
        public Result PreFixExpression(Token<SimpleExpressionToken> operation, Result value)
        {
            return new Result()
            {
                Value = -value.Value, 
                StartColumn = operation.Position.Column,
                EndColumn = value.EndColumn
            };
        }

       

        [Operand]
        [Production("operand : primary_value")]
        public Result OperandValue(Result value)
        {
            return value;
        }


        [Production("primary_value : DOUBLE")]
        public Result OperandDouble(Token<SimpleExpressionToken> value)
        {
            return new Result()
            {
                Value = value.DoubleValue,
                StartColumn = value.Position.Column,
                EndColumn = value.Position.Column + value.Value.Length
            };
        }
        
        [Production("primary_value : INT")]
        public Result OperandInt(Token<SimpleExpressionToken> value)
        {
            return new Result()
            {
                Value = value.DoubleValue,
                StartColumn = value.Position.Column,
                EndColumn = value.Position.Column + value.Value.Length
            };
        }

        [Production("primary_value : LPAREN Issue244Parser_expressions RPAREN")]
        public Result OperandParens(Token<SimpleExpressionToken> lparen, Result value, Token<SimpleExpressionToken> rparen)
        {
            return new Result()
            {
                Value = value.Value,
                StartColumn = lparen.Position.Column,
                EndColumn = rparen.Position.Column + rparen.Value.Length
            };
        }
    }
}