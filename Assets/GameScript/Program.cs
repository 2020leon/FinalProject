using System;

namespace FinalProject
{
    class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("# Game Log");
            GameIO.GameIn = new Example.GameIn();
            GameIO.GameOut = new Example.GameOut();
            Game game;
            do {
                game = new Game();
                game.Start();
                game.DoRounds().Wait();
                Console.WriteLine("\n## " + game.Status);
            } while (false);
        }
    }
}
