using cky.Inputs;
using ConnectFoods.Grid.StateMachine;
using UnityEngine;

namespace ConnectFoods.Grid.States
{
    public class Clicking_State : Grid_BaseState
    {
        public Clicking_State(Grid_StateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {
            if (stateMachine.IsThereMove() == false)
            {
                stateMachine.SwitchStateTo_Shuffle();
            }
            else
            {
                SubscribeHandleEvents();
            }
        }

        public override void Exit()
        {
            UnsubscribeHandleEvents();
        }

        public override void Tick(float deltaTime)
        {
            //stateMachine.Fall();
            //stateMachine.Fill();
        }

        private void HandleUp(GameObject cell)
        {
            if (cell != null && cell.TryGetComponent<ICell>(out var iCell))
            {
                stateMachine.ClickedCell = iCell;

                stateMachine.SwitchStateTo_Explosion();

                stateMachine.ClickedCell = null;
            }
        }



        #region Handle Events

        private void SubscribeHandleEvents()
        {
            TouchManager2D.HandleUp += HandleUp;
        }

        private void UnsubscribeHandleEvents()
        {
            TouchManager2D.HandleUp -= HandleUp;
        }

        #endregion
    }
}