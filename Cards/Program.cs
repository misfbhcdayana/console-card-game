using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Cards
{
    class Program
    {
        static string[,] boardValues = new string[4, 4];
        static bool[,] revealed = new bool[4, 4];
        static void Main(string[] args)
        {
            InitializeBoard();
            while (!AllMatched())
            {
                DisplayBoard();

                //Get the cordinates of the first card and reveal
                Console.Write("Enter the first card (row and column, e.g 1 2) : ");
                (int r1, int c1) = GetCordinates();
                revealed[r1, c1] = true;

                //Get the cordinates of the second card
                DisplayBoard();
                Console.Write("Enter the second card (row and column, e.g 1 2) : ");
                (int r2, int c2) = GetCordinates();

                //prevent selecting the same card twice and reveal the second card
                while (r1==r2 && c1 == c2)
                {
                    Console.WriteLine("Cordinates already selected. Try again");
                    Console.Write("Enter the second card (row and column, e.g 1 2) : ");
                    (r2, c2) = GetCordinates();
                }
                revealed[r2, c2] = true;

                DisplayBoard();
                
                //Check for a match in the cards
                if (boardValues[r1, c1] == boardValues[r2, c2])
                {
                    Console.WriteLine("Congrats, It's a match!!!");
                }
                else
                {
                    Console.WriteLine("Not a match!");
                    Thread.Sleep(3000); //Pause to show cards
                    revealed[r1, c1] = false;
                    revealed[r2, c2] = false;
                    DisplayBoard();
                }
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            DisplayBoard();
            Console.WriteLine("Congrats!! You've matched all pairs!");
            Console.ReadKey();
        }//Main
        static void InitializeBoard()
        {
            List<string> symbols = new List<string>() { "A", "B", "C", "D", "E", "F", "G", "H" };
            //duplicate the symbols
            List<string> cards = new List<string>();
            foreach(var k in symbols)
            {
                cards.Add(k);
                cards.Add(k);
            }
            //shuffle the cards
            Random rand = new Random();
            for(int i=0; i<cards.Count; i++)
            {
                //swap the cards on position i and r
                int r = rand.Next(i, cards.Count);
                string temp = cards[i];
                cards[i] = cards[r];
                cards[r] = temp;
            }
            //Fill the board
            int index = 0;
            for (int row=0; row <4; row++)
            {
                for (int col = 0; col<4; col++)
                {
                    boardValues[row, col] = cards[index];
                    revealed[row, col] = false;
                    index++;
                }
            }

        }//initializeBoard

        static void DisplayBoard()
        {
            Console.Clear();
            Console.WriteLine("    0   1   2   3");
            Console.WriteLine("  +---+---+---+---+");
            for (int row=0; row<4; row++)
            {
                Console.Write(row + " |");
                for (int col=0; col<4; col++)
                {
                    if (revealed[row, col])
                        Console.Write(" " + boardValues[row, col] + " |");
                    else
                        Console.Write(" * |");
                }
                Console.WriteLine();
                Console.WriteLine("  +---+---+---+---+");
            }
        }//Display board
        static (int, int) GetCordinates()
        {
            int row, col;
            while (true)
            {
                string[] Parts = Console.ReadLine().Split();
                if (Parts.Length == 2 && int.TryParse(Parts[0], out row) 
                    && int.TryParse(Parts[1], out col) && row>=0 && row<4
                    && col>=0 && col<4)
                {
                    break;
                }
                Console.WriteLine("Invalid input. Enter row and column (0 to 3): ");
            }
            return (row, col);
        }//GetCordinates
        static bool AllMatched()
        {
            for (int r=0; r<4; r++)
            {
                for(int c=0; c<4; c++)
                {
                    if (!revealed[r, c])
                    {
                        return false;
                    }
                }
            }
            return true;
        }//Allmatched
    }
}
