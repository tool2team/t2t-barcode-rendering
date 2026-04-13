//-----------------------------------------------------------------------
// <copyright file="Code128.cs" company="Zen Design Corp">
//     Copyright © Zen Design Corp 2008 - 2012. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using SkiaSharp;
using T2t.Barcode.Core;
using T2t.Barcode.Core.Code128;

namespace T2t.Barcode.Skia;

/// <summary>
/// <b>Code128BarcodeDraw</b> is a type-safe extension of <see cref="T:BarcodeDraw"/>
/// that can render complete Code128 barcodes with checksum.
/// </summary>
public class Code128BarcodeDraw
    : BarcodeDrawBase<Code128GlyphFactory, Code128Checksum>
{
    #region Public Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="Code128BarcodeDraw"/>
    /// class.
    /// </summary>
    /// <param name="checksum">The checksum.</param>
    public Code128BarcodeDraw(Code128Checksum checksum)
        : base(checksum.Factory, checksum, 11)
    {
    }
    #endregion

    #region Public Methods
    /// <summary>
    /// Overridden. Gets a <see cref="T:BarcodeMetrics"/> object 
    /// containing default settings for the specified maximum bar height.
    /// </summary>
    /// <param name="maxHeight">The maximum barcode height.</param>
    /// <returns>
    /// A <see cref="T:BarcodeMetrics"/> object.
    /// </returns>
    public override BarcodeMetrics GetDefaultMetrics(int maxHeight)
    {
        return new BarcodeMetrics1d(1, maxHeight);
    }

    /// <summary>
    /// Overridden. Gets a <see cref="T:BarcodeMetrics"/> object containing the print
    /// metrics needed for printing a barcode of the specified physical
    /// size on a device operating at the specified resolution.
    /// </summary>
    /// <param name="desiredBarcodeDimensions">The desired barcode dimensions in hundredth of an inch.</param>
    /// <param name="printResolution">The print resolution in pixels per inch.</param>
    /// <param name="barcodeCharLength">Length of the barcode in characters.</param>
    /// <returns>
    /// A <see cref="T:BarcodeMetrics"/> object.
    /// </returns>
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
    /// <summary>
    /// Overridden. Gets the glyphs needed to render a full barcode.
    /// </summary>
    /// <param name="text">Text to convert into bar-code.</param>
    /// <returns>A collection of <see cref="T:Glyph"/> objects.</returns>
    protected override Glyph[] GetFullBarcode(string text)
    {
        return Code128Encoder.Encode(text, Factory, Checksum);
    }

    /// <summary>
    /// Overridden. Gets the length in pixels needed to render the 
    /// specified barcode.
    /// </summary>
    /// <param name="barcode">Barcode glyphs to be analysed.</param>
    /// <param name="interGlyphSpace">Amount of inter-glyph space.</param>
    /// <param name="barMinWidth">Minimum bar width.</param>
    /// <param name="barMaxWidth">Maximum bar width.</param>
    /// <returns></returns>
    protected override int GetBarcodeLength(Glyph[] barcode,
        int interGlyphSpace, int barMinWidth, int barMaxWidth)
    {
        return base.GetBarcodeLength(barcode, interGlyphSpace,
            barMinWidth, barMaxWidth) - (9 * barMinWidth);
    }
    #endregion
}
