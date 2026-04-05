//-----------------------------------------------------------------------
// <copyright file="CodeEan8.cs" company="Zen Design Corp">
//     Copyright © Zen Design Corp 2008 - 2012. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using SkiaSharp;
using System.Text.RegularExpressions;
using T2t.Barcode.Core;
using T2t.Barcode.Core.CodeEan8;

namespace T2t.Barcode.Skia;

public class CodeEan8BarcodeDraw
    : BarcodeDrawBase<CodeEan8GlyphFactory, CodeEan8Checksum>
{
    #region Public Constructors
    public CodeEan8BarcodeDraw(CodeEan8Checksum checksum)
        : base(checksum.Factory, checksum, 7)
    {
    }
    #endregion

    #region Public Methods
    public override BarcodeMetrics GetDefaultMetrics(int maxHeight)
    {
        return new BarcodeMetrics1d(1, 1, maxHeight - 5, maxHeight);
    }

    public override BarcodeMetrics GetPrintMetrics(
        SKSizeI desiredBarcodeDimensions, SKSizeI printResolution,
        int barcodeCharLength)
    {
        int maxHeight = desiredBarcodeDimensions.Height * printResolution.Height / 100;
        int minHeight = maxHeight * 85 / 100;
        int narrowBarWidth = printResolution.Width * desiredBarcodeDimensions.Width /
            (100 * (24 + (barcodeCharLength * 11)));
        return new BarcodeMetrics1d(narrowBarWidth, narrowBarWidth, minHeight, maxHeight);
    }
    #endregion

    #region Protected Methods
    protected override Glyph[] GetFullBarcode(string text)
    {
        Match m = Regex.Match(text, @"^\s*(?<barcode>[0-9]{7})\s*$");
        if (!m.Success)
        {
            throw new ArgumentException("Invalid barcode.");
        }
        string barcodeText = m.Groups["barcode"].Value;

        if (Checksum != null)
        {
            barcodeText += Checksum.GetChecksumChar(barcodeText);
        }

        List<Glyph> result = new();
        result.AddRange(Factory.GetGlyphs(barcodeText, true));

        for (int index = 0; index < result.Count; ++index)
        {
            if (result[index] is CompositeGlyph composite)
            {
                if (index < 4)
                {
                    result[index] = composite.First;
                }
                else
                {
                    result[index] = composite.Second;
                }
            }
        }

        result.Insert(4, Factory.GetRawGlyph('|'));
        result.Insert(0, Factory.GetRawGlyph('*'));
        result.Add(Factory.GetRawGlyph('*'));
        return result.ToArray();
    }

    protected override int GetGlyphHeight(Glyph glyph, int barMinHeight, int barMaxHeight)
    {
        if (glyph.Character == '*' || glyph.Character == '|')
        {
            return barMaxHeight;
        }
        return barMinHeight;
    }

    protected override int GetDefaultInterGlyphSpace(int barMinWidth, int barMaxWidth)
    {
        return 0;
    }
    #endregion
}
