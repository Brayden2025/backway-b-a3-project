using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
/*////////////////////////////////////////////////////////////////////////
 * Copyright (c)
 * Mohawk College, 135 Fennell Ave W, Hamilton, Ontario, Canada L9C 0E5
 * Game Design (374): GAME 10003 Game Development Foundations
 *////////////////////////////////////////////////////////////////////////

using Raylib_cs;

namespace MohawkGame2D
{
    /// <summary>
    ///     Access text drawing functions.
    /// </summary>
    /// <remarks>
    ///     A static wrapper to standardize raylib's text API.
    /// </remarks>
    public static class Text
    {

        #region Fields and Properties

        /// <summary>
        ///     Records whether or not default fonts have been loaded.
        /// </summary>
        private static bool hasInitialized = false;

        /// <summary>
        ///     Internally track fonts to speed up duplicate loads and properly unload when game is quit.
        /// </summary>
        private static readonly Dictionary<string, Font> loadedFonts = [];

        /// <summary>
        ///     Text color.
        /// </summary>
        public static Color Color { get; set; } = Color.Black;

        /// <summary>
        ///     Text font.
        /// </summary>
        public static Font Font { get; set; }

        /// <summary>
        ///     Name of <see cref="Font"/>.
        /// </summary>
        public static string FontName { get; private set; } = string.Empty;

        /// <summary>
        ///     Text kerning (space between letters) in pixels.
        /// </summary>
        /// <remarks>
        ///     Default is 0px.
        /// </remarks>
        public static int Kerning { get; set; } = 0;

        /// <summary>
        ///     Get an array of all loaded music.
        /// </summary>
        public static Font[] LoadedFonts => [.. loadedFonts.Values];

        /// <summary>
        ///     Default monospace font.
        /// </summary>
        public static Font MonospaceFont { get; private set; }

        /// <summary>
        ///     Name of <see cref="MonospaceFont"/>.
        /// </summary>
        public static string MonospaceFontName { get; private set; } = string.Empty;

        /// <summary>
        ///     Text rotation in degrees (0-360), clockwise.
        /// </summary>
        public static float Rotation { get; set; } = 0;

        /// <summary>
        ///     Text size in pixels.
        /// </summary>
        public static int Size { get; set; } = 32;

        #endregion

        #region Public Methods

        /// <summary>
        ///     Draws <paramref name="text"/> at position (<paramref name="x"/>,
        ///     <paramref name="y"/>) on screen.
        /// </summary>
        /// <param name="text">The text to draw.</param>
        /// <param name="x">The X position to draw text at.</param>
        /// <param name="y">The Y position to draw text at.</param>
        public static void Draw(string text, float x, float y)
            => Draw(text, new(x, y));

        /// <summary>
        ///     Draws <paramref name="text"/> at <paramref name="position"/> on screen.
        /// </summary>
        /// <param name="text">The text to draw.</param>
        /// <param name="position">The position to draw text at.</param>
        public static void Draw(string text, Vector2 position)
            => Draw(text, position, Font);

        /// <summary>
        ///     Draws <paramref name="text"/> at position (<paramref name="x"/>,
        ///     <paramref name="y"/>) on screen using <paramref name="font"/>.
        /// </summary>
        /// <param name="text">The text to draw.</param>
        /// <param name="x">The X position to draw text at.</param>
        /// <param name="y">The Y position to draw text at.</param>
        /// <param name="font">The font to draw with.</param>
        public static void Draw(string text, float x, float y, Font font)
            => Draw(text, new(x, y), font);

        /// <summary>
        ///     Draws <paramref name="text"/> at <paramref name="position"/> on screen
        ///     using <paramref name="font"/>.
        /// </summary>
        /// <param name="text">The text to draw.</param>
        /// <param name="position">The position to draw text at.</param>
        /// <param name="font">The font to draw with.</param>
        public static void Draw(string text, Vector2 position, Font font)
        {
            Raylib.DrawTextPro(font.RaylibFont, text, position, Vector2.Zero, Rotation, Size, Kerning, Color);
        }

        /// <summary>
        ///     Loads the inital fonts.
        /// </summary>
        public static void Initialize()
        {
            // Prevent calls after the first
            if (hasInitialized)
                return;

            // Load platform-dependant monospace font
            string monospaceFontPath = GetOsDefaultMonospaceFontPath();
            MonospaceFontName = Path.GetFileName(monospaceFontPath);
            MonospaceFont = Raylib.LoadFont(monospaceFontPath);
            SetDefaultFontFilter(MonospaceFont);
            ResetFont();
            hasInitialized = true;
        }

        /// <summary>
        ///     Loads the typeface specified at <paramref name="filePath"/>
        ///     with a raasterized pixel sive of <see cref="Text.Size"/>.
        /// </summary>
        /// <param name="filePath">The path to the font file.</param>
        /// <returns>
        ///     Returns the loaded <see cref="MohawkGame2D.Font"/>.
        /// </returns>
        public static Font LoadFont(string filePath)
            => LoadFont(filePath, Size);

