//-----------------------------------------------------------------------
// <copyright file="Code25Checksum.cs" company="Zen Design Corp">
//     Copyright © Zen Design Corp 2008 - 2012. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace T2t.Barcode.Core.Code25;

public sealed class Code25Checksum : FactoryChecksum<Code25GlyphFactory>
{
    #region Private Fields
    private static Code25Checksum _theStdChecksum;
    private static Code25Checksum _theIntChecksum;
    private static readonly object _syncChecksum = new();
    #endregion

    #region Private Constructors
    private Code25Checksum(Code25GlyphFactory factory)
        : base(factory)
    {
    }
    #endregion

    #region Public Properties
    public static Code25Checksum StandardInstance
    {
        get
        {
            if (_theStdChecksum == null)
            {
                lock (_syncChecksum)
                {
                    _theStdChecksum ??= new Code25Checksum(
                            Code25GlyphFactory.StandardInstance);
                }
            }
            return _theStdChecksum;
        }
    }

    public static Code25Checksum InterleavedInstance
    {
        get
        {
            if (_theIntChecksum == null)
            {
                lock (_syncChecksum)
                {
                    _theIntChecksum ??= new Code25Checksum(
                            Code25GlyphFactory.InterleavedInstance);
                }
            }
            return _theIntChecksum;
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

        bool even = true;
        int oddTotal = 0, evenTotal = 0;
        for (int index = text.Length - 1; index >= 0; --index, even = !even)
        {
            char ch = text[index];
            if (!char.IsDigit(ch))
            {
                throw new InvalidOperationException("text contains invalid characters - numbers only.");
            }

            int digit = ch - '0';
            if (even)
            {
                evenTotal += digit * 3;
            }
            else
            {
                oddTotal += digit;
            }
        }

        int checkDigit = 10 - ((evenTotal + oddTotal) % 10);
        return [Factory.GetRawGlyph(checkDigit)];
    }
    #endregion
}
