//-----------------------------------------------------------------------
// <copyright file="BarcodeDraw.cs" company="Zen Design Corp">
//     Copyright � Zen Design Corp 2008 - 2012. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------



using System.Globalization;
using T2t.Barcode.Core;

namespace T2t.Barcode.Svg;
/// <summary>
/// <c>BarcodeMetrics1d</c> defines the measurement metrics used to render
/// a 1 dimensional barcode.
/// </summary>
[Serializable]
public class BarcodeMetrics1d : BarcodeMetrics
{
    #region Private Fields
    private int _minWidth;
    private int _maxWidth;
    private int _minHeight;
    private int _maxHeight;
    private int? _interGlyphSpacing;
    #endregion

    #region Public Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="BarcodeMetrics1d"/> class.
    /// </summary>
    public BarcodeMetrics1d()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BarcodeMetrics1d"/> class.
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    public BarcodeMetrics1d(int width, int height)
    {
        _minWidth = _maxWidth = width;
        _minHeight = _maxHeight = height;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BarcodeMetrics1d"/> class.
    /// </summary>
    /// <param name="minWidth"></param>
    /// <param name="maxWidth"></param>
    /// <param name="height"></param>
    public BarcodeMetrics1d(int minWidth, int maxWidth, int height)
    {
        _minWidth = minWidth;
        _maxWidth = maxWidth;
        _minHeight = _maxHeight = height;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BarcodeMetrics1d"/> class.
    /// </summary>
    /// <param name="minWidth"></param>
    /// <param name="maxWidth"></param>
    /// <param name="minHeight"></param>
    /// <param name="maxHeight"></param>
    public BarcodeMetrics1d(
    int minWidth, int maxWidth, int minHeight, int maxHeight)
    {
        _minWidth = minWidth;
        _maxWidth = maxWidth;
        _minHeight = minHeight;
        _maxHeight = maxHeight;
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets/sets the minimum bar width.
    /// </summary>
    public int MinWidth
    {
        get
        {
            return _minWidth;
        }
        set
        {
            _minWidth = value;
        }
    }

    /// <summary>
    /// Gets/sets the maximum bar width.
    /// </summary>
    public int MaxWidth
    {
        get
        {
            return _maxWidth;
        }
        set
        {
            _maxWidth = value;
        }
    }

    /// <summary>
    /// Gets/sets the minimum bar height.
    /// </summary>
    public int MinHeight
    {
        get
        {
            return _minHeight;
        }
        set
        {
            _minHeight = value;
        }
    }

    /// <summary>
    /// Gets/sets the maximum bar height.
    /// </summary>
    public int MaxHeight
    {
        get
        {
            return _maxHeight;
        }
        set
        {
            _maxHeight = value;
        }
    }

    /// <summary>
    /// Gets/sets the amount of inter-glyph spacing to apply.
    /// </summary>
    /// <remarks>
    /// By default this is set to -1 which forces the barcode drawing
    /// classes to use the default value specified by the symbology.
    /// </remarks>
    public int? InterGlyphSpacing
    {
        get
        {
            return _interGlyphSpacing;
        }
        set
        {
            _interGlyphSpacing = value;
        }
    }
    #endregion
}

/// <summary>
/// <c>BarcodeMetrics2d</c> defines the measurement metrics used to render
/// a 2 dimensional barcode.
/// </summary>
[Serializable]
public class BarcodeMetrics2d : BarcodeMetrics
{
}

/// <summary>
/// <c>BarcodeDraw</c> is an abstract base class for all barcode drawing
/// classes.
/// </summary>
public abstract class BarcodeDraw
{
    protected const string SvgTmpl = """<svg version = "1.1" width="{0}" height="{1}" xmlns="http://www.w3.org/2000/svg"><g>{2}</g></svg>""";
    protected const string RectTmpl = """<rect x="{0}" y="{1}" width="{2}" height="{3}" fill="{4}" />""";

    #region Protected Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="T:T2t.Barcode.Svg.BarcodeDraw"/> class.
    /// </summary>
    protected BarcodeDraw()
    {
    }
    #endregion

    #region Public Methods
    /// <summary>
    /// Draws the specified text using the supplied barcode metrics.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="metrics">A <see cref="T:T2t.Barcode.Svg.BarcodeMetrics"/> object.</param>
    /// <returns>
    /// A string containing the rendered barcode.
    /// </returns>
    public abstract string Draw(string text, BarcodeMetrics metrics);

    /// <summary>
    /// Draws the specified text using the default barcode metrics for
    /// the specified maximum barcode height.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="maxBarHeight">The maximum bar height.</param>
    /// <returns>
    /// A string containing the rendered barcode.
    /// </returns>
    public string Draw(string text, int maxBarHeight)
    {
        BarcodeMetrics defaultMetrics = GetDefaultMetrics(maxBarHeight);
        return Draw(text, defaultMetrics);
    }

    /// <summary>
    /// Draws the specified text using the default barcode metrics for
    /// the specified maximum barcode height.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="maxBarHeight">The maximum bar height.</param>
    /// <param name="scale">
    /// The scale factor to use when rendering the barcode.
    /// </param>
    /// <returns>
    /// A string containing the rendered barcode.
    /// </returns>
    public string Draw(string text, int maxBarHeight, float scale)
    {
        BarcodeMetrics defaultMetrics = GetDefaultMetrics(maxBarHeight);
        defaultMetrics.Scale = scale;
        return Draw(text, defaultMetrics);
    }

    /// <summary>
    /// Gets a <see cref="T:T2t.Barcode.Svg.BarcodeMetrics"/> object containing default
    /// settings for the specified maximum bar height.
    /// </summary>
    /// <param name="maxHeight">The maximum barcode height.</param>
    /// <returns>A <see cref="T:T2t.Barcode.Svg.BarcodeMetrics"/> object.</returns>
    public abstract BarcodeMetrics GetDefaultMetrics(int maxHeight);

    /// <summary>
    /// Gets a <see cref="T:BarcodeMetrics"/> object containing the print
    /// metrics needed for printing a barcode of the specified physical
    /// size on a device operating at the specified resolution.
    /// </summary>
    /// <param name="desiredBarcodeDimensions">The desired barcode dimensions in hundredth of an inch.</param>
    /// <param name="printResolution">The print resolution in pixels per inch.</param>
    /// <param name="barcodeCharLength">Length of the barcode in characters.</param>
    /// <returns>A <see cref="T:T2t.Barcode.Svg.BarcodeMetrics"/> object.</returns>
    public abstract BarcodeMetrics GetPrintMetrics(
        T2Size desiredBarcodeDimensions, T2Size printResolution,
        int barcodeCharLength);
    #endregion
}

/// <summary>
/// <b>BarcodeDrawBase</b> deals with rendering a barcode using the associated
/// glyph factory and optional checksum generator classes.
/// </summary>
/// <typeparam name="TGlyphFactory">
/// A <see cref="T:T2t.Barcode.Core.GlyphFactory"/> derived type.
/// </typeparam>
/// <typeparam name="TChecksum">
/// A <see cref="T:T2t.Barcode.Svg.Checksum"/> derived type.
/// </typeparam>
public abstract class BarcodeDrawBase<TGlyphFactory, TChecksum> : BarcodeDraw
    where TGlyphFactory : GlyphFactory
    where TChecksum : Checksum
{
    #region Private Fields
    private readonly TGlyphFactory _factory;
    private readonly TChecksum _checksum;
    private readonly int _encodingBitCount;
    private readonly int _widthBitCount;
    #endregion

    #region Protected Constructors
    /// <summary>
    /// Initialises an instance of <see cref="T:T2t.Barcode.Svg.BarcodeDraw"/>.
    /// </summary>
    /// <param name="factory">The factory.</param>
    /// <param name="encodingBitCount">
    /// Number of bits in each encoded glyph.
    /// Set to <c>0</c> for variable length bit encoded glyphs.
    /// </param>
    protected BarcodeDrawBase(TGlyphFactory factory, int encodingBitCount)
    {
        _factory = factory;
        _encodingBitCount = encodingBitCount;
    }

    /// <summary>
    /// Initialises an instance of <see cref="T:T2t.Barcode.Svg.BarcodeDraw"/>.
    /// </summary>
    /// <param name="factory">The factory.</param>
    /// <param name="encodingBitCount">
    /// Number of bits in each encoded glyph.
    /// Set to <c>0</c> for variable length bit encoded glyphs.
    /// </param>
    /// <param name="widthBitCount">Width of the width bit.</param>
    protected BarcodeDrawBase(TGlyphFactory factory, int encodingBitCount,
        int widthBitCount)
    {
        _factory = factory;
        _encodingBitCount = encodingBitCount;
        _widthBitCount = widthBitCount;
    }

    /// <summary>
    /// Initialises an instance of <see cref="T:T2t.Barcode.Svg.BarcodeDraw"/>.
    /// </summary>
    /// <param name="factory">The factory.</param>
    /// <param name="checksum">The checksum.</param>
    /// <param name="encodingBitCount">
    /// Number of bits in each encoded glyph.
    /// Set to <c>0</c> for variable length bit encoded glyphs.
    /// </param>
    protected BarcodeDrawBase(TGlyphFactory factory, TChecksum checksum,
        int encodingBitCount)
    {
        _factory = factory;
        _checksum = checksum;
        _encodingBitCount = encodingBitCount;
    }

    /// <summary>
    /// Initialises an instance of <see cref="T:T2t.Barcode.Svg.BarcodeDraw"/>.
    /// </summary>
    /// <param name="factory">The factory.</param>
    /// <param name="checksum">The checksum.</param>
    /// <param name="encodingBitCount">
    /// Number of bits in each encoded glyph.
    /// Set to <c>0</c> for variable length bit encoded glyphs.
    /// </param>
    /// <param name="widthBitCount">Width of the width bit.</param>
    protected BarcodeDrawBase(TGlyphFactory factory, TChecksum checksum,
        int encodingBitCount, int widthBitCount)
    {
        _factory = factory;
        _checksum = checksum;
        _encodingBitCount = encodingBitCount;
        _widthBitCount = widthBitCount;
    }
    #endregion

    #region Public Methods
    /// <summary>
    /// Draws the specified text using the supplied barcode metrics.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="metrics">A <see cref="T:T2t.Barcode.Svg.BarcodeMetrics"/> object.</param>
    /// <returns></returns>
    public override sealed string Draw(string text, BarcodeMetrics metrics)
    {
        return Draw1d(text, (BarcodeMetrics1d)metrics);
    }
    #endregion

    #region Protected Properties
    /// <summary>
    /// Gets the <typeparamref name="TGlyphFactory"/> glyph factory.
    /// </summary>
    /// <value>The <typeparamref name="TGlyphFactory" /> factory.</value>
    protected TGlyphFactory Factory
    {
        get
        {
            return _factory;
        }
    }

    /// <summary>
    /// Gets the <typeparamref name="TChecksum"/> checksum object.
    /// </summary>
    /// <value>The <typeparamref name="TChecksum" /> checksum.</value>
    protected TChecksum Checksum
    {
        get
        {
            return _checksum;
        }
    }

    /// <summary>
    /// Gets the number of bits used for a glyph encoding.
    /// </summary>
    /// <value>Number of bits used to represent the glyph encoding.</value>
    protected int EncodingBitCount
    {
        get
        {
            return _encodingBitCount;
        }
    }

    /// <summary>
    /// Gets the number of bits used to encode the glyph width 
    /// information.
    /// </summary>
    /// <value>Number of bits used to represent bar width encoding.</value>
    protected int WidthBitCount
    {
        get
        {
            return _widthBitCount;
        }
    }
    #endregion

    #region Protected Methods
    /// <summary>
    /// Draws the specified text using the supplied barcode metrics.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="metrics">A <see cref="T:T2t.Barcode.Svg.BarcodeMetrics"/> object.</param>
    /// <returns>
    /// A string containing the rendered barcode.
    /// </returns>
    protected virtual string Draw1d(string text, BarcodeMetrics1d metrics)
    {
        // Determine number of pixels required for final image
        Glyph[] barcode = GetFullBarcode(text);

        // Determine amount of inter-glyph space
        float interGlyphSpace;
        if (metrics.InterGlyphSpacing.HasValue)
        {
            interGlyphSpace = metrics.InterGlyphSpacing.Value;
        }
        else
        {
            interGlyphSpace = GetDefaultInterGlyphSpace(
                metrics.MinWidth, metrics.MaxWidth);
        }

        // Determine bar code length in pixels
        float totalImageWidth = GetBarcodeLength(
                barcode,
                interGlyphSpace * metrics.Scale,
                metrics.MinWidth * metrics.Scale,
                metrics.MaxWidth * metrics.Scale);

        // Create image of correct size
        T2Rect bounds = new(0, 0, (int)totalImageWidth, metrics.MaxHeight);
        List<string> content = Render(
            barcode,
            bounds,
            interGlyphSpace * metrics.Scale,
                    metrics.MinHeight,
                    metrics.MinWidth * metrics.Scale,
                    metrics.MaxWidth * metrics.Scale);

        return string.Format(CultureInfo.InvariantCulture, SvgTmpl, totalImageWidth, metrics.MaxHeight, string.Join('\n', content));
    }

    /// <summary>
    /// Gets the default amount of inter-glyph space to apply.
    /// </summary>
    /// <param name="barMinWidth">The min bar width in pixels.</param>
    /// <param name="barMaxWidth">The max bar width in pixels.</param>
    /// <returns>The amount of inter-glyph spacing to apply in pixels.</returns>
    /// <remarks>
    /// By default this method returns zero.
    /// </remarks>
    protected virtual float GetDefaultInterGlyphSpace(
        float barMinWidth, float barMaxWidth)
    {
        return 0;
    }

    /// <summary>
    /// Gets the glyphs needed to render a full barcode.
    /// </summary>
    /// <param name="text">Text to convert into bar-code.</param>
    /// <returns>A collection of <see cref="T:T2t.Barcode.Core.Glyph"/> objects.</returns>
    protected abstract Glyph[] GetFullBarcode(string text);

    /// <summary>
    /// Gets the length in pixels needed to render the specified barcode.
    /// </summary>
    /// <param name="barcode">Barcode glyphs to be analysed.</param>
    /// <param name="interGlyphSpace">Amount of inter-glyph space.</param>
    /// <param name="barMinWidth">Minimum barcode width.</param>
    /// <param name="barMaxWidth">Maximum barcode width.</param>
    /// <returns>The barcode width in pixels.</returns>
    /// <remarks>
    /// Currently this method does not account for any "quiet space"
    /// around the barcode as dictated by each symbology standard.
    /// </remarks>
    protected virtual float GetBarcodeLength(
        Glyph[] barcode, float interGlyphSpace, float barMinWidth, float barMaxWidth)
    {
        // Determine bar code length in pixels
        float totalImageWidth = GetBarcodeInterGlyphLength(barcode, interGlyphSpace);
        foreach (BarGlyph glyph in barcode)
        {
            // Determine encoding bit-width for this character
            int encodingBitCount = GetEncodingBitCount(glyph);
            if (glyph is IBinaryPitchGlyph)
            {
                IBinaryPitchGlyph binaryGlyph = (IBinaryPitchGlyph)glyph;
                int widthIndex = WidthBitCount - 1;
                bool lastBitState = false;
                for (int bitIndex = encodingBitCount - 1; bitIndex >= 0; --bitIndex)
                {
                    // Determine whether the bit state is changing
                    int bitmask = 1 << bitIndex;
                    bool currentBitState = false;
                    if ((bitmask & binaryGlyph.BitEncoding) != 0)
                    {
                        currentBitState = true;
                    }

                    // Adjust the width bit checker
                    if (bitIndex < (encodingBitCount - 1) &&
                        lastBitState != currentBitState)
                    {
                        --widthIndex;
                    }
                    lastBitState = currentBitState;

                    // Determine width encoding bit mask
                    bitmask = 1 << widthIndex;
                    if ((bitmask & binaryGlyph.WidthEncoding) != 0)
                    {
                        totalImageWidth += barMaxWidth;
                    }
                    else
                    {
                        totalImageWidth += barMinWidth;
                    }
                }
            }
            else
            {
                totalImageWidth += encodingBitCount * barMinWidth;
            }
        }
        return totalImageWidth;
    }

    /// <summary>
    /// Gets the glyph's barcode encoding bit count.
    /// </summary>
    /// <param name="glyph">A <see cref="T:T2t.Barcode.Core.Glyph"/> to be queried.</param>
    /// <returns>Number of bits needed to encode the glyph.</returns>
    /// <remarks>
    /// By default this method returns the underlying encoding bit width.
    /// If the glyph implements <see cref="T:T2t.Barcode.Svg.IVaryLengthGlyph"/> then the
    /// encoding width is requested from the interface.
    /// </remarks>
    protected virtual int GetEncodingBitCount(Glyph glyph)
    {
        int bitEncodingWidth = this.EncodingBitCount;
        if (glyph is IVaryLengthGlyph)
        {
            IVaryLengthGlyph varyLengthGlyph = (IVaryLengthGlyph)glyph;
            bitEncodingWidth = varyLengthGlyph.BitEncodingWidth;
        }
        return bitEncodingWidth;
    }

    /// <summary>
    /// Gets the glyph's width encoding bit count.
    /// </summary>
    /// <param name="glyph">A <see cref="T:T2t.Barcode.Core.Glyph"/> to be queried.</param>
    /// <returns>Number of bits needed to encode the width of the glyph.</returns>
    /// <remarks>
    /// By default this method returns the underlying width bit count.
    /// </remarks>
    protected virtual int GetWidthBitCount(Glyph glyph)
    {
        int widthBitCount = this.WidthBitCount;
        return widthBitCount;
    }

    /// <summary>
    /// Gets the total width in pixels for the specified barcode glyphs
    /// incorporating the specified inter-glyph spacing.
    /// </summary>
    /// <param name="barcode">
    /// Collection of <see cref="T:T2t.Barcode.Core.Glyph"/> objects to be rendered.
    /// </param>
    /// <param name="interGlyphSpace">Amount of inter-glyph space (in pixels) to be applied.</param>
    /// <returns>Width in pixels.</returns>
    protected float GetBarcodeInterGlyphLength(Glyph[] barcode,
        float interGlyphSpace)
    {
        return (barcode.Length - 1) * interGlyphSpace;
    }

    /// <summary>
    /// Renders the specified bar-code to the specified graphics port.
    /// </summary>
    /// <param name="barcode">A collection of <see cref="T:T2t.Barcode.Core.Glyph"/> objects representing the
    /// barcode to be rendered.</param>
    /// <param name="bounds">The bounding rectangle.</param>
    /// <param name="interGlyphSpace">The inter glyph space in pixels.</param>
    /// <param name="barMinHeight">Minimum bar height in pixels.</param>
    /// <param name="barMinWidth">Small bar width in pixels.</param>
    /// <param name="barMaxWidth">Large bar width in pixels.</param>
    /// <remarks>
    /// This method clears the background and then calls
    /// <see cref="M:RenderBars"/> to perform the actual bar drawing.
    /// </remarks>
    protected virtual List<string> Render(
        Glyph[] barcode,
        T2Rect bounds,
        float interGlyphSpace,
        float barMinHeight,
        float barMinWidth,
        float barMaxWidth)
    {
        // Render the background
        List<string> bars = new()
        {
            string.Format(CultureInfo.InvariantCulture, RectTmpl, bounds.Left, bounds.Top, bounds.Width, bounds.Height, "white")
        };

        // Render the bars
        bars.AddRange(RenderBars(barcode, bounds, interGlyphSpace, barMinHeight,
                barMinWidth, barMaxWidth));

        return bars;
    }

    /// <summary>
    /// Renders the barcode bars.
    /// </summary>
    /// <param name="barcode">A collection of <see cref="T:T2t.Barcode.Core.Glyph"/> objects representing the
    /// barcode to be rendered.</param>
    /// <param name="bounds">The bounding rectangle.</param>
    /// <param name="interGlyphSpace">The inter glyph space in pixels.</param>
    /// <param name="barMinHeight">Minimum bar height in pixels.</param>
    /// <param name="barMinWidth">Small bar width in pixels.</param>
    /// <param name="barMaxWidth">Large bar width in pixels.</param>
    /// <remarks>
    /// By default this method renders each glyph by calling the
    /// <see cref="M:RenderBar"/> method, applying the specified
    /// inter-glyph spacing as necessary.
    /// </remarks>
    protected virtual List<string> RenderBars(
        Glyph[] barcode,
        T2Rect bounds,
        float interGlyphSpace,
        float barMinHeight,
        float barMinWidth,
        float barMaxWidth)
    {
        float barOffset = 0;

        List<string> res = new();

        for (int index = 0; index < barcode.Length; ++index)
        {
            BarGlyph glyph = (BarGlyph)barcode[index];

            res.AddRange(RenderBar(index, glyph, bounds, ref barOffset, barMinHeight,
                barMinWidth, barMaxWidth));

            // Account for inter glyph spacing
            barOffset += interGlyphSpace;
        }
        return res;
    }

    /// <summary>
    /// Renders the bar-code glyph.
    /// </summary>
    /// <param name="glyphIndex">Index of the glyph.</param>
    /// <param name="glyph">A <see cref="T:T2t.Barcode.Core.Glyph"/> object to be rendered.</param>
    /// <param name="bounds">The bounding rectangle.</param>
    /// <param name="barOffset">The bar offset.</param>
    /// <param name="barMinHeight">Minimum bar height in pixels.</param>
    /// <param name="barMinWidth">Small bar width in pixels.</param>
    /// <param name="barMaxWidth">Large bar width in pixels.</param>
    /// <exception cref="T:System.InvalidOperationException">
    /// Thrown if the encoding bit count is zero or variable-pitch
    /// bar rendering is attempted.
    /// </exception>
    protected virtual List<string> RenderBar(
        int glyphIndex,
        BarGlyph glyph,
    T2Rect bounds,
        ref float barOffset,
        float barMinHeight,
        float barMinWidth,
        float barMaxWidth)
    {
        List<string> bars = new();
        // Sanity check
        int encodingBitCount = GetEncodingBitCount(glyph);
        if (encodingBitCount == 0)
        {
            throw new InvalidOperationException(
                "Encoding bit width must be greater than zero.");
        }

        // Allow derived classes to modify the glyph bits
        int glyphBits = GetGlyphEncoding(glyphIndex, glyph);

        // Get glyph height
        float height = GetGlyphHeight(glyph, barMinHeight, bounds.Height);
        if (glyph is IBinaryPitchGlyph binGlyph)
        {

            // Render glyph
            int widthIndex = WidthBitCount - 1;
            bool lastBitState = false;
            for (int bitIndex = encodingBitCount - 1; bitIndex >= 0; --bitIndex)
            {
                int bitMask = 1 << bitIndex;
                float barWidth = barMinWidth;

                bool currentBitState = false;
                if ((bitMask & glyphBits) != 0)
                {
                    currentBitState = true;
                }

                // Adjust the width bit checker
                if (bitIndex < (encodingBitCount - 1) &&
                    lastBitState != currentBitState)
                {
                    --widthIndex;
                }
                lastBitState = currentBitState;

                // Determine width encoding bit mask
                int widthMask = 1 << widthIndex;
                if ((widthMask & binGlyph.WidthEncoding) != 0)
                {
                    barWidth = barMaxWidth;
                }

                if ((binGlyph.BitEncoding & bitMask) != 0)
                {
                    bars.Add(string.Format(RectTmpl, barOffset, bounds.Top, barWidth, height, "black"));
                }

                // Update offset
                barOffset += barWidth;
            }
        }
        else
        {
            for (int bitIndex = encodingBitCount - 1; bitIndex >= 0; --bitIndex)
            {
                int bitMask = 1 << bitIndex;
                if ((glyphBits & bitMask) != 0)
                {
                    bars.Add(string.Format(CultureInfo.InvariantCulture, RectTmpl, barOffset, bounds.Top, barMinWidth, height, "black"));
                }

                // Update offset
                barOffset += barMinWidth;
            }
        }
        return bars;
    }

    /// <summary>
    /// Gets the glyph encoding.
    /// </summary>
    /// <param name="glyphIndex">Index of the glyph.</param>
    /// <param name="glyph">The glyph.</param>
    /// <returns></returns>
    /// <remarks>
    /// By default this method simply returns the glyph bit encoding
    /// however some algorithms may chose to modify the encoding
    /// based on positional information.
    /// </remarks>
    protected virtual int GetGlyphEncoding(int glyphIndex, BarGlyph glyph)
    {
        return glyph.BitEncoding;
    }

    /// <summary>
    /// Gets the height of the glyph.
    /// </summary>
    /// <param name="glyph">A <see cref="T:T2t.Barcode.Core.Glyph"/> to be queried.</param>
    /// <param name="barMinHeight">Minimum bar height in pixels.</param>
    /// <param name="barMaxHeight">Maximum bar height in pixels.</param>
    /// <returns>The height of associated glyph.</returns>
    /// <remarks>
    /// By default this method returns the maximum bar height.
    /// </remarks>
    protected virtual float GetGlyphHeight(Glyph glyph, float barMinHeight, float barMaxHeight)
    {
        return barMaxHeight;
    }
    #endregion
}
