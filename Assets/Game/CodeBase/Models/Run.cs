using System;

namespace Game.CodeBase.Models
{
    public class Run
    {
        public event Action Updated;

        public int Score { get; private set; }
        public PlayerPosition PlayerPosition { get; private set; }

        public void SetScore(int newScore)
        {
            Score = newScore;
            Updated?.Invoke();
        }

        public void IncrementScore()
        {
            Score++;
        }

        public void SetPlayerPosition(PlayerPosition position)
        {
            PlayerPosition = position;
            Updated?.Invoke();
        }
    }
}
