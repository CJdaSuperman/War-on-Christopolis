using UnityEngine;

namespace Managers
{
    /// <summary>
    /// Component for the GameObject to manage UI canvases
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private Canvas m_pauseMenu;

        private GameObject PauseMenu { get => m_pauseMenu.gameObject; }

        private void Awake()
        {
            if (!m_pauseMenu)
                Debug.LogError($"{gameObject.name} doesn't have a reference to the pause menu.");
        }

        private void Start()
        {
            PauseMenu.SetActive(false);
        }

        private void Update()
        {
            if (InputManager.PauseGame())
                TogglePause();
        }

        public void TogglePause() => PauseMenu.SetActive(!PauseMenu.activeInHierarchy);
    }
}
