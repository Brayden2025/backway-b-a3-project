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
    ///     Access texture drawing functions.
    /// </summary>
    /// <remarks>
    ///     A static wrapper to standardize raylib's texture drawing API.
    /// </remarks>
    public static class Graphics
    {

        #region Fields and Properties

        /// <summary>
        ///     Internally track textures to speed up duplicate loads and properly unload when game is quit
        /// </summary>
        private static readonly Dictionary<string, Texture2D> loadedTextures = [];

        /// <summary>
        ///     Get an array of all loaded music.
        /// </summary>
        public static Texture2D[] LoadedTextures => [.. loadedTextures.Values];

        /// <summary>
        ///     Angle rotation of graphics in degrees.
        /// </summary>
        public static float Rotation { get; set; } = 0;

        /// <summary>
        ///     Scale of graphics. Default is 1.
        /// </summary>
        public static float Scale { get; set; } = 1;

        /// <summary>
        ///     Color tint of graphics. DEfault is white.
        /// </summary>
        public static Color Tint { get; set; } = Color.White;

        #endregion

        #region Public Methods

        /// <summary>
        ///     Draw a <paramref name="texture"/> graphic to the screen at
        ///     position (<paramref name="x"/>, <paramref name="y"/>).
        /// </summary>
        /// <param name="texture">The texture to draw.</param>
        /// <param name="x">The X position to draw at.</param>
        /// <param name="y">The Y position to draw at.</param>
        public static void Draw(Texture2D texture, float x, float y)
            => Draw(texture, new Vector2(x, y));

        /// <summary>
        ///     Draw a <paramref name="texture"/> graphic to the screen at
        ///     <paramref name="position"/>.
        /// </summary>
        /// <param name="texture">The texture to draw.</param>
        /// <param name="position">The position to draw at.</param>
        public static void Draw(Texture2D texture, Vector2 position)
        {
            Raylib.DrawTextureEx(texture, position, Rotation, Scale, Tint);
        }

        /// <summary>
        ///     Draw a subset of <paramref name="texture"/> graphic at screen
        ///     <paramref name="position"/>. Subset begins at upper-left corner
        ///     <paramref name="subsetOrigin"/> and size of <paramref name="subsetSize"/>.
        /// </summary>
        /// <param name="texture">The texture to draw.</param>
        /// <param name="position">The position to draw at.</param>
        /// <param name="subsetOrigin">Subset origin within the texture.</param>
        /// <param name="subsetSize">Subset size within the texture.</param>
        public static void DrawSubset(Texture2D texture, Vector2 position, Vector2 subsetOrigin, Vector2 subsetSize)
            => DrawSubset(texture, position, subsetOrigin, subsetSize, Vector2.Zero);

        /// <summary>
        ///     Draw a subset of <paramref name="texture"/> graphic at screen
        ///     <paramref name="position"/>. Subset begins at upper-left corner
        ///     <paramref name="subsetOrigin"/> and size of <paramref name="subsetSize"/>.
        ///     <paramref name="rotationOrigin"/> is the point around which the
        ///     subset is rotated; default is upper-left corner.
        /// </summary>
        /// <param name="texture">The texture to draw.</param>
        /// <param name="position">The position to draw at.</param>
        /// <param name="subsetOrigin">Subset origin within the texture.</param>
        /// <param name="subsetSize">Subset size within the texture.</param>
        /// <param name="rotationOrigin">Rotation origin of the texture subset.</param>
        public static void DrawSubset(Texture2D texture, Vector2 position, Vector2 subsetOrigin, Vector2 subsetSize, Vector2 rotationOrigin)
        {
            // Source in texture/spritesheet/atlas
            var source = new Rectangle()
            {
                Position = subsetOrigin,
                Size = subsetSize,
            };
            // destination on screen
            var destination = new Rectangle()
            {
                Position = position,
                Size = subsetSize * Scale,
            };
            Raylib.DrawTexturePro(texture, source, destination, rotationOrigin, Rotation, Tint);
        }

        /// <summary>
        ///     Loads texture at <paramref name="filePath"/> into GPU memory.
        /// </summary>
        /// <remarks>
        ///     This is slow and reads from disk. Reuse the resulting <see cref="Texture2D"/> 
        ///     where possible rather than laoding again from disk.
        /// </remarks>
        /// <param name="filePath">The texture file path.</param>
        /// <returns>
        ///     Returns the loaded texture.
        /// </returns>
        public static Texture2D LoadTexture(string filePath)
        {
            // Return existing instance if reloading same asset.
            if (loadedTextures.TryGetValue(filePath, out Texture2D value))
                return value;

            // Load asset from disk. Assign it file path and file name.
            Texture2D texture = new()
            {
                RaylibTexture2D = Raylib.LoadTexture(filePath),
                FilePath = filePath,
                FileName = Path.GetFileNameWithoutExtension(filePath)
            };

            // Add to reference dictionary for data reused on duplicate load calls.
            loadedTextures.Add(filePath, texture);

            // Return newly loaded value.
            return texture;
        }

        /// <summary>
        ///     Unloads <paramref name="texture"/> from GPU memory.
        /// </summary>
        /// <param name="texture">The texture to unload from GPU memory.</param>
        public static void UnloadTexture(Texture2D texture)
        {
            loadedTextures.Remove(texture.FilePath);
            Raylib.UnloadTexture(texture);
        }

        #endregion

    }
}