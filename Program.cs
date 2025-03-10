
using sloth.Lexer;
using System.Security.Principal;

namespace sloth
{
    class Program
    {
        private static string prompt = ">> ";
        static void REPL()
        {
            Console.WriteLine("Hello " + WindowsIdentity.GetCurrent().Name + "! This is the Sloth programming language!\n");
            Console.WriteLine("Feel free to type in commands\n");

            while (true)
            {
                Console.Write(prompt);

                string input = Console.ReadLine();

                Lexer.Lexer lexer = new Lexer.Lexer(input);

                Token token = lexer.NextToken();
                
                while (token.Type != TokenType.EOF)
                {
                    Console.WriteLine($"{{ {token.Type.ToString()} {token.Literal} }}");
                    token = lexer.NextToken();
                }
            }
        }
        static void Main(string[] args)
        {
            REPL();
        }
    }
}
