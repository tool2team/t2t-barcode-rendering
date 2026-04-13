//-----------------------------------------------------------------------
// 
//-----------------------------------------------------------------------

namespace T2t.Barcode.Core.Code11;

/// <summary>
/// Provides Code11 encoding logic shared across all rendering platforms.
/// </summary>
public static class Code11Encoder
{
    /// <summary>
    /// Encodes text into Code11 glyphs including optional checksum and start/stop characters.
    /// </summary>
    /// <param name="text">The text to encode.</param>
    /// <param name="factory">The glyph factory.</param>
    /// <param name="checksum">The checksum calculator (optional).</param>
    /// <returns>An array of glyphs representing the complete Code11 barcode.</returns>
    public static Glyph[] Encode(string text, Code11GlyphFactory factory, Code11Checksum checksum)
    {
        List<Glyph> result = new();
        result.AddRange(factory.GetGlyphs(text));
        if (checksum != null)
        {
            result.AddRange(checksum.GetChecksum(text));
        }
        result.Insert(0, factory.GetRawGlyph('*'));
        result.Add(factory.GetRawGlyph('*'));
        return result.ToArray();
    }
}
