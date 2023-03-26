
namespace ConnectFoods.Game
{
    [System.Serializable]
    public class GameState
    {
        public delegate void StateChangedEventHandler(State newState);
        public event StateChangedEventHandler OnStateChanged;

        public enum State { MainMenu, Playing, Paused, GameOver }
        private State currentState;

        public State CurrentState
        {
            get { return currentState; }
            set
            {
                if (currentState != value)
                {
                    currentState = value;
                    OnStateChanged?.Invoke(currentState);
                }
            }
        }
    }
}