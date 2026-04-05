//-----------------------------------------------------------------------
// <copyright file="Code93Checksum.cs" company="Zen Design Corp">
//     Copyright © Zen Design Corp 2008 - 2012. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace T2t.Barcode.Core.Code93;

public sealed class Code93Checksum : FactoryChecksum<Code93GlyphFactory>
{
    #region Private Fields
    private static Code93Checksum _theChecksum;
    private static readonly object _syncChecksum = new();
    #endregion

    #region Private Constructors
    private Code93Checksum()
        : base(Code93GlyphFactory.Instance)
    {
    }
    #endregion

    #region Public Properties
    public static Code93Checksum Instance
    {
        get
        {
            if (_theChecksum == null)
            {
                lock (_syncChecksum)
                {
                    _theChecksum ??= new Code93Checksum();
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

        char firstCheck = GetChecksumChar(text, 20);
        text += firstCheck;
        char secondCheck = GetChecksumChar(text, 15);

        List<Glyph> result = new();
        result.Add(Factory.GetRawGlyph(firstCheck));
        result.Add(Factory.GetRawGlyph(secondCheck));
        return result.ToArray();
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
