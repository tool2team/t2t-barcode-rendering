//-----------------------------------------------------------------------
// <copyright file="Code11GlyphFactory.cs" company="Zen Design Corp">
//     Copyright © Zen Design Corp 2008 - 2012. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace T2t.Barcode.Core.Code11;

/// <summary>
/// <b>Code11GlyphFactory</b> concrete implementation of 
/// <see cref="GlyphFactory"/> for providing Code 11 bar-code glyph
/// objects.
/// </summary>
public sealed class Code11GlyphFactory : GlyphFactory
{
    #region Private Fields
    private static Code11GlyphFactory _theFactory;
    private static readonly object _syncFactory = new();

    private BarGlyph[] _glyphs;
    #endregion

    #region Private Constructors
    private Code11GlyphFactory()
    {
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets the global instance.
    /// </summary>
    /// <value>The instance.</value>
    public static Code11GlyphFactory Instance
    {
        get
        {
            if (_theFactory == null)
            {
                lock (_syncFactory)
                {
                    _theFactory ??= new Code11GlyphFactory();
                }
            }
            return _theFactory;
        }
    }
    #endregion

    #region Protected Methods
    /// <summary>
    /// Overridden. Gets the collection of <see cref="T:BarGlyph"/> that
    /// represent the raw bar-code glyphs for the given bar-code symbology.
    /// </summary>
    /// <returns></returns>
    protected override BarGlyph[] GetGlyphs()
    {
        _glyphs ??=
            [
                    new BinaryPitchVaryLengthGlyph ('0', 0x2B, 0x01, 6),
                    new BinaryPitchVaryLengthGlyph ('1', 0x6B, 0x11, 7),
                    new BinaryPitchVaryLengthGlyph ('2', 0x4B, 0x09, 7),
                    new BinaryPitchVaryLengthGlyph ('3', 0x65, 0x18, 7),
                    new BinaryPitchVaryLengthGlyph ('4', 0x5B, 0x05, 7),
                    new BinaryPitchVaryLengthGlyph ('5', 0x6D, 0x14, 7),
                    new BinaryPitchVaryLengthGlyph ('6', 0x4D, 0x0C, 7),
                    new BinaryPitchVaryLengthGlyph ('7', 0x53, 0x03, 7),
                    new BinaryPitchVaryLengthGlyph ('8', 0x69, 0x12, 7),
                    new BinaryPitchVaryLengthGlyph ('9', 0x35, 0x10, 6),
                    new BinaryPitchVaryLengthGlyph ('-', 0x2D, 0x04, 6),
                    new BinaryPitchVaryLengthGlyph ('*', 0x59, 0x06, 7)
            ];
        return _glyphs;
    }

    /// <summary>
    /// Overridden. Gets the collection of <see cref="T:CompositeGlyph"/>
    /// that represent the composite bar-code glyphs for the given 
    /// bar-code symbology.
    /// </summary>
    /// <returns></returns>
    protected override CompositeGlyph[] GetCompositeGlyphs()
    {
        return Array.Empty<CompositeGlyph>();
    }
    #endregion
}
