//-----------------------------------------------------------------------
// <copyright file="CompositeGlyph.cs" company="Zen Design Corp">
//     Copyright © Zen Design Corp 2008 - 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace T2t.Barcode.Core;

/// <summary>
/// Represents a composite glyph.
/// </summary>
/// <remarks>
/// Composite glyphs are used exclusively to represent traps for 
/// implementing full ASCII encoding mechanisms used in a variety of
/// barcode symbologies. Both Code 39 and Code 93 use this scheme.
/// </remarks>
public class CompositeGlyph : Glyph
{
    #region Private Fields
    private readonly BarGlyph _first;
    private readonly BarGlyph _second;
    #endregion

    #region Public Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="T:T2t.Barcode.Drawing.CompositeGlyph"/>
    /// class.
    /// </summary>
    /// <param name="character">The character.</param>
    /// <param name="first">The first.</param>
    /// <param name="second">The second.</param>
    public CompositeGlyph(char character, BarGlyph first, BarGlyph second)
        : base(character)
    {
        _first = first;
        _second = second;
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets the first <see cref="T:T2t.Barcode.Drawing.BarGlyph"/> object.
    /// </summary>
    /// <value>A <see cref="T:T2t.Barcode.Drawing.BarGlyph"/>.</value>
    public BarGlyph First
    {
        get
        {
            return _first;
        }
    }

    /// <summary>
    /// Gets the second <see cref="T:T2t.Barcode.Drawing.BarGlyph"/> object.
    /// </summary>
    /// <value>A <see cref="T:T2t.Barcode.Drawing.BarGlyph"/>.</value>
    public BarGlyph Second
    {
        get
        {
            return _second;
        }
    }
    #endregion
}
