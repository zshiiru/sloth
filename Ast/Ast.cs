using System.Text;
using sloth.Ast.Types;
using sloth.Lexer.Token;

namespace sloth.AST;

public interface INode
{
    string TokenLiteral();
    string ToString();
}

public interface IExpression : INode
{
    public void ExpressionNode();
}

public class Identifier(Token token, string value) : IExpression // implements Expression here
{
    private readonly Token _token = token; // IDENT TOKEN
    public readonly string Value = value;

    public string TokenLiteral()
    {
        return _token.Literal;
    }

    public override string ToString()
    {
        return Value;
    }

    public void ExpressionNode()
    {
    }
}

public class SlothProgram
{
    public List<IStatement> Statements = [];

    public string TokenLiteral()
    {
        return Statements.Count > 0 ? Statements[0].TokenLiteral() : "";
    }

    public override string ToString()
    {
        StringBuilder stringBuilder = new();

        foreach (var statement in Statements) stringBuilder.Append(statement.ToString());

        return stringBuilder.ToString();
    }
}