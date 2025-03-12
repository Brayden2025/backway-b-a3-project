using System.Diagnostics;
/*////////////////////////////////////////////////////////////////////////
 * Copyright (c)
 * Mohawk College, 135 Fennell Ave W, Hamilton, Ontario, Canada L9C 0E5
 * Game Design (374): GAME 10003 Game Development Foundations
 *////////////////////////////////////////////////////////////////////////

using Raylib_cs;

namespace MohawkGame2D
{
    /// <summary>
    ///     Access time information.
    /// </summary>
    /// <remarks>
    ///     A static wrapper to standardize raylib's time API.
    /// </remarks>
    public static class Time
    {

        #region Fields and Properties

        /// <summary>
        ///     When manually setting time, this stores the offset relative to start time.
        /// </summary>
        private static double timeOffset = 0;

        /// <summary>
        ///     The time between the last frame and this frame in seconds.
        /// </summary>
        public static float DeltaTime => GetDeltaTime();

        /// <summary>
        ///     How many frames have elapsed since the program started.
        /// </summary>
        public static int FramesElapsed { get; set; }

        /// <summary>
        ///     How much time in seconds has elapsed since the program started.
        /// </summary>
        public static float SecondsElapsed
        {
            get => (float)SecondsElapsedPrecise;
            set => SecondsElapsedPrecise = value;
        }

        #endregion

        #region Private Methods (and private properties)

        /// <summary>
        ///     How much time in seconds has elapsed (as a <see cref="double"/>).
        /// </summary>
        private static double SecondsElapsedPrecise
        {
            get => Raylib.GetTime() - timeOffset;
            set => timeOffset = Raylib.GetTime() - value;
        }

        private static float GetDeltaTime()
        {
            bool isDebugging = Debugger.IsAttached;
            float deltaTime = isDebugging ? GetFixedDeltaTime() : GetDynamicDeltaTime();
            return deltaTime;
        }

        private static float GetDynamicDeltaTime()
        {
            float dynamicDeltaTime = Raylib.GetFrameTime();
            return dynamicDeltaTime;
        }

        private static float GetFixedDeltaTime()
        {
            float fixedDeltaTime = 1f / Window.TargetFPS;
            return fixedDeltaTime;
        }

        #endregion

    }
}
