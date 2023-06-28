//-----------------------------------------------------------------------
// <copyright file="IBinaryPitchGlyph.cs" company="Zen Design Corp">
//     Copyright © Zen Design Corp 2008 - 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace T2t.Barcode.Drawing
{
    /// <summary>
    /// <c>IBinaryPitchGlyph</c> defines the contract with binary pitched
    /// glyphs.
    /// </summary>
    public interface IBinaryPitchGlyph : IBarGlyph
    {
        /// <summary>
        /// Gets the width encoding.
        /// </summary>
        /// <value>The width encoding.</value>
        short WidthEncoding
        {
            get;
        }
    }
}
