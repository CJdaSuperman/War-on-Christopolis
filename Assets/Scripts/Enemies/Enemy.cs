using UI;
using UnityEngine;

namespace Enemies
{
    /// <summary>
    /// Controls enemy GameObjects
    /// </summary>
    public class Enemy : MonoBehaviour
    {
        [SerializeField]
        private BoxCollider m_boxCollider;

        [SerializeField]
        private ParticleSystem m_deathFXPrefab;

        [SerializeField]
        private int m_scorePerHit;

        [SerializeField]
        private int m_hits;

        private void Awake()
        {
            if (!m_boxCollider)
                Debug.LogError($"{gameObject.name} doesn't have a reference to its BoxCollider.");

            if (!m_deathFXPrefab)
                Debug.LogError($"{gameObject.name} doesn't have a reference to its death particles prefab.");
        }

        private void OnParticleCollision(GameObject other)
        {
            ProcessHit();

            if (m_hits < 1)
                KillEnemy();
        }

        private void ProcessHit()
        {
            ScoreBoard.ScoreHit(m_scorePerHit);
            m_hits--;
        }

        private void KillEnemy()
        {
            ParticleSystem deathFXClone = Instantiate(m_deathFXPrefab, transform.position, Quaternion.identity);
            Destroy(deathFXClone.gameObject, deathFXClone.main.duration);
            Destroy(gameObject);
        }
    }
}
