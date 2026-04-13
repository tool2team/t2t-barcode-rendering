//-----------------------------------------------------------------------
// 
//-----------------------------------------------------------------------

namespace T2t.Barcode.Core.Code25;

/// <summary>
/// Provides Code25 encoding logic shared across all rendering platforms.
/// </summary>
public static class Code25Encoder
{
    /// <summary>
    /// Encodes text into Code25 glyphs including optional checksum and start/stop characters.
    /// </summary>
    /// <param name="text">The text to encode.</param>
    /// <param name="factory">The glyph factory.</param>
    /// <param name="checksum">The checksum calculator (optional).</param>
    /// <returns>An array of glyphs representing the complete Code25 barcode.</returns>
    public static Glyph[] Encode(string text, Code25GlyphFactory factory, Code25Checksum checksum)
    {
        List<Glyph> result = [];

        if (factory is Code25InterleavedGlyphFactory)
        {
            bool isOddLength = false;
            if ((text.Length % 2) == 1)
            {
                isOddLength = true;
            }

            if (isOddLength)
            {
                text = "0" + text;
            }
        }

        result.AddRange(factory.GetGlyphs(text));
        if (checksum != null)
        {
            result.AddRange(checksum.GetChecksum(text));
        }

        result.Insert(0, factory.GetRawGlyph('-'));
        result.Add(factory.GetRawGlyph('*'));
        return [.. result];
    }
}
