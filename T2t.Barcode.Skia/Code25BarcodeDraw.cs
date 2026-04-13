//-----------------------------------------------------------------------
// <copyright file="Code25.cs" company="Zen Design Corp">
//     Copyright © Zen Design Corp 2008 - 2012. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using SkiaSharp;
using T2t.Barcode.Core;
using T2t.Barcode.Core.Code25;

namespace T2t.Barcode.Skia;

public class Code25BarcodeDraw
    : BarcodeDrawBase<Code25GlyphFactory, Code25Checksum>
{
    #region Public Constructors
    public Code25BarcodeDraw(Code25GlyphFactory factory)
        : base(factory, 0)
    {
    }

    public Code25BarcodeDraw(Code25Checksum checksum)
        : base(checksum.Factory, checksum, 0)
    {
    }
    #endregion

    #region Public Methods
    public override BarcodeMetrics GetDefaultMetrics(int maxHeight)
    {
        return new BarcodeMetrics1d(1, 1, maxHeight);
    }

    public override BarcodeMetrics GetPrintMetrics(
        SKSizeI desiredBarcodeDimensions, SKSizeI printResolution,
        int barcodeCharLength)
    {
        int maxHeight = desiredBarcodeDimensions.Height * printResolution.Height / 100;
        int narrowBarWidth = printResolution.Width * desiredBarcodeDimensions.Width /
                (100 * (24 + (barcodeCharLength * 11)));
        return new BarcodeMetrics1d(narrowBarWidth, maxHeight);
    }
    #endregion

    #region Protected Methods
    protected override Glyph[] GetFullBarcode(string text)
    {
        return Code25Encoder.Encode(text, Factory, Checksum);
    }

    protected override int GetDefaultInterGlyphSpace(int barMinWidth, int barMaxWidth)
    {
        if (Factory is Code25StandardGlyphFactory)
        {
            return barMinWidth;
        }
        else
        {
            return 0;
        }
    }

    protected override int GetBarcodeLength(
        Glyph[] barcode, int interGlyphSpace, int barMinWidth, int barMaxWidth)
    {
        if (Factory is not Code25InterleavedGlyphFactory)
        {
            return base.GetBarcodeLength(barcode, interGlyphSpace, barMinWidth, barMaxWidth);
        }

        return (((barcode.Length - 2) * 7) + 8) * barMinWidth;
    }

    protected override void RenderBars(Glyph[] barcode, SKCanvas dc, SKRectI bounds, int interGlyphSpace, int barMinHeight, int barMinWidth, int barMaxWidth)
    {
        if (!(Factory is Code25InterleavedGlyphFactory))
        {
            base.RenderBars(barcode, dc, bounds, interGlyphSpace, barMinHeight, barMinWidth, barMaxWidth);
            return;
        }

        int barOffset = 0;
        using SKPaint paint = new() { Color = SKColors.Black };
        for (int index = 0; index < barcode.Length;)
        {
            BarGlyph glyph = (BarGlyph)barcode[index];
            float height = GetGlyphHeight(glyph, barMinHeight, bounds.Height);

            if (index == 0 || index == (barcode.Length - 1))
            {
                RenderBar(index, glyph, dc, paint, bounds, ref barOffset, barMinHeight,
                    barMinWidth, barMaxWidth);
                ++index;
            }
            else
            {
                int encodingBitCount = 5;
                BarGlyph firstGlyph = (BarGlyph)barcode[index];
                BarGlyph secondGlyph = (BarGlyph)barcode[index + 1];

                for (int bitIndex = encodingBitCount - 1; bitIndex >= 0; --bitIndex)
                {
                    int bitMask = 1 << bitIndex;
                    if ((firstGlyph.BitEncoding & bitMask) != 0)
                    {
                        dc.DrawRect(barOffset, bounds.Top, barMinWidth * 2, height, paint);
                        barOffset += barMinWidth * 2;
                    }
                    else
                    {
                        dc.DrawRect(barOffset, bounds.Top, barMinWidth, height, paint);
                        barOffset += barMinWidth;
                    }

                    if ((secondGlyph.BitEncoding & bitMask) != 0)
                    {
                        barOffset += barMinWidth * 2;
                    }
                    else
                    {
                        barOffset += barMinWidth;
                    }
                }

                index += 2;
            }

            barOffset += interGlyphSpace;
        }
    }
    #endregion
}
