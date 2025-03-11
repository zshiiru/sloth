using sloth.AST;
using sloth.Lexer;
using sloth.Parser;
using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace sloth.Tests
{
    public class ParserTests
    {

        private readonly ITestOutputHelper _output;

        public ParserTests(ITestOutputHelper output)
        {
            _output = output;
        }

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
            CheckParserErrors(parser);

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
                var name = tests[i];
                var stmt = program.Statements[i];
                if (stmt.TokenLiteral() != "let")
                {
                    Assert.Equal("let", stmt.TokenLiteral());
                }

                var letStmt = stmt as LetStatement;
                if (letStmt == null)
                {
                    Assert.IsType<LetStatement>(stmt);
                }

                if (letStmt.Name.Value != name)
                {
                    Assert.Equal(name, letStmt.Name.Value);
                }

                if (letStmt.Name.TokenLiteral() != name)
                {
                    Assert.Equal(name, letStmt.Name.TokenLiteral());
                }
            }
        }

        [Fact]
        public void TestReturnStatements()
        {
            var input = @"
return 5;
return 10;
return 993322;
";
            var lexer = new Lexer.Lexer(input);
            var parser = new Parser.Parser(lexer);

            var program = parser.ParseProgram();
            CheckParserErrors(parser);

            Assert.NotNull(program);
            Assert.Equal(3, program.Statements.Count);


            for (int i = 0; i < program.Statements.Count; i++)
            {
                IStatement statement = program.Statements[i];

                Assert.True(statement is ReturnStatement);
                Assert.Equal("return", statement.TokenLiteral());
            }
        }

        private void CheckParserErrors(Parser.Parser parser)
        {
            List<string> ParserErrors = parser.GetErrors();

            if (ParserErrors.Count == 0)
                return;

            this._output.WriteLine($"parser has {ParserErrors.Count} errors");

            foreach (string error in ParserErrors)
            {
                this._output.WriteLine($"parser error: {error}");
            }
        }
    }
}
