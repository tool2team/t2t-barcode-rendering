//-----------------------------------------------------------------------
// <copyright file="CodeEan13GlyphFactory.cs" company="Zen Design Corp">
//     Copyright © Zen Design Corp 2008 - 2012. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace T2t.Barcode.Core.CodeEan13;

/// <summary>
/// <b>CodeEan13Checksum</b> implements a Code EAN-13 checksum generator
/// as a singleton class.
/// </summary>
/// <remarks>
/// This class cannot be inherited from.
/// </remarks>
public sealed class CodeEan13Checksum : FactoryChecksum<CodeEan13GlyphFactory>
{
    #region Private Fields
    private static CodeEan13Checksum _theChecksum;
    private static readonly object _syncChecksum = new();
    #endregion

    #region Private Constructors
    /// <summary>
    /// Initialises a new instance of <see cref="T:CodeEan13Checksum"/> class.
    /// </summary>
    private CodeEan13Checksum()
        : base(CodeEan13GlyphFactory.Instance)
    {
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets the static <see cref="T:CodeEan13Checksum"/> object instance.
    /// </summary>
    public static CodeEan13Checksum Instance
    {
        get
        {
            if (_theChecksum == null)
            {
                lock (_syncChecksum)
                {
                    _theChecksum ??= new CodeEan13Checksum();
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
        char check = (char)(GetChecksumChar(text) - '0' + 'k');
        return Factory.GetGlyphs(check, allowComposite);
    }

    /// <summary>
    /// Gets the checksum char.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <returns></returns>
    public char GetChecksumChar(string text)
    {
        int sum = 0;
        int textLength = text.Length - 1;
        for (int index = 0; index < text.Length; ++index)
        {
            int digitValue = text[textLength - index] - '0';
            if ((index % 2) == 0)
            {
                sum += digitValue * 3;
            }
            else
            {
                sum += digitValue;
            }
        }
        sum %= 10;
        if (sum > 0)
        {
            sum = 10 - sum;
        }
        return (char)('0' + sum);
    }
    #endregion
}
