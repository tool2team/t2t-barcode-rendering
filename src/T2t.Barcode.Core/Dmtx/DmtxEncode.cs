using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace T2t.Barcode.Core.Dmtx
{
    public class DmtxEncode
    {
        #region public Objects
        public class DMCodeUtility
        {
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

            public static string FromASCIIByteArray(byte[] characters)
            {
                ASCIIEncoding encoding = new();
                string constructedString = encoding.GetString(characters);
                return constructedString;
            }

            public static string FromUnicodeByteArray(byte[] characters)
            {
                UnicodeEncoding encoding = new();
                string constructedString = encoding.GetString(characters);
                return constructedString;
            }

            public static byte[] AsciiStringToByteArray(string str)
            {
                ASCIIEncoding encoding = new();
                return encoding.GetBytes(str);
            }

            public static byte[] UnicodeStringToByteArray(string str)
            {
                UnicodeEncoding encoding = new();
                return encoding.GetBytes(str);
            }
        }

        public class SystemUtils
        {
            /// <summary>Reads a number of characters from the current source Stream and writes the data to the target array at the specified index.</summary>
            /// <param name="sourceStream">The source Stream to read from.</param>
            /// <param name="target">Contains the array of characteres read from the source Stream.</param>
            /// <param name="start">The starting index of the target array.</param>
            /// <param name="count">The maximum number of characters to read from the source Stream.</param>
            /// <returns>The number of characters read. The number will be less than or equal to count depending on the data available in the source Stream. Returns -1 if the end of the stream is reached.</returns>
            public static int ReadInput(System.IO.Stream sourceStream, sbyte[] target, int start, int count)
            {
                // Returns 0 bytes if not enough space in target
                if (target.Length == 0)
                    return 0;

                byte[] receiver = new byte[target.Length];
                int bytesRead = sourceStream.Read(receiver, start, count);

                // Returns -1 if EOF
                if (bytesRead == 0)
                    return -1;

                for (int i = start; i < start + bytesRead; i++)
                    target[i] = (sbyte)receiver[i];

                return bytesRead;
            }

            /// <summary>Reads a number of characters from the current source TextReader and writes the data to the target array at the specified index.</summary>
            /// <param name="sourceTextReader">The source TextReader to read from</param>
            /// <param name="target">Contains the array of characteres read from the source TextReader.</param>
            /// <param name="start">The starting index of the target array.</param>
            /// <param name="count">The maximum number of characters to read from the source TextReader.</param>
            /// <returns>The number of characters read. The number will be less than or equal to count depending on the data available in the source TextReader. Returns -1 if the end of the stream is reached.</returns>
            public static int ReadInput(System.IO.TextReader sourceTextReader, short[] target, int start, int count)
            {
                // Returns 0 bytes if not enough space in target
                if (target.Length == 0)
                    return 0;

                char[] charArray = new char[target.Length];
                int bytesRead = sourceTextReader.Read(charArray, start, count);

                // Returns -1 if EOF
                if (bytesRead == 0)
                    return -1;

                for (int index = start; index < start + bytesRead; index++)
                    target[index] = (short)charArray[index];

                return bytesRead;
            }

            /*******************************/
            /// <summary>
            /// Writes the exception stack trace to the received stream
            /// </summary>
            /// <param name="throwable">Exception to obtain information from</param>
            /// <param name="stream">Output sream used to write to</param>
            public static void WriteStackTrace(System.Exception throwable, System.IO.TextWriter stream)
            {
                stream.Write(throwable.StackTrace);
                stream.Flush();
            }

            /// <summary>
            /// Performs an unsigned bitwise right shift with the specified number
            /// </summary>
            /// <param name="number">Number to operate on</param>
            /// <param name="bits">Ammount of bits to shift</param>
            /// <returns>The resulting number from the shift operation</returns>
            public static int URShift(int number, int bits)
            {
                if (number >= 0)
                    return number >> bits;
                else
                    return (number >> bits) + (2 << ~bits);
            }

            /// <summary>
            /// Performs an unsigned bitwise right shift with the specified number
            /// </summary>
            /// <param name="number">Number to operate on</param>
            /// <param name="bits">Ammount of bits to shift</param>
            /// <returns>The resulting number from the shift operation</returns>
            public static int URShift(int number, long bits)
            {
                return URShift(number, (int)bits);
            }

            /// <summary>
            /// Performs an unsigned bitwise right shift with the specified number
            /// </summary>
            /// <param name="number">Number to operate on</param>
            /// <param name="bits">Ammount of bits to shift</param>
            /// <returns>The resulting number from the shift operation</returns>
            public static long URShift(long number, int bits)
            {
                if (number >= 0)
                    return number >> bits;
                else
                    return (number >> bits) + (2L << ~bits);
            }

            /// <summary>
            /// Performs an unsigned bitwise right shift with the specified number
            /// </summary>
            /// <param name="number">Number to operate on</param>
            /// <param name="bits">Ammount of bits to shift</param>
            /// <returns>The resulting number from the shift operation</returns>
            public static long URShift(long number, long bits)
            {
                return URShift(number, (int)bits);
            }

            /*******************************/
            /// <summary>
            /// Converts an array of sbytes to an array of bytes
            /// </summary>
            /// <param name="sbyteArray">The array of sbytes to be converted</param>
            /// <returns>The new array of bytes</returns>
            public static byte[] ToByteArray(sbyte[] sbyteArray)
            {
                byte[] byteArray = null;

                if (sbyteArray != null)
                {
                    byteArray = new byte[sbyteArray.Length];
                    for (int index = 0; index < sbyteArray.Length; index++)
                        byteArray[index] = (byte)sbyteArray[index];
                }
                return byteArray;
            }

            /// <summary>
            /// Converts a string to an array of bytes
            /// </summary>
            /// <param name="sourceString">The string to be converted</param>
            /// <returns>The new array of bytes</returns>
            public static byte[] ToByteArray(string sourceString)
            {
                return System.Text.UTF8Encoding.UTF8.GetBytes(sourceString);
            }

            /// <summary>
            /// Converts a array of object-type instances to a byte-type array.
            /// </summary>
            /// <param name="tempObjectArray">Array to convert.</param>
            /// <returns>An array of byte type elements.</returns>
            public static byte[] ToByteArray(object[] tempObjectArray)
            {
                byte[] byteArray = null;
                if (tempObjectArray != null)
                {
                    byteArray = new byte[tempObjectArray.Length];
                    for (int index = 0; index < tempObjectArray.Length; index++)
                        byteArray[index] = (byte)tempObjectArray[index];
                }
                return byteArray;
            }

            /*******************************/
            /// <summary>
            /// Receives a byte array and returns it transformed in an sbyte array
            /// </summary>
            /// <param name="byteArray">Byte array to process</param>
            /// <returns>The transformed array</returns>
            public static sbyte[] ToSByteArray(byte[] byteArray)
            {
                sbyte[] sbyteArray = null;
                if (byteArray != null)
                {
                    sbyteArray = new sbyte[byteArray.Length];
                    for (int index = 0; index < byteArray.Length; index++)
                        sbyteArray[index] = (sbyte)byteArray[index];
                }
                return sbyteArray;
            }


            /*******************************/
            /// <summary>
            /// Converts an array of sbytes to an array of chars
            /// </summary>
            /// <param name="sByteArray">The array of sbytes to convert</param>
            /// <returns>The new array of chars</returns>
            public static char[] ToCharArray(sbyte[] sByteArray)
            {
                return System.Text.UTF8Encoding.UTF8.GetChars(ToByteArray(sByteArray));
            }

            /// <summary>
            /// Converts an array of bytes to an array of chars
            /// </summary>
            /// <param name="byteArray">The array of bytes to convert</param>
            /// <returns>The new array of chars</returns>
            public static char[] ToCharArray(byte[] byteArray)
            {
                return System.Text.UTF8Encoding.UTF8.GetChars(byteArray);
            }

        }
        #endregion

        #region Private Fields
        DmtxScheme _scheme;
        DmtxSymbolSize _sizeIdxRequest;
        int _marginSize;
        int _moduleSize;
        DmtxMessage _message;
        DmtxRegion _region;

        #endregion

        #region Public Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        public DmtxEncode()
        {
            _scheme = DmtxScheme.DmtxSchemeAscii;
            _sizeIdxRequest = DmtxSymbolSize.DmtxSymbolSquareAuto;
            _marginSize = 10;
            _moduleSize = 5;
        }
        #endregion

        public virtual bool[,] CalculateDmCode(byte[] qrcodeData)
        {
            bool[,] rawData = null;
            byte[] buf = new byte[4096];

            /* Encode input string into data codewords */
            DmtxSymbolSize sizeIdx = _sizeIdxRequest;
            int dataWordCount = EncodeDataCodewords(buf, qrcodeData, ref sizeIdx);
            if (dataWordCount <= 0)
            {
                return rawData;
            }

            /* EncodeDataCodewords() should have updated any auto sizeIdx to a real one */
            if (sizeIdx == DmtxSymbolSize.DmtxSymbolSquareAuto || sizeIdx == DmtxSymbolSize.DmtxSymbolRectAuto)
            {
                throw new Exception("Invalid symbol size for encoding!");
            }

            /* Add pad characters to match a standard symbol size (whether smallest or requested) */
            int padCount = AddPadChars(buf, ref dataWordCount, DmtxCommon.GetSymbolAttribute(DmtxSymAttribute.DmtxSymAttribSymbolDataWords, sizeIdx));

            /* XXX we can remove a lot of this redundant data */
            _region = new DmtxRegion
            {
                SizeIdx = sizeIdx,
                SymbolRows = DmtxCommon.GetSymbolAttribute(DmtxSymAttribute.DmtxSymAttribSymbolRows, sizeIdx),
                SymbolCols = DmtxCommon.GetSymbolAttribute(DmtxSymAttribute.DmtxSymAttribSymbolCols, sizeIdx),
                MappingRows = DmtxCommon.GetSymbolAttribute(DmtxSymAttribute.DmtxSymAttribMappingMatrixRows, sizeIdx),
                MappingCols = DmtxCommon.GetSymbolAttribute(DmtxSymAttribute.DmtxSymAttribMappingMatrixCols, sizeIdx)
            };

            /* Allocate memory for message and array */
            _message = new DmtxMessage(sizeIdx, DmtxFormat.Matrix) { PadCount = padCount };
            for (int i = 0; i < dataWordCount; i++)
            {
                _message.Code[i] = buf[i];
            }

            /* Generate error correction codewords */
            DmtxCommon.GenReedSolEcc(_message, _region.SizeIdx);

            /* Module placement in region */
            ModulePlacementEcc200(_message.Array, _message.Code,
                  _region.SizeIdx, DmtxConstants.DmtxModuleOnRGB);

            //int width = 2 * _marginSize + (_region.SymbolCols * _moduleSize);
            //int height = 2 * _marginSize + (_region.SymbolRows * _moduleSize);

            rawData = new bool[_region.SymbolCols, _region.SymbolRows];

            for (int symbolRow = 0; symbolRow < _region.SymbolRows; symbolRow++)
            {
                for (int symbolCol = 0; symbolCol < _region.SymbolCols; symbolCol++)
                {
                    int moduleStatus = _message.SymbolModuleStatus(_region.SizeIdx, symbolRow, symbolCol);
                    rawData[symbolCol, _region.SymbolRows - symbolRow - 1] = (moduleStatus & DmtxConstants.DmtxModuleOnBlue) != 0x00;
                }
            }

            return rawData;
        }

        public static int ModulePlacementEcc200(byte[] modules, byte[] codewords, DmtxSymbolSize sizeIdx, int moduleOnColor)
        {
            if ((moduleOnColor & (DmtxConstants.DmtxModuleOnRed | DmtxConstants.DmtxModuleOnGreen | DmtxConstants.DmtxModuleOnBlue)) == 0)
            {
                throw new Exception("Error with module placement ECC 200");
            }

            int mappingRows = DmtxCommon.GetSymbolAttribute(DmtxSymAttribute.DmtxSymAttribMappingMatrixRows, sizeIdx);
            int mappingCols = DmtxCommon.GetSymbolAttribute(DmtxSymAttribute.DmtxSymAttribMappingMatrixCols, sizeIdx);

            /* Start in the nominal location for the 8th bit of the first character */
            int chr = 0;
            int row = 4;
            int col = 0;

            do
            {
                /* Repeatedly first check for one of the special corner cases */
                if (row == mappingRows && col == 0)
                    PatternShapeSpecial1(modules, mappingRows, mappingCols, codewords, chr++, moduleOnColor);
                else if (row == mappingRows - 2 && col == 0 && mappingCols % 4 != 0)
                    PatternShapeSpecial2(modules, mappingRows, mappingCols, codewords, chr++, moduleOnColor);
                else if (row == mappingRows - 2 && col == 0 && mappingCols % 8 == 4)
                    PatternShapeSpecial3(modules, mappingRows, mappingCols, codewords, chr++, moduleOnColor);
                else if (row == mappingRows + 4 && col == 2 && mappingCols % 8 == 0)
                    PatternShapeSpecial4(modules, mappingRows, mappingCols, codewords, chr++, moduleOnColor);

                /* Sweep upward diagonally, inserting successive characters */
                do
                {
                    if (row < mappingRows && col >= 0 && (modules[row * mappingCols + col] & DmtxConstants.DmtxModuleVisited) == 0)
                        PatternShapeStandard(modules, mappingRows, mappingCols, row, col, codewords, chr++, moduleOnColor);
                    row -= 2;
                    col += 2;
                } while (row >= 0 && col < mappingCols);
                row += 1;
                col += 3;

                /* Sweep downward diagonally, inserting successive characters */
                do
                {
                    if (row >= 0 && col < mappingCols && (modules[row * mappingCols + col] & DmtxConstants.DmtxModuleVisited) == 0)
                        PatternShapeStandard(modules, mappingRows, mappingCols, row, col, codewords, chr++, moduleOnColor);
                    row += 2;
                    col -= 2;
                } while (row < mappingRows && col >= 0);
                row += 3;
                col += 1;
                /* ... until the entire modules array is scanned */
            } while (row < mappingRows || col < mappingCols);

            /* If lower righthand corner is untouched then fill in the fixed pattern */
            if ((modules[mappingRows * mappingCols - 1] &
                  DmtxConstants.DmtxModuleVisited) == 0)
            {

                modules[mappingRows * mappingCols - 1] |= (byte)moduleOnColor;
                modules[mappingRows * mappingCols - mappingCols - 2] |= (byte)moduleOnColor;
            } /* XXX should this fixed pattern also be used in reading somehow? */

            /* XXX compare that chr == region->dataSize here */
            return chr; /* XXX number of codewords read off */
        }

        public static void PatternShapeStandard(byte[] modules, int mappingRows, int mappingCols, int row, int col, byte[] codeword, int codeWordIndex, int moduleOnColor)
        {
            PlaceModule(modules, mappingRows, mappingCols, row - 2, col - 2, codeword, codeWordIndex, DmtxMaskBit.DmtxMaskBit1, moduleOnColor);
            PlaceModule(modules, mappingRows, mappingCols, row - 2, col - 1, codeword, codeWordIndex, DmtxMaskBit.DmtxMaskBit2, moduleOnColor);
            PlaceModule(modules, mappingRows, mappingCols, row - 1, col - 2, codeword, codeWordIndex, DmtxMaskBit.DmtxMaskBit3, moduleOnColor);
            PlaceModule(modules, mappingRows, mappingCols, row - 1, col - 1, codeword, codeWordIndex, DmtxMaskBit.DmtxMaskBit4, moduleOnColor);
            PlaceModule(modules, mappingRows, mappingCols, row - 1, col, codeword, codeWordIndex, DmtxMaskBit.DmtxMaskBit5, moduleOnColor);
            PlaceModule(modules, mappingRows, mappingCols, row, col - 2, codeword, codeWordIndex, DmtxMaskBit.DmtxMaskBit6, moduleOnColor);
            PlaceModule(modules, mappingRows, mappingCols, row, col - 1, codeword, codeWordIndex, DmtxMaskBit.DmtxMaskBit7, moduleOnColor);
            PlaceModule(modules, mappingRows, mappingCols, row, col, codeword, codeWordIndex, DmtxMaskBit.DmtxMaskBit8, moduleOnColor);
        }

        public static void PatternShapeSpecial1(byte[] modules, int mappingRows, int mappingCols, byte[] codeword, int codeWordIndex, int moduleOnColor)
        {
            PlaceModule(modules, mappingRows, mappingCols, mappingRows - 1, 0, codeword, codeWordIndex, DmtxMaskBit.DmtxMaskBit1, moduleOnColor);
            PlaceModule(modules, mappingRows, mappingCols, mappingRows - 1, 1, codeword, codeWordIndex, DmtxMaskBit.DmtxMaskBit2, moduleOnColor);
            PlaceModule(modules, mappingRows, mappingCols, mappingRows - 1, 2, codeword, codeWordIndex, DmtxMaskBit.DmtxMaskBit3, moduleOnColor);
            PlaceModule(modules, mappingRows, mappingCols, 0, mappingCols - 2, codeword, codeWordIndex, DmtxMaskBit.DmtxMaskBit4, moduleOnColor);
            PlaceModule(modules, mappingRows, mappingCols, 0, mappingCols - 1, codeword, codeWordIndex, DmtxMaskBit.DmtxMaskBit5, moduleOnColor);
            PlaceModule(modules, mappingRows, mappingCols, 1, mappingCols - 1, codeword, codeWordIndex, DmtxMaskBit.DmtxMaskBit6, moduleOnColor);
            PlaceModule(modules, mappingRows, mappingCols, 2, mappingCols - 1, codeword, codeWordIndex, DmtxMaskBit.DmtxMaskBit7, moduleOnColor);
            PlaceModule(modules, mappingRows, mappingCols, 3, mappingCols - 1, codeword, codeWordIndex, DmtxMaskBit.DmtxMaskBit8, moduleOnColor);
        }

        public static void PatternShapeSpecial2(byte[] modules, int mappingRows, int mappingCols, byte[] codeword, int codeWordIndex, int moduleOnColor)
        {
            PlaceModule(modules, mappingRows, mappingCols, mappingRows - 3, 0, codeword, codeWordIndex, DmtxMaskBit.DmtxMaskBit1, moduleOnColor);
            PlaceModule(modules, mappingRows, mappingCols, mappingRows - 2, 0, codeword, codeWordIndex, DmtxMaskBit.DmtxMaskBit2, moduleOnColor);
            PlaceModule(modules, mappingRows, mappingCols, mappingRows - 1, 0, codeword, codeWordIndex, DmtxMaskBit.DmtxMaskBit3, moduleOnColor);
            PlaceModule(modules, mappingRows, mappingCols, 0, mappingCols - 4, codeword, codeWordIndex, DmtxMaskBit.DmtxMaskBit4, moduleOnColor);
            PlaceModule(modules, mappingRows, mappingCols, 0, mappingCols - 3, codeword, codeWordIndex, DmtxMaskBit.DmtxMaskBit5, moduleOnColor);
            PlaceModule(modules, mappingRows, mappingCols, 0, mappingCols - 2, codeword, codeWordIndex, DmtxMaskBit.DmtxMaskBit6, moduleOnColor);
            PlaceModule(modules, mappingRows, mappingCols, 0, mappingCols - 1, codeword, codeWordIndex, DmtxMaskBit.DmtxMaskBit7, moduleOnColor);
            PlaceModule(modules, mappingRows, mappingCols, 1, mappingCols - 1, codeword, codeWordIndex, DmtxMaskBit.DmtxMaskBit8, moduleOnColor);
        }

        public static void PatternShapeSpecial3(byte[] modules, int mappingRows, int mappingCols, byte[] codeword, int codeWordIndex, int moduleOnColor)
        {
            PlaceModule(modules, mappingRows, mappingCols, mappingRows - 3, 0, codeword, codeWordIndex, DmtxMaskBit.DmtxMaskBit1, moduleOnColor);
            PlaceModule(modules, mappingRows, mappingCols, mappingRows - 2, 0, codeword, codeWordIndex, DmtxMaskBit.DmtxMaskBit2, moduleOnColor);
            PlaceModule(modules, mappingRows, mappingCols, mappingRows - 1, 0, codeword, codeWordIndex, DmtxMaskBit.DmtxMaskBit3, moduleOnColor);
            PlaceModule(modules, mappingRows, mappingCols, 0, mappingCols - 2, codeword, codeWordIndex, DmtxMaskBit.DmtxMaskBit4, moduleOnColor);
            PlaceModule(modules, mappingRows, mappingCols, 0, mappingCols - 1, codeword, codeWordIndex, DmtxMaskBit.DmtxMaskBit5, moduleOnColor);
            PlaceModule(modules, mappingRows, mappingCols, 1, mappingCols - 1, codeword, codeWordIndex, DmtxMaskBit.DmtxMaskBit6, moduleOnColor);
            PlaceModule(modules, mappingRows, mappingCols, 2, mappingCols - 1, codeword, codeWordIndex, DmtxMaskBit.DmtxMaskBit7, moduleOnColor);
            PlaceModule(modules, mappingRows, mappingCols, 3, mappingCols - 1, codeword, codeWordIndex, DmtxMaskBit.DmtxMaskBit8, moduleOnColor);
        }

        public static void PatternShapeSpecial4(byte[] modules, int mappingRows, int mappingCols, byte[] codeword, int codeWordIndex, int moduleOnColor)
        {
            PlaceModule(modules, mappingRows, mappingCols, mappingRows - 1, 0, codeword, codeWordIndex, DmtxMaskBit.DmtxMaskBit1, moduleOnColor);
            PlaceModule(modules, mappingRows, mappingCols, mappingRows - 1, mappingCols - 1, codeword, codeWordIndex, DmtxMaskBit.DmtxMaskBit2, moduleOnColor);
            PlaceModule(modules, mappingRows, mappingCols, 0, mappingCols - 3, codeword, codeWordIndex, DmtxMaskBit.DmtxMaskBit3, moduleOnColor);
            PlaceModule(modules, mappingRows, mappingCols, 0, mappingCols - 2, codeword, codeWordIndex, DmtxMaskBit.DmtxMaskBit4, moduleOnColor);
            PlaceModule(modules, mappingRows, mappingCols, 0, mappingCols - 1, codeword, codeWordIndex, DmtxMaskBit.DmtxMaskBit5, moduleOnColor);
            PlaceModule(modules, mappingRows, mappingCols, 1, mappingCols - 3, codeword, codeWordIndex, DmtxMaskBit.DmtxMaskBit6, moduleOnColor);
            PlaceModule(modules, mappingRows, mappingCols, 1, mappingCols - 2, codeword, codeWordIndex, DmtxMaskBit.DmtxMaskBit7, moduleOnColor);
            PlaceModule(modules, mappingRows, mappingCols, 1, mappingCols - 1, codeword, codeWordIndex, DmtxMaskBit.DmtxMaskBit8, moduleOnColor);
        }

        public static void PlaceModule(byte[] modules, int mappingRows, int mappingCols, int row, int col, byte[] codeword, int codeWordIndex, DmtxMaskBit mask, int moduleOnColor)
        {
            if (row < 0)
            {
                row += mappingRows;
                col += 4 - (mappingRows + 4) % 8;
            }
            if (col < 0)
            {
                col += mappingCols;
                row += 4 - (mappingCols + 4) % 8;
            }

            /* If module has already been assigned then we are decoding the pattern into codewords */
            if ((modules[row * mappingCols + col] & DmtxConstants.DmtxModuleAssigned) != 0)
            {
                if ((modules[row * mappingCols + col] & moduleOnColor) != 0)
                    codeword[codeWordIndex] |= (byte)mask;
                else
                    codeword[codeWordIndex] &= (byte)(0xff ^ (int)mask);
            }
            /* Otherwise we are encoding the codewords into a pattern */
            else
            {
                if ((codeword[codeWordIndex] & (byte)mask) != 0x00)
                    modules[row * mappingCols + col] |= (byte)moduleOnColor;

                modules[row * mappingCols + col] |= (byte)DmtxConstants.DmtxModuleAssigned;
            }

            modules[row * mappingCols + col] |= (byte)DmtxConstants.DmtxModuleVisited;
        }

        int EncodeDataCodewords(byte[] buf, byte[] inputString, ref DmtxSymbolSize sizeIdx)
        {
            var dataWordCount = _scheme switch
            {
                DmtxScheme.DmtxSchemeAutoBest => EncodeAutoBest(buf, inputString),
                DmtxScheme.DmtxSchemeAutoFast => 0,
                _ => EncodeSingleScheme(buf, inputString, _scheme),
            };

            /* XXX must fix ... will need to handle sizeIdx requests here because it is
               needed by Encode...() for triplet termination */

            /* parameter sizeIdx is requested value, returned sizeIdx is decision */
            sizeIdx = FindCorrectSymbolSize(dataWordCount, sizeIdx);
            if (sizeIdx == DmtxSymbolSize.DmtxSymbolShapeAuto)
                return 0;

            return dataWordCount;
        }

        private static int AddPadChars(byte[] buf, ref int dataWordCount, int paddedSize)
        {
            int padCount = 0;

            /* First pad character is not randomized */
            if (dataWordCount < paddedSize)
            {
                padCount++;
                buf[dataWordCount++] = DmtxConstants.DmtxCharAsciiPad;
            }

            /* All remaining pad characters are randomized based on character position */
            while (dataWordCount < paddedSize)
            {
                padCount++;
                buf[dataWordCount] = Randomize253State(DmtxConstants.DmtxCharAsciiPad, dataWordCount + 1);
                dataWordCount++;
            }

            return padCount;
        }

        private static byte Randomize253State(byte codewordValue, int codewordPosition)
        {
            int pseudoRandom = (149 * codewordPosition % 253) + 1;
            int tmp = codewordValue + pseudoRandom;
            if (tmp > 254)
                tmp -= 254;

            if (tmp < 0 || tmp > 255)
            {
                throw new Exception("Error randomizing 253 state!");
            }

            return (byte)tmp;
        }

        private static DmtxSymbolSize FindCorrectSymbolSize(int dataWords, DmtxSymbolSize sizeIdxRequest)
        {
            DmtxSymbolSize sizeIdx;

            if (dataWords <= 0)
                return DmtxSymbolSize.DmtxSymbolShapeAuto;

            if (sizeIdxRequest == DmtxSymbolSize.DmtxSymbolSquareAuto || sizeIdxRequest == DmtxSymbolSize.DmtxSymbolRectAuto)
            {
                int idxBeg;
                int idxEnd;
                if (sizeIdxRequest == DmtxSymbolSize.DmtxSymbolSquareAuto)
                {
                    idxBeg = 0;
                    idxEnd = DmtxConstants.DmtxSymbolSquareCount;
                }
                else
                {
                    idxBeg = DmtxConstants.DmtxSymbolSquareCount;
                    idxEnd = DmtxConstants.DmtxSymbolSquareCount + DmtxConstants.DmtxSymbolRectCount;
                }

                for (sizeIdx = (DmtxSymbolSize)idxBeg; sizeIdx < (DmtxSymbolSize)idxEnd; sizeIdx++)
                {
                    if (DmtxCommon.GetSymbolAttribute(DmtxSymAttribute.DmtxSymAttribSymbolDataWords, sizeIdx) >= dataWords)
                    {
                        break;
                    }
                }

                if (sizeIdx == (DmtxSymbolSize)idxEnd)
                {
                    return DmtxSymbolSize.DmtxSymbolShapeAuto;
                }
            }
            else
            {
                sizeIdx = sizeIdxRequest;
            }

            if (dataWords > DmtxCommon.GetSymbolAttribute(DmtxSymAttribute.DmtxSymAttribSymbolDataWords, sizeIdx))
            {
                return DmtxSymbolSize.DmtxSymbolShapeAuto;
            }

            return sizeIdx;
        }

        private int EncodeSingleScheme(byte[] buf, byte[] codewords, DmtxScheme scheme)
        {
            DmtxChannel channel = new();

            InitChannel(channel, codewords);

            while (channel.InputIndex < channel.Input.Length)
            {
                bool err = EncodeNextWord(channel, scheme);
                if (!err)
                    return 0;

                /* DumpChannel(&channel); */

                if (channel.Invalid != DmtxChannelStatus.DmtxChannelValid)
                {
                    return 0;
                }
            }
            /* DumpChannel(&channel); */

            int size = channel.EncodedLength / 12;
            for (int i = 0; i < size; i++)
            {
                buf[i] = channel.EncodedWords[i];
            }

            return size;
        }

        private int EncodeAutoBest(byte[] buf, byte[] codewords)
        {
            DmtxScheme targetScheme;
            DmtxChannelGroup optimal = new();
            DmtxChannelGroup best = new();

            /* Intialize optimizing channels and encode first codeword from default ASCII */
            for (targetScheme = DmtxScheme.DmtxSchemeAscii; targetScheme <= DmtxScheme.DmtxSchemeBase256; targetScheme++)
            {
                DmtxChannel channel = optimal.Channels[(int)targetScheme];
                InitChannel(channel, codewords);
                bool err = EncodeNextWord(channel, targetScheme);
                if (err)
                    return 0;
            }

            /* For each remaining word in the input stream, test the efficiency of
               getting to this encodation scheme for each input character by
               switching here from each of the other channels (which are always
               optimally encoded) */
            while (optimal.Channels[0].InputIndex < optimal.Channels[0].Input.Length)
            { /* XXX only tracking first channel */

                for (targetScheme = DmtxScheme.DmtxSchemeAscii; targetScheme <= DmtxScheme.DmtxSchemeBase256; targetScheme++)
                {
                    best.Channels[(int)targetScheme] = FindBestChannel(optimal, targetScheme);
                }
                optimal = best;
            }

            /* Choose a winner now that all channels are finished */
            DmtxChannel winner = optimal.Channels[(int)DmtxScheme.DmtxSchemeAscii];
            for (targetScheme = DmtxScheme.DmtxSchemeAscii + 1; targetScheme <= DmtxScheme.DmtxSchemeBase256; targetScheme++)
            {
                if (optimal.Channels[(int)targetScheme].Invalid != DmtxChannelStatus.DmtxChannelValid)
                {
                    continue;
                }

                if (optimal.Channels[(int)targetScheme].EncodedLength < winner.EncodedLength)
                {
                    winner = optimal.Channels[(int)targetScheme];
                }
            }

            /* XXX get rid of buf concept and try to do something with channel -> matrix copy instead */
            int winnerSize = winner.EncodedLength / 12;
            for (int i = 0; i < winnerSize; i++)
            {
                buf[i] = winner.EncodedWords[i];
            }

            return winnerSize;
        }

        private DmtxChannel FindBestChannel(DmtxChannelGroup group, DmtxScheme targetScheme)
        {
            DmtxChannel winner = null;

            for (DmtxScheme encFrom = DmtxScheme.DmtxSchemeAscii; encFrom <= DmtxScheme.DmtxSchemeBase256; encFrom++)
            {

                DmtxChannel channel = group.Channels[(int)encFrom];

                /* If from channel doesn't hold valid data because it couldn't
                   represent the previous value then skip it */
                if (channel.Invalid != DmtxChannelStatus.DmtxChannelValid)
                {
                    continue;
                }

                /* If channel has already processed all of its input values then it
                   cannot be used as a starting point */
                if (channel.InputIndex == channel.Input.Length)
                    continue;

                bool err = EncodeNextWord(channel, targetScheme);
                if (err == false)
                {
                    /* XXX fix this */
                }

                /* If channel scheme can't represent next word then stop for this channel */
                if ((channel.Invalid & DmtxChannelStatus.DmtxChannelUnsupportedChar) != 0)
                {
                    winner = channel;
                    break;
                }

                /* If channel scheme was unable to unlatch here then skip */
                if ((channel.Invalid & DmtxChannelStatus.DmtxChannelCannotUnlatch) != 0)
                {
                    continue;
                }

                if (winner == null || channel.CurrentLength < winner.CurrentLength)
                {
                    winner = channel;
                }
            }

            return winner;
        }

        private bool EncodeNextWord(DmtxChannel channel, DmtxScheme targetScheme)
        {
            /* Change to new encodation scheme if necessary */
            if (channel.EncScheme != targetScheme)
            {
                ChangeEncScheme(channel, targetScheme, DmtxUnlatch.Explicit);
                if (channel.Invalid != DmtxChannelStatus.DmtxChannelValid)
                    return false;
            }

            if (channel.EncScheme != targetScheme)
            {
                throw new Exception("For encoding, channel scheme must equal target scheme!");
            }

            /* Encode next input value */
            return channel.EncScheme switch
            {
                DmtxScheme.DmtxSchemeAscii => EncodeAsciiCodeword(channel),
                DmtxScheme.DmtxSchemeC40 => EncodeTripletCodeword(channel),
                DmtxScheme.DmtxSchemeText => EncodeTripletCodeword(channel),
                DmtxScheme.DmtxSchemeX12 => EncodeTripletCodeword(channel),
                DmtxScheme.DmtxSchemeEdifact => EncodeEdifactCodeword(channel),
                DmtxScheme.DmtxSchemeBase256 => EncodeBase256Codeword(channel),
                _ => false,
            };
        }

        private static bool EncodeBase256Codeword(DmtxChannel channel)
        {
            int i;
            int newDataLength;
            int headerByteCount;
            byte[] headerByte = new byte[2];

            if (channel.EncScheme != DmtxScheme.DmtxSchemeBase256)
            {
                throw new Exception("Invalid encoding scheme selected!");
            }

            int firstBytePtrIndex = channel.FirstCodeWord / 12;
            headerByte[0] = DmtxMessage.UnRandomize255State(channel.EncodedWords[firstBytePtrIndex], channel.FirstCodeWord / 12 + 1);

            /* newSchemeLength contains size byte(s) too */
            if (headerByte[0] <= 249)
            {
                newDataLength = headerByte[0];
            }
            else
            {
                newDataLength = 250 * (headerByte[0] - 249);
                newDataLength += DmtxMessage.UnRandomize255State(channel.EncodedWords[firstBytePtrIndex + 1], channel.FirstCodeWord / 12 + 2);
            }

            newDataLength++;

            if (newDataLength <= 249)
            {
                headerByteCount = 1;
                headerByte[0] = (byte)newDataLength;
                headerByte[1] = 0; /* unused */
            }
            else
            {
                headerByteCount = 2;
                headerByte[0] = (byte)(newDataLength / 250 + 249);
                headerByte[1] = (byte)(newDataLength % 250);
            }

            /* newDataLength does not include header bytes */
            if (newDataLength <= 0 || newDataLength > 1555)
            {
                throw new Exception("Encoding failed, data length out of range!");
            }

            /* One time shift of codewords when passing the 250 byte size threshold */
            if (newDataLength == 250)
            {
                for (i = channel.CurrentLength / 12 - 1; i > channel.FirstCodeWord / 12; i--)
                {
                    byte valueTmp = DmtxMessage.UnRandomize255State(channel.EncodedWords[i], i + 1);
                    channel.EncodedWords[i + 1] = Randomize255State(valueTmp, i + 2);
                }
                IncrementProgress(channel, 12);
                channel.EncodedLength += 12; /* ugly */
            }

            /* Update scheme length in Base 256 header */
            for (i = 0; i < headerByteCount; i++)
            {
                channel.EncodedWords[firstBytePtrIndex + i] = Randomize255State(headerByte[i], channel.FirstCodeWord / 12 + i + 1);
            }

            PushInputWord(channel, Randomize255State(channel.Input[channel.InputIndex], channel.CurrentLength / 12 + 1));
            IncrementProgress(channel, 12);
            channel.InputIndex++;

            /* XXX will need to introduce an EndOfSymbolBase256() that recognizes
               opportunity to encode headerLength of 0 if remaining Base 256 message
               exactly matches symbol capacity */

            return true;
        }

        private bool EncodeEdifactCodeword(DmtxChannel channel)
        {
            if (channel.EncScheme != DmtxScheme.DmtxSchemeEdifact)
            {
                throw new Exception("Invalid encoding scheme selected!");
            }

            byte inputValue = channel.Input[channel.InputIndex];

            if (inputValue < 32 || inputValue > 94)
            {
                channel.Invalid = DmtxChannelStatus.DmtxChannelUnsupportedChar;
                return false;
            }

            PushInputWord(channel, (byte)(inputValue & 0x3f));
            IncrementProgress(channel, 9);
            channel.InputIndex++;

            CheckForEndOfSymbolEdifact(channel);

            return true;
        }

        private void CheckForEndOfSymbolEdifact(DmtxChannel channel)
        {
            /* This function tests if the remaining input values can be completed using
             * one of the valid end-of-symbol cases, and finishes encodation if possible.
             *
             * This function must exit in ASCII encodation.  EDIFACT must always be
             * unlatched, although implicit Unlatch is possible.
             *
             * End   Symbol  ASCII  EDIFACT  End        Codeword
             * Case  Words   Words  Values   Condition  Sequence
             * ----  ------  -----  -------  ---------  -------------------------------
             * (a)        1      0           Special    PAD
             * (b)        1      1           Special    ASCII (could be 2 digits)
             * (c)        1   >= 2           Continue   Need larger symbol
             * (d)        2      0           Special    PAD PAD
             * (e)        2      1           Special    ASCII PAD
             * (f)        2      2           Special    ASCII ASCII
             * (g)        2   >= 3           Continue   Need larger symbol
             * (h)      N/A    N/A        0  Normal     UNLATCH
             * (i)      N/A    N/A     >= 1  Continue   Not end of symbol
             *
             * Note: All "Special" cases (a,b,d,e,f) require clean byte boundary to start
             */

            /* Count remaining input values assuming EDIFACT encodation */
            if (channel.InputIndex > channel.Input.Length)
            {
                throw new Exception("Input index out of range while encoding!");
            }

            int edifactValues = channel.Input.Length - channel.InputIndex;

            /* Can't end symbol right now if there are 5+ values remaining
               (noting that '9999' can still terminate in case (f)) */
            if (edifactValues > 4) /* subset of (i) -- performance only */
                return;

            /* Find minimum symbol size big enough to accomodate remaining codewords */
            /* XXX broken -- what if someone asks for DmtxSymbolRectAuto or specific sizeIdx? */

            int currentByte = channel.CurrentLength / 12;
            DmtxSymbolSize sizeIdx = FindCorrectSymbolSize(currentByte, DmtxSymbolSize.DmtxSymbolSquareAuto);
            /* XXX test for sizeIdx == DmtxUndefined here */
            int symbolCodewords = DmtxCommon.GetSymbolAttribute(DmtxSymAttribute.DmtxSymAttribSymbolDataWords, sizeIdx) - currentByte;

            /* Test for special case condition */
            if (channel.CurrentLength % 12 == 0 &&
                  (symbolCodewords == 1 || symbolCodewords == 2))
            {
                /* Count number of codewords left to write (assuming ASCII) */
                /* XXX temporary hack ... later create function that knows about shifts and digits */
                int asciiCodewords = edifactValues;

                if (asciiCodewords <= symbolCodewords)
                { /* (a,b,d,e,f) */
                    ChangeEncScheme(channel, DmtxScheme.DmtxSchemeAscii, DmtxUnlatch.Implicit);

                    /* XXX this loop should produce exactly asciiWords codewords ... assert somehow? */
                    for (int i = 0; i < edifactValues; i++)
                    {
                        bool err = EncodeNextWord(channel, DmtxScheme.DmtxSchemeAscii);
                        if (err == false)
                        {
                            return;
                        }
                        if (channel.Invalid != DmtxChannelStatus.DmtxChannelValid)
                        {
                            throw new Exception("Error checking for end of symbol edifact");
                        }
                    }
                }
                /* else (c,g) -- do nothing */
            }
            else if (edifactValues == 0)
            { /* (h) */
                ChangeEncScheme(channel, DmtxScheme.DmtxSchemeAscii, DmtxUnlatch.Explicit);
            }
            /* else (i) -- do nothing */

            return;
        }

        private static void PushInputWord(DmtxChannel channel, byte codeword)
        {
            /* XXX should this assertion actually be a legit runtime test? */
            if (channel.EncodedLength / 12 > 3 * 1558)
            {
                throw new Exception("Can't push input word, encoded length exceeds limits!");
            }/* increased for Mosaic */

            /* XXX this is currently pretty ugly, but can wait until the
               rewrite. What is required is to go through and decide on a
               consistent approach (probably that all encodation schemes use
               currentLength except for triplet-based schemes which use
               currentLength and encodedLength).  All encodation schemes should
               maintain both currentLength and encodedLength though.  Perhaps
               another approach would be to maintain currentLength and "extraLength" */

            switch (channel.EncScheme)
            {
                case DmtxScheme.DmtxSchemeAscii:
                    channel.EncodedWords[channel.CurrentLength / 12] = codeword;
                    channel.EncodedLength += 12;
                    break;

                case DmtxScheme.DmtxSchemeC40:
                    channel.EncodedWords[channel.EncodedLength / 12] = codeword;
                    channel.EncodedLength += 12;
                    break;

                case DmtxScheme.DmtxSchemeText:
                    channel.EncodedWords[channel.EncodedLength / 12] = codeword;
                    channel.EncodedLength += 12;
                    break;

                case DmtxScheme.DmtxSchemeX12:
                    channel.EncodedWords[channel.EncodedLength / 12] = codeword;
                    channel.EncodedLength += 12;
                    break;

                case DmtxScheme.DmtxSchemeEdifact:
                    /* EDIFACT is the only encodation scheme where we don't encode up to the
                       next byte boundary.  This is because EDIFACT can be unlatched at any
                       point, including mid-byte, so we can't guarantee what the next
                       codewords will be.  All other encodation schemes only unlatch on byte
                       boundaries, allowing us to encode to the next boundary knowing that
                       we have predicted the only codewords that could be used while in this
                       scheme. */

                    /* write codeword value to next 6 bits (might span codeword bytes) and
                       then pad any remaining bits until next byte boundary with zero bits. */
                    int pos = channel.CurrentLength % 4;
                    int startByte = ((channel.CurrentLength + 9) / 12) - pos;

                    DmtxQuadruplet quad = GetQuadrupletValues(channel.EncodedWords[startByte],
                                                              channel.EncodedWords[startByte + 1],
                                                              channel.EncodedWords[startByte + 2]);
                    quad.Value[pos] = codeword;

                    for (int i = pos + 1; i < 4; i++)
                        quad.Value[i] = 0;

                    /* Only write the necessary codewords */
                    switch (pos)
                    {
                        case 3:
                            channel.EncodedWords[startByte + 2] = (byte)(((quad.Value[2] & 0x03) << 6) | quad.Value[3]);
                            break;
                        case 2:
                            channel.EncodedWords[startByte + 2] = (byte)(((quad.Value[2] & 0x03) << 6) | quad.Value[3]);
                            break;
                        case 1:
                            channel.EncodedWords[startByte + 1] = (byte)(((quad.Value[1] & 0x0f) << 4) | (quad.Value[2] >> 2));
                            break;
                        case 0:
                            channel.EncodedWords[startByte] = (byte)((quad.Value[0] << 2) | (quad.Value[1] >> 4));
                            break;
                    }

                    channel.EncodedLength += 9;
                    break;

                case DmtxScheme.DmtxSchemeBase256:
                    channel.EncodedWords[channel.CurrentLength / 12] = codeword;
                    channel.EncodedLength += 12;
                    break;
            }
        }

        private bool EncodeTripletCodeword(DmtxChannel channel)
        {
            int[] outputWords = new int[4];       /* biggest: upper shift to non-basic set */
            byte[] buffer = new byte[6];  /* biggest: 2 words followed by 4-word upper shift */
            DmtxTriplet triplet = new();

            if (channel.EncScheme != DmtxScheme.DmtxSchemeX12 &&
                channel.EncScheme != DmtxScheme.DmtxSchemeText &&
                channel.EncScheme != DmtxScheme.DmtxSchemeC40)
            {
                throw new Exception("Invalid encoding scheme selected!");
            }

            if (channel.CurrentLength > channel.EncodedLength)
            {
                throw new Exception("Encoding length out of range!");
            }

            /* If there are no pre-encoded codewords then generate some */
            if (channel.CurrentLength == channel.EncodedLength)
            {

                if (channel.CurrentLength % 12 != 0)
                {
                    throw new Exception("Invalid encoding length!");
                }

                /* Ideally we would only encode one codeword triplet here (the
                   minimum that you can do at a time) but we can't leave the channel
                   with the last encoded word as a shift.  The following loop
                   prevents this condition by encoding until we have a clean break or
                   until we reach the end of the input data. */

                int ptrIndex = channel.InputIndex;

                int tripletCount = 0;
                for (; ; )
                {

                    /* Fill array with at least 3 values (the minimum necessary to
                       encode a triplet), but possibly up to 6 values due to presence
                       of upper shifts.  Note that buffer may already contain values
                       from a previous iteration of the outer loop, and this step
                       "tops off" the buffer to make sure there are at least 3 values. */

                    while (tripletCount < 3 && ptrIndex < channel.Input.Length)
                    {
                        byte inputWord = channel.Input[ptrIndex++];
                        int count = GetC40TextX12Words(outputWords, inputWord, channel.EncScheme);

                        if (count == 0)
                        {
                            channel.Invalid = DmtxChannelStatus.DmtxChannelUnsupportedChar;
                            return false;
                        }

                        for (int i = 0; i < count; i++)
                        {
                            buffer[tripletCount++] = (byte)outputWords[i];
                        }
                    }

                    /* Take the next 3 values from buffer to encode */
                    triplet.Value[0] = buffer[0];
                    triplet.Value[1] = buffer[1];
                    triplet.Value[2] = buffer[2];

                    if (tripletCount >= 3)
                    {
                        PushTriplet(channel, triplet);
                        buffer[0] = buffer[3];
                        buffer[1] = buffer[4];
                        buffer[2] = buffer[5];
                        tripletCount -= 3;
                    }

                    /* If we reach the end of input and have not encountered a clean
                       break opportunity then complete the symbol here */

                    if (ptrIndex == channel.Input.Length)
                    {
                        /* tripletCount represents the number of values in triplet waiting to be pushed
                           inputCount represents the number of values after inputPtr waiting to be pushed */
                        while (channel.CurrentLength < channel.EncodedLength)
                        {
                            IncrementProgress(channel, 8);
                            channel.InputIndex++;
                        }

                        /* If final triplet value was shift then IncrementProgress will
                           overextend us .. hack it back a little.  Note that this means
                           this barcode is invalid unless one of the specific end-of-symbol
                           conditions explicitly allows it. */
                        if (channel.CurrentLength == channel.EncodedLength + 8)
                        {
                            channel.CurrentLength = channel.EncodedLength;
                            channel.InputIndex--;
                        }

                        if (channel.Input.Length < channel.InputIndex)
                        {
                            throw new Exception("Channel input index exceeds range!");
                        }

                        int inputCount = channel.Input.Length - channel.InputIndex;

                        bool err = ProcessEndOfSymbolTriplet(channel, triplet, tripletCount, inputCount);
                        if (err == false)
                            return false;
                        break;
                    }

                    /* If there are no triplet values remaining in the buffer then
                       break.  This guarantees that we will always stop encoding on a
                       clean "unshifted" break */

                    if (tripletCount == 0)
                        break;
                }
            }

            /* Pre-encoded codeword is available for consumption */
            if (channel.CurrentLength < channel.EncodedLength)
            {
                IncrementProgress(channel, 8);
                channel.InputIndex++;
            }

            return true;
        }

        private static int GetC40TextX12Words(int[] outputWords, byte inputWord, DmtxScheme encScheme)
        {
            if (encScheme != DmtxScheme.DmtxSchemeX12 &&
                encScheme != DmtxScheme.DmtxSchemeText &&
                encScheme != DmtxScheme.DmtxSchemeC40)
            {
                throw new Exception("Invalid encoding scheme selected!");
            }

            int count = 0;

            /* Handle extended ASCII with Upper Shift character */
            if (inputWord > 127)
            {
                if (encScheme == DmtxScheme.DmtxSchemeX12)
                {
                    return 0;
                }

                outputWords[count++] = DmtxConstants.DmtxCharTripletShift2;
                outputWords[count++] = 30;
                inputWord -= 128;
            }

            /* Handle all other characters according to encodation scheme */
            if (encScheme == DmtxScheme.DmtxSchemeX12)
            {
                if (inputWord == 13)
                    outputWords[count++] = 0;
                else if (inputWord == 42)
                    outputWords[count++] = 1;
                else if (inputWord == 62)
                    outputWords[count++] = 2;
                else if (inputWord == 32)
                    outputWords[count++] = 3;
                else if (inputWord >= 48 && inputWord <= 57)
                    outputWords[count++] = inputWord - 44;
                else if (inputWord >= 65 && inputWord <= 90)
                    outputWords[count++] = inputWord - 51;
            }
            else
            { /* encScheme is C40 or Text */
                if (inputWord <= 31)
                {
                    outputWords[count++] = DmtxConstants.DmtxCharTripletShift1;
                    outputWords[count++] = inputWord;
                }
                else if (inputWord == 32)
                {
                    outputWords[count++] = 3;
                }
                else if (inputWord <= 47)
                {
                    outputWords[count++] = DmtxConstants.DmtxCharTripletShift2;
                    outputWords[count++] = inputWord - 33;
                }
                else if (inputWord <= 57)
                {
                    outputWords[count++] = inputWord - 44;
                }
                else if (inputWord <= 64)
                {
                    outputWords[count++] = DmtxConstants.DmtxCharTripletShift2;
                    outputWords[count++] = inputWord - 43;
                }
                else if (inputWord <= 90 && encScheme == DmtxScheme.DmtxSchemeC40)
                {
                    outputWords[count++] = inputWord - 51;
                }
                else if (inputWord <= 90 && encScheme == DmtxScheme.DmtxSchemeText)
                {
                    outputWords[count++] = DmtxConstants.DmtxCharTripletShift3;
                    outputWords[count++] = inputWord - 64;
                }
                else if (inputWord <= 95)
                {
                    outputWords[count++] = DmtxConstants.DmtxCharTripletShift2;
                    outputWords[count++] = inputWord - 69;
                }
                else if (inputWord == 96 && encScheme == DmtxScheme.DmtxSchemeText)
                {
                    outputWords[count++] = DmtxConstants.DmtxCharTripletShift3;
                    outputWords[count++] = 0;
                }
                else if (inputWord <= 122 && encScheme == DmtxScheme.DmtxSchemeText)
                {
                    outputWords[count++] = inputWord - 83;
                }
                else if (inputWord <= 127)
                {
                    outputWords[count++] = DmtxConstants.DmtxCharTripletShift3;
                    outputWords[count++] = inputWord - 96;
                }
            }

            return count;
        }

        private bool ProcessEndOfSymbolTriplet(DmtxChannel channel, DmtxTriplet triplet, int tripletCount, int inputCount)
        {
            bool err;

            /* In this function we process some special cases from the Data Matrix
             * standard, and as such we circumvent the normal functions for
             * accomplishing certain tasks.  This breaks our public counts, but this
             * function always marks the end of processing so it will not affect
             * anything downstream.  This approach allows the normal encoding functions
             * to be built with very strict checks and assertions.
             *
             * EXIT CONDITIONS:
             *
             *   triplet  symbol  action
             *   -------  ------  -------------------
             *         1       0  need bigger symbol
             *         1       1  special case (d)
             *         1       2  special case (c)
             *         1       3  unlatch ascii pad
             *         1       4  unlatch ascii pad pad
             *         2       0  need bigger symbol
             *         2       1  need bigger symbol
             *         2       2  special case (b)
             *         2       3  unlatch ascii ascii
             *         2       4  unlatch ascii ascii pad
             *         3       0  need bigger symbol
             *         3       1  need bigger symbol
             *         3       2  special case (a)
             *         3       3  c40 c40 unlatch
             *         3       4  c40 c40 unlatch pad
             */

            /* We should always reach this point on a byte boundary */
            if (channel.CurrentLength % 12 != 0)
            {
                throw new Exception("Invalid current length for encoding!");
            }

            /* XXX Capture how many extra input values will be counted ... for later adjustment */
            int inputAdjust = tripletCount - inputCount;

            /* Find minimum symbol size big enough to accomodate remaining codewords */
            int currentByte = channel.CurrentLength / 12;

            DmtxSymbolSize sizeIdx = FindCorrectSymbolSize(currentByte + ((inputCount == 3) ? 2 : inputCount), _sizeIdxRequest);

            if (sizeIdx == DmtxSymbolSize.DmtxSymbolShapeAuto)
                return false;

            /* XXX test for sizeIdx == DmtxUndefined here */
            int remainingCodewords = DmtxCommon.GetSymbolAttribute(DmtxSymAttribute.DmtxSymAttribSymbolDataWords, sizeIdx) - currentByte;

            /* XXX the big problem with all of these special cases is what if one of
               these last words requires multiple bytes in ASCII (like upper shift?).
               We probably need to add a test against this and then just force an
               unlatch if we see this coming. */

            /* Special case (d): Unlatch is implied (switch manually) */
            if (inputCount == 1 && remainingCodewords == 1)
            {
                ChangeEncScheme(channel, DmtxScheme.DmtxSchemeAscii, DmtxUnlatch.Implicit);
                err = EncodeNextWord(channel, DmtxScheme.DmtxSchemeAscii);
                if (err == false)
                    return false;
                if (channel.Invalid != DmtxChannelStatus.DmtxChannelValid || channel.InputIndex != channel.Input.Length)
                {
                    throw new Exception("Error processing end of symbol triplet!");
                }
            }
            else if (remainingCodewords == 2)
            {
                /* Special case (a): Unlatch is implied */
                if (tripletCount == 3)
                {
                    PushTriplet(channel, triplet);
                    IncrementProgress(channel, 24);
                    channel.EncScheme = DmtxScheme.DmtxSchemeAscii;
                    channel.InputIndex += 3;
                    channel.InputIndex -= inputAdjust;
                }
                /* Special case (b): Unlatch is implied */
                else if (tripletCount == 2)
                {
                    /*       assert(2nd C40 is not a shift character); */
                    triplet.Value[2] = 0;
                    PushTriplet(channel, triplet);
                    IncrementProgress(channel, 24);
                    channel.EncScheme = DmtxScheme.DmtxSchemeAscii;
                    channel.InputIndex += 2;
                    channel.InputIndex -= inputAdjust;
                }
                /* Special case (c) */
                else if (tripletCount == 1)
                {
                    ChangeEncScheme(channel, DmtxScheme.DmtxSchemeAscii, DmtxUnlatch.Explicit);
                    err = EncodeNextWord(channel, DmtxScheme.DmtxSchemeAscii);
                    if (err == false)
                        return false;
                    if (channel.Invalid != DmtxChannelStatus.DmtxChannelValid)
                    {
                        throw new Exception("Error processing end of symbol triplet!");
                    }
                    /* XXX I can still think of a case that looks ugly here.  What if
                       the final 2 C40 codewords are a Shift word and a non-Shift
                       word.  This special case will unlatch after the shift ... which
                       is probably legal but I'm not loving it.  Give it more thought. */
                }
            }
            else
            {
                /*    assert(remainingCodewords == 0 || remainingCodewords >= 3); */

                currentByte = channel.CurrentLength / 12;
                remainingCodewords = DmtxCommon.GetSymbolAttribute(DmtxSymAttribute.DmtxSymAttribSymbolDataWords, sizeIdx) - currentByte;

                if (remainingCodewords > 0)
                {
                    ChangeEncScheme(channel, DmtxScheme.DmtxSchemeAscii, DmtxUnlatch.Explicit);

                    while (channel.InputIndex < channel.Input.Length)
                    {
                        err = EncodeNextWord(channel, DmtxScheme.DmtxSchemeAscii);
                        if (err == false)
                            return false;

                        if (channel.Invalid != DmtxChannelStatus.DmtxChannelValid)
                        {
                            throw new Exception("Error processing end of symbol triplet!");
                        }
                    }
                }
            }

            if (channel.InputIndex != channel.Input.Length)
            {
                throw new Exception("Could not fully process end of symbol triplet!");
            }

            return true;
        }

        private static void PushTriplet(DmtxChannel channel, DmtxTriplet triplet)
        {
            int tripletValue = (1600 * triplet.Value[0]) + (40 * triplet.Value[1]) + triplet.Value[2] + 1;
            PushInputWord(channel, (byte)(tripletValue / 256));
            PushInputWord(channel, (byte)(tripletValue % 256));
        }

        private static bool EncodeAsciiCodeword(DmtxChannel channel)
        {
            if (channel.EncScheme != DmtxScheme.DmtxSchemeAscii)
            {
                throw new Exception("Invalid encoding scheme selected!");
            }

            byte inputValue = channel.Input[channel.InputIndex];

            /* XXX this is problematic ... We should not be looking backward in the
               channel to determine what state we're in. Add the necessary logic to
               fix the current bug (prevprev != 253) but when rewriting encoder later
               make sure double digit ascii as treated as a forward-encoded condition.
               i.e., encode ahead of where we currently stand, and not comparable to other
               channels because currently sitting between byte boundaries (like the
               triplet-based schemes). Much simpler. */

            /* XXX another thought on the future rewrite: if adopting a forward-encoding
               approach on double digits then the following input situation:

                  digit digit c40a c40b c40c

               would create:

                  ASCII_double C40_triplet1ab C40_triplet2bc

               although it might be more efficient in some cases to do

                  digit C40_triplet1(digit a) C40_triplet2(a b)

               (I can't think of a situation like this, but I can't rule it out either)
               Unfortunately the forward encoding approach would never allow ascii to unlatch
               between the ASCII_double input words.

               One approach that would definitely avoid this is to treat ASCII_dd as a
               separate channel when using "--best".  However, when encoding to single-
               scheme ascii you would always use the ASCII_dd approach.

               This function, EncodeAsciiCodeword(), should have another parameter to flag
               whether or not to compress double digits. When encoding in single scheme
               ascii, then compress the double digits. If using --best then use both options
               as separate channels. */

            /* 2nd digit char in a row - overwrite first digit word with combined value */
            if (IsDigit(inputValue) && channel.CurrentLength >= channel.FirstCodeWord + 12)
            {
                int prevIndex = (channel.CurrentLength - 12) / 12;
                byte prevValue = (byte)(channel.EncodedWords[prevIndex] - 1);

                byte prevPrevValue = (prevIndex > channel.FirstCodeWord / 12) ? channel.EncodedWords[prevIndex - 1] : (byte)0;

                if (prevPrevValue != 235 && IsDigit(prevValue))
                {
                    channel.EncodedWords[prevIndex] = (byte)(10 * (prevValue - '0') + (inputValue - '0') + 130);
                    channel.InputIndex++;
                    return true;
                }
            }

            /* Extended ASCII char */
            if (inputValue == DmtxConstants.DmtxCharFNC1)
            {
                PushInputWord(channel, DmtxConstants.DmtxCharFNC1);
                IncrementProgress(channel, 12);
                channel.InputIndex++;
                return true;
            }
            if (inputValue >= 128)
            {
                PushInputWord(channel, DmtxConstants.DmtxCharAsciiUpperShift);
                IncrementProgress(channel, 12);
                inputValue -= 128;
            }

            PushInputWord(channel, (byte)(inputValue + 1));
            IncrementProgress(channel, 12);
            channel.InputIndex++;

            return true;
        }

        private static bool IsDigit(byte inputValue)
        {
            return inputValue >= 48 && inputValue <= 57;
        }

        private static void ChangeEncScheme(DmtxChannel channel, DmtxScheme targetScheme, DmtxUnlatch unlatchType)
        {
            if (channel.EncScheme == targetScheme)
            {
                throw new Exception("Target scheme already equals channel scheme, cannot be changed!");
            }

            /* Unlatch to ASCII (base encodation scheme) */
            switch (channel.EncScheme)
            {
                case DmtxScheme.DmtxSchemeAscii:
                    /* Nothing to do */
                    if (channel.CurrentLength % 12 != 0)
                    {
                        throw new Exception("Invalid current length detected encoding ascii code");
                    }
                    break;

                case DmtxScheme.DmtxSchemeC40:
                case DmtxScheme.DmtxSchemeText:
                case DmtxScheme.DmtxSchemeX12:

                    /* Can't unlatch unless currently at a byte boundary */
                    if ((channel.CurrentLength % 12) != 0)
                    {
                        channel.Invalid = DmtxChannelStatus.DmtxChannelCannotUnlatch;
                        return;
                    }

                    /* Can't unlatch if last word in previous triplet is a shift */
                    if (channel.CurrentLength != channel.EncodedLength)
                    {
                        channel.Invalid = DmtxChannelStatus.DmtxChannelCannotUnlatch;
                        return;
                    }

                    /* Unlatch to ASCII and increment progress */
                    if (unlatchType == DmtxUnlatch.Explicit)
                    {
                        PushInputWord(channel, (byte)DmtxConstants.DmtxCharTripletUnlatch);
                        IncrementProgress(channel, 12);
                    }
                    break;

                case DmtxScheme.DmtxSchemeEdifact:

                    /* must overwrite next 6 bits (after current) with 011111 (31) and
                       then fill remaining bits until next byte bounday with zeros
                       then set encodedLength, encodedTwothirdsbits, currentLength,
                       currentTwothirdsbits.  PushInputWord guarantees that remaining
                       bits are padded to 0, so just push the unlatch code and then
                       increment current and encoded length */
                    if (channel.CurrentLength % 3 != 0)
                    {
                        throw new Exception("Error changing encryption scheme, current length is invalid!");
                    }

                    if (unlatchType == DmtxUnlatch.Explicit)
                    {
                        PushInputWord(channel, (byte)DmtxConstants.DmtxCharEdifactUnlatch);
                        IncrementProgress(channel, 9);
                    }

                    /* Advance progress to next byte boundary */
                    int advance = channel.CurrentLength % 4 * 3;
                    channel.CurrentLength += advance;
                    channel.EncodedLength += advance;
                    /* assert(remaining bits are zero); */
                    break;

                case DmtxScheme.DmtxSchemeBase256:

                    /* since Base 256 stores the length value at the beginning of the
                       string instead of using an unlatch character, "unlatching" Base
                       256 involves going to the beginning of this stretch of Base 256
                       codewords and update the placeholder with the current length.
                       Note that the Base 256 length value can either be 1 or 2 bytes,
                       depending on the length of the current stretch of Base 256
                       chars.  However, this value will already have the correct
                       number of codewords allocated since this is checked every time
                       a new Base 256 codeword is pushed to the channel. */
                    break;
            }
            channel.EncScheme = DmtxScheme.DmtxSchemeAscii;

            /* Latch to new encodation scheme */
            switch (targetScheme)
            {
                case DmtxScheme.DmtxSchemeAscii:
                    /* Nothing to do */
                    break;
                case DmtxScheme.DmtxSchemeC40:
                    PushInputWord(channel, DmtxConstants.DmtxCharC40Latch);
                    IncrementProgress(channel, 12);
                    break;
                case DmtxScheme.DmtxSchemeText:
                    PushInputWord(channel, DmtxConstants.DmtxCharTextLatch);
                    IncrementProgress(channel, 12);
                    break;
                case DmtxScheme.DmtxSchemeX12:
                    PushInputWord(channel, DmtxConstants.DmtxCharX12Latch);
                    IncrementProgress(channel, 12);
                    break;
                case DmtxScheme.DmtxSchemeEdifact:
                    PushInputWord(channel, DmtxConstants.DmtxCharEdifactLatch);
                    IncrementProgress(channel, 12);
                    break;
                case DmtxScheme.DmtxSchemeBase256:
                    PushInputWord(channel, DmtxConstants.DmtxCharBase256Latch);
                    IncrementProgress(channel, 12);

                    /* Write temporary field length (0 indicates remainder of symbol) */
                    PushInputWord(channel, Randomize255State(0, 2));
                    IncrementProgress(channel, 12);
                    break;
            }
            channel.EncScheme = targetScheme;
            channel.FirstCodeWord = channel.CurrentLength - 12;
            if (channel.FirstCodeWord % 12 != 0)
            {
                throw new Exception("Error while changin encoding scheme, invalid first code word!");
            }
        }

        private static byte Randomize255State(byte codewordValue, int codewordPosition)
        {
            int pseudoRandom = (149 * codewordPosition % 255) + 1;
            int tmp = codewordValue + pseudoRandom;

            return (byte)((tmp <= 255) ? tmp : tmp - 256);
        }

        private static void IncrementProgress(DmtxChannel channel, int encodedUnits)
        {
            /* XXX this function became a misnomer when we started incrementing by
             * an amount other than what was specified with the C40/Text exception.
             * Maybe a new name/convention is in order.
             */

            /* In C40 and Text encodation schemes while we normally use 5 1/3 bits
             * to encode a regular character, we also must account for the extra
             * 5 1/3 bits (for a total of 10 2/3 bits that gets used for a shifted
             * character.
             */

            if (channel.EncScheme == DmtxScheme.DmtxSchemeC40 || channel.EncScheme == DmtxScheme.DmtxSchemeText)
            {

                int pos = channel.CurrentLength % 6 / 2;
                int startByte = (channel.CurrentLength / 12) - (pos >> 1);
                DmtxTriplet triplet = GetTripletValues(channel.EncodedWords[startByte], channel.EncodedWords[startByte + 1]);

                /* Note that we will alway increment progress according to a whole
                   input codeword, so the value at "pos" is guaranteed to not be in
                   a shifted state. */
                if (triplet.Value[pos] <= 2)
                    channel.CurrentLength += 8;
            }

            channel.CurrentLength += encodedUnits;
        }

        private static DmtxTriplet GetTripletValues(byte cw1, byte cw2)
        {
            DmtxTriplet triplet = new();

            /* XXX this really belongs with the decode functions */

            int compact = (cw1 << 8) | cw2;
            triplet.Value[0] = (byte)((compact - 1) / 1600);
            triplet.Value[1] = (byte)((compact - 1) / 40 % 40);
            triplet.Value[2] = (byte)((compact - 1) % 40);

            return triplet;
        }

        private static DmtxQuadruplet GetQuadrupletValues(byte cw1, byte cw2, byte cw3)
        {
            DmtxQuadruplet quad = new();

            /* XXX this really belongs with the decode functions */

            quad.Value[0] = (byte)(cw1 >> 2);
            quad.Value[1] = (byte)(((cw1 & 0x03) << 4) | ((cw2 & 0xf0) >> 4));
            quad.Value[2] = (byte)(((cw2 & 0x0f) << 2) | ((cw3 & 0xc0) >> 6));
            quad.Value[3] = (byte)(cw3 & 0x3f);

            return quad;
        }

        private static void InitChannel(DmtxChannel channel, byte[] codewords)
        {
            channel.EncScheme = DmtxScheme.DmtxSchemeAscii;
            channel.Invalid = DmtxChannelStatus.DmtxChannelValid;
            channel.InputIndex = 0;
            channel.Input = codewords;
        }


        public byte[] GetRawDataAndSetEncoding(string code, DmtxEncoderOptions options)
        {
            byte[] result = options.Encoding.GetBytes(code);
            Scheme = options.Scheme;
            if (options.Scheme == DmtxScheme.DmtxSchemeAsciiGS1)
            {
                List<byte> prefixedRawData = new(new[] { (byte)232 });
                prefixedRawData.AddRange(result);
                result = prefixedRawData.ToArray();
                Scheme = DmtxScheme.DmtxSchemeAscii;
            }
            return result;
        }

        #region Properties

        public DmtxScheme Scheme
        {
            get { return _scheme; }
            set { _scheme = value; }
        }

        public DmtxSymbolSize SizeIdxRequest
        {
            get { return _sizeIdxRequest; }
            set { _sizeIdxRequest = value; }
        }

        public int MarginSize
        {
            get { return _marginSize; }
            set { _marginSize = value; }
        }

        public int ModuleSize
        {
            get { return _moduleSize; }
            set { _moduleSize = value; }
        }

        public DmtxMessage Message
        {
            get { return _message; }
            set { _message = value; }
        }

        public DmtxRegion Region
        {
            get { return _region; }
            set { _region = value; }
        }
        #endregion
    }
}
