//-----------------------------------------------------------------------
// <copyright file="CodeEan8Checksum.cs" company="Zen Design Corp">
//     Copyright © Zen Design Corp 2008 - 2012. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace T2t.Barcode.Core.CodeEan8;

public sealed class CodeEan8Checksum : FactoryChecksum<CodeEan8GlyphFactory>
{
    #region Private Fields
    private static CodeEan8Checksum _theChecksum;
    private static readonly object _syncChecksum = new();
    #endregion

    #region Private Constructors
    private CodeEan8Checksum()
        : base(CodeEan8GlyphFactory.Instance)
    {
    }
    #endregion

    #region Public Properties
    public static CodeEan8Checksum Instance
    {
        get
        {
            if (_theChecksum == null)
            {
                lock (_syncChecksum)
                {
                    _theChecksum ??= new CodeEan8Checksum();
                }
            }
            return _theChecksum;
        }
    }
    #endregion

    #region Public Methods
    public override Glyph[] GetChecksum(string text, bool allowComposite)
    {
        if (string.IsNullOrEmpty(text))
        {
            throw new ArgumentNullException(nameof(text));
        }

        char check = GetChecksumChar(text);
        return Factory.GetGlyphs(check, allowComposite);
    }

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
