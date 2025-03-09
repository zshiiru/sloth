using sloth.token;
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
                Console.WriteLine(Char);
            }

            Position = ReadPosition;
            ReadPosition += 1;
        }

        /* Get the next token */
        public Token NextToken()
        {
            Token token;

            switch (Char)
            {
                case '=':
                    token = NewToken(TokenType.ASSIGN, Char);
                    break;
                case ';':
                    token = NewToken(TokenType.SEMICOLON, Char);
                    break;
                case '(':
                    token = NewToken(TokenType.LPAREN, Char);
                    break;
                case ')':
                    token = NewToken(TokenType.RPAREN, Char);
                    break;
                case ',':
                    token = NewToken(TokenType.COMMA, Char);
                    break;
                case '+':
                    token = NewToken(TokenType.PLUS, Char);
                    break;
                case '{':
                    token = NewToken(TokenType.LBRACE, Char);
                    break;
                case '}':
                    token = NewToken(TokenType.RBRACE, Char);
                    break;
                case '\0':
                    token.Literal = "";
                    token.Type = TokenType.EOF;
                    break;
                default:
                    token.Literal = "";
                    token.Type = TokenType.ILLEGAL;
                    break;
            }

            ReadChar();
            return token;
        }

        private Token NewToken(TokenType tokenType, char ch)
        {
            return new Token(tokenType, ch.ToString());
        }
    }
}
