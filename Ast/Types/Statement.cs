using System.Text;
using sloth.Lexer;

namespace sloth.AST;

public interface IStatement : INode
{
    public void StatementNode();
}

public class LetStatement : IStatement
{
    public Identifier Name;
    public Token Token;
    public IExpression Value;

    public string TokenLiteral()
    {
        return Token.Literal;
    }

    public void StatementNode()
    {
    }

    public override string ToString()
    {
        StringBuilder buffer = new();

        buffer.Append(TokenLiteral() + " ");
        buffer.Append(Name.ToString());
        buffer.Append(" = ");

        if (Value != null) buffer.Append(Value.ToString());

        buffer.Append(";");

        return buffer.ToString();
    }
}

public class ExpressionStatement : IStatement
{
    public IExpression Expression;
    public Token Token;

    public string TokenLiteral()
    {
        return Token.Literal;
    }

    public void StatementNode()
    {
    }

    public override string ToString()
    {
        if (Expression != null)
            return Expression.ToString();

        return "";
    }
}

public class ReturnStatement : IStatement
{
    public IExpression ReturnValue;
    public Token Token;

    public string TokenLiteral()
    {
        return Token.Literal;
    }

    public void StatementNode()
    {
    }

    public override string ToString()
    {
        StringBuilder buffer = new();

        buffer.Append(TokenLiteral() + " ");

        if (ReturnValue != null) buffer.Append(ReturnValue.ToString());

        buffer.Append(";");

        return buffer.ToString();
    }
}