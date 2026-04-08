//-----------------------------------------------------------------------
// <copyright file="CodeQr.cs" company="Zen Design Corp">
//     Copyright © Zen Design Corp 2012. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------


using SkiaSharp;
using System.Reflection;
using System.Text;
using T2t.Barcode.Core;
using T2t.Barcode.Core.CodeQr;

namespace T2t.Barcode.Skia;
/// <summary>
/// <c>CodeQrBarcodeDraw</c> extends <see cref="BarcodeDraw"/> to support
/// rendering QR barcodes.
/// </summary>
public class CodeQrBarcodeDraw : BarcodeDraw
{
        #region Public Methods
    /// <summary>
    /// Draws the specified text using the supplied barcode metrics.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="metrics">A <see cref="T:T2t.Barcode.Skia.BarcodeMetrics"/> object.</param>
    /// <returns></returns>
    public override sealed SKBitmap Draw<T>(string text, T metrics)
    {
        if (string.IsNullOrEmpty(text)) throw new ArgumentNullException(nameof(text), "text cannot be null or empty.");
        if (metrics is not BarcodeMetricsQr mQr) throw new ArgumentException($"metrics must be of type {nameof(BarcodeMetricsQr)}.", nameof(metrics));

        return DrawQr(text, mQr);
    }

    /// <summary>
    /// Gets a <see cref="T:T2t.Barcode.Skia.BarcodeMetrics"/> object containing default
    /// settings for the specified maximum bar height.
    /// </summary>
    /// <param name="maxHeight">The maximum barcode height.</param>
    /// <returns>
    /// A <see cref="T:T2t.Barcode.Skia.BarcodeMetrics"/> object.
    /// </returns>
    public override BarcodeMetrics GetDefaultMetrics(int maxHeight)
    {
        return new BarcodeMetricsQr
        {
            Scale = 4,
            Version = 7,
            EncodeMode = QrEncodeMode.Byte,
            ErrorCorrection = QrErrorCorrection.M,
            ForegroundColor = SKColors.Black,
            BackgroundColor = SKColors.White
        };
    }

    /// <summary>
    /// Gets a <see cref="T:BarcodeMetrics"/> object containing the print
    /// metrics needed for printing a barcode of the specified physical
    /// size on a device operating at the specified resolution.
    /// </summary>
    /// <param name="desiredBarcodeDimensions">The desired barcode dimensions in hundredth of an inch.</param>
    /// <param name="printResolution">The print resolution in pixels per inch.</param>
    /// <param name="barcodeCharLength">Length of the barcode in characters.</param>
    /// <returns>
    /// A <see cref="T:T2t.Barcode.Skia.BarcodeMetrics"/> object.
    /// </returns>
    public override BarcodeMetrics GetPrintMetrics(
        SKSizeI desiredBarcodeDimensions, SKSizeI printResolution, int barcodeCharLength)
    {
        return GetDefaultMetrics(30);
    }
    #endregion

    #region Protected Methods
    /// <summary>
    /// Draws the qr.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="metrics">The metrics.</param>
    /// <returns></returns>
    protected virtual SKBitmap DrawQr(string text, BarcodeMetricsQr metrics)
    {
        // Use Core encoder to get matrix
        var encoderOptions = new CodeQrEncoderOptions
        {
            Version = metrics.Version,
            EncodeMode = metrics.EncodeMode,
            ErrorCorrection = metrics.ErrorCorrection
        };

        Encoding encoding = Code2dUtility.IsUnicode(text) ? Encoding.Unicode : Encoding.ASCII;
        bool[][] matrix = CodeQrEncoder.EncodeMatrix(text, encoding, encoderOptions);

        // Render matrix with Skia-specific code
        int width = (int)(matrix.Length * metrics.Scale) + 1;
        int height = (int)(matrix.Length * metrics.Scale) + 1;

        SKBitmap image = new(width, height);
        using SKCanvas g = new(image);
        using SKPaint brush = new() { Color = metrics.BackgroundColor };

        g.DrawRect(new SKRect(0, 0, width, height), brush);
        brush.Color = metrics.ForegroundColor;

        for (int i = 0; i < matrix.Length; i++)
        {
            for (int j = 0; j < matrix.Length; j++)
            {
                if (matrix[j][i])
                {
                    g.DrawRect(j * metrics.Scale, i * metrics.Scale, metrics.Scale, metrics.Scale, brush);
                }
            }
        }

        return image;
    }
    #endregion
}

/// <summary>
/// <c>BarcodeMetricsQr</c> extends <see cref="BarcodeMetrics2d"/> to
/// provide configuration properties used to render QR barcodes.
/// </summary>
public class BarcodeMetricsQr : BarcodeMetrics2d
{
    /// <summary>
    /// Gets or sets the version used to render a QR barcode.
    /// </summary>
    /// <value>The version.</value>
    /// <remarks>
    /// The version determines the maximum amount of information that can
    /// be encoded in a QR barcode.
    /// If a value of 0 is used then the most compact representation for a
    /// given piece of text will be used.
    /// If the value is too small for the text to be rendered then an
    /// exception will be thrown during rendering.
    /// </remarks>
    public int Version
    {
        get;
        set;
    }

    /// <summary>
    /// Gets or sets the encoding mode.
    /// </summary>
    /// <value>The encode mode.</value>
    public QrEncodeMode EncodeMode
    {
        get;
        set;
    }

    /// <summary>
    /// Gets or sets the error correction scheme.
    /// </summary>
    /// <value>The error correction scheme.</value>
    public QrErrorCorrection ErrorCorrection
    {
        get;
        set;
    }

    /// <summary>
    /// Color of QR code
    /// </summary>
    public SKColor ForegroundColor
    {
        get;
        set;
    }

    /// <summary>
    /// Color of background of QR code
    /// </summary>
    public SKColor BackgroundColor
    {
        get;
        set;
    }
}
