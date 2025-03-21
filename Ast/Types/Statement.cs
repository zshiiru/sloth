using System.Text;
using sloth.AST;
using sloth.Lexer.Token;

namespace sloth.Ast.Types;

public interface IStatement : INode
{
    public void StatementNode();
}

public class LetStatement(Identifier name, IExpression value, Token token) : IStatement
{
    public Identifier Name = name;
    private readonly Token _token = token;
    private readonly IExpression _value = value;

    public string TokenLiteral()
    {
        return _token.Literal;
    }

    public void StatementNode()
    {
    }

    public override string ToString()
    {
        StringBuilder buffer = new();

        buffer.Append(TokenLiteral() + " ");
        buffer.Append(Name);
        buffer.Append(" = ");
        buffer.Append(_value.ToString());
        buffer.Append(';');

        return buffer.ToString();
    }
}

public class ExpressionStatement(IExpression? expression, Token token) : IStatement
{
    public readonly IExpression? Expression = expression;
    private readonly Token _token = token;

    public string TokenLiteral()
    {
        return _token.Literal;
    }

    public void StatementNode()
    {
    }

    public override string ToString()
    {
        return Expression != null ? Expression.ToString() : "";
    }
}

public class ReturnStatement(IExpression? returnValue, Token token) : IStatement
{
    private readonly IExpression? _returnValue = returnValue;
    private readonly Token _token = token;

    public string TokenLiteral()
    {
        return _token.Literal;
    }

    public void StatementNode()
    {
    }

    public override string ToString()
    {
        StringBuilder buffer = new();

        buffer.Append(TokenLiteral() + " ");
        if (_returnValue != null) buffer.Append(_returnValue.ToString());
        buffer.Append(';');

        return buffer.ToString();
    }
}