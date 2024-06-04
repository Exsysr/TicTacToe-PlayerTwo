using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Dynamic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    internal class DB
    {
        string connectionString = "datasource=localhost;port=3306;username=root;password=;database=tictactoe";

        public List<Turn> GetTurn()
        {
            List<Turn> turns = new List<Turn>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM `playermove`\r\nWHERE ID = (\r\nSELECT MAX(ID) FROM `playermove`)", conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Turn turn = new Turn();
                        turn.ID = reader.GetInt32(0);
                        turn.playerone = reader.GetInt32(1);
                        turn.playertwo = reader.GetInt32(2);
                        turns.Add(turn);
                    }
                }
            }
            return turns;
        }

        public List<Turn> SetTurn(Turn turn)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("INSERT INTO playermove (`PlayerOne`, `PlayerTwo`) VALUES (@playerOne, @PlayerTwo)", conn);

            cmd.Parameters.AddWithValue("@PlayerOne", turn.playerone);
            cmd.Parameters.AddWithValue("@PlayerTwo", turn.playertwo);

            cmd.ExecuteNonQuery();
            conn.Close();
            return GetTurn();
        }

        public List<Move> MoveFirst()
        {
            List<Move> moves = new List<Move>();
            GetTurn();
            List<Turn> turns = GetTurn();
            Turn trns = new Turn();
            trns.playerone = turns[0].playerone;
            trns.playertwo = turns[0].playertwo;
            int p1 = trns.playerone;
            int p2 = trns.playertwo;

            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();

            if (p1 == p2)
            {
                Random random = new Random();
                p1 = random.Next(1, 6);
                p2 = random.Next(1, 6);
                MySqlCommand cmd = new MySqlCommand("INSERT INTO `playermove` (`PlayerOne`, `PlayerTwo`) VALUES (@p1, @p2)", conn);
                cmd.Parameters.AddWithValue("@p1", p1);
                cmd.Parameters.AddWithValue("@p2", p2);
                cmd.ExecuteNonQuery();
                conn.Close();
                return MoveFirst();
            }
            else if (p1 > p2)
            {
                MySqlCommand cmd = new MySqlCommand("INSERT INTO `moves` (`P1`, `P2`) VALUES (1, 0)", conn);
                cmd.ExecuteNonQuery();
            }
            else if (p1 < p2)
            {
                MySqlCommand cmd = new MySqlCommand("INSERT INTO `moves` (`P1`, `P2`) VALUES (0, 1)", conn);
                cmd.ExecuteNonQuery();
            }

            conn.Close();
            return moves;
        }

        public List<Move> GetFirstMove()
        {
            List<Move> moves = new List<Move>();
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM `moves` WHERE ID = (SELECT MAX(ID) FROM `moves`)", conn);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Move move = new Move();
                    move.ID = reader.GetInt32(0);
                    move.move1 = reader.GetBoolean(1);
                    move.move2 = reader.GetBoolean(2);
                    moves.Add(move);
                }
            }
            conn.Close();
            return moves;
        }

        public List<Player> SetPlayerMove(int x, int y)
        {
            List<Player> players = new List<Player>();
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("INSERT INTO `playertwo` (`X`, `Y`) VALUES (@x, @y)", conn);
            cmd.Parameters.AddWithValue("@x", x);
            cmd.Parameters.AddWithValue("@y", y);
            cmd.ExecuteNonQuery();
            conn.Close();
            return players;
        }

        public List<Player> GetPlayerMove()
        {
            List<Player> players = new List<Player>();
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM `playerone` WHERE ID = (SELECT MAX(ID) FROM `playerone`)", conn);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Player player = new Player();
                    player.ID = reader.GetInt32(0);
                    player.X = reader.GetInt32(1);
                    player.Y = reader.GetInt32(2);
                    players.Add(player);
                }
            }
            conn.Close();
            return players;
        }

        public List<PlayerTurn> GetPlayerTurn()
        {
            List<PlayerTurn> playerTurns = new List<PlayerTurn>();
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM `turn` WHERE ID = (SELECT MAX(ID) FROM `turn`)", conn);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    PlayerTurn playerTurn = new PlayerTurn();
                    playerTurn.ID = reader.GetInt32(0);
                    playerTurn.playerone = reader.GetBoolean(1);
                    playerTurn.playertwo = reader.GetBoolean(2);
                    playerTurns.Add(playerTurn);
                }
            }
            conn.Close();
            return playerTurns;
        }

        public List<PlayerTurn> SetPlayerTurn(bool a, bool b)
        {
            List<PlayerTurn> playerTurns = new List<PlayerTurn>();
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("INSERT INTO `turn` (`P1T`, `P2T`) VALUES (@playerOne, @PlayerTwo)", conn);
            cmd.Parameters.AddWithValue("@PlayerOne", a);
            cmd.Parameters.AddWithValue("@PlayerTwo", b);
            cmd.ExecuteNonQuery();
            conn.Close();
            return playerTurns;
        }
    }
}
