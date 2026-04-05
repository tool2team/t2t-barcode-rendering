//-----------------------------------------------------------------------
// <copyright file="Code39.cs" company="Zen Design Corp">
//     Copyright © Zen Design Corp 2008 - 2012. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using T2t.Barcode.Core;
using T2t.Barcode.Core.Code39;

namespace T2t.Barcode.Svg;

public class Code39BarcodeDraw
    : BinaryPitchBarcodeDraw<Code39GlyphFactory, Code39Checksum>
{
    #region Public Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="Code39BarcodeDraw"/>
    /// class.
    /// </summary>
    /// <param name="factory">The factory.</param>
    public Code39BarcodeDraw(Code39GlyphFactory factory)
        : base(factory, 12, 9)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Code39BarcodeDraw"/>
    /// class.
    /// </summary>
    /// <param name="checksum">The checksum.</param>
    public Code39BarcodeDraw(Code39Checksum checksum)
        : base(checksum.Factory, checksum, 12, 9)
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
        return new BarcodeMetrics1d(1, 2, maxHeight);
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
        T2Size desiredBarcodeDimensions, T2Size printResolution,
        int barcodeCharLength)
    {
        int maxHeight = desiredBarcodeDimensions.Height * printResolution.Height / 100;
        int narrowBarWidth = printResolution.Width * desiredBarcodeDimensions.Width /
            (100 * (10 + (barcodeCharLength * 9)));
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
