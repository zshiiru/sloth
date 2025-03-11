using sloth.AST;
using sloth.Lexer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace sloth.Tests
{
    public class ASTTests
    {
        [Fact]
        public void TestToString()
        {
            AST.SlothProgram program = new();

            LetStatement letStatement = new();
            letStatement.Token = new Token(TokenType.LET, "let");
            letStatement.Name = new Identifier(new Token(TokenType.IDENT, "myVar"), "myVar");
            letStatement.Value = new Identifier(new Token(TokenType.IDENT, "anotherVar"), "anotherVar");

            program.Statements = new List<IStatement>() {
                letStatement
            };

            Assert.Equal(program.ToString(), "let myVar = anotherVar;");
        }
    }
}
