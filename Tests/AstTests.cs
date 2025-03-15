using sloth.AST;
using sloth.Ast.Types;
using sloth.Lexer.Token;
using Xunit;

namespace sloth.Tests;

public class AstTests
{
    [Fact]
    public void TestToString()
    {
        SlothProgram program = new();

        var name = new Identifier(new Token(TokenType.IDENT, "myVar"), "myVar");
        var value = new Identifier(new Token(TokenType.IDENT, "anotherVar"), "anotherVar");
        var token = new Token(TokenType.LET, "let");

        LetStatement letStatement = new(
            name, value, token
        );

        program.Statements = [letStatement];

        Assert.Equal("let myVar = anotherVar;", program.ToString());
    }
}