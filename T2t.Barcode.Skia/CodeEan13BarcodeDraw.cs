//-----------------------------------------------------------------------
// <copyright file="CodeEan13.cs" company="Zen Design Corp">
//     Copyright © Zen Design Corp 2008 - 2012. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using SkiaSharp;
using System.Text.RegularExpressions;
using T2t.Barcode.Core;
using T2t.Barcode.Core.CodeEan13;

namespace T2t.Barcode.Skia;

public partial class CodeEan13BarcodeDraw
    : BarcodeDrawBase<CodeEan13GlyphFactory, CodeEan13Checksum>
{
    #region Public Constructors
    public CodeEan13BarcodeDraw(CodeEan13Checksum checksum)
        : base(checksum.Factory, checksum, 7)
    {
    }
    #endregion

    #region Public Methods
    public override BarcodeMetrics GetDefaultMetrics(int maxHeight)
    {
        return new BarcodeMetrics1d(1, 1, (int)(maxHeight * .9), maxHeight);
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
        Match m = RxEan13().Match(text);
        if (!m.Success)
        {
            throw new ArgumentException("Invalid barcode.");
        }
        string barcodeText = m.Groups["barcode"].Value;

        byte[] parityTable =
        [
                0x00,
                0x0B,
                0x0D,
                0x0E,
                0x13,
                0x19,
                0x1C,
                0x15,
                0x16,
                0x1A,
        ];

        char parityDigit = barcodeText[0];
        byte parity = parityTable[parityDigit - '0'];
        if (Checksum != null)
        {
            barcodeText += Checksum.GetChecksumChar(barcodeText);
        }
        barcodeText = barcodeText[1..];

        List<Glyph> result = [.. Factory.GetGlyphs(barcodeText, true)];

        int parityIndex = 32;
        for (int index = 0; index < result.Count; ++index)
        {
            if (result[index] is CompositeGlyph)
            {
                byte effectiveParity = (index < 6) ? parity : (byte)0;

                CompositeGlyph composite = (CompositeGlyph)result[index];
                if ((parityIndex & effectiveParity) == 0)
                {
                    result[index] = composite.First;
                }
                else
                {
                    result[index] = composite.Second;
                }
                if (parityIndex > 1)
                {
                    parityIndex /= 2;
                }
            }
        }

        result.Insert(6, Factory.GetRawGlyph('|'));
        result.Insert(0, Factory.GetRawGlyph('*'));
        result.Add(Factory.GetRawGlyph('*'));
        return [.. result];
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

    [GeneratedRegex(@"^\s*(?<barcode>[0-9]{12})\s*$")]
    private static partial Regex RxEan13();
    #endregion
}
