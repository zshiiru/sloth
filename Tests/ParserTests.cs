using sloth.AST;
using sloth.Lexer;
using sloth.Parser;
using System;
using System.Collections.Generic;
using Xunit;

namespace sloth.Tests
{
    public class ParserTests
    {
        [Fact]
        public void TestLetStatements()
        {
            var input = @"
let x = 5;
let y = 10;
let foobar = 838383;
";
            var lexer = new Lexer.Lexer(input);
            var parser = new Parser.Parser(lexer);

            var program = parser.ParseProgram();
            Assert.NotNull(program);
            Assert.Equal(3, program.Statements.Count);

            var tests = new List<string>
            {
                ("x"),
                ("y"),
                ("foobar")
            };

            for (int i = 0; i < tests.Count; i++)
            {
                var test = tests[i];
                var stmt = program.Statements[i];
                if (!TestLetStatement(stmt, test))
                {
                    return;
                }
            }
        }

        private bool TestLetStatement(IStatement stmt, string name)
        {
            if (stmt.TokenLiteral() != "let")
            {
                Assert.Equal("let", stmt.TokenLiteral());
                return false;
            }

            var letStmt = stmt as LetStatement;
            if (letStmt == null)
            {
                Assert.IsType<LetStatement>(stmt);
                return false;
            }

            if (letStmt.Name.Value != name)
            {
                Assert.Equal(name, letStmt.Name.Value);
                return false;
            }

            if (letStmt.Name.TokenLiteral() != name)
            {
                Assert.Equal(name, letStmt.Name.TokenLiteral());
                return false;
            }

            return true;
        }
    }
}
