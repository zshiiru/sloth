
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
            {'\0', TokenType.EOF }
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
                Console.WriteLine(Char);
            }

            Position = ReadPosition;
            ReadPosition += 1;
        }

        /* Get the next token */
        public Token NextToken()
        {

            Token token;

            if (LexerTokenDictionary.TryGetValue(Char, out TokenType tokenType)) {
                token = NewToken(tokenType, Char);

                if (tokenType == TokenType.EOF)
                    token.Literal = "";

            } else {
                token.Literal = "";
                token.Type = TokenType.ILLEGAL;
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
