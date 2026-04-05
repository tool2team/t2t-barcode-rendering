//-----------------------------------------------------------------------
// 
//-----------------------------------------------------------------------

namespace T2t.Barcode.Core.CodeDm;

/// <summary>
/// <c>BarcodeMetricsDm</c> extends <see cref="BarcodeMetrics2d"/> to
/// provide configuration properties used to render DM barcodes.
/// </summary>
public class BarcodeMetricsDm<TColor> : BarcodeMetrics2d
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
    public int Version { get; set; }

    /// <summary>
    /// Color of DM code
    /// </summary>
    public TColor ForegroundColor { get; set; }

    /// <summary>
    /// Color of background of DM code
    /// </summary>
    public TColor BackgroundColor { get; set; }
}
