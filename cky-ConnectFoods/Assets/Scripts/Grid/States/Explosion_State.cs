using ConnectFoods.Enums;
using ConnectFoods.Grid.StateMachine;
using System;

namespace ConnectFoods.Grid.States
{
    public class Explosion_State : Grid_BaseState
    {
        private Action Explode;

        public Explosion_State(Grid_StateMachine stateMachine) : base(stateMachine)
        {
            switch (stateMachine.ControlType)
            {
                case ControlTypes.Click:
                    Explode += stateMachine.TryExplodeClickedCellWithNeighbours;
                    break;
                case ControlTypes.Connect:
                    Explode += stateMachine.ExplodeConnectedCells;
                    break;
                case ControlTypes.Swipe:

                    break;
            }
        }

        public override void Enter()
        {
            stateMachine.ResetHints();

            Explode?.Invoke();
        }

        public override void Exit()
        {

        }

        public override void Tick(float deltaTime)
        {
            //stateMachine.Fall();
            //stateMachine.Fill();

            stateMachine.SwitchStateTo_ControlState();
        }
    }
}