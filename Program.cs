namespace HelloWorld;
class Program
{
    static void Main(string[] args)
    {
        Game game = new Game();
        game.Start();
    }

    public class Game
    {
        private Board board;
        private char currentPlayer;

        public Game()
        {
            this.currentPlayer = 'X'; 
            this.board = new Board();
        }

        public void Start()
        {
            string status = "Select a square. ";
            while(!IsGameOver())
            {
                Console.WriteLine($"It is {this.currentPlayer}'s turn.\n");
                this.board.Display();
                Console.Write(status);
                int square = Convert.ToInt32(Console.ReadLine());
                bool canPlace = this.board.Place(this.currentPlayer, square);
                status = !canPlace ? "This square is occupied. Select a different square. " : "Select a square. ";
                if (canPlace) SwitchPlayer();
            }
        }

        private bool IsGameOver()
        {
            Console.Clear();
            if (this.board.IsDraw())
            {
                Console.WriteLine("This game is a draw.\n");
                this.board.Display();;
                return true;
            }
            if (this.board.IsWinner('X'))
            {
                Console.WriteLine("Player X has won.\n");
                this.board.Display();
                return true;
            }
            if (this.board.IsWinner('O'))
            {
                Console.WriteLine("Player O has won.\n");
                this.board.Display();
                return true;
            }
            return false;
        }

        private void SwitchPlayer()
        {
            if (this.currentPlayer == 'X')
            {
                this.currentPlayer = 'O';
            }
            else if (this.currentPlayer == 'O')
            {
                this.currentPlayer = 'X';
            }
        }
    }

    public class Board
    {
        private int currentNumTurns = 1; 
        private char[,] board = new char[3,3]
        {
            { '7', '8', '9' },
            { '4', '5', '6' },
            { '1', '2', '3' }
        };

        public void Display()
        {
            for(int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    Console.Write($" {board[row, col]} ");
                    if (col + 1 < board.GetLength(1)) Console.Write("|");
                }
                if (row + 1 < board.GetLength(0)) Console.WriteLine("\n---+---+---");
            }
            Console.Write("\n\n");
        }

        public bool Place(char player, int square)
        {
            (int x, int y) point = square switch
            { 
                1 => (2, 0), 
                2 => (2, 1),
                3 => (2, 2),
                4 => (1, 0),
                5 => (1, 1),
                6 => (1, 2),
                7 => (0, 0),
                8 => (0, 1),
                9 => (0, 2),
                _ => (0, 0)
            };
            if (this.board[point.x, point.y] == 'X' || this.board[point.x, point.y] == 'O') return false;
            this.board[point.x, point. y] = player;
            this.currentNumTurns++;
            return true;
        }

        public bool IsWinner(char player)
        {
            // Check rows
            if (this.board[0, 0] == player && this.board[0, 1] == player && this.board[0, 2] == player) return true;
            if (this.board[1, 0] == player && this.board[1, 1] == player && this.board[1, 2] == player) return true;
            if (this.board[2, 0] == player && this.board[2, 1] == player && this.board[2, 2] == player) return true;

            // Check columns
            if (this.board[0, 0] == player && this.board[1, 0] == player && this.board[2, 0] == player) return true;
            if (this.board[0, 1] == player && this.board[1, 1] == player && this.board[2, 1] == player) return true;
            if (this.board[0, 2] == player && this.board[1, 2] == player && this.board[2, 2] == player) return true;

            // Check diagonals
            if (this.board[0, 0] == player && this.board[1, 1] == player && this.board[2, 2] == player) return true;
            if (this.board[0, 2] == player && this.board[1, 1] == player && this.board[2, 0] == player) return true;
            return false;
        }

        public bool IsDraw()
        {
            return this.currentNumTurns > 9;
        }
    }

}
