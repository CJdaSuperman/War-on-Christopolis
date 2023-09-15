using UnityEngine;

namespace Managers
{
    /// <summary>
    /// Manages the input for the game
    /// </summary>
    public static class InputManager
    {
        private const string HorizontalAxis = "Horizontal";
        private const string VerticalAxis = "Vertical";

        public static float GetHorizontalAxis() => Input.GetAxis(HorizontalAxis);

        public static float GetVerticalAxis() => Input.GetAxis(VerticalAxis);

        public static bool IsFiring() => Input.GetKey(KeyCode.Space);

        public static bool PauseGame() => Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.Escape);
    }
}
