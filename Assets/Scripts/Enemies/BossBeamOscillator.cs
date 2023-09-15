using UnityEngine;

namespace Enemies
{
    /// <summary>
    /// Defines how the boss beam will oscillate
    /// </summary>
    [DisallowMultipleComponent]
    public class BossBeamOscillator : MonoBehaviour
    {
        private const float Tau = Mathf.PI * 2f;

        [SerializeField]
        private Transform m_start, m_end;

        [SerializeField]
        private float m_period;

        private void Update() => Oscillate();

        private void Oscillate()
        {
            if (m_period <= Mathf.Epsilon) 
                return;

            float cycles = Time.time / m_period;

            float rawSinWave = Mathf.Sin(cycles * Tau); //passes a radian value into the Mathf.Sin function

            float rotationMovementFactor = rawSinWave / 2f + 0.5f;

            transform.localRotation = Quaternion.Lerp(m_start.rotation, m_end.rotation, rotationMovementFactor);
        }
    }
}
