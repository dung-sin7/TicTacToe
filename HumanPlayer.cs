public class HumanPlayer : Player
{

    public HumanPlayer(Mark symbol, Board board, string name) : base(symbol, board)
    {
        this.name = name;
    }

    public override int[]? GetMove()
    {
        int row, col;
        do
        {
            if (board.IsFull()) return null;
            try
            {
                Console.WriteLine($"Player {name} with {symbol}, enter your move");
                Console.Write($"Enter the row of the cell you want to mark (1-{board.Size}): ");
                row = int.Parse(Console.ReadLine());
                Console.Write($"Enter the column of the cell you want to mark (1-{board.Size}): ");
                col = int.Parse(Console.ReadLine());
                if (row <= 0 || row > board.Size || col <= 0 || col > board.Size)
                {
                    throw new ArgumentOutOfRangeException($"Row and column must be between 1 and {board.Size}.");
                }

                if (!board.IsEmpty(row - 1, col - 1))
                {
                    throw new ArgumentException("This cell has already been marked. Please choose another cell.");
                }
                break;
            }
            catch (FormatException)
            {
                Console.WriteLine($"Invalid input. Please enter a number between 1 and {board.Size}.");
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
        Console.WriteLine($"{this.name} player {symbol} chooses row {row}, col {col}");
        return new int[] { row - 1, col - 1 };
    }
}