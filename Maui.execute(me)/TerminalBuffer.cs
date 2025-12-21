using Microsoft.Maui.Graphics;

namespace World.Execute;

public struct TerminalCell
{
    public char Char;
    public Color Fg;
    public Color Bg;
    public bool Bold;
}

public sealed class TerminalBuffer
{
    public int Cols { get; }
    public int Rows { get; }

    private readonly TerminalCell[,] _cells;

    public TerminalBuffer(int cols, int rows)
    {
        Cols = cols;
        Rows = rows;
        _cells = new TerminalCell[Rows, Cols];
        Clear();
    }

    public ref TerminalCell this[int row, int col] => ref _cells[row, col];

    // -----------------------------
    // Clear helpers
    // -----------------------------

    public void Clear()
    {
        for (int y = 0; y < Rows; y++)
            ClearRow(y);
    }

    public void ClearRow(int row)
    {
        for (int x = 0; x < Cols; x++)
        {
            _cells[row, x] = new TerminalCell
            {
                Char = ' ',
                Fg = Colors.White,
                Bg = Colors.Black,
                Bold = false
            };
        }
    }

    // -----------------------------
    // Scrolling
    // -----------------------------

    /// <summary>
    /// Scrolls the buffer up by one line.
    /// </summary>
    public void ScrollUp()
        => ScrollUp(1);

    /// <summary>
    /// Scrolls the buffer up by N lines.
    /// </summary>
    public void ScrollUp(int lines)
    {
        if (lines <= 0)
            return;

        if (lines >= Rows)
        {
            Clear();
            return;
        }

        for (int y = lines; y < Rows; y++)
        {
            for (int x = 0; x < Cols; x++)
            {
                _cells[y - lines, x] = _cells[y, x];
            }
        }

        for (int y = Rows - lines; y < Rows; y++)
            ClearRow(y);
    }
}
