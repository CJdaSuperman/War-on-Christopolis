using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Buttons
{
    /// <summary>
    /// The behavior for the continue button
    /// </summary>
    public class ContinueButton : MonoBehaviour
    {
        [SerializeField]
        private Button m_button;

        [SerializeField]
        private UIManager m_uiManager;

        private void Awake()
        {
            if (!m_button)
                Debug.LogError($"{gameObject.name} doesn't have a reference to it's button.");

            if (!m_uiManager)
                Debug.LogError($"{gameObject.name} doesn't have a reference to the UI manager.");
        }

        private void Start() => m_button.onClick.AddListener(m_uiManager.TogglePause);
    }
}
