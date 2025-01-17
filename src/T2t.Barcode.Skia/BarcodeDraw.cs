//-----------------------------------------------------------------------
// <copyright file="BarcodeDraw.cs" company="Zen Design Corp">
//     Copyright � Zen Design Corp 2008 - 2012. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------


using SkiaSharp;
using T2t.Barcode.Core;

namespace T2t.Barcode.Skia;

/// <summary>
/// <c>BarcodeDraw</c> is an abstract base class for all barcode drawing
/// classes.
/// </summary>
public abstract class BarcodeDraw
{
    #region Protected Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="T:T2t.Barcode.Skia.BarcodeDraw"/> class.
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
    /// <param name="metrics">A <see cref="T:T2t.Barcode.Skia.BarcodeMetrics"/> object.</param>
    /// <returns>
    /// An <see cref="SKImage"/> object containing the rendered barcode.
    /// </returns>
    public abstract SKBitmap Draw(string text, BarcodeMetrics metrics);

    /// <summary>
    /// Draws the specified text using the default barcode metrics for
    /// the specified maximum barcode height.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="maxBarHeight">The maximum bar height.</param>
    /// <returns>
    /// An <see cref="SKImage"/> object containing the rendered barcode.
    /// </returns>
    public SKBitmap Draw(string text, int maxBarHeight)
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
    /// An <see cref="SKImage"/> object containing the rendered barcode.
    /// </returns>
    public SKBitmap Draw(string text, int maxBarHeight, float scale)
    {
        BarcodeMetrics defaultMetrics = GetDefaultMetrics(maxBarHeight);
        defaultMetrics.Scale = scale;
        return Draw(text, defaultMetrics);
    }

    /// <summary>
    /// Gets a <see cref="T:T2t.Barcode.Skia.BarcodeMetrics"/> object containing default
    /// settings for the specified maximum bar height.
    /// </summary>
    /// <param name="maxHeight">The maximum barcode height.</param>
    /// <returns>A <see cref="T:T2t.Barcode.Skia.BarcodeMetrics"/> object.</returns>
    public abstract BarcodeMetrics GetDefaultMetrics(int maxHeight);

    /// <summary>
    /// Gets a <see cref="T:BarcodeMetrics"/> object containing the print
    /// metrics needed for printing a barcode of the specified physical
    /// size on a device operating at the specified resolution.
    /// </summary>
    /// <param name="desiredBarcodeDimensions">The desired barcode dimensions in hundredth of an inch.</param>
    /// <param name="printResolution">The print resolution in pixels per inch.</param>
    /// <param name="barcodeCharLength">Length of the barcode in characters.</param>
    /// <returns>A <see cref="T:T2t.Barcode.Skia.BarcodeMetrics"/> object.</returns>
    public abstract BarcodeMetrics GetPrintMetrics(
        SKSizeI desiredBarcodeDimensions, SKSizeI printResolution,
        int barcodeCharLength);
    #endregion
}

