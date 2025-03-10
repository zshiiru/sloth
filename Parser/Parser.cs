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

        public Parser(Lexer.Lexer lexer)
        {
            this.lexer = lexer;

            // Read two tokens, so curToken and peekToken are both set
            NextToken();
            NextToken();
        }

        public void NextToken()
        {
            currentToken = peekToken;
            peekToken = lexer.NextToken();
        }

        public AST.SlothProgram ParseProgram()
        {
            return new AST.SlothProgram();
        }
    }
}
