using sloth.Lexer.Token;

namespace sloth;

internal static class Program
{
    private const string Prompt = ">> ";

    private static void Repl()
    {
        Console.WriteLine("Hello! This is the Sloth programming language!\n");
        Console.WriteLine("Feel free to type in commands\n");

        while (true)
        {
            Console.Write(Prompt);

            var input = Console.ReadLine();

            if (input == null) continue;
            
            var lexer = new Lexer.Lexer(input);

            var token = lexer.NextToken();

            while (token.Type != TokenType.EOF)
            {
                Console.WriteLine($"{{ {token.Type.ToString()} {token.Literal} }}");
                token = lexer.NextToken();
            }
        }
    }

    private static void Main(string[] args)
    {
        Repl();
    }
}