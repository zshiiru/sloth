
using System.Text;
using sloth.AST;
using sloth.Lexer.Token;

namespace sloth.Ast.Types;

public class PrefixExpression(Token token, string op) : IExpression
{
    private Token token = token;
    public string Operator = op;
    public IExpression Right;
    
    public void ExpressionNode()
    {
        throw new NotImplementedException();
    }

    public string TokenLiteral()
    {
        return token.Literal;
    }

    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();

        builder.Append("(");
        builder.Append(Operator);
        builder.Append(Right.ToString());
        builder.Append(")");
        
        return builder.ToString();
    }
}