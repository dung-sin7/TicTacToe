public class Board
{
    private Mark[,] board;
    private int size;
    public int Size => size;
    // Constructor
    public Board(int size)
    {
        this.size = size;
        board = new Mark[size, size];
        // Initialize board with empty spaces
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                board[i, j] = Mark.Empty;
            }
        }
    }

    public static char MarkToChar(Mark mark)
    {
        switch (mark)
        {
            case Mark.Empty:
                return ' ';
            case Mark.X:
                return 'X';
            case Mark.O:
                return 'O';
            default:
                return ' ';
        }
    }
    public void DrawBoard()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                Console.Write($"| {MarkToChar(board[i, j])} ");
            }
            Console.WriteLine("|");
            if (i == size - 1) return;
            for (int j = 0; j < size; j++)
            {
                Console.Write($"----");
            }
            Console.WriteLine($"-");
        }
    }

    public bool IsFull()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (board[i, j] == Mark.Empty)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public bool IsEmpty(int row, int col)
    {
        return board[row, col] == Mark.Empty;
    }

    public void PlaceMarker(int row, int col, Mark marker)
    {
        board[row, col] = marker;
    }

    public bool CheckForWin(Mark marker)
    {
        // Check rows
        for (int i = 0; i < size; i++)
        {
            bool rowWin = true;

            for (int j = 0; j < size; j++)
            {
                if (board[i, j] != marker)
                {
                    rowWin = false;
                    break;
                }
            }

            if (rowWin)
            {
                return true;
            }
        }

        // Check columns
        for (int i = 0; i < size; i++)
        {
            bool colWin = true;

            for (int j = 0; j < size; j++)
            {
                if (board[j, i] != marker)
                {
                    colWin = false;
                    break;
                }
            }

            if (colWin)
            {
                return true;
            }
        }

        // Check diagonals
        bool diagWin1 = true;
        bool diagWin2 = true;

        for (int i = 0; i < size; i++)
        {
            if (board[i, i] != marker)
            {
                diagWin1 = false;
            }

            if (board[i, size - i - 1] != marker)
            {
                diagWin2 = false;
            }
        }

        return diagWin1 || diagWin2;
    }
}