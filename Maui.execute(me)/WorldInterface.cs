using SkiaSharp;

namespace World.Execute;

public static class WorldInterface
{
    private static AnsiParser? _ansiParser;
    private static TerminalBuffer? _buffer;
    private static TerminalView? _console;

    private static int x = 0, y = 0;

    public static void Create(Window window, SKTypeface font)
    {
        _ansiParser = new AnsiParser();
        _buffer = new TerminalBuffer(120, 40);
        _console = new TerminalView(_buffer, font);

        window.Page = new ContentPage
        {
            Padding = 0,
            BackgroundColor = Colors.Black,
            Content = _console
        };

    }

    // ---- Write API ----

    public static void Write(string text)
    {
        if (_ansiParser == null || _buffer == null) return;

        _ansiParser.Write(text, _buffer, ref x, ref y);
        RequestRedraw();
    }

    public static void Write(char c)
        => Write(c.ToString());

    public static void Write(string format, params object[] args)
        => Write(string.Format(format, args));

    public static void WriteLine(string text = "")
        => Write(text + "\n");

    public static void WriteLine(object obj)
        => WriteLine(obj?.ToString() ?? "");

    public static void WriteLine(string format, params object[] args)
        => WriteLine(string.Format(format, args));

    // ---- Console compatibility ----

    public static void Clear()
    {
        if (_buffer == null) return;

        _buffer.Clear();
        x = 0;
        y = 0;

        RequestRedraw();
    }


    // In MAUI this is intentionally a no-op (kept for legacy compatibility)
    public static void Run()
    {
    }

    // ---- Helpers ----

    private static void RequestRedraw()
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            // _view?.Invalidate();
            _console?.InvalidateSurface();
        });
    }
}
