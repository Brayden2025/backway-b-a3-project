/*////////////////////////////////////////////////////////////////////////
 * Copyright (c)
 * Mohawk College, 135 Fennell Ave W, Hamilton, Ontario, Canada L9C 0E5
 * Game Design (374): GAME 10003 Game Development Foundations
 *////////////////////////////////////////////////////////////////////////

namespace MohawkGame2D
{
    /// <summary>
    ///     Defines the various controller buttons (digital inputs) on a generic way.
    /// </summary>
    /// <remarks>
    ///     Wrapper around Raylib's GamepadButton
    /// </remarks>
    public enum ControllerButton
    {
        /// <summary>
        ///     Unknown button, just for error checking.
        /// </summary>
        //Unknown,

        /// <summary>
        ///     Controller left directional-pad up button.
        /// </summary>
        LeftFaceUp,

        /// <summary>
        ///     Controller left directional-pad right button.
        /// </summary>
        LeftFaceRight,

        /// <summary>
        ///    Controller left directional-pad down button.
        /// </summary>
        LeftFaceDown,

        /// <summary>
        ///     Controller left directional-pad left button.
        /// </summary>
        LeftFaceLeft,

        /// <summary>
        ///     Controller right button up (i.e. PS3: Triangle, Xbox: Y).
        /// </summary>
        RightFaceUp,

        /// <summary>
        ///     Controller right button right (i.e. PS3: Square, Xbox: X).
        /// </summary>
        RightFaceRight,

        /// <summary>
        ///     Controller right button down (i.e. PS3: Cross, Xbox: A).
        /// </summary>
        RightFaceDown,

        /// <summary>
        ///     Controller right button left (i.e. PS3: Circle, Xbox: B).
        /// </summary>
        RightFaceLeft,

        /// <summary>
        ///     Controller top/back trigger left (first), it could be a trailing button.
        /// </summary>
        LeftTrigger1,

        /// <summary>
        ///     Controller top/back trigger left (second), it could be a trailing button.
        /// </summary>
        LeftTrigger2,

        /// <summary>
        ///     Controller top/back trigger right (first), it could be a trailing button.
        /// </summary>
        RightTrigger1,

        /// <summary>
        ///     Controller top/back trigger right (second), it could be a trailing button.
        /// </summary>
        RightTrigger2,

        /// <summary>
        ///     Controller center buttons, left one (i.e. PS3: Select).
        /// </summary>
        MiddleLeft,

        /// <summary>
        ///     Controller center buttons, middle one (i.e. PS3: PS, Xbox: XBOX).
        /// </summary>
        Middle,

        /// <summary>
        ///     Controller center buttons, right one (i.e. PS3: Start).
        /// </summary>
        MiddleRight,

        /// <summary>
        ///     Controller left side joystick button (i.e: push into joystick).
        /// </summary>
        LeftThumb,

        /// <summary>
        ///     Controller right side joystick button (i.e: push into joystick).
        /// </summary>
        RightThumb
    }
}