using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards
{
    public class Board
    {
        public int Rows { get; }
        public int Cols { get; }
        public Card[,] Grid { get; }

        public Board(int rows, int cols)
        {
            Rows = rows;
            Cols = cols;
            Grid = new Card[Rows, Cols];
            Initialize();
        }

        private void Initialize()
        {
            int NumOfCards = Rows * Cols;
            List<string> cards = new List<string>();
            //populate symbols based on the number of pairs, and duplicate at once
            for (char c='A'; cards.Count < NumOfCards; c++)
            {
                cards.Add(c.ToString());
                cards.Add(c.ToString());
            }
            //shuffle the cards
            Random Rand = new Random();
            for (int i=0; i< cards.Count; i++)
            {
                int r = Rand.Next(i, cards.Count);
                string temp = cards[i];
                cards[i] = cards[r];
                cards[r] = temp;
            }
            //Fill the board
            int index = 0;
            for(int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Cols; c++)
                {
                    Grid[r, c] = new Card(cards[index]);
                    index++;
                }
            }
        }//Initialize
        public void DisplayBoard()
        {
            Console.Clear();
            //top of board showing column indexes
            Console.Write("   ");
            for (int c = 1; c <= Cols; c++)
            {
                Console.Write($" {c}  ");
            }
            Console.WriteLine();
            //line beneath grid header
            Console.Write("  +");
            for (int c = 0; c < Cols; c++)
            {
                Console.Write("---+");
            }
            Console.WriteLine();
            //each individual row
            for (int r = 0; r < Rows; r++)
            {
                Console.Write($"{r+1} |");
                for (int c = 0; c < Cols; c++)
                {
                    //if the value is revealed, display it, else, display an * in it's place
                    Console.Write(Grid[r, c].Revealed ? $" {Grid[r, c].Value} |" : " * |");
                }
                Console.WriteLine();
                //line beneath each row
                Console.Write("  +");
                for (int c = 0; c < Cols; c++)
                {
                    Console.Write("---+");
                }
                Console.WriteLine();
            }
        }//DisplayBoard
        public bool AllRevealed()
        {
            foreach(var card in Grid)
            {
                if (!card.Revealed)
                {
                    return false;
                }
            }
            return true;
        }//Allrevealed
    }
}
