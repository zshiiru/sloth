using sloth.AST;
using sloth.Lexer.Token;

namespace sloth.Ast.Types;

public class IntegerLiteral(Token token) : IExpression
{
    public readonly Token Token = token;
    public Int64 Value;

    public void ExpressionNode()
    {
        throw new NotImplementedException();
    }

    public string TokenLiteral()
    {
        return Token.Literal;
    }

    public override string ToString()
    {
        return Token.Literal;
    }
}