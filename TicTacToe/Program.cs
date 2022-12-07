
using System.Diagnostics;

namespace TicTacToe
{
    internal class Program
    {
        static void coloredMessage(ConsoleColor color, string text_message) {
            Console.ForegroundColor = color;
            Console.Write(text_message);
            Console.ResetColor();
        }
        static void Main(string[] args)
        {
            MainGame tic = new MainGame();
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + ("Welcome to TicTacToe".Length / 2)) + "}", "Welcome to TicTacToe"));
                Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + ("Press 'S' to start a new game".Length / 2)+5) + "}", "Press 'S' to start a new game"));
                Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + ("Press 'Q' to quit the game".Length / 2)+3) + "}", "Press 'Q' to quit the game"));
                Console.Write(String.Format("{0," + ((Console.WindowWidth / 2) + ("------Your choice: ".Length / 2)) + "}", "------Your choice: "));
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Blue;
                char c;
                try {
                    c = char.Parse(Console.ReadLine());
                }catch(Exception InvalidChoiceException) {
                    c = '\0';
                }
                
                Console.ResetColor();
                while (char.ToUpper(c) != 'Q' && char.ToUpper(c) != 'S')
                {

                    coloredMessage(ConsoleColor.Red, "Please enter a valid choice : ");
                    try {
                        c = char.Parse(Console.ReadLine());
                    } catch (Exception InvalidChoiceException) {
                        c = '\0';
                    }
                }
                if (c == 'q' || c == 'Q') {
                    Environment.Exit(1);
                } else {
                    tic.displayMainMenu();
                    while (tic.gameActive == true) {
                        tic.playerMove();
                        Console.Clear();
                    }
                }
            }
            
        }
    }
}