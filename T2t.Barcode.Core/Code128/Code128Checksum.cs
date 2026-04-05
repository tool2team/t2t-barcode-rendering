//-----------------------------------------------------------------------
// <copyright file="Code128Checksum.cs" company="Zen Design Corp">
//     Copyright © Zen Design Corp 2008 - 2012. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace T2t.Barcode.Core.Code128;

/// <summary>
/// <b>Code128Checksum</b> implements a Code128 checksum generator as a
/// singleton class.
/// </summary>
/// <remarks>
/// This class cannot be inherited from.
/// </remarks>
public sealed class Code128Checksum : FactoryChecksum<Code128GlyphFactory>
{
    #region Private Fields
    private static Code128Checksum _theChecksum;
    private static readonly object _syncChecksum = new();
    #endregion

    #region Private Constructors
    /// <summary>
    /// Initialises a new instance of <see cref="T:Code128Checksum"/> class.
    /// </summary>
    private Code128Checksum()
        : base(Code128GlyphFactory.Instance)
    {
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets the static <see cref="T:Code128Checksum"/> object instance.
    /// </summary>
    public static Code128Checksum Instance
    {
        get
        {
            if (_theChecksum == null)
            {
                lock (_syncChecksum)
                {
                    _theChecksum ??= new Code128Checksum();
                }
            }
            return _theChecksum;
        }
    }
    #endregion

    #region Public Methods
    /// <summary>
    /// Overridden. Gets an array of <see cref="T:Glyph"/> objects that
    /// represent the checksum for the specified text string.
    /// </summary>
    /// <param name="text">Text to be processed.</param>
    /// <param name="allowComposite">if set to <c>true</c> [allow composite].</param>
    /// <returns>
    /// A collection of <see cref="T:Glyph"/> objects representing
    /// the checksum information.
    /// </returns>
    public override Glyph[] GetChecksum(string text, bool allowComposite)
    {
        if (string.IsNullOrEmpty(text))
        {
            throw new ArgumentNullException(nameof(text));
        }

        // Determine checksum
        Glyph[] fullGlyph = Factory.GetGlyphs(text, false);
        long checksum = Factory.GetRawGlyphIndex((BarGlyph)fullGlyph[0]);
        for (int index = 1; index < fullGlyph.Length; ++index)
        {
            checksum += index * Factory.GetRawGlyphIndex(
                (BarGlyph)fullGlyph[index]);
        }
        checksum %= 103;
        return [Factory.GetRawGlyph((int)checksum)];
    }
    #endregion

    #region Private Methods
    private char GetChecksumChar(string text, int weighting)
    {
        int weightedSum = 0;
        for (int index = 0; index < text.Length; ++index)
        {
            int checkValue = Factory.GetRawCharIndex(text[text.Length - index - 1]);
            int weight = (index % weighting) + 1;
            weightedSum += weight * checkValue;
        }
        return Factory.GetRawGlyph(weightedSum % 47).Character;
    }
    #endregion
}
