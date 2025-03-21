using sloth.AST;
using sloth.Ast.Types;
using Xunit;
using Xunit.Abstractions;

namespace sloth.Tests;

public class ParserTests(ITestOutputHelper output)
{
    [Fact]
    public void TestLetStatements()
    {
        const string input = """

                             let x = 5;
                             let y = 10;
                             let foobar = 838383;

                             """;
        var lexer = new Lexer.Lexer(input);
        var parser = new Parser.Parser(lexer);

        var program = parser.ParseProgram();
        CheckParserErrors(parser);

        Assert.NotNull(program);
        Assert.Equal(3, program.Statements.Count);

        var tests = new List<string>
        {
            "x",
            "y",
            "foobar"
        };

        for (var i = 0; i < tests.Count; i++)
        {
            var name = tests[i];
            var stmt = program.Statements[i];
            if (stmt.TokenLiteral() != "let") Assert.Equal("let", stmt.TokenLiteral());

            var letStmt = stmt as LetStatement;
            Assert.IsType<LetStatement>(stmt);

            if (letStmt?.Name.Value != name) Assert.Equal(name, letStmt?.Name.Value);

            if (letStmt?.Name.TokenLiteral() != name) Assert.Equal(name, letStmt?.Name.TokenLiteral());
        }
    }

    [Fact]
    public void TestReturnStatements()
    {
        const string input = """

                             return 5;
                             return 10;
                             return 993322;

                             """;
        var lexer = new Lexer.Lexer(input);
        var parser = new Parser.Parser(lexer);

        var program = parser.ParseProgram();
        CheckParserErrors(parser);

        Assert.NotNull(program);
        Assert.Equal(3, program.Statements.Count);


        foreach (var statement in program.Statements)
        {
            Assert.True(statement is ReturnStatement);
            Assert.Equal("return", statement.TokenLiteral());
        }
    }

    [Fact]
    public void TestIdentifierExpression()
    {
        const string input = "foobar;";
        var lexer = new Lexer.Lexer(input);
        var parser = new Parser.Parser(lexer);
        var program = parser.ParseProgram();
        CheckParserErrors(parser);
        
        Assert.Single(program.Statements);
        
        IStatement statement = program.Statements[0];
        Assert.True(program.Statements[0] is ExpressionStatement);

        ExpressionStatement expressionStatement = (ExpressionStatement)statement;
        Assert.True(expressionStatement.Expression is Identifier);

        Identifier expressionStatementIdentifier = (Identifier)expressionStatement.Expression;
        Assert.True(expressionStatementIdentifier.Value == "foobar");
        Assert.True(expressionStatementIdentifier.TokenLiteral() == "foobar");

    }
    private void CheckParserErrors(Parser.Parser parser)
    {
        var parserErrors = parser.GetErrors();

        if (parserErrors.Count == 0)
            return;

        output.WriteLine($"parser has {parserErrors.Count} errors");

        foreach (var error in parserErrors) output.WriteLine($"parser error: {error}");
    }
}