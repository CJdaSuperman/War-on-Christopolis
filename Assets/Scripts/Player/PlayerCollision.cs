using System;
using UnityEngine;

namespace Player
{
    /// <summary>
    /// Defines the behavior upon player collision
    /// </summary>
    public class PlayerCollision : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem m_deathFX;

        [SerializeField]
        private SceneTransition m_sceneTransition;

        public event Action OnCollision;

        private void Awake()
        {
            if (!m_deathFX)
                Debug.LogError($"{gameObject.name} doesn't have a reference to the death particles.");

            if (!m_sceneTransition)
                Debug.LogError($"{gameObject.name} doesn't have a reference to the scene transition.");
        }

        private void OnTriggerEnter(Collider other)
        {
            m_deathFX.gameObject.SetActive(true);
            m_sceneTransition.ReloadScene();
            OnCollision?.Invoke();
        }
    }
}
