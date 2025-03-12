/*////////////////////////////////////////////////////////////////////////
 * Copyright (c)
 * Mohawk College, 135 Fennell Ave W, Hamilton, Ontario, Canada L9C 0E5
 * Game Design (374): GAME 10003 Game Development Foundations
 *////////////////////////////////////////////////////////////////////////

namespace MohawkGame2D
{
    /// <summary>
    ///     Represents a music file (audio over 10s long).
    /// </summary>
    /// <remarks>
    ///     Wrapper around Raylib.Music
    /// </remarks>
    public readonly record struct Music
    {
        /// <summary>
        ///     File path of this music.
        /// </summary>
        public string FilePath { get; init; }

        /// <summary>
        ///     Name of this music file.
        /// </summary>
        public string FileName { get; init; }


        [GeneratorTools.OmitFromDocumentation]
        public Raylib_cs.Music RaylibMusic { get; init; }

        [GeneratorTools.OmitFromDocumentation]
        public static implicit operator Music(Raylib_cs.Music raylibMusic)
        {
            var font = new Music()
            {
                RaylibMusic = raylibMusic,
            };
            return font;
        }

        [GeneratorTools.OmitFromDocumentation]
        public static implicit operator Raylib_cs.Music(Music music)
        {
            var raylibMusic = music.RaylibMusic;
            return raylibMusic;
        }

        public override readonly string ToString()
        {
            string value = $"{nameof(Music)}({FilePath})";
            return value;
        }
    }
}