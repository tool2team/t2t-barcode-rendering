//-----------------------------------------------------------------------
// <copyright file="QRCodeUtility.cs" company="Zen Design Corp">
//     Copyright © Zen Design Corp 2012. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Text;

namespace T2t.Barcode.Core;

/// <summary>
/// Utility class for QR Code encoding operations.
/// Provides methods for Unicode detection and byte array conversions.
/// </summary>
public static class Code2dUtility
{
    /// <summary>
    /// Determines whether the specified string contains Unicode characters.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>true if the string contains Unicode characters; otherwise, false.</returns>
    public static bool IsUnicode(string value)
    {
        byte[] ascii = AsciiStringToByteArray(value);
        byte[] unicode = UnicodeStringToByteArray(value);
        string value1 = FromASCIIByteArray(ascii);
        string value2 = FromUnicodeByteArray(unicode);
        if (value1 != value2)
            return true;
        return false;
    }

    /// <summary>
    /// Determines whether the specified byte array contains Unicode characters.
    /// </summary>
    /// <param name="byteData">The byte array to check.</param>
    /// <returns>true if the byte array contains Unicode characters; otherwise, false.</returns>
    public static bool IsUnicode(byte[] byteData)
    {
        string value1 = FromASCIIByteArray(byteData);
        string value2 = FromUnicodeByteArray(byteData);
        byte[] ascii = AsciiStringToByteArray(value1);
        byte[] unicode = UnicodeStringToByteArray(value2);
        if (ascii[0] != unicode[0])
            return true;
        return false;
    }

    /// <summary>
    /// Converts an ASCII byte array to a string.
    /// </summary>
    /// <param name="characters">The byte array to convert.</param>
    /// <returns>A string constructed from the ASCII byte array.</returns>
    public static string FromASCIIByteArray(byte[] characters)
    {
        ASCIIEncoding encoding = new();
        string constructedString = encoding.GetString(characters);
        return constructedString;
    }

    /// <summary>
    /// Converts a Unicode byte array to a string.
    /// </summary>
    /// <param name="characters">The byte array to convert.</param>
    /// <returns>A string constructed from the Unicode byte array.</returns>
    public static string FromUnicodeByteArray(byte[] characters)
    {
        UnicodeEncoding encoding = new();
        string constructedString = encoding.GetString(characters);
        return constructedString;
    }

    /// <summary>
    /// Converts a string to an ASCII byte array.
    /// </summary>
    /// <param name="str">The string to convert.</param>
    /// <returns>An ASCII byte array representation of the string.</returns>
    public static byte[] AsciiStringToByteArray(string str)
    {
        ASCIIEncoding encoding = new();
        return encoding.GetBytes(str);
    }

    /// <summary>
    /// Converts a string to a Unicode byte array.
    /// </summary>
    /// <param name="str">The string to convert.</param>
    /// <returns>A Unicode byte array representation of the string.</returns>
    public static byte[] UnicodeStringToByteArray(string str)
    {
        UnicodeEncoding encoding = new();
        return encoding.GetBytes(str);
    }
}
