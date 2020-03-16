using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [Tooltip("in seconds")] [SerializeField] float loadLevelDelay = 1f;
    [Tooltip("FX prefab on player")]  [SerializeField] GameObject deathFX;
    
    void OnTriggerEnter(Collider other) => StartDeathSequence();

    void StartDeathSequence()
    {
        SendMessage("OnPlayerDeath");
        deathFX.SetActive(true);
        Invoke(nameof(ReloadScene), loadLevelDelay);
    }

    void ReloadScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
 }
