//-----------------------------------------------------------------------
// <copyright file="IBarGlyph.cs" company="Zen Design Corp">
//     Copyright © Zen Design Corp 2008 - 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace T2t.Barcode.Svg
{
    /// <summary>
    /// <c>IBarGlyph</c> extends <see cref="T:T2t.Barcode.Skia.IGlyph"/> by 
    /// specifying a bit encoding for a given character. 
    /// The bits indicate where bars are drawn.
    /// </summary>
    public interface IBarGlyph : IGlyph
    {
        /// <summary>
        /// Gets the bit encoding.
        /// </summary>
        /// <value>The bit encoding.</value>
        short BitEncoding
        {
            get;
        }
    }
}
