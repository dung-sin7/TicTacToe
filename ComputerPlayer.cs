public class ComputerPlayer : Player
{
    public ComputerPlayer(Mark symbol, Board board) : base(symbol, board)
    {
        this.name = "Computer";
    }

    public override int[]? GetMove()
    {
        Random rand = new Random();

        int row, col;
        do
        {
            if (board.IsFull()) return null;
            row = rand.Next(0, board.Size);
            col = rand.Next(0, board.Size);
        } while (!board.IsEmpty(row, col));

        Console.WriteLine($"{this.name} player {symbol} chooses row {row + 1}, col {col + 1}");

        return new int[] { row, col };
    }
}