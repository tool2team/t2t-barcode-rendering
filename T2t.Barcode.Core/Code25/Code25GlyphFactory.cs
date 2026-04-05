//-----------------------------------------------------------------------
// <copyright file="Code25GlyphFactory.cs" company="Zen Design Corp">
//     Copyright © Zen Design Corp 2008 - 2012. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace T2t.Barcode.Core.Code25;

public abstract class Code25GlyphFactory : GlyphFactory
{
    #region Private Fields
    private static Code25StandardGlyphFactory _theStdFactory;
    private static Code25InterleavedGlyphFactory _theIntFactory;
    private static readonly object _syncFactory = new();
    #endregion

    #region Protected Constructors
    protected Code25GlyphFactory()
    {
    }
    #endregion

    #region Public Properties
    public static Code25StandardGlyphFactory StandardInstance
    {
        get
        {
            if (_theStdFactory == null)
            {
                lock (_syncFactory)
                {
                    _theStdFactory ??= new Code25StandardGlyphFactory();
                }
            }
            return _theStdFactory;
        }
    }

    public static Code25InterleavedGlyphFactory InterleavedInstance
    {
        get
        {
            if (_theIntFactory == null)
            {
                lock (_syncFactory)
                {
                    _theIntFactory ??= new Code25InterleavedGlyphFactory();
                }
            }
            return _theIntFactory;
        }
    }
    #endregion

    #region Protected Methods
    protected override CompositeGlyph[] GetCompositeGlyphs()
    {
        return Array.Empty<CompositeGlyph>();
    }
    #endregion
}

public sealed class Code25StandardGlyphFactory : Code25GlyphFactory
{
    #region Private Fields
    private BarGlyph[] _glyphs;
    #endregion

    #region Private Constructors
    public Code25StandardGlyphFactory()
    {
    }
    #endregion

    #region Protected Methods
    protected override BarGlyph[] GetGlyphs()
    {
        _glyphs ??=
            [
                    new VaryLengthGlyph ('0', 0x15DD, 13),
                    new VaryLengthGlyph ('1', 0x1D57, 13),
                    new VaryLengthGlyph ('2', 0x1757, 13),
                    new VaryLengthGlyph ('3', 0x1DD5, 13),
                    new VaryLengthGlyph ('4', 0x15D7, 13),
                    new VaryLengthGlyph ('5', 0x1D75, 13),
                    new VaryLengthGlyph ('6', 0x1775, 13),
                    new VaryLengthGlyph ('7', 0x1577, 13),
                    new VaryLengthGlyph ('8', 0x1D5D, 13),
                    new VaryLengthGlyph ('9', 0x175D, 13),
                    new VaryLengthGlyph ('-', 0x006D, 7),
                    new VaryLengthGlyph ('*', 0x006B, 7)
            ];
        return _glyphs;
    }
    #endregion
}

public sealed class Code25InterleavedGlyphFactory : Code25GlyphFactory
{
    #region Private Fields
    private BarGlyph[] _glyphs;
    #endregion

    #region Private Constructors
    public Code25InterleavedGlyphFactory()
    {
    }
    #endregion

    #region Protected Methods
    protected override BarGlyph[] GetGlyphs()
    {
        _glyphs ??=
            [
                    new VaryLengthGlyph ('0', 6, 5),
                    new VaryLengthGlyph ('1', 17, 5),
                    new VaryLengthGlyph ('2', 9, 5),
                    new VaryLengthGlyph ('3', 24, 5),
                    new VaryLengthGlyph ('4', 5, 5),
                    new VaryLengthGlyph ('5', 20, 5),
                    new VaryLengthGlyph ('6', 12, 5),
                    new VaryLengthGlyph ('7', 3, 5),
                    new VaryLengthGlyph ('8', 18, 5),
                    new VaryLengthGlyph ('9', 10, 5),
                    new VaryLengthGlyph ('-', 0x0A, 4),
                    new VaryLengthGlyph ('*', 0x0D, 4)
            ];
        return _glyphs;
    }
    #endregion
}
