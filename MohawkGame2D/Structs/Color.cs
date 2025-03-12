/*////////////////////////////////////////////////////////////////////////
 * Copyright (c)
 * Mohawk College, 135 Fennell Ave W, Hamilton, Ontario, Canada L9C 0E5
 * Game Design (374): GAME 10003 Game Development Foundations
 *////////////////////////////////////////////////////////////////////////

using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.InteropServices;

namespace MohawkGame2D
{
    /// <summary>
    ///     Represents an RGBA color (32-bit) using 8-bit byte color components.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct Color
    {
        #region Fields and Properties

        [FieldOffset(0)] private uint raw;
        [FieldOffset(0)] private byte r;
        [FieldOffset(1)] private byte g;
        [FieldOffset(2)] private byte b;
        [FieldOffset(3)] private byte a;

        /// <summary>
        ///     Red colour channel.
        /// </summary>
        public int R
        {
            readonly get => r;
            set => r = ConstrainAsByte(value);
        }
        /// <summary>
        ///     Green colour channel.
        /// </summary>
        public int G
        {
            readonly get => g;
            set => g = ConstrainAsByte(value);
        }
        /// <summary>
        ///     Blue colour channel.
        /// </summary>
        public int B
        {
            readonly get => b;
            set => b = ConstrainAsByte(value);
        }
        /// <summary>
        ///     Alpha colour channel.
        /// </summary>
        public int A
        {
            readonly get => a;
            set => a = ConstrainAsByte(value);
        }

        #endregion

        #region Operators

        [GeneratorTools.OmitFromDocumentation]
        public static implicit operator Raylib_cs.Color(Color color)
        {
            Raylib_cs.Color raylibColor = new(color.r, color.g, color.b, color.a);
            return raylibColor;
        }

        [GeneratorTools.OmitFromDocumentation]
        public static implicit operator Color(Raylib_cs.Color raylibColor)
        {
            Color color = new(raylibColor.R, raylibColor.G, raylibColor.B, raylibColor.A);
            return color;
        }

        public static bool operator ==(Color lhs, Color rhs)
        {
            bool areSame = lhs.raw == rhs.raw;
            return areSame;
        }

        public static bool operator !=(Color lhs, Color rhs)
        {
            bool areDifferent = lhs.raw != rhs.raw;
            return areDifferent;
        }

        #endregion

        #region Constructors

        /// <summary>
        ///     Create a new color. Black.
        /// </summary>
        public Color()
        {
            r = g = b = 0;
            a = 255;
        }

        /// <summary>
        ///     Create a new grayscale color using the <paramref name="intensity"/> value.
        /// </summary>
        /// <param name="intensity">The intesity (brightness).</param>
        public Color(int intensity)
        {
            r = g = b = ConstrainAsByte(intensity);
            a = 255;
        }

        /// <summary>
        ///     Create a new grayscale color using the <paramref name="intensity"/> value
        ///     with <paramref name="opacity"/>.
        /// </summary>
        /// <param name="intensity">The intesity (brightness).</param>
        /// <param name="opacity">0 for fully translucid, 255 for fully opaque.</param>
        public Color(int intensity, int opacity)
        {
            r = g = b = ConstrainAsByte(intensity);
            A = opacity;
        }

        /// <summary>
        ///     Creates a new RGB color.
        /// </summary>
        /// <param name="r">Red color channel.</param>
        /// <param name="g">Green color channel.</param>
        /// <param name="b">Blue color channel.</param>
        public Color(int r, int g, int b)
        {
            R = r;
            G = g;
            B = b;
            a = 255;
        }

        /// <summary>
        ///     Creates a new RGBA color.
        /// </summary>
        /// <param name="r">Red color channel.</param>
        /// <param name="g">Green color channel.</param>
        /// <param name="b">Blue color channel.</param>
        /// <param name="a">Alpha channel.</param>
        public Color(int r, int g, int b, int a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        /// <summary>
        ///     Creates a new color from hex string.
        /// </summary>
        /// <param name="value">Color value as hex string.</param>
        /// <remarks>
        ///     Color components are ordered R, G, B, A.
        ///     Valid inputs include hex color "rrggbb", "rrggbbaa",
        ///     such as "ff00aa" and "00bb7780". Leading # is permitted,
        ///     such as "#ffddaa".
        /// </remarks>
        public Color(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                raw = 0;

            // Sanitive value
            value = value.Replace("#", "");
            value = value.Trim();
            value = value.ToLower();

            // Validate string characters
            for (int i = 0; i < value.Length; i++)
            {
                char c = value[i];
                bool isValidNumber = c >= '0' && c <= '9';
                bool isValidLetter = c >= 'a' && c <= 'f';
                bool isInvalid = !isValidNumber & !isValidLetter;
                if (isInvalid)
                {
                    string msg = $"Value contains non-hexadecimal character {c} ({value}).";
                    throw new ArgumentException(msg);
                }
            }

            // Validate string length
            bool validLength =
                value.Length == 3 || // "rgb"
                value.Length == 4 || // "rgba"
                value.Length == 6 || // "rrggbb"
                value.Length == 8;   // "rrggbbaa"
            if (!validLength)
            {
                string msg = $"Color value {value} is of an unexpected length.";
                throw new ArgumentException(msg);
            }

            // Parse
            if (value.Length == 6 || value.Length == 8)
            {
                // Color
                r = byte.Parse(value[0..2], NumberStyles.HexNumber);
                g = byte.Parse(value[2..4], NumberStyles.HexNumber);
                b = byte.Parse(value[4..6], NumberStyles.HexNumber);
                // Alpha
                if (value.Length == 8)
                    a = byte.Parse(value[6..8], NumberStyles.HexNumber);
                else
                    a = byte.MaxValue;
            }
            else if (value.Length == 3 || value.Length == 4)
            {
                // Color
                r = byte.Parse($"{value[0]}{value[0]}", NumberStyles.HexNumber);
                g = byte.Parse($"{value[1]}{value[1]}", NumberStyles.HexNumber);
                b = byte.Parse($"{value[2]}{value[2]}", NumberStyles.HexNumber);
                // Alpha
                if (value.Length == 4)
                    a = byte.Parse($"{value[3]}{value[3]}", NumberStyles.HexNumber);
                else
                    a = byte.MaxValue;
            }
        }

        #endregion

        #region Pre-defined Colors and Shades

        /// <summary>
        ///     RGB(0, 0, 0)
        /// </summary>
        public static readonly Color Black = new(0);
        /// <summary>
        ///     RGB(63, 63, 63)
        /// </summary>
        public static readonly Color DarkGray = new(63);
        /// <summary>
        ///     RGB(127, 127, 127)
        /// </summary>
        public static readonly Color Gray = new(127);
        /// <summary>
        ///     RGB(195, 195, 195)
        /// </summary>
        public static readonly Color LightGray = new(195);
        /// <summary>
        ///     RGB(240, 240, 240)
        /// </summary>
        public static readonly Color OffWhite = new(240);
        /// <summary>
        ///     RGB(255, 255, 255)
        /// </summary>
        public static readonly Color White = new(255);

        /// <summary>
        ///     Fully transparent. RGBA(0, 0, 0, 0)
        /// </summary>
        public static readonly Color Clear = new(0, 0);

        /// <summary>
        ///     RGB(255, 0, 0)
        /// </summary>
        public static readonly Color Red = new(255, 0, 0);
        /// <summary>
        ///     RGB(255, 255, 0)
        /// </summary>
        public static readonly Color Yellow = new(255, 255, 0);
        /// <summary>
        ///     RGB(0, 255, 0)
        /// </summary>
        public static readonly Color Green = new(0, 255, 0);
        /// <summary>
        ///     RGB(0, 255, 255)
        /// </summary>
        public static readonly Color Cyan = new(0, 255, 255);
        /// <summary>
        ///     RGB(0, 0, 255)
        /// </summary>
        public static readonly Color Blue = new(0, 0, 255);
        /// <summary>
        ///     RGB(255, 0, 255)
        /// </summary>
        public static readonly Color Magenta = new(255, 0, 255);

        #endregion

        #region Private Methods

        private static byte ConstrainAsByte(int value)
        {
            byte byteValue = (byte)System.Math.Clamp(value, 0, 255);
            return byteValue;
        }

        #endregion

        public override readonly bool Equals([NotNullWhen(true)] object? obj)
        {
            if (obj is not Color)
                return false;

            bool equals = this == (Color)obj;
            return equals;
        }

        public override readonly int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override readonly string ToString()
        {
            string value = $"{nameof(Color)}({R},{G},{B},{A})";
            return value;
        }

    }
}