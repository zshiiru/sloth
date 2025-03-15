using sloth.AST;
using sloth.Lexer;
using Xunit;

namespace sloth.Tests;

public class AstTests
{
    [Fact]
    public void TestToString()
    {
        SlothProgram program = new();

        LetStatement letStatement = new()
        {
            Token = new Token(TokenType.LET, "let"),
            Name = new Identifier(new Token(TokenType.IDENT, "myVar"), "myVar"),
            Value = new Identifier(new Token(TokenType.IDENT, "anotherVar"), "anotherVar")
        };

        program.Statements = [letStatement];

        Assert.Equal(program.ToString(), "let myVar = anotherVar;");
    }
}