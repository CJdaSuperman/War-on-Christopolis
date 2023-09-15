using UnityEngine;

namespace Player
{
    /// <summary>
    /// Controls the transform changes of the Player ship
    /// </summary>
    public class PlayerShipMotor
    {
        private Transform m_playerShipTransform;
        private Vector3 m_currentPosition;

        public PlayerShipMotor(Transform playerShipTransform)
        {
            m_playerShipTransform = playerShipTransform;
            m_currentPosition = m_playerShipTransform.position;
        }

        /// <summary>
        /// Set the new position of the player ship
        /// </summary>
        /// <param name="xOffsetThisFrame"> The offset applied to the x coordinate </param>
        /// <param name="yOffsetThisFrame"> The offset applied to the y coordinate </param>
        /// <param name="xCoordLimit">      The limit for a x coordinate offset    </param>
        /// <param name="yCoordLimit">      The limit for a y coordinate offset    </param>
        public void SetNewPosition(float xOffsetThisFrame,
                                   float yOffsetThisFrame,
                                   float xCoordLimit,
                                   float yCoordLimit)
        {
            m_currentPosition = m_playerShipTransform.localPosition;

            float rawXPos = m_currentPosition.x + xOffsetThisFrame;
            float rawYPos = m_currentPosition.y + yOffsetThisFrame;

            float xPos = Mathf.Clamp(rawXPos, -xCoordLimit, xCoordLimit);
            float yPos = Mathf.Clamp(rawYPos, -yCoordLimit, yCoordLimit);

            m_currentPosition.Set(xPos, yPos, m_currentPosition.z);

            m_playerShipTransform.localPosition = m_currentPosition;
        }

        public void SetNewRotation(float pitch, float yaw, float roll)
        {
            m_playerShipTransform.localRotation = Quaternion.Euler(pitch, yaw, roll);
        }
    }
}
