using System;
using System.Collections.Generic;
using System.Threading;

class BlinkRush
{
    static void Main()
    {
        // Array of valid keys to choose from
        string[] validKeys = new string[] 
        { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J",  "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T" };

        Random rng = new Random();
        int score = 0;
        double timeLimit = 3000; // upper limit so first round
        double minTimeLimit = 500; // after a few right gets way faster
        double timeReductionFactor = 0.9; // can be adjusted but currently gest 10 percent faster

        Console.WriteLine("Welcome to Blink Rush!");
        Console.WriteLine("Press the key that appears but be careful! Time gets shorter each win!");
        Console.WriteLine("Press any key to start...");
        Console.ReadKey(true);

        while (true)
        {
            string targetKey = validKeys[rng.Next(validKeys.Length)];
            Console.Clear();
            Console.WriteLine($"Press: {targetKey}");

            DateTime startTime = DateTime.Now;

            while ((DateTime.Now - startTime).TotalMilliseconds < timeLimit)
            {
                if (Console.KeyAvailable)
                {
                    string input = Console.ReadKey(true).Key.ToString().ToUpper(); // uppercase check cause losing to caps log would be sad 

                    if (input == targetKey)
                    {
                        score++;
                        Console.WriteLine("Correct!");
                        timeLimit = Math.Max(minTimeLimit, timeLimit * timeReductionFactor);
                        Thread.Sleep(500); // slight pause cause it was too fast 
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"Wrong key! You pressed {input}. Game Over.");
                        Console.WriteLine($"Final Score: {score}");
                        return;
                    }
                }
            }

            if (!Console.KeyAvailable && (DateTime.Now - startTime).TotalMilliseconds >= timeLimit)
            {
                Console.WriteLine("Too slow! Game Over.");
                Console.WriteLine($"Final Score: {score}");
                return;
            }
        }
    }
}
