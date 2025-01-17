namespace T2t.Barcode.Core;

/// <summary>
/// <b>BarGlyph</b> extends the <see cref="T:T2t.Barcode.Core.Glyph"/> base
/// class and adds a bit-encoding to describe a bar-code glyph.
/// </summary>
public class BarGlyph : Glyph, IBarGlyph
{
    #region Private Fields
    private readonly short _bitEncoding;
    #endregion

    #region Public Constructors
    /// <summary>
    /// Initialises a new instance of the <see cref="T:T2t.Barcode.Core.Glyph"/> 
    /// class with the specified bit encoding.
    /// </summary>
    /// <param name="character">Character represented by glyph.</param>
    /// <param name="bitEncoding">The bit-encoding value for this character.</param>
    public BarGlyph(char character, short bitEncoding)
        : base(character)
    {
        _bitEncoding = bitEncoding;
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets the bit encoding for this glyph.
    /// </summary>
    /// <value>A <see cref="T:System.Int16"/> representing the bit encoding.</value>
    public short BitEncoding
    {
        get
        {
            return _bitEncoding;
        }
    }
    #endregion
}
