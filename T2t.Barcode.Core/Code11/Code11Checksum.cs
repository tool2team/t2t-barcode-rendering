//-----------------------------------------------------------------------
// <copyright file="Code11Checksum.cs" company="Zen Design Corp">
//     Copyright © Zen Design Corp 2008 - 2012. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace T2t.Barcode.Core.Code11;

/// <summary>
/// <b>Code11Checksum</b> implements a Code11 checksum generator as a
/// singleton class.
/// </summary>
/// <remarks>
/// This class cannot be inherited from.
/// </remarks>
public sealed class Code11Checksum : FactoryChecksum<Code11GlyphFactory>
{
    #region Private Fields
    private static Code11Checksum _theChecksum;
    private static readonly object _syncChecksum = new();
    #endregion

    #region Private Constructors
    /// <summary>
    /// Initialises a new instance of <see cref="T:Code11Checksum"/> class.
    /// </summary>
    private Code11Checksum()
        : base(Code11GlyphFactory.Instance)
    {
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets the static <see cref="T:Code11Checksum"/> object instance.
    /// </summary>
    public static Code11Checksum Instance
    {
        get
        {
            if (_theChecksum == null)
            {
                lock (_syncChecksum)
                {
                    _theChecksum ??= new Code11Checksum();
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
    /// <param name="text">The text.</param>
    /// <param name="allowComposite">if set to <c>true</c> [allow composite].</param>
    /// <returns>
    /// A collection of <see cref="T:Glyph"/> objects that
    /// represent the checksum.
    /// </returns>
    public override Glyph[] GetChecksum(string text, bool allowComposite)
    {
        if (string.IsNullOrEmpty(text))
        {
            throw new ArgumentNullException(nameof(text));
        }

        // Determine checksum
        if (text.Length > 10)
        {
            char checkC = GetChecksumChar(text, 11);
            text += checkC;
            char checkK = GetChecksumChar(text, 9);
            string checkCK = string.Format("{0}{1}", checkC, checkK);
            return Factory.GetGlyphs(checkCK, allowComposite);
        }
        else
        {
            char checkC = GetChecksumChar(text, 11);
            return Factory.GetGlyphs(checkC, allowComposite);
        }
    }
    #endregion

    #region Private Methods
    private char GetChecksumChar(string text, int weight)
    {
        int sum = 0;
        for (int index = 0; index < text.Length; ++index)
        {
            int checkValue = Factory.GetRawCharIndex(text[text.Length - index - 1]);
            sum += checkValue * ((index % 10) + 1);
        }
        return Factory.GetRawGlyph(sum % weight).Character;
    }
    #endregion
}
