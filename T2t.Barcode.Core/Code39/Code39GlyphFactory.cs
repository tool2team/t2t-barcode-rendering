//-----------------------------------------------------------------------
// <copyright file="Code39GlyphFactory.cs" company="Zen Design Corp">
//     Copyright © Zen Design Corp 2008 - 2012. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace T2t.Barcode.Core.Code39;

public sealed class Code39GlyphFactory : GlyphFactory
{
    #region Private Fields
    private static Code39GlyphFactory _theFactory;
    private static readonly object _syncFactory = new();

    private BarGlyph[] _glyphs;
    private CompositeGlyph[] _compositeGlyphs;
    #endregion

    #region Private Constructors
    private Code39GlyphFactory()
    {
    }
    #endregion

    #region Public Properties
    public static Code39GlyphFactory Instance
    {
        get
        {
            if (_theFactory == null)
            {
                lock (_syncFactory)
                {
                    _theFactory ??= new Code39GlyphFactory();
                }
            }
            return _theFactory;
        }
    }
    #endregion

    #region Protected Methods
    protected override BarGlyph[] GetGlyphs()
    {
        _glyphs ??=
            [
                    new BinaryPitchGlyph ('0', 0xA6D, 0x034),
                    new BinaryPitchGlyph ('1', 0xD2B, 0x121),
                    new BinaryPitchGlyph ('2', 0xB2B, 0x061),
                    new BinaryPitchGlyph ('3', 0xD95, 0x160),
                    new BinaryPitchGlyph ('4', 0xA6B, 0x031),
                    new BinaryPitchGlyph ('5', 0xD35, 0x130),
                    new BinaryPitchGlyph ('6', 0xB35, 0x070),
                    new BinaryPitchGlyph ('7', 0xA5B, 0x025),
                    new BinaryPitchGlyph ('8', 0xD2D, 0x124),
                    new BinaryPitchGlyph ('9', 0xB2D, 0x064),
                    new BinaryPitchGlyph ('A', 0xD4B, 0x109),
                    new BinaryPitchGlyph ('B', 0xB4B, 0x049),
                    new BinaryPitchGlyph ('C', 0xDA5, 0x148),
                    new BinaryPitchGlyph ('D', 0xACB, 0x019),
                    new BinaryPitchGlyph ('E', 0xD65, 0x118),
                    new BinaryPitchGlyph ('F', 0xB65, 0x058),
                    new BinaryPitchGlyph ('G', 0xA9B, 0x00D),
                    new BinaryPitchGlyph ('H', 0xD4D, 0x10C),
                    new BinaryPitchGlyph ('I', 0xB4D, 0x04C),
                    new BinaryPitchGlyph ('J', 0xACD, 0x01C),
                    new BinaryPitchGlyph ('K', 0xD53, 0x103),
                    new BinaryPitchGlyph ('L', 0xB53, 0x043),
                    new BinaryPitchGlyph ('M', 0xDA9, 0x142),
                    new BinaryPitchGlyph ('N', 0xAD3, 0x013),
                    new BinaryPitchGlyph ('O', 0xD69, 0x112),
                    new BinaryPitchGlyph ('P', 0xB69, 0x052),
                    new BinaryPitchGlyph ('Q', 0xAB3, 0x007),
                    new BinaryPitchGlyph ('R', 0xD59, 0x106),
                    new BinaryPitchGlyph ('S', 0xB59, 0x046),
                    new BinaryPitchGlyph ('T', 0xAD9, 0x016),
                    new BinaryPitchGlyph ('U', 0xCAB, 0x181),
                    new BinaryPitchGlyph ('V', 0x9AB, 0x0C1),
                    new BinaryPitchGlyph ('W', 0xCD5, 0x1C0),
                    new BinaryPitchGlyph ('X', 0x96B, 0x091),
                    new BinaryPitchGlyph ('Y', 0xCB5, 0x190),
                    new BinaryPitchGlyph ('Z', 0x9B5, 0x0D0),
                    new BinaryPitchGlyph ('-', 0x95B, 0x085),
                    new BinaryPitchGlyph ('.', 0xCAD, 0x184),
                    new BinaryPitchGlyph (' ', 0x9AD, 0x0C4),
                    new BinaryPitchGlyph ('$', 0x925, 0x0A8),
                    new BinaryPitchGlyph ('/', 0x929, 0x0A2),
                    new BinaryPitchGlyph ('+', 0x949, 0x08A),
                    new BinaryPitchGlyph ('%', 0xA49, 0x02A),
                    new BinaryPitchGlyph ('*', 0x96D, 0x094),
            ];
        return _glyphs;
    }