        /// <summary>
        ///     Loads the typeface specified at <paramref name="filePath"/>
        ///     with a raasterized pixel sive of <paramref name="fontSize"/>.
        /// </summary>
        /// <param name="filePath">The path to the font file.</param>
        /// <param name="fontSize">The font's pixel size. If drawn at a different scale, it may look blurry.</param>
        /// <returns>
        ///     Returns the loaded <see cref="MohawkGame2D.Font"/>.
        /// </returns>
        public static Font LoadFont(string filePath, int fontSize)
        {
            // Unique ID for this font at this size
            string fontKey = $"{filePath}@{fontSize}px";

            // Check if font exists
            if (loadedFonts.TryGetValue(fontKey, out Font value))
            {
                // Return existing instance of same font and size.
                if (value.RaylibFont.BaseSize == fontSize)
                    return value;
                // else
                // load font again at new size and store it
            }

            // Else, try and load from disk
            bool success = File.Exists(filePath);
            if (success)
            {
                // Load asset from disk. Assign it file path and file name.
                Font font = new()
                {
                    RaylibFont = Raylib.LoadFontEx(filePath, fontSize, null, 0),
                    FilePath = filePath,
                    FileName = Path.GetFileNameWithoutExtension(filePath),
                    Key = fontKey,
                    Size = fontSize,
                };

                // Set font filter mode
                SetDefaultFontFilter(font);

                // Add to reference dictionary for data reused on duplicate load calls.
                loadedFonts.Add(fontKey, font);

                // Return newly loaded value.
                return font;
            }
            else
            {
                string msg =
                    $"{nameof(LoadFont)}: failed to find font {filePath}. " +
                    $"Returning default font {MonospaceFontName}.";
                Console.WriteLine(msg);
                return MonospaceFont;
            }
        }

        /// <summary>
        ///     Loads the typeface with <paramref name="filename"/> and 
        ///     <paramref name="extension"/> in the user's system font directory (folder)
        ///     with a raasterized pixel sive of <see cref="Text.Size"/>.
        /// </summary>
        /// <param name="filename">The font's file name.</param>
        /// <param name="extension">The font's extension.</param>
        /// <returns>
        ///     Returns the loaded <see cref="MohawkGame2D.Font"/>.
        /// </returns>
        public static Font LoadFont(string filename, string extension)
            => LoadFont(filename, extension, Size);

        /// <summary>
        ///     Loads the typeface with <paramref name="filename"/> and 
        ///     <paramref name="extension"/> in the user's system font directory (folder)
        ///     with a raasterized pixel sive of <paramref name="fontSize"/>.
        /// </summary>
        /// <param name="filename">The font's file name.</param>
        /// <param name="extension">The font's extension.</param>
        /// <param name="fontSize">The font's pixel size. If drawn at a different scale, it may look blurry.</param>
        /// <returns>
        ///     Returns the loaded <see cref="MohawkGame2D.Font"/>.
        /// </returns>
        public static Font LoadFont(string filename, string extension, int fontSize)
        {
            // Get path to font.
            string filePath = GetOsFontPath(filename, extension);
            Font font = LoadFont(filePath, fontSize);
            return font;
        }

        /// <summary>
        ///     Resets <see cref="Font"/> to the default font.
        /// </summary>
        public static void ResetFont()
        {
            // Set main as the monospace font
            FontName = MonospaceFontName;
            Font = MonospaceFont;
        }

        /// <summary>
        ///     Unloads a <paramref name="font"/> from memory.
        /// </summary>
        /// <param name="font">The font to unload.</param>
        public static void UnloadFont(Font font)
        {
            loadedFonts.Remove(font.Key);
            Raylib.UnloadFont(font);
        }

        #endregion

        #region Private Methods

        private static string GetOsDefaultMonospaceFontPath()
        {
            string[] fontFileNames = GetOsDefaultMonospaceFontNames();
            string[] extensions = ["ttf", "otf"];

            foreach (string fileName in fontFileNames)
            {
                foreach (string extension in extensions)
                {
                    string filePath = GetOsFontPath(fileName, extension);
                    if (File.Exists(filePath))
                    {
                        return filePath;
                    }
                }
            }

            // If failed, print message...
            string msg =
                $"{nameof(GetOsDefaultMonospaceFontPath)}: " +
                $"failed to load any font file.";
            Console.WriteLine(msg);
            // then return empty string.
            return string.Empty;
        }

        private static string[] GetOsDefaultMonospaceFontNames()
        {
            string[] fontFileName = [];
            switch (Environment.OSVersion.Platform)
            {
                // Windows
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.Win32NT:
                case PlatformID.WinCE:
                    fontFileName = ["Consola", "lucon", "cour"];
                    break;
                // macOS
                case PlatformID.MacOSX:
                    fontFileName = ["SFMono-Regular", "Menlo-Regular", "Monaco-Regular"];
                    break;
                // Assume Linux
                case PlatformID.Unix:
                    fontFileName = ["DejaVu Sans Mono"];
                    break;
                default:
                    string msg =
                        $"{nameof(GetOsDefaultMonospaceFontNames)}: " +
                        $"unhandled OS {Environment.OSVersion.Platform}.";
                    Console.WriteLine(msg);
                    break;
            }
            return fontFileName;
        }

        private static string GetOsFontPath(string fontName, string type)
        {
            string fontsDir = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);
            string fontPath = $"{fontsDir}/{fontName}.{type}";
            return fontPath;
        }
        private static void SetDefaultFontFilter(Font font)
        {
            // Choose filter
            TextureFilter filter = TextureFilter.Anisotropic16X;
            // Set filter
            Raylib.SetTextureFilter(font.RaylibFont.Texture, filter);
        }

        #endregion

    }
}