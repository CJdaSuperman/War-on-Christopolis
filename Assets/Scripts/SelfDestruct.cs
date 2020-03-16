using UnityEngine;

//used because in the Enemy Script, the OnParticleCollision() method instantiates the death FX
//prefab, but when the object ships get destroyed, all that's left are copies of the death FX
//objects. So this script destroys them. 
public class SelfDestruct : MonoBehaviour
{
    [Tooltip("in seconds")] [SerializeField] float destructionDelay;

    void Start() => Destroy(gameObject, destructionDelay);
}
