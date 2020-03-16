using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    [Header("General")]        
    [Tooltip("in ms^-1")][SerializeField] float xSpeed;
    [Tooltip("in ms^-1")] [SerializeField] float ySpeed;
    [SerializeField] float xBoundary;
    [SerializeField] float yBoundary;

    [Header("Screen-Positioned Based")]
    //depending on the position of the ship, the pitch (rotation along x-axis) will be 5 times 
    //the current position; and it's negative because the ship will pitch opposite of the direction
    //it's going. i.e. when y position is two, rotate up by 10--creating a ration of -5
    [SerializeField] float positionPitchFactor = -5f;
    [SerializeField] float positionYawFactor = 1.5f;

    [Header("Control-throw Based")]
    //how noticeable the nose will turn up/down when the key is pressed
    //NOTE: only having the position factor will make the ship straight ahead of it's current
    //position, the control pitch factor is what give the ship that look of the nose point up/down
    [SerializeField] float controlPitchFactor = -20f;        
    [SerializeField] float controlRollFactor = -20f;

    float horizontalThrow, VerticalThrow;
    bool isControlEnabled = true;

    [SerializeField] GameObject[] guns;

    [SerializeField] GameObject exitMenuUI;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
            ToggleExitMenu();
        
        if(isControlEnabled)
        {
            ProcessMovement();
            ProcessRotation();
            ProcessFiring();
        }        
    }    

    void ToggleExitMenu()
    {
        exitMenuUI.SetActive(!exitMenuUI.activeSelf);

        PauseGame();
    }

    void PauseGame()
    {
        if (exitMenuUI.activeSelf)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;
    }

    void ProcessMovement()
    {
        //captures input along horizontal axis--a on a keyboard is left, d is right; a joystick
        //will use standard left and right movement along x-axis
        horizontalThrow = CrossPlatformInputManager.GetAxis("Horizontal");

        VerticalThrow = CrossPlatformInputManager.GetAxis("Vertical");

        //distance ship will move is how far is the throw, multiplied by how fast you want to move,
        //multiplied by the frame time--which makes it frame independent
        float xOffsetThisFrame = horizontalThrow * xSpeed * Time.deltaTime;
        float yOffsetThisFrame = VerticalThrow * ySpeed * Time.deltaTime;

        //the current position of the object adding the distance it will move
        float rawNewXPos = transform.localPosition.x + xOffsetThisFrame;
        float rawNewYPos = transform.localPosition.y + yOffsetThisFrame;

        //this says if the position you're moving to is trying to go outside of the boundaries,
        //make that position the final position
        float xPos = Mathf.Clamp(rawNewXPos, -xBoundary, xBoundary);
        float yPos = Mathf.Clamp(rawNewYPos, -yBoundary, yBoundary);

        //assigns the local position with these vector coordinates
        transform.localPosition = new Vector3(xPos, transform.localPosition.y,
            transform.localPosition.z);

        transform.localPosition = new Vector3(transform.localPosition.x, yPos,
            transform.localPosition.z);
    }

    void ProcessRotation()
    {
        //depending on the vertical location, the pitch will be the position multiplied by the
        //pitch factor
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;

        float pitch = TurnNosePosition(pitchDueToPosition);

        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = horizontalThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    float TurnNosePosition(float pitchDueToPosition)
    {
        float pitchDueToControlThrow = VerticalThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        return pitch;
    }

    void ProcessFiring()
    {
        if (CrossPlatformInputManager.GetButton("Fire"))
            SetGunsActive(true);
        else
            SetGunsActive(false);
    }

    void SetGunsActive(bool isActive)
    {
        foreach (GameObject gun in guns)
        {
            //without accessing the emission module of the component, letting go of the Fire
            //button stops all particles, even the ones that have been shot. I want to 
            //keep the ones that were fired out in the world, so thats under emission
            var emissionModule = gun.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }                        
    }

    public void OnPlayerDeath() => isControlEnabled = false;    
}
