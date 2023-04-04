using ConnectFoods.Grid.StateMachine;

namespace ConnectFoods.Grid.States
{
    public class Shuffle_State : Grid_BaseState
    {
        private float _timeCounter = 0.0f;

        public Shuffle_State(Grid_StateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {
            _timeCounter = 0.0f;

            Shuffle();
        }

        public override void Exit()
        {

        }

        public override void Tick(float deltaTime)
        {
            _timeCounter += deltaTime;

            if (_timeCounter >= stateMachine.LevelSettings.ShuffleTime)
            {
                stateMachine.SwitchStateTo_ControlState();
            }
        }

        private void Shuffle() => stateMachine.Shuffle();
    }
}