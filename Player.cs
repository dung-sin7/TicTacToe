public abstract class Player
{
    protected Mark symbol;
    public Mark Symbol => symbol;
    protected Board board;
    protected string name = "Unknow";
    public string Name => name;
    public Player(Mark symbol, Board board)
    {
        this.symbol = symbol;
        this.board = board;
    }

    public abstract int[]? GetMove();
}