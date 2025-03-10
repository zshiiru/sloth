using sloth.Lexer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sloth.AST
{
    public interface INode
    {
        string TokenLiteral();
    }

    public interface IStatement : INode
    {
        public void StatementNode();
    }

    public interface IExpression : INode
    {
        public void ExpressionNode();
    }



    public class LetStatement : IStatement
    {
        public Token Token;
        public Identifier Name;
        public IExpression Value;

        public string TokenLiteral()
        {
            return Token.Literal;
        }
         
        public void StatementNode()
        {

        }
    }

    public class Identifier
    {
        public Token Token; // IDENT TOKEN
        public string Value;
        public string TokenLiteral()
        {
            return Token.Literal;
        }
    }

    public class SlothProgram
    {
        public List<IStatement> Statements;

        public SlothProgram()
        {
            Statements = new();
        }

        public string TokenLiteral()
        {
            if (Statements.Count > 0)
            {
                return Statements[0].TokenLiteral();
            } else
            {
                return "";
            }
        }
    }
}
