using sloth.AST;
using sloth.Ast.Types;
using sloth.Lexer.Token;

namespace sloth.Parser;

public delegate IExpression PrefixParse();

public delegate IExpression InfixParse(IExpression expression);

public class Parser
{
    private readonly Error _errorHandler;

    private readonly Lexer.Lexer _lexer;
    private Token _currentToken;
    private Token _peekToken;

    private Dictionary<TokenType, PrefixParse> _prefixParseMap = [];
    private Dictionary<TokenType, InfixParse> _infixParseMap = [];

    public Parser(Lexer.Lexer lexer)
    {
        _lexer = lexer;

        // Classes purely for organization
        _errorHandler = new Error();

        // Read two tokens, so curToken and peekToken are both set
        NextToken();
        NextToken();
    }

    // Error interfaces
    public List<string> GetErrors()
    {
        return _errorHandler.GetErrors();
    }

    private void NextToken()
    {
        _currentToken = _peekToken;
        _peekToken = _lexer.NextToken();
    }

    private LetStatement? ParseLetStatement()
    {
        var statement = new LetStatement(null, null, _currentToken);

        // Check if the next token is a IDENT
        if (!ExpectPeek(TokenType.IDENT))
            return null; // Missing IDENT after let


        // Add identifier to let statement
        Identifier
            identifier =
                new(_currentToken,
                    _currentToken.Literal); // ExpectPeek grabs the next token, so our identifier is the currentToken

        statement.Name = identifier;

        // TODO: We're skipping the expressions until we
        // encounter a semicolon
        // TODO: (THE VALUE IS MISSING!!!)
        while (_currentToken.Type != TokenType.SEMICOLON) NextToken();

        return statement;
    }

    private ReturnStatement ParseReturnStatement()
    {
        var statement = new ReturnStatement(null, _currentToken);
        NextToken();

        // TODO: We're skipping the expressions until we
        // encounter a semicolon
        while (_currentToken.Type != TokenType.SEMICOLON) NextToken();

        return statement;
    }

    private IStatement? ParseStatement()
    {
        switch (_currentToken.Type)
        {
            case TokenType.LET:
                return ParseLetStatement();
            case TokenType.RETURN:
                return ParseReturnStatement();
            default:
                return null;
        }
    }

    // Function to help enforce the order of tokens
    private bool ExpectPeek(TokenType tokenType)
    {
        if (_peekToken.Type == tokenType)
        {
            NextToken();
            return true;
        }

        // Log error
        _errorHandler.PeekError(_currentToken.Type, tokenType);
        return false;
    }

    private void RegisterPrefix(TokenType tokenType, PrefixParse prefixParse)
    {
        _prefixParseMap[tokenType] = prefixParse;
    }

    private void RegisterInfix(TokenType tokenType, InfixParse infixParse)
    {
        _infixParseMap[tokenType] = infixParse;
    }
    
    public SlothProgram ParseProgram()
    {
        SlothProgram program = new()
        {
            Statements = []
        };

        while (_currentToken.Type != TokenType.EOF)
        {
            var statement = ParseStatement();
            if (statement != null) program.Statements.Add(statement);

            NextToken();
        }

        return program;
    }
}