using System;
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
    ///     Access window information.
    /// </summary>
    public static class Window
    {

        #region Fields and Properties

        /// <summary>
        ///     Window height in pixels.
        /// </summary>
        private static int height = 256;

        /// <summary>
        ///     Window width in pixels.
        /// </summary>
        private static int width = 256;

        /// <summary>
        ///     Program window target FPS.
        /// </summary>
        private static int targetFPS = 60;

        /// <summary>
        ///     Program window title.
        /// </summary>
        private static string title = "2D Game Template";

        /// <summary>
        ///     How many frames-per-second the window is running at.
        /// </summary>
        public static float CurrentFPS => Raylib.GetFPS();

        /// <summary>
        ///     Height of window in pixels.
        /// </summary>
        public static int Height
        {
            get => height;
            set => SetWidth(value);
        }

        /// <summary>
        ///     Size of window in pixels.
        /// </summary>
        public static Vector2 Size
        {
            get => new(width, height);
            set => SetSize(value);
        }

        /// <summary>
        ///     How many frames-per-second (FPS) the game tries to output every second.
        /// </summary>
        public static int TargetFPS
        {
            get => targetFPS;
            set => SetTargetFpsOrWarn(value);
        }

        /// <summary>
        ///     Title displayed on top of program window.
        /// </summary>
        public static string Title
        {
            get => title;
            set => title = value;
        }

        /// <summary>
        ///     Width of window in pixels.
        /// </summary>
        public static int Width
        {
            get => width;
            set => SetHeight(value);
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Clears the window canvas to the specified <paramref name="color"/>.
        /// </summary>
        /// <param name="color">The background color to paint.</param>
        public static void ClearBackground(Color color)
        {
            // Not sure what it does, but not working with double buffer
            //Raylib.ClearBackground(color);

            // Alternative
            Raylib.DrawRectangle(0, 0, width, height, color);
        }

        /// <summary>
        ///     Set the window size in pixels.
        /// </summary>
        /// <param name="width">Width of window in pixels.</param>
        /// <param name="height">Height of window in pixels.</param>
        public static void SetSize(int width, int height)
        {
            Window.width = width;
            Window.height = height;
            Raylib.SetWindowSize(width, height);
        }

        /// <summary>
        ///     Set the program window title.
        /// </summary>
        /// <param name="value">The new title to display.</param>
        public static void SetTitle(string value)
        {
            Raylib.SetWindowTitle(value);
        }

        /// <summary>
        ///     VSync. Sets the FPS target to the monitor's refresh rate.
        /// </summary>
        /// <remarks>
        ///     The monitor selected is based on which monitor the
        ///     window is currently in.
        /// </remarks>
        public static void SetFpsToMonitorRefreshRate()
        {
            int monitorIndex = Raylib.GetCurrentMonitor();
            int hz = Raylib.GetMonitorRefreshRate(monitorIndex);
            Raylib.SetTargetFPS(hz);
        }

        #endregion

        #region Private Methods

        private static void SetHeight(int height)
        {
            Window.height = height;
            Raylib.SetWindowSize(width, height);
        }

        private static void SetSize(Vector2 size)
        {
            int width = (int)size.X;
            int height = (int)size.Y;
            Raylib.SetWindowSize(width, height);
        }

        private static void SetTargetFpsOrWarn(int targetFPS)
        {
            if (targetFPS <= 0)
            {
                string msg = "FPS must be greater than 0!";
                Console.WriteLine(msg);
            }

            Window.targetFPS = targetFPS;
            Raylib.SetTargetFPS(targetFPS);
        }

        private static void SetWidth(int width)
        {
            Window.width = width;
            Raylib.SetWindowSize(width, height);
        }

        #endregion

    }
}
