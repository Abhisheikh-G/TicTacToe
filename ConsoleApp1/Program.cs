using System;

namespace ConsoleApp1
{
    internal class Program
    {

        static void Main(string[] args)
        {
            TicTacToe newGame = new TicTacToe();
            newGame.playGame();
        }

    }



    public class TicTacToe
    {
        string[,] gameBoard = new string[3, 3];

        public void playGame()
        {
            Console.WriteLine("Welcome to Tic-Tac-Toe!");
            Console.WriteLine("_______________________");
            for (int i = 0; i < 20; i++)
            {
                bool validCoordinates = false;
                SelectionCoordinates selectedCoords = PromptUser(i % 2 == 0 ? 0 : 1);

                while (!validCoordinates)
                {
                    if (gameBoard[selectedCoords.x, selectedCoords.y] == null)
                    {
                        gameBoard[selectedCoords.x, selectedCoords.y] = i % 2 == 0 ? "X" : "O";
                        validCoordinates = true;
                    }
                    else
                    {
                        Console.WriteLine("That space has already been selected.");
                        selectedCoords = PromptUser(i % 2 == 0 ? 0 : 1);
                    }
                }
                if (CheckWinner(i % 2 == 0 ? "X" : "O")) break;
            }

        }

        private Boolean CheckWinner(string selection)
        {
            string playerLabel = selection == "X" ? "Player 1" : "Player 2";
            bool isDiagonal = false, isHorizontal = false, isVertical = false;
            int diagonalCount = 0, verticalCount = 0, horizontalCount = 0;
            for (int i = 0, j = 2; i < gameBoard.GetLength(0); i++, j--)
            {


                if (gameBoard[i, j] == selection || gameBoard[i, i] == selection)
                {
                    diagonalCount++;
                    isDiagonal = diagonalCount == 3;
                }
                else
                {
                    diagonalCount = 0;
                }


                for (int k = 0; k < gameBoard.GetLength(1); k++)
                {

                    if (gameBoard[i, k] == selection.ToString())
                    {
                        horizontalCount++;
                        isHorizontal = horizontalCount == 3;
                    }
                    else
                    {
                        horizontalCount = 0;
                    }


                    if (gameBoard[k, i] == selection.ToString())
                    {
                        verticalCount++;
                        isVertical = verticalCount == 3;
                    }
                    else
                    {
                        verticalCount = 0;
                    }

                }
            }

            if (isDiagonal || isHorizontal || isVertical)
            {
                Console.WriteLine();
                Console.WriteLine("Congratulations on winning {0}", playerLabel);
                displayGameBoard();
                return true;
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("No winner yet..");
                return false;
            }
        }

        private SelectionCoordinates PromptUser(int currentPlayer)
        {
            bool validRowSelection = false;
            string rowSelection, columnSelection;
            int parsedRowSelection, parsedColumnSelection;
            bool parsedRowSuccess = false, parsedColumnSuccess = false;
            string playerName = currentPlayer % 2 == 0 ? "Player One (X)" : "Player Two (O)";
            Console.WriteLine("{0}, please choose a space on the board.", playerName);
            displayGameBoard();
            while (!validRowSelection)
            {
                Console.WriteLine("Please select a row between 0 - 2.");
                rowSelection = Console.ReadLine();
                parsedRowSuccess = int.TryParse(rowSelection, out parsedRowSelection);

                if (parsedRowSuccess && parsedRowSelection <= 2 && parsedRowSelection >= 0)
                {
                    validRowSelection = true;
                    Console.WriteLine("You selected row {0}, now please select a column between 0 - 2.", parsedRowSelection);
                    columnSelection = Console.ReadLine();
                    parsedColumnSuccess = int.TryParse(columnSelection, out parsedColumnSelection);
                    if (parsedColumnSuccess && parsedColumnSelection <= 2 && parsedColumnSelection >= 0)
                    {

                        Console.WriteLine("You selected row {0} and column {1}", parsedRowSelection, parsedColumnSelection);
                        return new SelectionCoordinates(parsedRowSelection, parsedColumnSelection);
                    }
                    else
                    {
                        parsedColumnSuccess = false;
                        while (!parsedColumnSuccess || parsedColumnSelection > 2 || parsedColumnSelection < 0)
                        {
                            Console.WriteLine("Please select a column between 0 - 2.");
                            columnSelection = Console.ReadLine();
                            parsedColumnSuccess = int.TryParse(columnSelection, out parsedColumnSelection);
                        }

                    }
                }
                else
                {
                    parsedRowSuccess = false;
                    while (!parsedRowSuccess)
                    {
                        Console.WriteLine("Please select a row between 0 - 2.");
                        rowSelection = Console.ReadLine();
                        parsedRowSuccess = int.TryParse(rowSelection, out parsedRowSelection);
                    }

                }
            }
            return new SelectionCoordinates(0, 0);

        }

        private void displayGameBoard()
        {
            Console.WriteLine();
            for (int i = 0; i < 4; i++)
            {
                Console.Write("{0}", i == 0 ? "\t" : "Col " + (i - 1) + " \t");
            }
            Console.WriteLine();
            for (int i = 0; i < gameBoard.GetLength(0); i++)
            {
                Console.Write("Row {0}:  ", i);
                for (int j = 0; j < gameBoard.GetLength(1); j++)
                {

                    if (gameBoard[i, j] != null)
                    {
                        Console.Write("{0}\t", gameBoard[i, j]);
                    }
                    else
                    {
                        Console.Write("[{0}, {1}]  ", i, j);
                    }
                }
                Console.WriteLine();
            }
        }

        public class SelectionCoordinates
        {
            public int x { get; set; }
            public int y { get; set; }
            public SelectionCoordinates(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }
    }
}