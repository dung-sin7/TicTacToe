
using System.Text.Json;

[Serializable]
class Game
{
    private Board board { get; set; }
    private Player player1 { get; set; }
    private Player player2 { get; set; }

    private int numGames { get; set; }
    private int numPlayer1Wins { get; set; }
    private int numPlayer2Wins { get; set; }
    private int numDraws { get; set; }

    public Game()
    {
        board = new Board(3);
        numGames = 0;
        numPlayer1Wins = 0;
        numPlayer2Wins = 0;
        numDraws = 0;
    }

    private void GetMoveAndPlace(Player player)
    {
        int[]? move = player.GetMove();
        if (move == null) return;
        board.PlaceMarker(move[0], move[1], player.Symbol);
    }

    private bool CheckWinOrDraw(Player player, int numPlayerWins, int numDraws)
    {
        if (board.CheckForWin(player.Symbol))
        {
            Console.WriteLine($"{player.Name} {player.Symbol} wins!");
            numPlayerWins++;
            return true;
        }
        if (board.IsFull())
        {
            Console.WriteLine("Draw!");
            numDraws++;
            return true;
        }
        return false;
    }

    public void StartGame()
    {
        Mode mode;
        do
        {
            try
            {
                Console.WriteLine($"1: PVP");
                Console.WriteLine($"2: PVE");
                Console.WriteLine($"3: Watch");
                Console.WriteLine($"Choose Mode:");
                int choose = int.Parse(Console.ReadLine());
                if (choose <= 0 || choose > 3)
                {
                    throw new ArgumentOutOfRangeException($"Choose must be between 1 and 3.");
                }
                switch (choose)
                {
                    case 1:
                        mode = Mode.PVP;
                        break;
                    case 2:
                        mode = Mode.PVE;
                        break;
                    case 3:
                        mode = Mode.WATCH;
                        break;
                    default:
                        mode = Mode.WATCH;
                        break;
                }
                break;
            }
            catch (FormatException)
            {
                Console.WriteLine($"Invalid input. Please enter a number between 1 and 3.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        } while (true);
        this.Play(mode);
    }

    public string InputName(int numPlayer)
    {
        string name;
        do
        {
            try
            {
                Console.Write($"Enter name for Player {numPlayer} ({Board.MarkToChar(Mark.O)}): ");
                name = Console.ReadLine();
                if (String.IsNullOrEmpty(name))
                {
                    throw new ArgumentOutOfRangeException($"Name must be fill.");
                }
                break;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        } while (true);
        return name;
    }
    public void Play(Mode mode)
    {
        // Create human player
        string name1, name2;

        switch (mode)
        {
            case Mode.PVP:
                name1 = InputName(1);
                name2 = InputName(2);
                player1 = new HumanPlayer(Mark.X, board, name1);
                player2 = new HumanPlayer(Mark.O, board, name2);
                break;
            case Mode.PVE:
                name1 = InputName(1);
                player1 = new HumanPlayer(Mark.X, board, name1);
                player2 = new ComputerPlayer(Mark.O, board);
                break;
            case Mode.WATCH:
                player1 = new ComputerPlayer(Mark.X, board);
                player2 = new ComputerPlayer(Mark.O, board);
                break;
            default:
                player1 = new ComputerPlayer(Mark.X, board);
                player2 = new ComputerPlayer(Mark.O, board);
                break;
        }


        board.DrawBoard();
        while (true)
        {
            // Get move from player 1
            this.GetMoveAndPlace(player1);
            board.DrawBoard();
            if (this.CheckWinOrDraw(player1, numPlayer1Wins, numDraws)) break;

            this.GetMoveAndPlace(player2);
            board.DrawBoard();
            if (this.CheckWinOrDraw(player2, numPlayer2Wins, numDraws)) break;
        }
        numGames++;
    }

}