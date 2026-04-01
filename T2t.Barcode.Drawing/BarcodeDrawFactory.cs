//-----------------------------------------------------------------------
// <copyright file="BarcodeDrawFactory.cs" company="Zen Design Corp">
//     Copyright © Zen Design Corp 2008 - 2012. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using T2t.Barcode.Core;

namespace T2t.Barcode.Drawing;

/// <summary>
/// <b>BarcodeDrawFactory</b> returns draw agents capable of generating 
/// the appropriate bar-code image.
/// </summary>
public static class BarcodeDrawFactory
{
    #region Private Fields
    private static Code39BarcodeDraw _code39WithoutChecksum;
    private static Code39BarcodeDraw _code39WithChecksum;
    private static Code93BarcodeDraw _code93WithChecksum;
    private static Code128BarcodeDraw _code128WithChecksum;
    private static Code11BarcodeDraw _code11WithoutChecksum;
    private static Code11BarcodeDraw _code11WithChecksum;
    private static CodeEan13BarcodeDraw _codeEan13WithChecksum;
    private static CodeEan8BarcodeDraw _codeEan8WithChecksum;
    private static Code25BarcodeDraw _code25StandardWithoutChecksum;
    private static Code25BarcodeDraw _code25StandardWithChecksum;
    private static Code25BarcodeDraw _code25InterleavedWithoutChecksum;
    private static Code25BarcodeDraw _code25InterleavedWithChecksum;
    private static CodeQrBarcodeDraw _codeQr;
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets an agent capable of rendering a Code39 barcode without
    /// adding a checksum glyph.
    /// </summary>
    /// <value>A <see cref="T:T2t.Barcode.Drawing.Code39BarcodeDraw"/> object.</value>
    public static Code39BarcodeDraw Code39WithoutChecksum
    {
        get
        {
            _code39WithoutChecksum ??= new Code39BarcodeDraw(
                    Code39GlyphFactory.Instance);
            return _code39WithoutChecksum;
        }
    }

    /// <summary>
    /// Gets an agent capable of rendering a Code39 barcode with an
    /// added checksum glyph.
    /// </summary>
    /// <value>A <see cref="T:T2t.Barcode.Drawing.Code39BarcodeDraw"/> object.</value>
    public static Code39BarcodeDraw Code39WithChecksum
    {
        get
        {
            _code39WithChecksum ??= new Code39BarcodeDraw(
                    Code39Checksum.Instance);
            return _code39WithChecksum;
        }
    }

    /// <summary>
    /// Gets an agent capable of rendering a Code93 barcode with added
    /// checksum glyphs.
    /// </summary>
    /// <value>A <see cref="T:T2t.Barcode.Drawing.Code93BarcodeDraw"/> object.</value>
    public static Code93BarcodeDraw Code93WithChecksum
    {
        get
        {
            _code93WithChecksum ??= new Code93BarcodeDraw(
                    Code93Checksum.Instance);
            return _code93WithChecksum;
        }
    }

    /// <summary>
    /// Gets an agent capable of rendering a Code128 barcode with added
    /// checksum glyphs.
    /// </summary>
    /// <value>A <see cref="T:T2t.Barcode.Drawing.Code128BarcodeDraw"/> object.</value>
    public static Code128BarcodeDraw Code128WithChecksum
    {
        get
        {
            _code128WithChecksum ??= new Code128BarcodeDraw(
                    Code128Checksum.Instance);
            return _code128WithChecksum;
        }
    }

    /// <summary>
    /// Gets an agent capable of rendering a Code11 barcode.
    /// </summary>
    /// <value>A <see cref="T:T2t.Barcode.Drawing.Code11BarcodeDraw"/> object.</value>
    public static Code11BarcodeDraw Code11WithoutChecksum
    {
        get
        {
            _code11WithoutChecksum ??= new Code11BarcodeDraw(
                    Code11GlyphFactory.Instance);
            return _code11WithoutChecksum;
        }
    }

    /// <summary>
    /// Gets an agent capable of rendering a Code11 barcode with added
    /// checksum glyphs.
    /// </summary>
    /// <value>A <see cref="T:T2t.Barcode.Drawing.Code11BarcodeDraw"/> object.</value>
    public static Code11BarcodeDraw Code11WithChecksum
    {
        get
        {
            _code11WithChecksum ??= new Code11BarcodeDraw(
                    Code11Checksum.Instance);
            return _code11WithChecksum;
        }
    }

    /// <summary>
    /// Gets an agent capable of rendering a Code EAN-13 barcode with
    /// added checksum glyphs.
    /// </summary>
    /// <value>A <see cref="T:T2t.Barcode.Drawing.CodeEan13BarcodeDraw"/> object.</value>
    public static CodeEan13BarcodeDraw CodeEan13WithChecksum
    {
        get
        {
            _codeEan13WithChecksum ??= new CodeEan13BarcodeDraw(
                    CodeEan13Checksum.Instance);
            return _codeEan13WithChecksum;
        }
    }

