namespace sloth.Lexer.Token;

/* Define the types of tokens possible */
public enum TokenType
{
    ILLEGAL,
    EOF,

    // Identifiers + literals
    IDENT, // add, foobar, x, y, ...
    INT,

    // Operators
    ASSIGN,
    PLUS,
    MINUS,
    BANG,
    ASTERISK,
    SLASH,

    LT,
    GT,

    EQ,
    NOT_EQ,

    // Delimiters
    COMMA,
    SEMICOLON,

    LPAREN,
    RPAREN,
    LBRACE,
    RBRACE,

    // Keywords
    FUNCTION,
    LET,
    TRUE,
    FALSE,
    IF,
    ELSE,
    RETURN
}

/* Helper class helping with converting the TokenType to a string */
// ReSharper disable once UnusedType.Global
public static class TokenExtension
{
    /* Tokenizer translations */
    private static readonly Dictionary<TokenType, string> TokenStrings = new()
    {
        { TokenType.ILLEGAL, "ILLEGAL" },
        { TokenType.EOF, "EOF" },

        // Identifiers + literals
        { TokenType.IDENT, "IDENT" },
        { TokenType.INT, "INT" },

        // Operators
        { TokenType.ASSIGN, "=" },
        { TokenType.PLUS, "+" },
        { TokenType.MINUS, "-" },
        { TokenType.BANG, "!" },
        { TokenType.ASTERISK, "*" },
        { TokenType.SLASH, "/" },
        { TokenType.LT, "<" },
        { TokenType.GT, ">" },
        { TokenType.EQ, "=" },
        { TokenType.NOT_EQ, "!=" },

        // Delimiters
        { TokenType.COMMA, "," },
        { TokenType.SEMICOLON, ";" },

        { TokenType.LPAREN, "(" },
        { TokenType.RPAREN, ")" },
        { TokenType.LBRACE, "{" },
        { TokenType.RBRACE, "}" },

        // Keywords
        { TokenType.FUNCTION, "FUNCTION" },
        { TokenType.LET, "LET" },
        { TokenType.TRUE, "TRUE" },
        { TokenType.FALSE, "FALSE" },
        { TokenType.IF, "IF" },
        { TokenType.ELSE, "ELSE" },
        { TokenType.RETURN, "RETURN" }
    };

    /* Enum extension */
    // ReSharper disable once UnusedMember.Global
    public static string ToString(this TokenType tokenType)
    {
        return TokenStrings.GetValueOrDefault(tokenType, "ILLEGAL");
    }
}

/* Main token structure */
public struct Token(TokenType type, string literal)
{
    public TokenType Type = type;
    public string Literal = literal;
}