    protected override CompositeGlyph[] GetCompositeGlyphs()
    {
        _compositeGlyphs ??=
            [
                    new CompositeGlyph ((char) 0, GetRawGlyph('%'), GetRawGlyph ('U')),
                    new CompositeGlyph ((char) 1, GetRawGlyph('$'), GetRawGlyph ('A')),
                    new CompositeGlyph ((char) 2, GetRawGlyph('$'), GetRawGlyph ('B')),
                    new CompositeGlyph ((char) 3, GetRawGlyph('$'), GetRawGlyph ('C')),
                    new CompositeGlyph ((char) 4, GetRawGlyph('$'), GetRawGlyph ('D')),
                    new CompositeGlyph ((char) 5, GetRawGlyph('$'), GetRawGlyph ('E')),
                    new CompositeGlyph ((char) 6, GetRawGlyph('$'), GetRawGlyph ('F')),
                    new CompositeGlyph ((char) 7, GetRawGlyph('$'), GetRawGlyph ('G')),
                    new CompositeGlyph ((char) 8, GetRawGlyph('$'), GetRawGlyph ('H')),
                    new CompositeGlyph ((char) 9, GetRawGlyph('$'), GetRawGlyph ('I')),
                    new CompositeGlyph ((char) 10, GetRawGlyph('$'), GetRawGlyph ('J')),
                    new CompositeGlyph ((char) 11, GetRawGlyph('$'), GetRawGlyph ('K')),
                    new CompositeGlyph ((char) 12, GetRawGlyph('$'), GetRawGlyph ('L')),
                    new CompositeGlyph ((char) 13, GetRawGlyph('$'), GetRawGlyph ('M')),
                    new CompositeGlyph ((char) 14, GetRawGlyph('$'), GetRawGlyph ('N')),
                    new CompositeGlyph ((char) 15, GetRawGlyph('$'), GetRawGlyph ('O')),
                    new CompositeGlyph ((char) 16, GetRawGlyph('$'), GetRawGlyph ('P')),
                    new CompositeGlyph ((char) 17, GetRawGlyph('$'), GetRawGlyph ('Q')),
                    new CompositeGlyph ((char) 18, GetRawGlyph('$'), GetRawGlyph ('R')),
                    new CompositeGlyph ((char) 19, GetRawGlyph('$'), GetRawGlyph ('S')),
                    new CompositeGlyph ((char) 20, GetRawGlyph('$'), GetRawGlyph ('T')),
                    new CompositeGlyph ((char) 21, GetRawGlyph('$'), GetRawGlyph ('U')),
                    new CompositeGlyph ((char) 22, GetRawGlyph('$'), GetRawGlyph ('V')),
                    new CompositeGlyph ((char) 23, GetRawGlyph('$'), GetRawGlyph ('W')),
                    new CompositeGlyph ((char) 24, GetRawGlyph('$'), GetRawGlyph ('X')),
                    new CompositeGlyph ((char) 25, GetRawGlyph('$'), GetRawGlyph ('Y')),
                    new CompositeGlyph ((char) 26, GetRawGlyph('$'), GetRawGlyph ('Z')),
                    new CompositeGlyph ((char) 27, GetRawGlyph('%'), GetRawGlyph ('A')),
                    new CompositeGlyph ((char) 28, GetRawGlyph('%'), GetRawGlyph ('B')),
                    new CompositeGlyph ((char) 29, GetRawGlyph('%'), GetRawGlyph ('C')),
                    new CompositeGlyph ((char) 30, GetRawGlyph('%'), GetRawGlyph ('D')),
                    new CompositeGlyph ((char) 31, GetRawGlyph('%'), GetRawGlyph ('E')),
                    new CompositeGlyph (';', GetRawGlyph('%'), GetRawGlyph ('F')),
                    new CompositeGlyph ('<', GetRawGlyph('%'), GetRawGlyph ('G')),
                    new CompositeGlyph ('=', GetRawGlyph('%'), GetRawGlyph ('H')),
                    new CompositeGlyph ('>', GetRawGlyph('%'), GetRawGlyph ('I')),
                    new CompositeGlyph ('?', GetRawGlyph('%'), GetRawGlyph ('J')),
                    new CompositeGlyph ('[', GetRawGlyph('%'), GetRawGlyph ('K')),
                    new CompositeGlyph ('\\', GetRawGlyph('%'), GetRawGlyph ('L')),
                    new CompositeGlyph (']', GetRawGlyph('%'), GetRawGlyph ('M')),
                    new CompositeGlyph ('^', GetRawGlyph('%'), GetRawGlyph ('N')),
                    new CompositeGlyph ('_', GetRawGlyph('%'), GetRawGlyph ('O')),
                    new CompositeGlyph ('{', GetRawGlyph('%'), GetRawGlyph ('P')),
                    new CompositeGlyph ('|', GetRawGlyph('%'), GetRawGlyph ('Q')),
                    new CompositeGlyph ('}', GetRawGlyph('%'), GetRawGlyph ('R')),
                    new CompositeGlyph ('~', GetRawGlyph('%'), GetRawGlyph ('S')),
                    new CompositeGlyph ('@', GetRawGlyph('%'), GetRawGlyph ('V')),
                    new CompositeGlyph ('`', GetRawGlyph('%'), GetRawGlyph ('W')),
                    new CompositeGlyph ((char) 127, GetRawGlyph('%'), GetRawGlyph ('Z')),
                    new CompositeGlyph ('!', GetRawGlyph('/'), GetRawGlyph ('A')),
                    new CompositeGlyph ('\"', GetRawGlyph('/'), GetRawGlyph ('B')),
                    new CompositeGlyph ('#', GetRawGlyph('/'), GetRawGlyph ('C')),
                    new CompositeGlyph ('$', GetRawGlyph('/'), GetRawGlyph ('D')),
                    new CompositeGlyph ('%', GetRawGlyph('/'), GetRawGlyph ('E')),
                    new CompositeGlyph ('&', GetRawGlyph('/'), GetRawGlyph ('F')),
                    new CompositeGlyph ('\'', GetRawGlyph('/'), GetRawGlyph ('G')),
                    new CompositeGlyph ('(', GetRawGlyph('/'), GetRawGlyph ('H')),
                    new CompositeGlyph (')', GetRawGlyph('/'), GetRawGlyph ('I')),
                    new CompositeGlyph ('*', GetRawGlyph('/'), GetRawGlyph ('J')),
                    new CompositeGlyph ('+', GetRawGlyph('/'), GetRawGlyph ('K')),
                    new CompositeGlyph (',', GetRawGlyph('/'), GetRawGlyph ('L')),
                    new CompositeGlyph ('a', GetRawGlyph('+'), GetRawGlyph ('A')),
                    new CompositeGlyph ('b', GetRawGlyph('+'), GetRawGlyph ('B')),
                    new CompositeGlyph ('c', GetRawGlyph('+'), GetRawGlyph ('C')),
                    new CompositeGlyph ('d', GetRawGlyph('+'), GetRawGlyph ('D')),
                    new CompositeGlyph ('e', GetRawGlyph('+'), GetRawGlyph ('E')),
                    new CompositeGlyph ('f', GetRawGlyph('+'), GetRawGlyph ('F')),
                    new CompositeGlyph ('g', GetRawGlyph('+'), GetRawGlyph ('G')),
                    new CompositeGlyph ('h', GetRawGlyph('+'), GetRawGlyph ('H')),
                    new CompositeGlyph ('i', GetRawGlyph('+'), GetRawGlyph ('I')),
                    new CompositeGlyph ('j', GetRawGlyph('+'), GetRawGlyph ('J')),
                    new CompositeGlyph ('k', GetRawGlyph('+'), GetRawGlyph ('K')),
                    new CompositeGlyph ('l', GetRawGlyph('+'), GetRawGlyph ('L')),
                    new CompositeGlyph ('m', GetRawGlyph('+'), GetRawGlyph ('M')),
                    new CompositeGlyph ('n', GetRawGlyph('+'), GetRawGlyph ('N')),
                    new CompositeGlyph ('o', GetRawGlyph('+'), GetRawGlyph ('O')),
                    new CompositeGlyph ('p', GetRawGlyph('+'), GetRawGlyph ('P')),
                    new CompositeGlyph ('q', GetRawGlyph('+'), GetRawGlyph ('Q')),
                    new CompositeGlyph ('r', GetRawGlyph('+'), GetRawGlyph ('R')),
                    new CompositeGlyph ('s', GetRawGlyph('+'), GetRawGlyph ('S')),
                    new CompositeGlyph ('t', GetRawGlyph('+'), GetRawGlyph ('T')),
                    new CompositeGlyph ('u', GetRawGlyph('+'), GetRawGlyph ('U')),
                    new CompositeGlyph ('v', GetRawGlyph('+'), GetRawGlyph ('V')),
                    new CompositeGlyph ('w', GetRawGlyph('+'), GetRawGlyph ('W')),
                    new CompositeGlyph ('x', GetRawGlyph('+'), GetRawGlyph ('X')),
                    new CompositeGlyph ('y', GetRawGlyph('+'), GetRawGlyph ('Y')),
                    new CompositeGlyph ('z', GetRawGlyph('+'), GetRawGlyph ('Z'))
            ];
        return _compositeGlyphs;
    }
    #endregion
}