    /// <summary>
    /// Gets an agent capable of rendering a Code EAN-8 barcode with
    /// added checksum glyphs.
    /// </summary>
    /// <value>A <see cref="T:T2t.Barcode.Drawing.CodeEan8BarcodeDraw"/> object.</value>
    public static CodeEan8BarcodeDraw CodeEan8WithChecksum
    {
        get
        {
            _codeEan8WithChecksum ??= new CodeEan8BarcodeDraw(
                    CodeEan8Checksum.Instance);
            return _codeEan8WithChecksum;
        }
    }

    /// <summary>
    /// Gets an agent capable of rendering a Code 25 barcode without
    /// checksum glyphs.
    /// </summary>
    /// <value>A <see cref="T:T2t.Barcode.Drawing.Code25BarcodeDraw"/> object.</value>
    public static Code25BarcodeDraw Code25StandardWithoutChecksum
    {
        get
        {
            _code25StandardWithoutChecksum ??= new Code25BarcodeDraw(
                    Code25GlyphFactory.StandardInstance);
            return _code25StandardWithoutChecksum;
        }
    }

    /// <summary>
    /// Gets an agent capable of rendering a Code 25 barcode with
    /// added checksum glyphs.
    /// </summary>
    /// <value>A <see cref="T:T2t.Barcode.Drawing.Code25BarcodeDraw"/> object.</value>
    public static Code25BarcodeDraw Code25StandardWithChecksum
    {
        get
        {
            _code25StandardWithChecksum ??= new Code25BarcodeDraw(
                    Code25Checksum.StandardInstance);
            return _code25StandardWithChecksum;
        }
    }

    /// <summary>
    /// Gets an agent capable of rendering a Code 25 barcode without
    /// checksum glyphs.
    /// </summary>
    /// <value>A <see cref="T:T2t.Barcode.Drawing.Code25BarcodeDraw"/> object.</value>
    public static Code25BarcodeDraw Code25InterleavedWithoutChecksum
    {
        get
        {
            _code25InterleavedWithoutChecksum ??= new Code25BarcodeDraw(
                    Code25GlyphFactory.InterleavedInstance);
            return _code25InterleavedWithoutChecksum;
        }
    }

    /// <summary>
    /// Gets an agent capable of rendering a Code 25 barcode with
    /// added checksum glyphs.
    /// </summary>
    /// <value>A <see cref="T:T2t.Barcode.Drawing.Code25BarcodeDraw"/> object.</value>
    public static Code25BarcodeDraw Code25InterleavedWithChecksum
    {
        get
        {
            _code25InterleavedWithChecksum ??= new Code25BarcodeDraw(
                    Code25Checksum.InterleavedInstance);
            return _code25InterleavedWithChecksum;
        }
    }

    /// <summary>
    /// Gets an agent capable of rendering a Code QR barcode.
    /// </summary>
    /// <value>A <see cref="T:T2t.Barcode.Drawing.CodeQrBarcodeDraw"/> object.</value>
    public static CodeQrBarcodeDraw CodeQr
    {
        get
        {
            _codeQr ??= new CodeQrBarcodeDraw();
            return _codeQr;
        }
    }
    #endregion

    #region Public Methods
    /// <summary>
    /// Gets the barcode drawing object for rendering the specified
    /// barcode symbology.
    /// </summary>
    /// <param name="symbology">
    /// A value from the <see cref="T:T2t.Barcode.Drawing.BarcodeSymbology"/> enumeration.
    /// </param>
    /// <returns>
    /// A class derived from <see cref="T:T2t.Barcode.Drawing.BarcodeDraw"/>.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    /// Thrown if the specified symbology is invalid or unknown.
    /// </exception>
    public static BarcodeDraw GetSymbology(BarcodeSymbology symbology)
    {
        return symbology switch
        {
            BarcodeSymbology.Code39NC => Code39WithoutChecksum,
            BarcodeSymbology.Code39C => Code39WithChecksum,
            BarcodeSymbology.Code93 => Code93WithChecksum,
            BarcodeSymbology.Code128 => Code128WithChecksum,
            BarcodeSymbology.Code11NC => Code11WithoutChecksum,
            BarcodeSymbology.Code11C => Code11WithChecksum,
            BarcodeSymbology.CodeEan13 => CodeEan13WithChecksum,
            BarcodeSymbology.CodeEan8 => CodeEan8WithChecksum,
            BarcodeSymbology.Code25StandardNC => Code25StandardWithoutChecksum,
            BarcodeSymbology.Code25StandardC => Code25StandardWithChecksum,
            BarcodeSymbology.Code25InterleavedNC => Code25InterleavedWithoutChecksum,
            BarcodeSymbology.Code25InterleavedC => Code25InterleavedWithChecksum,
            BarcodeSymbology.CodeQr => CodeQr,
            _ => throw new ArgumentException("BarcodeSymbologyInvalid", nameof(symbology)),
        };
    }
    #endregion
}