using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] ParticleSystem deathFX;
    ScoreBoard scoreBoard;

    [SerializeField] int scorePerHit = 12;
	
    [SerializeField] int hits = 10;
    
    void Awake()
    {
        AddBoxCollider();
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    void AddBoxCollider()
    {
        Collider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = false;
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();

        if (hits < 1)
            KillEnemy();
    }

    void ProcessHit()
    {
        scoreBoard.ScoreHit(scorePerHit);
        hits--;
    }

    void KillEnemy()
    {
        ParticleSystem deathFXClone = Instantiate(deathFX, transform.position, Quaternion.identity);
        Destroy(deathFXClone.gameObject, deathFXClone.main.duration);

        Destroy(gameObject);
    }
}
