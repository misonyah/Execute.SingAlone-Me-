namespace World.Execute;

// NOTE: no tab support yet

public sealed class AnsiParser
{
    private Color _fg = Colors.White;
    private Color _bg = Colors.Black;

    public void Write(string text, TerminalBuffer buffer, ref int cx, ref int cy)
    {
        for (int i = 0; i < text.Length; i++)
        {
            char ch = text[i];

            // ANSI escape
            if (ch == '\x1b')
            {
                i = HandleEscape(text, i);
                continue;
            }

            // Newline
            if (ch == '\n')
            {
                cx = 0;
                cy++;
                EnsureCursorVisible(buffer, ref cy);
                continue;
            }

            // Carriage return
            if (ch == '\r')
            {
                cx = 0;
                continue;
            }

            if (ch == '\t')
            {
                cx = (cx + 8) & ~7; // next multiple of 8
                if (cx >= buffer.Cols)
                {
                    cx = 0;
                    cy++;
                    EnsureCursorVisible(buffer, ref cy);
                }
                continue;
            }

            // Line wrap
            if (cx >= buffer.Cols)
            {
                cx = 0;
                cy++;
                EnsureCursorVisible(buffer, ref cy);
            }

            ref var cell = ref buffer[cy, cx++];
            cell.Char = ch;
            cell.Fg = _fg;
            cell.Bg = _bg;
        }
    }

    // ---------------- ANSI ----------------

    private int HandleEscape(string s, int i)
    {
        // Expect ESC[
        if (i + 1 >= s.Length || s[i + 1] != '[')
            return i;

        int p = i + 2;
        int start = p;

        while (p < s.Length)
        {
            char c = s[p];

            if (c == 'm')
            {
                ParseSgr(s.AsSpan(start, p - start));
                return p; // <-- p points to 'm', next loop i++ skips past it
            }

            if ((c < '0' || c > '9') && c != ';')
                return p; // <-- skip invalid char

            p++;
        }

        return p - 1; // <-- if we reach end, move i to last processed
    }


    private void ParseSgr(ReadOnlySpan<char> parameters)
    {
        if (parameters.Length == 0)
        {
            Reset();
            return;
        }

        int value = 0;
        bool hasValue = false;

        for (int i = 0; i <= parameters.Length; i++)
        {
            if (i == parameters.Length || parameters[i] == ';')
            {
                ApplySgrCode(hasValue ? value : 0);
                value = 0;
                hasValue = false;
                continue;
            }

            value = value * 10 + (parameters[i] - '0');
            hasValue = true;
        }
    }

    private void ApplySgrCode(int code)
    {
        switch (code)
        {
            case 0: Reset(); break;

            // Foreground
            case 30: _fg = Colors.Black; break;
            case 31: _fg = Colors.Red; break;
            case 32: _fg = Colors.Lime; break;
            case 33: _fg = Colors.Yellow; break;
            case 34: _fg = Colors.Blue; break;
            case 35: _fg = Colors.Magenta; break;
            case 36: _fg = Colors.Cyan; break;
            case 37: _fg = Colors.White; break;

            // Background
            case 40: _bg = Colors.Black; break;
            case 41: _bg = Colors.Red; break;
            case 42: _bg = Colors.Lime; break;
            case 43: _bg = Colors.Yellow; break;
            case 44: _bg = Colors.Blue; break;
            case 45: _bg = Colors.Magenta; break;
            case 46: _bg = Colors.Cyan; break;
            case 47: _bg = Colors.White; break;

                // Unsupported → ignore
        }
    }

    private void Reset()
    {
        _fg = Colors.White;
        _bg = Colors.Black;
    }

    // ---------------- Scrolling ----------------

    private static void EnsureCursorVisible(TerminalBuffer buffer, ref int cy)
    {
        if (cy < buffer.Rows)
            return;

        buffer.ScrollUp(1);
        cy = buffer.Rows - 1;
    }
}
