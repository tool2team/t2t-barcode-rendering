//-----------------------------------------------------------------------
// 
//-----------------------------------------------------------------------

using System.Drawing;
using System.Text;
using T2t.Barcode.Core;
using T2t.Barcode.Core.Dmtx;
using static System.Net.Mime.MediaTypeNames;
using static T2t.Barcode.Core.Dmtx.DmtxEncode;
using Image = System.Drawing.Image;

namespace T2t.Barcode.Drawing;
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
    public override sealed System.Drawing.Image Draw(string text, BarcodeMetrics metrics)
    {
        if (string.IsNullOrEmpty(text)) throw new ArgumentNullException(nameof(text), "text cannot be null or empty.");
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

    public override BarcodeMetrics GetPrintMetrics(Size desiredBarcodeDimensions, Size printResolution, int barcodeCharLength)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region Protected Methods
    /// <summary>
    /// Draws the qr.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="metrics">The metrics.</param>
    /// <returns></returns>
    protected virtual Image DrawDm(string text, BarcodeMetricsDm metrics, Encoding encoding)
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

        throw new NotImplementedException("Drawing DM code is not implemented yet.");
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
