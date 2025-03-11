using sloth.AST;
using sloth.Lexer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sloth.Parser
{

    public class Parser
    {

        private Lexer.Lexer lexer;
        private Token currentToken;
        private Token peekToken;

        private Error errorHandler;

        public Parser(Lexer.Lexer lexer)
        {
            this.lexer = lexer;

            // Classes purely for organization
            this.errorHandler = new();

            // Read two tokens, so curToken and peekToken are both set
            NextToken();
            NextToken();
        }
        
        // Error interfaces
        public List<String> GetErrors() => errorHandler.GetErrors();

        public void NextToken()
        {
            currentToken = peekToken;
            peekToken = lexer.NextToken();
        }

        public IStatement? ParseLetStatement()
        {
            LetStatement statement = new LetStatement();
            statement.Token = currentToken;

            // Check if the next token is a IDENT
            if (!ExpectPeek(TokenType.IDENT))
                return null; // Missing IDENT after let


            // Add identifier to let statement
            Identifier identifier = new(currentToken, currentToken.Literal); // ExpectPeek grabs the next token, so our identifier is the currentToken

            statement.Name = identifier;

            // TODO: We're skipping the expressions until we
            // encounter a semicolon
            while (currentToken.Type != TokenType.SEMICOLON)
            {
                NextToken();
            }

            return statement;
        }

        public IStatement? ParseReturnStatement()
        {
            ReturnStatement statement = new ReturnStatement();
            statement.Token = currentToken;

            NextToken();

            // TODO: We're skipping the expressions until we
            // encounter a semicolon
            while (currentToken.Type != TokenType.SEMICOLON)
            {
                NextToken();
            }

            return statement;
        }
        public IStatement? ParseStatement()
        {
            switch (currentToken.Type)
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
        public bool ExpectPeek(TokenType tokenType)
        {
            if (peekToken.Type == tokenType)
            {
                NextToken();
                return true;
            }

            // Log error
            errorHandler.PeekError(currentToken.Type, tokenType);
            return false;
        }
        public AST.SlothProgram ParseProgram()
        {
            AST.SlothProgram program = new();

            program.Statements = new();

            while (currentToken.Type != TokenType.EOF)
            {
                IStatement statement = ParseStatement();
                if (statement != null)
                {
                    program.Statements.Add(statement);
                }

                NextToken();
            }

            return program;
        }

    }
}
