/*////////////////////////////////////////////////////////////////////////
 * Copyright (c)
 * Mohawk College, 135 Fennell Ave W, Hamilton, Ontario, Canada L9C 0E5
 * Game Design (374): GAME 10003 Game Development Foundations
 *////////////////////////////////////////////////////////////////////////

namespace MohawkGame2D
{
    /// <summary>
    ///     Defines the various controller axes (analog inputs) in a generic way.
    /// </summary>
    /// <remarks>
    ///     Wrapper around Raylib's GamepadAxis
    /// </remarks>
    public enum ControllerAxis
    {
        /// <summary>
        ///     Controller left stick horizontal axis.
        /// </summary>
        LeftX,

        /// <summary>
        ///     Controller left stick vertical axis.
        /// </summary>
        LeftY,

        /// <summary>
        ///     Controller right stick horizontal axis.
        /// </summary>
        RightX,

        /// <summary>
        ///     Controller right stick vertical axis.
        /// </summary>
        RightY,

        /// <summary>
        ///     Controller left side back trigger, pressure level: 1 through -1.
        /// </summary>
        LeftTrigger,

        /// <summary>
        ///     Controller right side back trigger, pressure level: 1 through -1.
        /// </summary>
        RightTrigger
    }
}