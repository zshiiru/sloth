
using sloth.Lexer;
using sloth.token;
using Xunit;

namespace sloth.Tests
{
    public class LexerTests
    {
        [Fact]
        public void TestNextToken()
        {
            string input = "=+(){},;";

            var tests = new (TokenType expectedType, string expectedLiteral)[]
            {
                (TokenType.ASSIGN, "="),
                (TokenType.PLUS, "+"),
                (TokenType.LPAREN, "("),
                (TokenType.RPAREN, ")"),
                (TokenType.LBRACE, "{"),
                (TokenType.RBRACE, "}"),
                (TokenType.COMMA, ","),
                (TokenType.SEMICOLON, ";"),
                (TokenType.EOF, "")
            };

            Lexer.Lexer lexer = new(input);

            for (int i = 0; i < tests.Length; i++)
            {
                var token = lexer.NextToken();

                Assert.Equal(tests[i].expectedType, token.Type);
                Assert.Equal(tests[i].expectedLiteral, token.Literal);
            }
        }
    }
}
