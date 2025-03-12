/*////////////////////////////////////////////////////////////////////////
 * Copyright (c)
 * Mohawk College, 135 Fennell Ave W, Hamilton, Ontario, Canada L9C 0E5
 * Game Design (374): GAME 10003 Game Development Foundations
 *////////////////////////////////////////////////////////////////////////

namespace MohawkGame2D
{
    /// <summary>
    ///     Represents a 2D texture.
    /// </summary>
    /// <remarks>
    ///     Wrapper around Raylib.Texture2D
    /// </remarks>
    public readonly record struct Texture2D
    {
        /// <summary>
        ///     File path of this texture.
        /// </summary>
        public string FilePath { get; init; }

        /// <summary>
        ///     Name of this texture file.
        /// </summary>
        public string FileName { get; init; }

        /// <summary>
        ///     Texture width in pixels.
        /// </summary>
        public int Width => RaylibTexture2D.Width;

        /// <summary>
        ///     Texture height in pixels.
        /// </summary>
        public int Height => RaylibTexture2D.Height;


        [GeneratorTools.OmitFromDocumentation]
        public Raylib_cs.Texture2D RaylibTexture2D { get; init; }

        [GeneratorTools.OmitFromDocumentation]
        public static implicit operator Texture2D(Raylib_cs.Texture2D raylibTexture2D)
        {
            var font = new Texture2D()
            {
                RaylibTexture2D = raylibTexture2D,
            };
            return font;
        }

        [GeneratorTools.OmitFromDocumentation]
        public static implicit operator Raylib_cs.Texture2D(Texture2D texture2D)
        {
            var raylibTexture2D = texture2D.RaylibTexture2D;
            return raylibTexture2D;
        }

        public override readonly string ToString()
        {
            string value = $"{nameof(Texture2D)}({FilePath})";
            return value;
        }
    }
}