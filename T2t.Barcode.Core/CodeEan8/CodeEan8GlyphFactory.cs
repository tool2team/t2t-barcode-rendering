//-----------------------------------------------------------------------
// <copyright file="CodeEan8GlyphFactory.cs" company="Zen Design Corp">
//     Copyright © Zen Design Corp 2008 - 2012. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace T2t.Barcode.Core.CodeEan8;

public sealed class CodeEan8GlyphFactory : GlyphFactory
{
    #region Private Fields
    private static CodeEan8GlyphFactory _theFactory;
    private static readonly object _syncFactory = new();

    private BarGlyph[] _glyphs;
    private CompositeGlyph[] _compositeGlyphs;
    #endregion

    #region Private Constructors
    private CodeEan8GlyphFactory()
    {
    }
    #endregion

    #region Public Properties
    public static CodeEan8GlyphFactory Instance
    {
        get
        {
            if (_theFactory == null)
            {
                lock (_syncFactory)
                {
                    _theFactory ??= new CodeEan8GlyphFactory();
                }
            }
            return _theFactory;
        }
    }
    #endregion

    #region Protected Methods
    public override Glyph[] GetGlyphs(string text, bool allowComposite)
    {
        List<Glyph> result = [];
        for (int index = 0; index < text.Length; ++index)
        {
            int digit = text[index] - '0';
            if (digit < 0 || digit > 9)
            {
                throw new ArgumentException("EAN13 barcode invalid.");
            }
            result.AddRange(GetGlyphs(text[index], allowComposite));
        }
        return [.. result];
    }

    protected override BarGlyph[] GetGlyphs()
    {
        _glyphs ??=
            [
                    new VaryLengthGlyph ('*', 0x05, 3),
                    new VaryLengthGlyph ('|', 0x0A, 5),
                    new BarGlyph ('A', 0x0D),
                    new BarGlyph ('B', 0x19),
                    new BarGlyph ('C', 0x13),
                    new BarGlyph ('D', 0x3D),
                    new BarGlyph ('E', 0x23),
                    new BarGlyph ('F', 0x31),
                    new BarGlyph ('G', 0x2F),
                    new BarGlyph ('H', 0x3B),
                    new BarGlyph ('I', 0x37),
                    new BarGlyph ('J', 0x0B),
                    new BarGlyph ('a', 0x72),
                    new BarGlyph ('b', 0x66),
                    new BarGlyph ('c', 0x6C),
                    new BarGlyph ('d', 0x42),
                    new BarGlyph ('e', 0x5C),
                    new BarGlyph ('f', 0x4E),
                    new BarGlyph ('g', 0x50),
                    new BarGlyph ('h', 0x44),
                    new BarGlyph ('i', 0x48),
                    new BarGlyph ('j', 0x74),
            ];
        return _glyphs;
    }

    protected override CompositeGlyph[] GetCompositeGlyphs()
    {
        _compositeGlyphs ??=
            [
                    new CompositeGlyph ('0', GetRawGlyph('A'), GetRawGlyph ('a')),
                    new CompositeGlyph ('1', GetRawGlyph('B'), GetRawGlyph ('b')),
                    new CompositeGlyph ('2', GetRawGlyph('C'), GetRawGlyph ('c')),
                    new CompositeGlyph ('3', GetRawGlyph('D'), GetRawGlyph ('d')),
                    new CompositeGlyph ('4', GetRawGlyph('E'), GetRawGlyph ('e')),
                    new CompositeGlyph ('5', GetRawGlyph('F'), GetRawGlyph ('f')),
                    new CompositeGlyph ('6', GetRawGlyph('G'), GetRawGlyph ('g')),
                    new CompositeGlyph ('7', GetRawGlyph('H'), GetRawGlyph ('h')),
                    new CompositeGlyph ('8', GetRawGlyph('I'), GetRawGlyph ('i')),
                    new CompositeGlyph ('9', GetRawGlyph('J'), GetRawGlyph ('j')),
            ];
        return _compositeGlyphs;
    }
    #endregion
}
