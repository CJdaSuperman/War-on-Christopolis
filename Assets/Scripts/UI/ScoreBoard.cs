using System;


namespace UI
{
    /// <summary>
    /// The defined behavior of the scoreboard
    /// </summary>
    public static class ScoreBoard
    {
        private static int s_score;

        public static int Score { get => s_score; }

        public static event Action OnUpdate;

        public static void ScoreHit(int scoreIncrease)
        {
            s_score += scoreIncrease;
            OnUpdate?.Invoke();
        }
    }
}
