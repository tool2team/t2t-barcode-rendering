//-----------------------------------------------------------------------
// <copyright file="Code128Glyph.cs" company="Zen Design Corp">
//     Copyright © Zen Design Corp 2008 - 2012. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace T2t.Barcode.Core.Code128;

/// <summary>
/// <b>Code128Glyph</b> extends <see cref="T:MultisetGlyph"/> by defining
/// the three Code128 barcode sections.
/// </summary>
public class Code128Glyph : MultisetGlyph
{
    #region Private Fields
    private readonly Code128SpecialGlyph[] _special;
    #endregion

    #region Public Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="Code128Glyph"/> class.
    /// </summary>
    /// <param name="first">The first set character.</param>
    /// <param name="second">The second set character.</param>
    /// <param name="third">The third set character.</param>
    /// <param name="bitEncoding">The glyph bit encoding.</param>
    public Code128Glyph(char first, char second, char third, short bitEncoding)
        : base([first, second, third], bitEncoding)
    {
        _special =
            [
                    Code128SpecialGlyph.None,
                    Code128SpecialGlyph.None,
                    Code128SpecialGlyph.None
            ];
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Code128Glyph"/> class.
    /// </summary>
    /// <param name="first">A <see cref="T:Code128SpecialGlyph"/> 
    /// representing the first set object.</param>
    /// <param name="second">A <see cref="T:Code128SpecialGlyph"/> 
    /// representing the second set object.</param>
    /// <param name="third">The third set character.</param>
    /// <param name="bitEncoding">The glyph bit encoding.</param>
    public Code128Glyph(Code128SpecialGlyph first, Code128SpecialGlyph second, char third, short bitEncoding)
        : base([(char)0, (char)0, third], bitEncoding)
    {
        if (first == Code128SpecialGlyph.None)
        {
            throw new ArgumentException("first cannot be None.");
        }
        if (second == Code128SpecialGlyph.None)
        {
            throw new ArgumentException("second cannot be None.");
        }
        _special =
            [
                    first,
                    second,
                    Code128SpecialGlyph.None
            ];
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Code128Glyph"/> class.
    /// </summary>
    /// <param name="first">A <see cref="T:Code128SpecialGlyph"/> 
    /// representing the first set object.</param>
    /// <param name="second">A <see cref="T:Code128SpecialGlyph"/> 
    /// representing the second set object.</param>
    /// <param name="third">A <see cref="T:Code128SpecialGlyph"/> 
    /// representing the third set object.</param>
    /// <param name="bitEncoding">The glyph bit encoding.</param>
    public Code128Glyph(Code128SpecialGlyph first, Code128SpecialGlyph second, Code128SpecialGlyph third, short bitEncoding)
        : base([(char)0, (char)0, (char)0], bitEncoding)
    {
        if (first == Code128SpecialGlyph.None)
        {
            throw new ArgumentException("first cannot be None.");
        }
        if (second == Code128SpecialGlyph.None)
        {
            throw new ArgumentException("second cannot be None.");
        }
        if (third == Code128SpecialGlyph.None)
        {
            throw new ArgumentException("third cannot be None.");
        }
        _special =
            [
                    first,
                    second,
                    third
            ];
    }
    #endregion

    #region Public Properties

    #endregion

    #region Public Methods
    /// <summary>
    /// Gets the <see cref="T:Code128SpecialGlyph"/> glyph by set.
    /// </summary>
    /// <param name="set">The set.</param>
    /// <returns></returns>
    public Code128SpecialGlyph GetSpecialBySet(int set)
    {
        if (set < 0 || set >= _special.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(set), set,
                "set out of range.");
        }
        return _special[set];
    }
    #endregion
}
