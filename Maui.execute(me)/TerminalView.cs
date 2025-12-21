using SkiaSharp;
using SkiaSharp.Views.Maui.Controls;
using SkiaSharp.Views.Maui;

namespace World.Execute;

public sealed class TerminalView : SKCanvasView
{
    private readonly TerminalBuffer _buffer;
    private readonly SKTypeface _typeface;

    private readonly SKPaint _fgPaint;
    private readonly SKPaint _bgPaint;

    private readonly int _glyphW = 8;
    private readonly int _glyphH = 16;


    private readonly float _ascent;

    public TerminalView(TerminalBuffer buffer, SKTypeface typeface)
    {
        _buffer = buffer;
        _typeface = typeface;

        EnableTouchEvents = false;
        IgnorePixelScaling = true;

        _fgPaint = new SKPaint
        {
            Typeface = _typeface,
            IsAntialias = false,
            FilterQuality = SKFilterQuality.None
        };

        _bgPaint = new SKPaint
        {
            Style = SKPaintStyle.Fill
        };

        PaintSurface += OnPaintSurface;
    }


    private void OnPaintSurface(object? sender, SKPaintSurfaceEventArgs e)
    {
        var canvas = e.Surface.Canvas;
        canvas.Clear(SKColors.Black);

        int cols = _buffer.Cols;
        int rows = _buffer.Rows;

        const float Border = 32f;
        const float baseGlyphW = 8f;
        const float baseGlyphH = 16f;

        float availW = e.Info.Width - Border * 2;
        float availH = e.Info.Height - Border * 2;
        if (availW <= 0 || availH <= 0)
            return;

        // ---- YOUR SCALING LOGIC ----
        float cellW = availW / cols;
        float cellH = availH / rows;

        float rawScale = MathF.Min(cellW / 8f, cellH / 16f);
        float scale = MathF.Round(rawScale * 4f) / 4f; // quarter-pixel steps
        scale = MathF.Max(0.25f, scale); // safety
                                         // ----------------------------

        float glyphW = baseGlyphW * scale;
        float glyphH = baseGlyphH * scale;

        float termW = cols * glyphW;
        float termH = rows * glyphH;

        // Center inside bordered area
        float offsetX = Border + (availW - termW) * 0.5f;
        float offsetY = Border + (availH - termH) * 0.5f;

        canvas.Translate(offsetX, offsetY);

        // Font setup
        _fgPaint.TextSize = glyphH;
        var fm = _fgPaint.FontMetrics;
        float ascent = -fm.Ascent;

        for (int y = 0; y < rows; y++)
        {
            float py = y * glyphH;

            for (int x = 0; x < cols; x++)
            {
                float px = x * glyphW;
                ref readonly var cell = ref _buffer[y, x];

                _bgPaint.Color = cell.Bg.ToSKColor();
                canvas.DrawRect(px, py, glyphW, glyphH, _bgPaint);

                if (cell.Char != '\0' && cell.Char != ' ')
                {
                    _fgPaint.Color = cell.Fg.ToSKColor();
                    canvas.DrawText(
                        cell.Char.ToString(),
                        px,
                        py + ascent,
                        _fgPaint);
                }
            }
        }
    }



}
