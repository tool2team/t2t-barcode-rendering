//-----------------------------------------------------------------------
// <copyright file="IGlyph.cs" company="Zen Design Corp">
//     Copyright © Zen Design Corp 2008 - 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace T2t.Barcode.Svg
{
    /// <summary>
    /// <c>IGlyph</c> defines barcode for a given character.
    /// </summary>
    public interface IGlyph
    {
        /// <summary>
        /// Gets the character.
        /// </summary>
        /// <value>The character.</value>
        char Character
        {
            get;
        }
    }
}