/// <summary>
/// <b>BarcodeDrawBase</b> deals with rendering a barcode using the associated
/// glyph factory and optional checksum generator classes.
/// </summary>
/// <typeparam name="TGlyphFactory">
/// A <see cref="T:T2t.Barcode.Skia.GlyphFactory"/> derived type.
/// </typeparam>
/// <typeparam name="TChecksum">
/// A <see cref="T:T2t.Barcode.Skia.Checksum"/> derived type.
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
    /// Initialises an instance of <see cref="T:T2t.Barcode.Skia.BarcodeDraw"/>.
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
    /// Initialises an instance of <see cref="T:T2t.Barcode.Skia.BarcodeDraw"/>.
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
    /// Initialises an instance of <see cref="T:T2t.Barcode.Skia.BarcodeDraw"/>.
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
    /// Initialises an instance of <see cref="T:T2t.Barcode.Skia.BarcodeDraw"/>.
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
    /// <param name="metrics">A <see cref="T:T2t.Barcode.Skia.BarcodeMetrics"/> object.</param>
    /// <returns></returns>
    public override sealed SKBitmap Draw(string text, BarcodeMetrics metrics)
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
    /// <param name="metrics">A <see cref="T:T2t.Barcode.Skia.BarcodeMetrics"/> object.</param>
    /// <returns>
    /// An <see cref="SKImage"/> object containing the rendered barcode.
    /// </returns>
    protected virtual SKBitmap Draw1d(string text, BarcodeMetrics1d metrics)
    {
        // Determine number of pixels required for final image
        Glyph[] barcode = GetFullBarcode(text);

        // Determine amount of inter-glyph space
        int interGlyphSpace;
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
        int totalImageWidth = GetBarcodeLength(
                barcode,
                (int)(interGlyphSpace * metrics.Scale),
                (int)(metrics.MinWidth * metrics.Scale),
                (int)(metrics.MaxWidth * metrics.Scale));

        // Create image of correct size
        SKBitmap image = new(totalImageWidth, metrics.MaxHeight);
        using SKCanvas dc = new(image);
        SKRectI bounds = new(0, 0, totalImageWidth, metrics.MaxHeight);
        Render(
            barcode,
            dc,
            bounds,
            (int)(interGlyphSpace * metrics.Scale),
            metrics.MinHeight,
            (int)(metrics.MinWidth * metrics.Scale),
            (int)(metrics.MaxWidth * metrics.Scale));

        // Handle rotation of image as necessary
        if (metrics.RenderVertically)
        {
            // TODO image.RotateFlip(RotateFlipType.Rotate90FlipNone);
        }

        return image;
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
    protected virtual int GetDefaultInterGlyphSpace(
    int barMinWidth, int barMaxWidth)
    {
        return 0;
    }

    /// <summary>
    /// Gets the glyphs needed to render a full barcode.
    /// </summary>
    /// <param name="text">Text to convert into bar-code.</param>
    /// <returns>A collection of <see cref="T:T2t.Barcode.Skia.Glyph"/> objects.</returns>
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
    protected virtual int GetBarcodeLength(
        Glyph[] barcode, int interGlyphSpace, int barMinWidth, int barMaxWidth)
    {
        // Determine bar code length in pixels
        int totalImageWidth = GetBarcodeInterGlyphLength(barcode, interGlyphSpace);
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
    /// <param name="glyph">A <see cref="T:T2t.Barcode.Skia.Glyph"/> to be queried.</param>
    /// <returns>Number of bits needed to encode the glyph.</returns>
    /// <remarks>
    /// By default this method returns the underlying encoding bit width.
    /// If the glyph implements <see cref="T:T2t.Barcode.Skia.IVaryLengthGlyph"/> then the
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
    /// <param name="glyph">A <see cref="T:T2t.Barcode.Skia.Glyph"/> to be queried.</param>
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
    /// Collection of <see cref="T:T2t.Barcode.Skia.Glyph"/> objects to be rendered.
    /// </param>
    /// <param name="interGlyphSpace">Amount of inter-glyph space (in pixels) to be applied.</param>
    /// <returns>Width in pixels.</returns>
    protected int GetBarcodeInterGlyphLength(Glyph[] barcode,
    int interGlyphSpace)
    {
        return (barcode.Length - 1) * interGlyphSpace;
    }

    /// <summary>
    /// Renders the specified bar-code to the specified graphics port.
    /// </summary>
    /// <param name="barcode">A collection of <see cref="T:T2t.Barcode.Skia.Glyph"/> objects representing the
    /// barcode to be rendered.</param>
    /// <param name="dc">A <see cref="T:System.Drawing.SKCanvas"/> representing the draw context.</param>
    /// <param name="bounds">The bounding rectangle.</param>
    /// <param name="interGlyphSpace">The inter glyph space in pixels.</param>
    /// <param name="barMinHeight">Minimum bar height in pixels.</param>
    /// <param name="barMinWidth">Small bar width in pixels.</param>
    /// <param name="barMaxWidth">Large bar width in pixels.</param>
    /// <remarks>
    /// This method clears the background and then calls
    /// <see cref="M:RenderBars"/> to perform the actual bar drawing.
    /// </remarks>
    protected virtual void Render(
        Glyph[] barcode,
        SKCanvas dc,
        SKRectI bounds,
        int interGlyphSpace,
        int barMinHeight,
        int barMinWidth,
        int barMaxWidth)
    {
        // Render the background
        using SKPaint paint = new() { Color = SKColors.White };
        dc.DrawRect(bounds, paint);

        // Render the bars
        RenderBars(barcode, dc, bounds, interGlyphSpace, barMinHeight,
            barMinWidth, barMaxWidth);
    }

    /// <summary>
    /// Renders the barcode bars.
    /// </summary>
    /// <param name="barcode">A collection of <see cref="T:T2t.Barcode.Skia.Glyph"/> objects representing the
    /// barcode to be rendered.</param>
    /// <param name="dc">A <see cref="T:System.Drawing.SKCanvas"/> representing the draw context.</param>
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
    protected virtual void RenderBars(
        Glyph[] barcode,
        SKCanvas dc,
        SKRectI bounds,
        int interGlyphSpace,
        int barMinHeight,
        int barMinWidth,
        int barMaxWidth)
    {
        int barOffset = 0;
        using SKPaint paint = new() { Color = SKColors.Black };
        for (int index = 0; index < barcode.Length; ++index)
        {
            BarGlyph glyph = (BarGlyph)barcode[index];

            RenderBar(index, glyph, dc, paint, bounds, ref barOffset, barMinHeight,
                barMinWidth, barMaxWidth);

            // Account for inter glyph spacing
            barOffset += interGlyphSpace;
        }
    }

    /// <summary>
    /// Renders the bar-code glyph.
    /// </summary>
    /// <param name="glyphIndex">Index of the glyph.</param>
    /// <param name="glyph">A <see cref="T:T2t.Barcode.Skia.Glyph"/> object to be rendered.</param>
    /// <param name="dc">A <see cref="T:System.Drawing.SKCanvas"/> representing the draw context.</param>
    /// <param name="bounds">The bounding rectangle.</param>
    /// <param name="barOffset">The bar offset.</param>
    /// <param name="barMinHeight">Minimum bar height in pixels.</param>
    /// <param name="barMinWidth">Small bar width in pixels.</param>
    /// <param name="barMaxWidth">Large bar width in pixels.</param>
    /// <exception cref="T:System.InvalidOperationException">
    /// Thrown if the encoding bit count is zero or variable-pitch
    /// bar rendering is attempted.
    /// </exception>
    protected virtual void RenderBar(
        int glyphIndex,
        BarGlyph glyph,
        SKCanvas dc,
    SKPaint paint,
    SKRectI bounds,
        ref int barOffset,
        int barMinHeight,
        int barMinWidth,
        int barMaxWidth)
    {
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
        int height = GetGlyphHeight(glyph, barMinHeight, bounds.Height);
        if (glyph is IBinaryPitchGlyph binGlyph)
        {

            // Render glyph
            int widthIndex = WidthBitCount - 1;
            bool lastBitState = false;
            for (int bitIndex = encodingBitCount - 1; bitIndex >= 0; --bitIndex)
            {
                int bitMask = 1 << bitIndex;
                int barWidth = barMinWidth;

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
                    SKRect rect = new(barOffset, bounds.Top, barOffset + barWidth, bounds.Top + height);
                    dc.DrawRect(rect, paint);
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
                    SKRect rect = new(barOffset, bounds.Top, barOffset + barMinWidth, bounds.Top + height);
                    dc.DrawRect(rect, paint);
                }

                // Update offset
                barOffset += barMinWidth;
            }
        }
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
    /// <param name="glyph">A <see cref="T:T2t.Barcode.Skia.Glyph"/> to be queried.</param>
    /// <param name="barMinHeight">Minimum bar height in pixels.</param>
    /// <param name="barMaxHeight">Maximum bar height in pixels.</param>
    /// <returns>The height of associated glyph.</returns>
    /// <remarks>
    /// By default this method returns the maximum bar height.
    /// </remarks>
    protected virtual int GetGlyphHeight(Glyph glyph, int barMinHeight, int barMaxHeight)
    {
        return barMaxHeight;
    }
    #endregion
}
