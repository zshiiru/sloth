using Newtonsoft.Json.Linq;
using sloth.Lexer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace sloth.AST
{
    public interface IStatement : INode
    {
        public void StatementNode();
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

        public void StatementNode() { }

        public override string ToString()
        {
            StringBuilder buffer = new();

            buffer.Append(TokenLiteral() + " ");
            buffer.Append(Name.ToString());
            buffer.Append(" = ");
            
            if (Value != null)
            {
                buffer.Append(Value.ToString());
            }

            buffer.Append(";");

            return buffer.ToString();
        }
    }

    public class ExpressionStatement : IStatement
    {
        public Token Token;
        public IExpression Expression;

        public string TokenLiteral()
        {
            return Token.Literal;
        }

        public void StatementNode() { }

        public override string ToString()
        {
            if (Expression != null) 
                return Expression.ToString();

            return "";
        }
    }
    public class ReturnStatement : IStatement
    {
        public Token Token;
        public IExpression ReturnValue;

        public string TokenLiteral()
        {
            return Token.Literal;
        }

        public void StatementNode() { }

        public override string ToString()
        {
            StringBuilder buffer = new();

            buffer.Append(TokenLiteral() + " ");

            if (ReturnValue != null)
            {
                buffer.Append(ReturnValue.ToString());
            }

            buffer.Append(";");

            return buffer.ToString();
        }
    }
}
