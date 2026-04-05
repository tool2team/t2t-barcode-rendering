//-----------------------------------------------------------------------
// <copyright file="Code93.cs" company="Zen Design Corp">
//     Copyright © Zen Design Corp 2008 - 2012. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Drawing;
using T2t.Barcode.Core;
using T2t.Barcode.Core.Code93;

namespace T2t.Barcode.Drawing;

public class Code93BarcodeDraw
    : BarcodeDrawBase<Code93GlyphFactory, Code93Checksum>
{
    #region Public Constructors
    public Code93BarcodeDraw(Code93Checksum checksum)
        : base(checksum.Factory, checksum, 9)
    {
    }
    #endregion

    #region Public Methods
    public override BarcodeMetrics GetDefaultMetrics(int maxHeight)
    {
        return new BarcodeMetrics1d(1, 2, maxHeight);
    }

    public override BarcodeMetrics GetPrintMetrics(
        Size desiredBarcodeDimensions, Size printResolution,
        int barcodeCharLength)
    {
        int maxHeight = desiredBarcodeDimensions.Height * printResolution.Height / 100;
        int narrowBarWidth = printResolution.Width * desiredBarcodeDimensions.Width /
            (100 * (24 + (barcodeCharLength * 12)));
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
        result.Add(Factory.GetRawGlyph('|'));
        return result.ToArray();
    }
    #endregion
}
