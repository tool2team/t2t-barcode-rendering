//-----------------------------------------------------------------------
// <copyright file="Code25.cs" company="Zen Design Corp">
//     Copyright © Zen Design Corp 2008 - 2012. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using T2t.Barcode.Core;
using T2t.Barcode.Core.Code25;

namespace T2t.Barcode.Svg;

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
        T2Size desiredBarcodeDimensions, T2Size printResolution,
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

    protected override float GetDefaultInterGlyphSpace(float barMinWidth, float barMaxWidth)
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

    protected override float GetBarcodeLength(
        Glyph[] barcode, float interGlyphSpace, float barMinWidth, float barMaxWidth)
    {
        if (Factory is not Code25InterleavedGlyphFactory)
        {
            return base.GetBarcodeLength(barcode, interGlyphSpace, barMinWidth, barMaxWidth);
        }

        return (((barcode.Length - 2) * 7) + 8) * barMinWidth;
    }

    protected override List<string> RenderBars(Glyph[] barcode, T2Rect bounds, float interGlyphSpace, float barMinHeight, float barMinWidth, float barMaxWidth)
    {
        List<string> res = new();
        if (Factory is not Code25InterleavedGlyphFactory)
        {
            return base.RenderBars(barcode, bounds, interGlyphSpace, barMinHeight, barMinWidth, barMaxWidth);
        }

        float barOffset = 0;
        for (int index = 0; index < barcode.Length;)
        {
            BarGlyph glyph = (BarGlyph)barcode[index];
            float height = GetGlyphHeight(glyph, barMinHeight, bounds.Height);

            if (index == 0 || index == (barcode.Length - 1))
            {
                res.AddRange(RenderBar(index, glyph, bounds, ref barOffset, barMinHeight,
                    barMinWidth, barMaxWidth));
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
                        res.Add(string.Format(RectTmpl, barOffset, bounds.Top, barMinWidth * 2, height, "black"));
                        barOffset += barMinWidth * 2;
                    }
                    else
                    {
                        res.Add(string.Format(RectTmpl, barOffset, bounds.Top, barMinWidth, height, "black"));
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
        return res;
    }
    #endregion
}
