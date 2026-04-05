//-----------------------------------------------------------------------
// <copyright file="Code39.cs" company="Zen Design Corp">
//     Copyright © Zen Design Corp 2008 - 2012. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using SkiaSharp;
using T2t.Barcode.Core;
using T2t.Barcode.Core.Code39;

namespace T2t.Barcode.Skia;

public class Code39BarcodeDraw
    : BinaryPitchBarcodeDraw<Code39GlyphFactory, Code39Checksum>
{
    #region Public Constructors
    public Code39BarcodeDraw(Code39GlyphFactory factory)
        : base(factory, 12, 9)
    {
    }

    public Code39BarcodeDraw(Code39Checksum checksum)
        : base(checksum.Factory, checksum, 12, 9)
    {
    }
    #endregion

    #region Public Methods
    public override BarcodeMetrics GetDefaultMetrics(int maxHeight)
    {
        return new BarcodeMetrics1d(1, 2, maxHeight);
    }

    public override BarcodeMetrics GetPrintMetrics(
        SKSizeI desiredBarcodeDimensions, SKSizeI printResolution,
        int barcodeCharLength)
    {
        int maxHeight = desiredBarcodeDimensions.Height * printResolution.Height / 100;
        int narrowBarWidth = printResolution.Width * desiredBarcodeDimensions.Width /
            (100 * (10 + (barcodeCharLength * 9)));
        return new BarcodeMetrics1d(narrowBarWidth, narrowBarWidth * 2, maxHeight);
    }
    #endregion

    #region Protected Methods
    protected override Glyph[] GetFullBarcode(string text)
    {
        List<Glyph> result = new();
        result.AddRange(Factory.GetGlyphs(text));
        if (Checksum != null)
        {
            result.AddRange(Checksum.GetChecksum(text));
        }
        result.Insert(0, Factory.GetRawGlyph('*'));
        result.Add(Factory.GetRawGlyph('*'));
        return result.ToArray();
    }
    #endregion
}
