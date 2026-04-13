//-----------------------------------------------------------------------
// <copyright file="Code93Encoder.cs" company="Zen Design Corp">
//     Copyright © Zen Design Corp 2008 - 2012. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace T2t.Barcode.Core.Code93;

public static class Code93Encoder
{
    public static Glyph[] Encode(string text, Code93GlyphFactory factory, Code93Checksum checksum)
    {
        List<Glyph> result = [.. factory.GetGlyphs(text)];
        if (checksum != null)
        {
            result.AddRange(checksum.GetChecksum(text));
        }
        result.Insert(0, factory.GetRawGlyph('*'));
        result.Add(factory.GetRawGlyph('*'));
        result.Add(factory.GetRawGlyph('|'));
        return [.. result];
    }
}
