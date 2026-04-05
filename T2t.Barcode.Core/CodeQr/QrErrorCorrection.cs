//-----------------------------------------------------------------------
// <copyright file="QrErrorCorrection.cs" company="Zen Design Corp">
//     Copyright © Zen Design Corp 2012. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace T2t.Barcode.Core.CodeQr;

/// <summary>
/// Defines the QR barcode error correction schemes.
/// </summary>
public enum QrErrorCorrection
{
    /// <summary>
    /// M error correction (Medium: ~15% recovery capacity).
    /// </summary>
    M = 0,

    /// <summary>
    /// L error correction (Low: ~7% recovery capacity).
    /// </summary>
    L = 1,

    /// <summary>
    /// H error correction (High: ~30% recovery capacity).
    /// </summary>
    H = 2,

    /// <summary>
    /// Q error correction (Quartile: ~25% recovery capacity).
    /// </summary>
    Q = 3,
}
