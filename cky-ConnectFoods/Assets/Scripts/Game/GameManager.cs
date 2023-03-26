using UnityEngine;

namespace ConnectFoods.Game
{
    public class GameManager : MonoBehaviour
    {
        private GameState gameState;
        [SerializeField] public GameState.State currentState;

        void Start()
        {
            gameState = new GameState();

            gameState.OnStateChanged += (newState) =>
            {
                Debug.Log("State changed to: " + newState);
                currentState = newState;
            };
            gameState.CurrentState = GameState.State.Playing;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                gameState.CurrentState = GameState.State.GameOver;
            }

            switch (gameState.CurrentState)
            {
                case GameState.State.MainMenu:
                    // main menu update code here
                    break;
                case GameState.State.Playing:
                    // in game update code here
                    break;
                case GameState.State.GameOver:
                    // game over update code here
                    break;
            }
        }

        private void HandleStateChanged(GameState.State newState)
        {
            switch (newState)
            {
                case GameState.State.MainMenu:
                    // handle main menu state change here
                    break;
                case GameState.State.Playing:
                    // handle in game state change here
                    break;
                case GameState.State.GameOver:
                    // handle game over state change here
                    break;
            }
        }
    }

}
