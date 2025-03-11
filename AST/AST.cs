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
        string ToString();
    }



    public interface IExpression : INode
    {
        public void ExpressionNode();
    }



    public class Identifier : IExpression // implements Expression here
    {
        public Token Token; // IDENT TOKEN
        public string Value;
        public string TokenLiteral() => Token.Literal;
        public string ToString() => Value;

        public Identifier(Token token, string value)
        {
            this.Token = token;
            this.Value = value;
        }

        public void ExpressionNode() { }
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

        public override string ToString()
        {
            StringBuilder stringBuilder = new();

            foreach (IStatement statement in Statements)
            {
                stringBuilder.Append(statement.ToString());
            }

            return stringBuilder.ToString();
        }
    }
}
