//-----------------------------------------------------------------------
// <copyright file="Code39Checksum.cs" company="Zen Design Corp">
//     Copyright © Zen Design Corp 2008 - 2012. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace T2t.Barcode.Core.Code39;

public sealed class Code39Checksum : FactoryChecksum<Code39GlyphFactory>
{
    #region Private Fields
    private static Code39Checksum _theChecksum;
    private static readonly object _syncChecksum = new();
    #endregion

    #region Private Constructors
    private Code39Checksum()
        : base(Code39GlyphFactory.Instance)
    {
    }
    #endregion

    #region Public Properties
    public static Code39Checksum Instance
    {
        get
        {
            if (_theChecksum == null)
            {
                lock (_syncChecksum)
                {
                    _theChecksum ??= new Code39Checksum();
                }
            }
            return _theChecksum;
        }
    }
    #endregion

    #region Public Methods
    public override Glyph[] GetChecksum(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            throw new ArgumentNullException(nameof(text));
        }

        char check = GetChecksumChar(text);
        return Factory.GetGlyphs(check);
    }
    #endregion

    #region Private Methods
    private char GetChecksumChar(string text)
    {
        int sum = 0;
        for (int index = 0; index < text.Length; ++index)
        {
            int checkValue = Factory.GetRawCharIndex(text[index]);
            if (checkValue > 42)
            {
                throw new ArgumentException("text string invalid for code39");
            }
            sum += checkValue;
        }
        return Factory.GetRawGlyph(sum % 43).Character;
    }
    #endregion
}
