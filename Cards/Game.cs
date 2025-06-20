using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Cards
{
    class Game
    {
        private Board GameBoard;

        public void Start()
        {
            TypeText("=== Memory Cards Game ===");
            //game rules
            TypeText("\nRules for card selection:");
            Console.WriteLine("- The grid rows and columns are labelled based on your selected difficulty.");
            Console.WriteLine("- e.g Medium rows will be labelled 1-4 and columns, 1-4");
            Console.WriteLine("- To select a card, enter the Row(R) and Column(C) number in the format RC");
            Console.WriteLine("- e.g The 2nd card on the 3 row should be choosen as 32");
            TypeText("\nHave fun!");
            Thread.Sleep(1000);
            //choose to play the game
            bool choice = true;
            SetDifficulty(ref choice, out int rows, out int cols);
            //Game loop
            while (choice)
            {
                //create a new instance of the game
                GameBoard = new Board(rows, cols);
                //while all the cards have not been revealed yet
                while (!GameBoard.AllRevealed())
                {
                    //Display the game board
                    GameBoard.DisplayBoard();
                    //the cordinates of the first card
                    Console.Write("Pick the first card (RC): ");
                    (int r1, int c1) = ReadPosition();
                    while (GameBoard.Grid[r1, c1].Revealed) //check if the card is revealed
                    {
                        Console.WriteLine("The card you are trying to selcted has already been revealed.");
                        Console.Write("Try another one: ");
                        //if the card was already open, take in different coordinates
                        (r1, c1) = ReadPosition();
                    }
                    //The card selected was closed
                    //Reveal the card and display the board
                    GameBoard.Grid[r1, c1].Revealed = true;
                    GameBoard.DisplayBoard();
                    //the cordinates of the second card
                    Console.Write("Pick the second card (RC): ");
                    (int r2, int c2) = ReadPosition();
                    while (GameBoard.Grid[r2, c2].Revealed || (r1 == r2 && c1 == c2)) //check if the card is revealed or not the same as the first one
                    {
                        Console.WriteLine("The card you are trying to select has already been revealed.");
                        Console.Write("Try another one: ");
                        //if the card was already open or is a duplicate of the first one, take in different coordinates
                        (r2, c2) = ReadPosition();
                    }
                    //The selected card was closed and not a duplicate
                    //Reveal and display the board
                    GameBoard.Grid[r2, c2].Revealed = true;
                    GameBoard.DisplayBoard();
                    Thread.Sleep(1000);
                    //check if they match
                    if (GameBoard.Grid[r1, c1].Value == GameBoard.Grid[r2, c2].Value)
                    {
                        Console.WriteLine("It's a match!!!");
                        Thread.Sleep(2000);
                    }
                    else //if they don't match, close, pause and display the board
                    {
                        Console.WriteLine("It's not a match!!!");
                        GameBoard.Grid[r1, c1].Revealed = false;
                        GameBoard.Grid[r2, c2].Revealed = false;
                        Thread.Sleep(2000);
                        GameBoard.DisplayBoard();
                    }
                }//while loop
                //If all cards have been revealed, stop the loopand display congratulations message
                Console.WriteLine("Congrats!! You've matched all pairs.");
                //check for difficulty or exit the game.
                SetDifficulty(ref choice, out rows, out cols);
            }//Game loop
            Console.Write("Press any key to close...");
            Console.ReadKey();
        }//Start


        private void SetDifficulty(ref bool _continue, out int _rows, out int _cols)
        {
            //_continue is set to true for all cases except for the Exit option
            //The loop only breaks (return;) when a valid option id choosen
            while (true)
            {
                Console.WriteLine();
                Console.Write("1. Easy (2x2 grid)" +
                    "\n2. Medium (4x4 grid)" +
                    "\n3. Hard (6x6 grid)" +
                    "\n4. Exit" +
                    "\n\nChoose an option: ");
                switch (Console.ReadLine())
                {
                    case "1":
                        _rows = _cols = 2;
                        _continue = true;
                        return;
                    case "2":
                        _rows = _cols = 4;
                        _continue = true;
                        return;
                    case "3":
                        _rows = _cols = 6;
                        _continue = true;
                        return;
                    case "4":
                        _rows = _cols = 0;
                        _continue = false;
                        return;
                    default:
                        Console.WriteLine("Invalid option. Try again: ");
                        break;
                }
            }
        }//SetDifficulty

        private (int row, int col) ReadPosition()
        {
            int r, c;
            string Parts = Console.ReadLine().Trim();
            while (true)
            {
                //if the input is 2 characters long, both are numbers and fall withing the range of the grid
                if (Parts.Length == 2 && int.TryParse(Parts[0].ToString(), out r) && r >= 1 && r <= GameBoard.Rows
                    && int.TryParse(Parts[1].ToString(), out c) && c >= 1 && c <= GameBoard.Cols)
                {
                    //if valid entries are given, exit the loop
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Try again: ");
                }
            }
            //the grid (say Medium) is displayed from 1 - 6, but the list will be indexed 0 - 5
            //so subtract one to return the actual list index to be used
            return (r-1, c-1);
        }

        private void TypeText(string _Text)
        {
            //print out the text with a delay, one character at a time
            foreach (char c in _Text)
            {
                Console.Write(c);
                Thread.Sleep(50);
            }
            Console.WriteLine();
        }
    }
}
