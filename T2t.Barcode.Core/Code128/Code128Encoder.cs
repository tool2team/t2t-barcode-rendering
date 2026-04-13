//-----------------------------------------------------------------------
// 
//-----------------------------------------------------------------------

namespace T2t.Barcode.Core.Code128;

/// <summary>
/// Provides Code128 encoding logic shared across all rendering platforms.
/// </summary>
public static class Code128Encoder
{
    /// <summary>
    /// Encodes text into Code128 glyphs including checksum, stop and terminator.
    /// </summary>
    /// <param name="text">The text to encode.</param>
    /// <param name="factory">The glyph factory.</param>
    /// <param name="checksum">The checksum calculator.</param>
    /// <returns>An array of glyphs representing the complete Code128 barcode.</returns>
    public static Glyph[] Encode(string text, Code128GlyphFactory factory, Code128Checksum checksum)
    {
        List<Glyph> result = [.. factory.GetGlyphs(text)];
        if (checksum != null)
        {
            result.AddRange(checksum.GetChecksum(text));
        }
        result.Add(factory.GetRawGlyph(106));   // Stop
        result.Add(factory.GetRawGlyph(107));   // Terminator
        return [.. result];
    }
}
