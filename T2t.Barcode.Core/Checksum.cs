//-----------------------------------------------------------------------
// <copyright file="Checksum.cs" company="Zen Design Corp">
//     Copyright © Zen Design Corp 2008 - 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace T2t.Barcode.Core;

/// <summary>
/// <b>Checksum</b> defines the base class for generating the bar-code
/// glyphs needed for adding checksum information.
/// </summary>
/// <remarks>
/// To implement a checksum class derived instances must implement the
/// sole abstract method <see cref="M:T2t.Barcode.Core.Checksum.GetChecksum"/>.
/// </remarks>
public abstract class Checksum
{
    #region Protected Constructors
    /// <summary>
    /// Initialises a new instance of <see cref="T:T2t.Barcode.Core.Checksum"/> class.
    /// </summary>
    protected Checksum()
    {
    }
    #endregion

    #region Public Methods
    /// <summary>
    /// Gets an array of <see cref="T:T2t.Barcode.Core.Glyph"/> objects that
    /// represent the checksum for the specified text string.
    /// </summary>
    /// <param name="text">Text to be processed.</param>
    /// <returns>
    /// A collection of <see cref="T:Zen.BarcodeGlyph"/> objects 
    /// representing the checksum information.
    /// </returns>
    public virtual Glyph[] GetChecksum(string text)
    {
        return GetChecksum(text, false);
    }

    /// <summary>
    /// Gets an array of <see cref="T:T2t.Barcode.Core.Glyph"/> objects that
    /// represent the checksum for the specified text string.
    /// </summary>
    /// <param name="text">Text to be processed.</param>
    /// <param name="allowComposite">if set to <c>true</c> to allow use of
    /// composite glyphs.</param>
    /// <returns>
    /// A collection of <see cref="T:T2t.Barcode.Core.Glyph"/> objects 
    /// representing the checksum information.
    /// </returns>
    public virtual Glyph[] GetChecksum(string text, bool allowComposite)
    {
        return Array.Empty<Glyph>();
    }
    #endregion
}

/// <summary>
/// <b>FactoryChecksum</b> defines the base class for all checksum classes
/// that are attached to an object derived from <see cref="T:T2t.Barcode.Core.GlyphFactory"/>
/// for fetching the checksum glyph characters.
/// </summary>
/// <typeparam name="T">
/// A factory class derived from <see cref="T:T2t.Barcode.Core.GlyphFactory"/>.
/// </typeparam>
public abstract class FactoryChecksum<T>
    : Checksum
    where T : GlyphFactory
{
    #region Private Fields
    private readonly T _factory;
    #endregion

    #region Protected Constructors
    /// <summary>
    /// Initialises a new instance of <see cref="T:T2t.Barcode.Core.FactoryChecksum"/>
    /// class with the specified <see cref="T:T2t.Barcode.Core.GlyphFactory"/> derived
    /// object.
    /// </summary>
    /// <param name="factory">
    /// Factory to associate with the checksum generator.
    /// </param>
    protected FactoryChecksum(T factory)
    {
        _factory = factory;
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets the <typeparamref name="T"/> type-safe glyph factory.
    /// </summary>
    /// <value>The <typeparamref name="T" /> factory.</value>
    public T Factory
    {
        get
        {
            return _factory;
        }
    }
    #endregion
}
