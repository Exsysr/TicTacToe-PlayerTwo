//PLAYER TWO
using System;

namespace TicTacToe
{
    class Program
    {
        static int[,] board = new int[3, 3];
        static string[,] plane = new string[3, 3];
        static int p1;
        static int p2;
        static bool player1;
        static bool player2;
        static bool Win = false;
        static string? winner;

        static void Main()
        {
            DB db = new DB();
            db.MoveFirst();
            List<Turn> turns = db.GetTurn();
            p1 = turns[0].playerone;
            p2 = turns[0].playertwo;

            Console.WriteLine("Welcome too TickTacToe!!!");
            Thread.Sleep(500);
            Console.WriteLine("Player 1 is X and Player 2 is O!");
            Thread.Sleep(500);
            Console.WriteLine("Lest role a dice to see who goes first!");
            Thread.Sleep(2000);
            Console.WriteLine("Player 1 rolled: " + p1);
            Thread.Sleep(500);
            Console.WriteLine("And...");
            Thread.Sleep(2000);
            Console.WriteLine("Player 2 rolled: " + p2);
            Thread.Sleep(500);

            FirstTurn();
            Playing();
        } 
        static void Playing()
        {
            while (!Win)
            {
                Turn();
                Console.Clear();
                CheckForWin();
            }
            
        } 
        static void DrawBoard()
        {
            for (int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    if (board[i, j] == 0)
                    {
                        plane[i, j] = " ";
                    }
                    else if (board[i, j] == 1)
                    {
                        plane[i, j] = "X";
                    }
                    else if (board[i, j] == 2)
                    {
                        plane[i, j] = "O";
                    }
                }
            }
            Console.WriteLine("   |  1. |  2. |  3. | ");
            Console.WriteLine("___|_____|_____|_____|");
            Console.WriteLine("   |     |     |     | ");
            Console.WriteLine("1. |  {0}  |  {1}  |  {2}  |", plane[0, 0], plane[0, 1], plane[0, 2]);
            Console.WriteLine("___|_____|_____|_____| ");
            Console.WriteLine("   |     |     |     | ");
            Console.WriteLine("2. |  {0}  |  {1}  |  {2}  |", plane[1, 0], plane[1, 1], plane[1, 2]);
            Console.WriteLine("___|_____|_____|_____| ");
            Console.WriteLine("   |     |     |     | ");
            Console.WriteLine("3. |  {0}  |  {1}  |  {2}  |", plane[2, 0], plane[2, 1], plane[2, 2]);
            Console.WriteLine("___|_____|_____|_____| ");
            Console.WriteLine("Player 1 is X and Player 2 is O! \n");
        } 
        static void CheckForWin()
        {
            // Check for horizontal win

            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] == 1 && board[i, 1] == 1 && board[i, 2] == 1)
                {
                    Console.Clear();
                    winner = "Player 1 wins!";
                    Win = true;
                }
                else if (board[i, 0] == 2 && board[i, 1] == 2 && board[i, 2] == 2)
                {
                    Console.Clear();
                    winner ="Player 2 wins!";
                    Win = true;
                }
            }

            // Check for vertical win
            for (int i = 0; i < 3; i++)
            {
                if (board[0, i] == 1 && board[1, i] == 1 && board[2, i] == 1)
                {
                    Console.Clear();
                    winner = "Player 1 wins!";
                    Win = true;
                }
                else if (board[0, i] == 2 && board[1, i] == 2 && board[2, i] == 2)
                {
                    Console.Clear();
                    winner = "Player 2 wins!";
                    Win = true;
                }
            }

            // Check for diagonal win
            if (board[0, 0] == 1 && board[1, 1] == 1 && board[2, 2] == 1)
            {
                Console.Clear();
                winner = "Player 1 wins!";
                Win = true;
            }
            else if (board[0, 0] == 2 && board[1, 1] == 2 && board[2, 2] == 2)
            {
                Console.Clear();
                winner = "Player 2 wins!";
                Win = true;
            }
            if (board[0, 2] == 1 && board[1, 1] == 1 && board[2, 0] == 1)
            {
                Console.Clear();
                winner = "Player 1 wins!";
                Win = true;
            }
            else if (board[0, 2] == 2 && board[1, 1] == 2 && board[2, 0] == 2)
            {
                Console.Clear();
                winner = "Player 2 wins!";
                Win = true;
            }

            // Check for draw

            int count = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] != 0)
                    {
                        count++;
                    }
                }
            }
            if (count == 9)
            {
                Console.Clear();
                winner = "The game is a draw!";
                Win = true;
            }

            if (Win)
            {
                DrawBoard();
                player2 = true;
                Console.WriteLine(winner);
                Console.WriteLine("Game Over!");
                Environment.Exit(0);
            }

        } 
        static void PlayerTurn()
        {
            DB dB = new DB();

            try
            {
                List<Player> players = dB.GetPlayerMove();
                Player player = new Player();
                player.X = players[0].X;
                player.Y = players[0].Y;
                if (board[player.X, player.Y] == 0)
                {
                    board[player.X, player.Y] = 1;
                }
            }
            catch
            {
            }

            DrawBoard();
            int x = 0;
            int y = 0;
            Console.Write("Enter the row number: ");
            try
            {
                x = int.Parse(Console.ReadLine()!) - 1;
                if (x > 2 || x < 0)
                {
                    Console.WriteLine(x + " is not a valid row number. Please enter a number between 0 and 3!");
                    Console.Clear();
                    PlayerTurn();
                }
            }
            catch
            {
                Console.WriteLine("Please enter a valid number!");
                Console.Clear();
                PlayerTurn();   
            }

            Console.Write("Enter the column number: ");
            try
            {
                y = int.Parse(Console.ReadLine()!) - 1;   
                if (y > 2 || y < 0)
                {
                    Console.WriteLine(y + " is not a valid column number. Please enter a number between 0 and 3!");
                    Console.Clear();
                    PlayerTurn();
                }
            }
            catch
            {
                Console.WriteLine("Please enter a valid number!");
                Console.Clear();
                PlayerTurn();
            }
            if (board[x, y] == 0)
            {
                board[x, y] = 2;
            }
            else
            {
                Console.WriteLine($"The spot ({x}, {y}) is already taken!");
                PlayerTurn();
            }
            
            dB.SetPlayerMove(x, y);
            dB.SetPlayerTurn(true, false);
            Playing();
        } 
        static void FirstTurn() 
        {
            DB db = new DB();
            List<Move> moves = db.GetFirstMove();
            player2 = moves[0].move2;
            if (player2)
            {
                Console.Clear();
                Console.WriteLine("Its your turn :) \n");
                Console.WriteLine(moves[0].move2);
                PlayerTurn();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Its not your turn :( \n");
                Console.WriteLine(moves[0].move2);
                DrawBoard();
                Turn();
            }
        } 
        static void Turn()
        {
            DB db = new DB();

            try
            {
                List<PlayerTurn> playerTurns = db.GetPlayerTurn();
                player2 = playerTurns[0].playertwo;
                if (player2)
                {
                    Thread.Sleep(1000);
                    Console.Clear();
                    Console.WriteLine("Its your turn :) \n");
                    PlayerTurn();
                }
                else
                {
                    Thread.Sleep(1000);
                    Console.Clear();
                    Console.WriteLine("Its not your turn :( \n");
                    DrawBoard();
                    while (!player2)
                    {
                        try
                        {
                            List<Player> players = db.GetPlayerMove();
                            Player player = new Player();
                            player.X = players[0].X;
                            player.Y = players[0].Y;
                            if (board[player.X, player.Y] == 0)
                            {
                                board[player.X, player.Y] = 1;
                            }
                        }
                        catch
                        {
                        }
                        List<PlayerTurn> playerTurns2 = db.GetPlayerTurn();
                        player2 = playerTurns2[0].playertwo;
                        CheckForWin();
                    }
                }
            }
            catch
            {
                player2 = false;
                Turn();
            }
        } 
    }
}