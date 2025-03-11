using sloth.Lexer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sloth.Parser
{
    public class Error
    {
        private List<String> errors;

        public Error()
        {
            errors = new();
        }

        public List<String> GetErrors() => errors;
        private void AddError(string error) => errors.Add(error);

        public void PeekError(TokenType currentTokenType, TokenType expectedTokenType)
        {
            AddError($"expected next token to be {expectedTokenType}, got {currentTokenType} instead");
        }
    }
}
