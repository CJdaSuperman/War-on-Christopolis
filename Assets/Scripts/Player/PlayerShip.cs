using Managers;
using UnityEngine;

namespace Player
{
    /// <summary>
    /// Controls the behavior of the Player ship GameObject
    /// </summary>
    public class PlayerShip : MonoBehaviour
    {
        [System.Serializable]
        private struct ShipMovementAttributes
        {
            [Tooltip("in ms^-1")]
            public float xSpeed;

            [Tooltip("in ms^-1")]
            public float ySpeed;

            [Tooltip("pitch factor (rotation around x-axis) depending on position")]
            public float positionPitchFactor;

            [Tooltip("yaw factor (rotation around y-axis) depending on position")]
            public float positionYawFactor;

            [Tooltip("pitch factor (rotation around x-axis) for nose direction")]
            public float controlPitchFactor;

            [Tooltip("roll factor (rotation around z-axis) for nose direction")]
            public float controlRollFactor;
        }

        [SerializeField]
        private ShipMovementAttributes m_movementAttributes;

        [Tooltip("The local position x coordinate limit")]
        [SerializeField]
        private float m_horizontalThrowLimit;

        [Tooltip("The local position y coordinate limit")]
        [SerializeField]
        private float m_verticalThrowLimit;

        [SerializeField]
        private PlayerGuns m_guns;

        [SerializeField]
        private PlayerCollision m_collisionHandler;

        private PlayerShipMotor m_motor;
        private float m_horizontalThrow, m_verticalThrow;
        private bool m_isControlEnabled = true;

        private void Awake()
        {
            if (!m_guns)
                Debug.LogError($"{gameObject.name} doesn't have a reference to the guns.");

            if (!m_collisionHandler)
                Debug.LogError($"{gameObject.name} doesn't have a reference to the collision handler.");
            else
                m_collisionHandler.OnCollision += () => m_isControlEnabled = false;

            m_motor = new PlayerShipMotor(gameObject.transform);
        }

        private void Update()
        {
            if (m_isControlEnabled)
            {
                m_horizontalThrow = InputManager.GetHorizontalAxis();
                m_verticalThrow = InputManager.GetVerticalAxis();

                SetPosition();
                SetRotation();

                m_guns.SetGunsActive(InputManager.IsFiring());
            }
        }

        private void SetPosition()
        {
            // The distance the ship will move is how far the throw is, multiplied by how fast 
            // you want to move, multiplied by the frame time--which makes it frame independent
            float xOffsetThisFrame = m_horizontalThrow * m_movementAttributes.xSpeed * Time.deltaTime;
            float yOffsetThisFrame = m_verticalThrow   * m_movementAttributes.ySpeed * Time.deltaTime;

            m_motor.SetNewPosition(xOffsetThisFrame,
                                   yOffsetThisFrame,
                                   m_horizontalThrowLimit,
                                   m_verticalThrowLimit);
        }

        private void SetRotation()
        {
            // Depending on the vertical location, the pitch will be the position multiplied by the
            // position pitch factor
            float pitchDueToPosition = transform.localPosition.y * m_movementAttributes.positionPitchFactor;

            // Only having the position pitch factor will not give a noticeable rotation to the ship's
            // nose, the control pitch factor is what gives the ship that look of the nose pointing up/down
            float pitchDueToControlThrow = m_verticalThrow * m_movementAttributes.controlPitchFactor;

            float pitch = pitchDueToPosition + pitchDueToControlThrow;
            float yaw = transform.localPosition.x * m_movementAttributes.positionYawFactor;
            float roll = m_horizontalThrow * m_movementAttributes.controlRollFactor;

            m_motor.SetNewRotation(pitch, yaw, roll);
        }
    }
}
