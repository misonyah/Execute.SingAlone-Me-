using SkiaSharp;
using SkiaSharp.Views.Maui.Controls;
using SkiaSharp.Views.Maui;
using System.Diagnostics;

namespace World.Execute;

public sealed class TerminalView : SKCanvasView
{
    private readonly TerminalBuffer _buffer;
    private readonly SKTypeface _typeface;

    private readonly SKPaint _fgPaint;
    private readonly SKPaint _bgPaint;

    private readonly SKPaint _scanlinePaint = new()
    {
        Color = new SKColor(0, 0, 0, 48),
        IsAntialias = false
    };

    private readonly SKPaint _decayPaint = new()
    {
        Color = new SKColor(0, 0, 0, 32),
        BlendMode = SKBlendMode.SrcOver
    };

    private readonly SKPaint _bloomPaint;

    private SKSurface? _phosphorSurface;
    private SKImageInfo _phosphorInfo;

    private readonly Stopwatch _clock = Stopwatch.StartNew();

    public TerminalView(TerminalBuffer buffer, SKTypeface typeface)
    {
        _buffer = buffer;
        _typeface = typeface;

        EnableTouchEvents = false;
        IgnorePixelScaling = true;

        _bloomPaint = new SKPaint
        {
            Typeface = _typeface,
            IsAntialias = true,
            FilterQuality = SKFilterQuality.Low,
            MaskFilter = SKMaskFilter.CreateBlur(SKBlurStyle.Normal, 1.5f)
        };

        _fgPaint = new SKPaint
        {
            Typeface = _typeface,
            IsAntialias = true,
            FilterQuality = SKFilterQuality.Low
        };

        _bgPaint = new SKPaint { Style = SKPaintStyle.Fill };

        PaintSurface += OnPaintSurface;

        Dispatcher.StartTimer(TimeSpan.FromMilliseconds(1000.0 / 60.0), () =>
        {
            InvalidateSurface();
            return true;
        });
    }

    private void OnPaintSurface(object? sender, SKPaintSurfaceEventArgs e)
    {
        float time = (float)_clock.Elapsed.TotalSeconds;

        if (_phosphorSurface == null ||
            _phosphorInfo.Width != e.Info.Width ||
            _phosphorInfo.Height != e.Info.Height)
        {
            _phosphorInfo = new SKImageInfo(e.Info.Width, e.Info.Height, SKColorType.RgbaF16, SKAlphaType.Premul);
            _phosphorSurface = SKSurface.Create(_phosphorInfo);

            _phosphorSurface.Canvas.Clear(SKColors.Black);
        }

        var pCanvas = _phosphorSurface.Canvas;

        pCanvas.DrawRect(
            SKRect.Create(_phosphorInfo.Width, _phosphorInfo.Height),
            _decayPaint);

        int cols = _buffer.Cols;
        int rows = _buffer.Rows;

        const float Border = 32f;
        const float baseGlyphW = 8f;
        const float baseGlyphH = 16f;

        float availW = e.Info.Width - Border * 2;
        float availH = e.Info.Height - Border * 2;
        if (availW <= 0 || availH <= 0)
            return;

        float rawScale = MathF.Min(availW / (cols * baseGlyphW),
                                   availH / (rows * baseGlyphH));
        float scale = MathF.Round(rawScale * 4f) / 4f;
        scale = MathF.Max(0.25f, scale);

        float glyphW = baseGlyphW * scale;
        float glyphH = baseGlyphH * scale;

        float termW = cols * glyphW;
        float termH = rows * glyphH;

        float offsetX = Border + (availW - termW) * 0.5f;
        float offsetY = Border + (availH - termH) * 0.5f;

        _fgPaint.TextSize = glyphH;
        _bloomPaint.TextSize = glyphH;

        var fm = _fgPaint.FontMetrics;
        float ascent = -fm.Ascent;

        // ---- DRAW NEW PHOSPHOR ENERGY (ABSOLUTE COORDS) ----
        for (int y = 0; y < rows; y++)
        {
            float py = offsetY + y * glyphH;

            for (int x = 0; x < cols; x++)
            {
                float px = offsetX + x * glyphW;
                ref readonly var cell = ref _buffer[y, x];

                if (cell.Char == '\0' || cell.Char == ' ')
                    continue;

                float flicker =
                    0.92f +
                    0.08f * MathF.Sin(time * 55f + x * 0.6f + y * 1.1f);

                var col = cell.Fg.ToSKColor()
                    .WithAlpha((byte)(255 * flicker));

                _fgPaint.Color = col;
                _bloomPaint.Color = col.WithAlpha(96);

                pCanvas.DrawText(cell.Char.ToString(), px, py + ascent, _bloomPaint);
                pCanvas.DrawText(cell.Char.ToString(), px, py + ascent, _fgPaint);
            }
        }

        // ---- PRESENT ----
        var canvas = e.Surface.Canvas;
        canvas.Clear(SKColors.Black);
        canvas.DrawImage(_phosphorSurface.Snapshot(), 0, 0);

        scanLineOffsetY = (scanLineOffsetY + 1f) % 2f;
        ApplyScanlines(canvas, e.Info.Width, e.Info.Height, scanLineOffsetY);
    }

    private float scanLineOffsetY = 0f;

    private void ApplyScanlines(SKCanvas canvas, int width, int height, float offsetY)
    {
        const int lineHeight = 2;

        for (int y = 0; y < height; y += lineHeight * 2)
        {
            canvas.DrawRect(0, y + offsetY, width, lineHeight, _scanlinePaint);
        }
    }
}
