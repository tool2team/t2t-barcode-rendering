﻿//-----------------------------------------------------------------------
// 
//-----------------------------------------------------------------------

using System.Text;
using T2t.Barcode.Core;
using T2t.Barcode.Core.Dmtx;
using static System.Net.Mime.MediaTypeNames;
using static T2t.Barcode.Core.Dmtx.DmtxEncode;

namespace T2t.Barcode.Svg;
/// <summary>
/// <c>CodeDmBarcodeDraw</c> extends <see cref="BarcodeDraw"/> to support
/// rendering DM barcodes.
/// </summary>
public class CodeDmBarcodeDraw : BarcodeDraw
{
    #region Public Methods
    /// <summary>
    /// Draws the specified text using the supplied barcode metrics.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="metrics">A <see cref="T:T2t.Barcode.Svg.BarcodeMetrics"/> object.</param>
    /// <returns></returns>
    public override sealed string Draw(string text, BarcodeMetrics metrics)
    {
        Encoding enc = DMCodeUtility.IsUnicode(text) ? Encoding.Unicode : Encoding.ASCII;

        return DrawDm(text, (BarcodeMetricsDm)metrics, enc);
    }

    /// <summary>
    /// Gets a <see cref="T:T2t.Barcode.Svg.BarcodeMetrics"/> object containing default
    /// settings for the specified maximum bar height.
    /// </summary>
    /// <param name="maxHeight">The maximum barcode height.</param>
    /// <returns>
    /// A <see cref="T:T2t.Barcode.Svg.BarcodeMetrics"/> object.
    /// </returns>
    public override BarcodeMetrics GetDefaultMetrics(int maxHeight)
    {
        return new BarcodeMetricsDm
        {
            Scale = 4,
            ForegroundColor = "black",
            BackgroundColor = "white"
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
    /// A <see cref="T:T2t.Barcode.Svg.BarcodeMetrics"/> object.
    /// </returns>
    public override BarcodeMetrics GetPrintMetrics(
        T2Size desiredBarcodeDimensions, T2Size printResolution, int barcodeCharLength)
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
    protected virtual string DrawDm(string text, BarcodeMetricsDm metrics, Encoding encoding)
    {
        DmtxEncoderOptions options = new() {  Encoding = encoding };
        DmtxEncode encoder = new()
        {
            ModuleSize = options.ModuleSize,
            MarginSize = options.MarginSize,
            SizeIdxRequest = options.SizeIdx,
            Scheme = options.Scheme
        };

        byte[] valAsByteArray = encoder.GetRawDataAndSetEncoding(text, options);

        bool[,] matrix = encoder.CalculateDmCode(valAsByteArray);
        int width = (int)(matrix.Length * metrics.Scale) + 1;
        int height = (int)(matrix.Length * metrics.Scale) + 1;

        List<string> rects = new()
        {
            string.Format(RectTmpl, 0, 0, width, height, metrics.BackgroundColor)
        };

        for (int j = 0; j < matrix.GetLength(1); j++)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                if (matrix[j, i])
                {
                    rects.Add(string.Format(RectTmpl, j * metrics.Scale, i * metrics.Scale, metrics.Scale, metrics.Scale, metrics.ForegroundColor));
                }
            }
        }
        return string.Format(SvgTmpl, matrix.Length * metrics.Scale + 1, matrix.Length * metrics.Scale + 1, string.Join('\n', rects));
    }
    #endregion
}

/// <summary>
/// <c>BarcodeMetricsDm</c> extends <see cref="BarcodeMetrics2d"/> to
/// provide configuration properties used to render DM barcodes.
/// </summary>
public class BarcodeMetricsDm : BarcodeMetrics2d
{
    /// <summary>
    /// Gets or sets the version used to render a DM barcode.
    /// </summary>
    /// <value>The version.</value>
    /// <remarks>
    /// The version determines the maximum amount of information that can
    /// be encoded in a DM barcode.
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
    /// Color of DM code
    /// </summary>
    public string ForegroundColor
    {
        get;
        set;
    }

    /// <summary>
    /// Color of background of DM code
    /// </summary>
    public string BackgroundColor
    {
        get;
        set;
    }
}