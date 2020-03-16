using UnityEngine;

[DisallowMultipleComponent] 
public class BossBeamOscillator : MonoBehaviour
{
    [SerializeField] Transform start, end;    

    [SerializeField] float period = 2f;    

    void Update() => Oscillate();

    void Oscillate()
    {
        if (period <= Mathf.Epsilon) { return; }

        float cycles = Time.time / period;

        const float tau = Mathf.PI * 2f; //about 6.28
        float rawSinWave = Mathf.Sin(cycles * tau); //passes a radian value into the Mathf.Sin function

        float rotationMovementFactor = rawSinWave / 2f + 0.5f;

        transform.localRotation = Quaternion.Lerp(start.rotation, end.rotation, rotationMovementFactor);
    }
}
