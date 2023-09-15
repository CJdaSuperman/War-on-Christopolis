using UnityEngine;

namespace UI.Menus
{
    /// <summary>
    /// Controls the PauseMenu GameObject
    /// </summary>
    public class PauseMenu : MonoBehaviour
    {
        private void OnEnable()
        {
            Time.timeScale = 0f;
        }

        private void OnDisable()
        {
            Time.timeScale = 1f;
        }
    }
}
