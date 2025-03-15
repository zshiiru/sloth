using sloth.Lexer.Token;

namespace sloth.Lexer;

/* Main lexer logic */
public class Lexer
{
    private readonly Dictionary<string, TokenType> _lexerKeywordDictionary = new()
    {
        { "fn", TokenType.FUNCTION },
        { "let", TokenType.LET },
        { "true", TokenType.TRUE },
        { "false", TokenType.FALSE },
        { "if", TokenType.IF },
        { "else", TokenType.ELSE },
        { "return", TokenType.RETURN }
    };

    private readonly Dictionary<char, TokenType> _lexerTokenDictionary = new()
    {
        { '=', TokenType.ASSIGN },
        { ';', TokenType.SEMICOLON },
        { '(', TokenType.LPAREN },
        { ')', TokenType.RPAREN },
        { ',', TokenType.COMMA },
        { '+', TokenType.PLUS },
        { '{', TokenType.LBRACE },
        { '}', TokenType.RBRACE },
        { '-', TokenType.MINUS },
        { '!', TokenType.BANG },
        { '*', TokenType.ASTERISK },
        { '/', TokenType.SLASH },
        { '<', TokenType.LT },
        { '>', TokenType.GT },
        { '\0', TokenType.EOF }
    };

    private char _char;

    private readonly string _input;
    private int _position;
    private int _readPosition;


    public Lexer(string input)
    {
        _input = input;

        ReadChar();
    }

    /* Read next char */
    private void ReadChar()
    {
        _char = _readPosition >= _input.Length ? '\0' : _input[_readPosition];
        _position = _readPosition;
        _readPosition += 1;
    }

    /* Get the next token */
    public Token.Token NextToken()
    {
        Token.Token token;

        SkipWhitespace();

        // Dictionary lookup
        if (_lexerTokenDictionary.TryGetValue(_char, out var tokenType))
        {
            token = NewToken(tokenType, _char);

            switch (tokenType)
            {
                case TokenType.EOF:
                    token.Literal = "";
                    break;
                // == case
                case TokenType.ASSIGN:
                {
                    if (PeekChar() == '=')
                    {
                        var lastChar = _char;
                        ReadChar();
                        token.Type = TokenType.EQ;
                        token.Literal = lastChar.ToString() + _char;
                    }

                    break;
                }
                // != case
                case TokenType.BANG:
                {
                    if (PeekChar() == '=')
                    {
                        var lastChar = _char;
                        ReadChar();
                        token.Type = TokenType.NOT_EQ;
                        token.Literal = lastChar.ToString() + _char;
                    }

                    break;
                }
            }
        }
        else
        {
            // Token missing, check for identifier
            if (char.IsAsciiLetter(_char) || _char == '_')
            {
                token.Literal = ReadIdentifier();

                // Determine which keyword the identifier is
                token.Type = _lexerKeywordDictionary.GetValueOrDefault(token.Literal, TokenType.IDENT);

                return token;
            }

            if (char.IsAsciiDigit(_char))
            {
                // Read number
                token.Type = TokenType.INT;
                token.Literal = ReadNumber();
                return token;
            }

            token = NewToken(TokenType.ILLEGAL, _char);
        }

        ReadChar();

        return token;
    }

    /* Read identifier */
    private string ReadIdentifier()
    {
        var oldPosition = _position;

        // Read until the identifier ends
        while (char.IsAsciiLetter(_char) || _char == '_') ReadChar();

        // Return identifier
        return _input.Substring(oldPosition, _position - oldPosition);
    }

    // Read number
    private string ReadNumber()
    {
        var oldPosition = _position;

        // Read until no more digits
        while (char.IsAsciiDigit(_char)) ReadChar();


        // Return identifier
        return _input.Substring(oldPosition, _position - oldPosition);
    }

    // Peek char
    private char PeekChar()
    {
        return _readPosition >= _input.Length ? '\0' : _input[_readPosition];
    }

    private void SkipWhitespace()
    {
        while (char.IsWhiteSpace(_char))
            ReadChar();
    }

    private static Token.Token NewToken(TokenType tokenType, char ch)
    {
        return new Token.Token(tokenType, ch.ToString());
    }
}