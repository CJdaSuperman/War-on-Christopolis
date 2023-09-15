using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// Updates the scoreboard text
    /// </summary>
    public class ScoreBoardText : MonoBehaviour
    {
        [SerializeField]
        private Text m_scoreText;

        private void Awake()
        {
            if (!m_scoreText)
                Debug.LogError($"{gameObject.name} doesn't have a reference to the scoreboard text.");
            else
                m_scoreText.text = "0";

            ScoreBoard.OnUpdate += () => m_scoreText.text = ScoreBoard.Score.ToString();
        }
    }
}
