
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sloth.Lexer
{


    /* Main lexer logic */
    public class Lexer
    {

        private readonly Dictionary<char, TokenType> LexerTokenDictionary = new()
        {
            {'=', TokenType.ASSIGN},
            {';', TokenType.SEMICOLON},
            {'(', TokenType.LPAREN },
            {')', TokenType.RPAREN },
            {',', TokenType.COMMA },
            {'+', TokenType.PLUS },
            {'{', TokenType.LBRACE },
            {'}', TokenType.RBRACE },
            {'-', TokenType.MINUS},
            {'!', TokenType.BANG },
            {'*', TokenType.ASTERISK},
            {'/', TokenType.SLASH},
            {'<', TokenType.LT},
            {'>', TokenType.GT},
            {'\0', TokenType.EOF }
        };

        private readonly Dictionary<string, TokenType> LexerKeywordDictionary = new()
        {
            {"fn", TokenType.FUNCTION},
            {"let", TokenType.LET},
            {"true", TokenType.TRUE},
            {"false", TokenType.FALSE},
            {"if", TokenType.IF},
            {"else", TokenType.ELSE},
            {"return", TokenType.RETURN},
        };

        public string Input;
        public int Position;
        public int ReadPosition;
        public char Char;

        

        public Lexer(string input)
        {
            Input = input;

            ReadChar();
        }

        /* Read next char */
        public void ReadChar()
        {
            if (ReadPosition >= Input.Length)
            {
                Char = '\0';
            } else
            {
                Char = Input[ReadPosition];
            }

            Position = ReadPosition;
            ReadPosition += 1;
        }

        /* Get the next token */
        public Token NextToken()
        {
            Token token;

            SkipWhitespace();

            // Dictionary lookup
            if (LexerTokenDictionary.TryGetValue(Char, out TokenType tokenType)) {
                token = NewToken(tokenType, Char);

                if (tokenType == TokenType.EOF)
                    token.Literal = "";

                // == case
                if (tokenType == TokenType.ASSIGN)
                {
                    if (PeekChar() == '=')
                    {
                        char lastChar = Char;
                        ReadChar();
                        token.Type = TokenType.EQ;
                        token.Literal = lastChar.ToString() + Char.ToString();
                    }
                }

                // != case
                if (tokenType == TokenType.BANG)
                {
                    if (PeekChar() == '=')
                    {
                        char lastChar = Char;
                        ReadChar();
                        token.Type = TokenType.NOT_EQ;
                        token.Literal = lastChar.ToString() + Char.ToString();
                    }
                }

            } else {
                // Token missing, check for identifier
                if (char.IsAsciiLetter(Char) || Char == '_') {
                    token.Literal = ReadIdentifier();

                    // Determine which keyword the identifier is
                    if (LexerKeywordDictionary.TryGetValue(token.Literal, out TokenType keywordTokenType))
                    {
                        token.Type = keywordTokenType;
                    } else
                    {
                        token.Type = TokenType.IDENT;
                    }

                    return token;
                } else if (char.IsAsciiDigit(Char)) {
                    // Read number
                    token.Type = TokenType.INT;
                    token.Literal = ReadNumber();
                    return token;
                } else {
                    token = NewToken(TokenType.ILLEGAL, Char);
                }
            }
            ReadChar();

            return token;
        }

        /* Read identifier */
        private string ReadIdentifier()
        {
            int oldPosition = Position;
            
            // Read until the identifier ends
            while (char.IsAsciiLetter(Char) || Char == '_')
            {
                ReadChar();
            }

            // Return identifier
            return Input.Substring(oldPosition, Position - oldPosition);

        }

        // Read number
        private string ReadNumber()
        {
            int oldPosition = Position;

            // Read until no more digits
            while (char.IsAsciiDigit(Char))
            {
                ReadChar();
            }


            // Return identifier
            return Input.Substring(oldPosition, Position - oldPosition);
        }

        // Peek char
        public char PeekChar()
        {
            if (ReadPosition >= Input.Length)
                return '\0';

            return Input[ReadPosition];
        }
        private void SkipWhitespace()
        {
            while (char.IsWhiteSpace(Char))
                ReadChar();
        }
        private Token NewToken(TokenType tokenType, char ch)
        {
            return new Token(tokenType, ch.ToString());
        }
    }
}
