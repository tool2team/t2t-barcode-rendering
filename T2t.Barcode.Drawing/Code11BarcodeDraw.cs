//-----------------------------------------------------------------------
// <copyright file="Code11.cs" company="Zen Design Corp">
//     Copyright © Zen Design Corp 2008 - 2012. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Drawing;
using T2t.Barcode.Core;
using T2t.Barcode.Core.Code11;

namespace T2t.Barcode.Drawing;

/// <summary>
/// <b>Code11BarcodeDraw</b> is a type-safe extension of <see cref="T:BarcodeDraw"/>
/// that can render complete Code11 barcodes with or without checksum.
/// </summary>
public class Code11BarcodeDraw
    : BinaryPitchVaryLengthBarcodeDraw<Code11GlyphFactory, Code11Checksum>
{
    #region Public Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="Code11BarcodeDraw"/>
    /// class.
    /// </summary>
    /// <param name="factory">The factory.</param>
    public Code11BarcodeDraw(Code11GlyphFactory factory)
        : base(factory, 0, 5)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Code11BarcodeDraw"/>
    /// class.
    /// </summary>
    /// <param name="checksum">The checksum.</param>
    public Code11BarcodeDraw(Code11Checksum checksum)
        : base(checksum.Factory, checksum, 0, 5)
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
        return new BarcodeMetrics1d(1, 3, maxHeight);
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
    /// <summary>
    /// Overridden. Gets the glyphs needed to render a full barcode.
    /// </summary>
    /// <param name="text">Text to convert into bar-code.</param>
    /// <returns>A collection of <see cref="T:Glyph"/> objects.</returns>
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
