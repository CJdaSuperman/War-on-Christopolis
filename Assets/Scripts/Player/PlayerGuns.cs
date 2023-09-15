using UnityEngine;

namespace Player
{
    /// <summary>
    /// Handles the emission of the player guns
    /// </summary>
    public class PlayerGuns : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem m_leftGun;

        [SerializeField]
        private ParticleSystem m_rightGun;

        private void Awake()
        {
            if (!m_leftGun)
                Debug.LogError($"{gameObject.name} doesn't have a reference to the left gun.");

            if (!m_rightGun)
                Debug.LogError($"{gameObject.name} doesn't have a reference to the right gun.");
        }

        public void SetGunsActive(bool isActive)
        {
            ParticleSystem.EmissionModule emissionMod;

            emissionMod = m_leftGun.emission;
            emissionMod.enabled = isActive;

            emissionMod = m_rightGun.emission;
            emissionMod.enabled = isActive;
        }
    }
}
