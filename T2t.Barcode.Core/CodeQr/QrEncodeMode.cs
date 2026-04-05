//-----------------------------------------------------------------------
// <copyright file="QrEncodeMode.cs" company="Zen Design Corp">
//     Copyright © Zen Design Corp 2012. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace T2t.Barcode.Core.CodeQr;

/// <summary>
/// Defines the QR barcode encoding modes.
/// </summary>
public enum QrEncodeMode
{
    /// <summary>
    /// Suitable for encoding any data.
    /// </summary>
    Byte = 0,

    /// <summary>
    /// Suitable for encoding numeric data.
    /// </summary>
    Numeric = 1,

    /// <summary>
    /// Suitable for encoding alpha-numeric data.
    /// </summary>
    AlphaNumeric = 2,
}
