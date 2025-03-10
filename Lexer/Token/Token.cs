using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;

namespace sloth.Lexer
{
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

        // Delimiters
        COMMA,
        SEMICOLON,

        LPAREN,
        RPAREN,
        LBRACE,
        RBRACE,

        // Keywords
        FUNCTION,
        LET
    }


    /* Helper class helping with converting the TokenType to a string */
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

            // Delimiters
            { TokenType.COMMA, "," },
            { TokenType.SEMICOLON, ";" },

            { TokenType.LPAREN, "(" },
            { TokenType.RPAREN, ")" },
            { TokenType.LBRACE, "{" },
            { TokenType.RBRACE, "}" },

            // Keywords
            { TokenType.FUNCTION, "FUNCTION" },
            { TokenType.LET, "LET" }
        };

        /* Enum extension */
        public static string ToString(this TokenType tokenType)
        {
            return TokenStrings.TryGetValue(tokenType, out string tokenString) ? tokenString : "ILLEGAL";
        }
    }


    /* Main token structure */
    public struct Token
    {
        public TokenType Type;
        public string Literal;

        public Token(TokenType type, string literal)
        {
            Type = type;
            Literal = literal;
        }
    }
}
