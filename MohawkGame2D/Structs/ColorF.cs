/*////////////////////////////////////////////////////////////////////////
 * Copyright (c)
 * Mohawk College, 135 Fennell Ave W, Hamilton, Ontario, Canada L9C 0E5
 * Game Design (374): GAME 10003 Game Development Foundations
 *////////////////////////////////////////////////////////////////////////

namespace MohawkGame2D
{
    /// <summary>
    ///     Represents an RGBA color (128-bit) using 32-bit float color components.
    /// </summary>
    public struct ColorF
    {
        #region Fields and Properties

        private float r;
        private float g;
        private float b;
        private float a;

        /// <summary>
        ///     Red colour channel.
        /// </summary>
        public float R
        {
            readonly get => r;
            set => r = ConstrainFloat0To1(value);
        }
        /// <summary>
        ///     Green colour channel.
        /// </summary>
        public float G
        {
            readonly get => g;
            set => g = ConstrainFloat0To1(value);
        }
        /// <summary>
        ///     Blue colour channel.
        /// </summary>
        public float B
        {
            readonly get => b;
            set => b = ConstrainFloat0To1(value);
        }
        /// <summary>
        ///     Alpha colour channel.
        /// </summary>
        public float A
        {
            readonly get => a;
            set => a = ConstrainFloat0To1(value);
        }

        #endregion

        #region Operators

        [GeneratorTools.OmitFromDocumentation]
        public static implicit operator Raylib_cs.Color(ColorF colorF)
        {
            byte r = FloatToByte(colorF.r);
            byte g = FloatToByte(colorF.g);
            byte b = FloatToByte(colorF.b);
            byte a = FloatToByte(colorF.a);
            Raylib_cs.Color raylibColor = new(r, g, b, a);
            return raylibColor;
        }

        [GeneratorTools.OmitFromDocumentation]
        public static implicit operator ColorF(Raylib_cs.Color raylibColor)
        {
            float r = ByteToFloat(raylibColor.R);
            float g = ByteToFloat(raylibColor.G);
            float b = ByteToFloat(raylibColor.B);
            float a = ByteToFloat(raylibColor.A);
            ColorF color = new(r, g, b, a);
            return color;
        }

        [GeneratorTools.OmitFromDocumentation]
        public static implicit operator Color(ColorF colorF)
        {
            byte r = FloatToByte(colorF.r);
            byte g = FloatToByte(colorF.g);
            byte b = FloatToByte(colorF.b);
            byte a = FloatToByte(colorF.a);
            Color color = new(r, g, b, a);
            return color;
        }

        [GeneratorTools.OmitFromDocumentation]
        public static implicit operator ColorF(Color color)
        {
            float r = ByteToFloat((byte)color.R);
            float g = ByteToFloat((byte)color.G);
            float b = ByteToFloat((byte)color.B);
            float a = ByteToFloat((byte)color.A);
            ColorF colorF = new(r, g, b, a);
            return colorF;
        }

        #endregion

        #region Constructors

        /// <summary>
        ///     Create a new color. Black.
        /// </summary>
        public ColorF()
        {
            r = g = b = 0f;
            a = 1f;
        }

        /// <summary>
        ///     Create a new grayscale color using the <paramref name="intensity"/> value.
        /// </summary>
        /// <param name="intensity">The intesity (brightness).</param>
        public ColorF(float intensity)
        {
            r = g = b = ConstrainFloat0To1(intensity);
            a = 1f;
        }

        /// <summary>
        ///     Create a new grayscale color using the <paramref name="intensity"/> value
        ///     with <paramref name="opacity"/>.
        /// </summary>
        /// <param name="intensity">The intesity (brightness).</param>
        /// <param name="opacity">0f for fully translucid, 1f for fully opaque.</param>
        public ColorF(float intensity, float opacity)
        {
            r = g = b = ConstrainFloat0To1(intensity);
            A = opacity;
        }

        /// <summary>
        ///     Creates a new RGB color.
        /// </summary>
        /// <param name="r">Red color channel.</param>
        /// <param name="g">Green color channel.</param>
        /// <param name="b">Blue color channel.</param>
        public ColorF(float r, float g, float b)
        {
            R = r;
            G = g;
            B = b;
            a = 1f;
        }

        /// <summary>
        ///     Creates a new RGBA color.
        /// </summary>
        /// <param name="r">Red color channel.</param>
        /// <param name="g">Green color channel.</param>
        /// <param name="b">Blue color channel.</param>
        /// <param name="a">Alpha channel.</param>
        public ColorF(float r, float g, float b, float a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        #endregion

        #region Private Methods

        private static float ConstrainFloat0To1(float value)
        {
            float zeroToOne = System.Math.Clamp(value, 0f, 1f);
            return zeroToOne;
        }

        private static byte FloatToByte(float value)
        {
            byte @byte = (byte)(value * 255);
            return @byte;
        }

        private static float ByteToFloat(byte value)
        {
            float @float = value / 255f;
            return @float;
        }

        #endregion

        public override readonly string ToString()
        {
            string value = $"{nameof(ColorF)}({R:0.00},{G:0.00},{B:0.00},{A:0.00})";
            return value;
        }
    }
}