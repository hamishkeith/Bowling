namespace ConcentraBowling
{
    using Microsoft.Extensions.DependencyInjection;
    using Interfaces;
    using Classes;
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            var diServiceResolver = new ServiceCollection()
            .AddSingleton<IGame, Game>()
            .BuildServiceProvider();

            var game = new Game();
            var frame = game.CurrentFrame;

            var i = 0;
            while (game.isPlaying)
            {
                if (frame != game.CurrentFrame)
                {
                    Console.WriteLine($"Frame: {frame} bowled");
                    frame = game.CurrentFrame;
                }
                
                if (i <= game.NumberOfPinsRemainingInFrame())
                {
                    Console.Write($"\rPress enter to bowl: {i}");
                    if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Enter)
                    {
                        game.Roll(i);
                        Console.WriteLine($"\rYou bowled a:            {i.ToString()}");
                    }
                }               
                i++;
                i = i > 10 ? 0 : i;
            }
            Console.WriteLine($"Game over! 10 frames bowled and scored {game.Score()} points");
            Console.ReadLine();
        }
    }
}
