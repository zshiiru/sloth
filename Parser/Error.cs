using sloth.Lexer.Token;

namespace sloth.Parser;

public class Error
{
    private readonly List<string> _errors = [];

    public List<string> GetErrors()
    {
        return _errors;
    }

    private void AddError(string error)
    {
        _errors.Add(error);
    }

    public void PeekError(TokenType currentTokenType, TokenType expectedTokenType)
    {
        AddError($"expected next token to be {expectedTokenType}, got {currentTokenType} instead");
    }
}