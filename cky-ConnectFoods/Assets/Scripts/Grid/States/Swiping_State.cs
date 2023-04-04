using cky.Inputs;
using ConnectFoods.Grid.StateMachine;
using UnityEngine;

namespace ConnectFoods.Grid.States
{
    public class Swiping_State : Grid_BaseState
    {
        public Swiping_State(Grid_StateMachine stateMachine) : base(stateMachine) { }

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

        }

        public override void Tick(float deltaTime)
        {
            //stateMachine.Fall();
            //stateMachine.Fill();
        }

        private void HandleClick(GameObject clickedGameObject)
        {
            //if (clickedGameObject == null) return;

            //if (clickedGameObject.TryGetComponent<ICell>(out var cell))
            //{
            //    if (cell.IsFull)
            //    {
            //        AddConnectedCell(cell);

            //        stateMachine.CollectedItemMatchType = cell.IItem.MatchType;
            //    }
            //}
        }

        private void HandleMove(GameObject objectTheCursorIsOn)
        {
            //if (objectTheCursorIsOn == null) return;

            //if (objectTheCursorIsOn.TryGetComponent<ICell>(out var cell))
            //{
            //    if (stateMachine.ConnectedCells.Contains(cell) == false)
            //    {
            //        if (stateMachine.IsLastConnectedCellNeighbours(cell))
            //        {
            //            if (stateMachine.IsCollectedItemMatchType(cell))
            //            {
            //                AddConnectedCell(cell);
            //            }
            //        }
            //    }
            //    else
            //    {
            //        RemoveConnectedCells(cell);
            //    }
            //}
        }

        private void HandleUp(GameObject obj)
        {
            //var matchCount = stateMachine.ConnectedCells.Count;

            //if (matchCount >= stateMachine.LevelSettings.MinMatchLimit)
            //{
            //    stateMachine.ResetHints();

            //    stateMachine.SwitchStateTo_Explosion();
            //}
            //else
            //{
            //    foreach (var cell in stateMachine.ConnectedCells)
            //    {
            //        cell.DeSelected();
            //    }
            //}

            //stateMachine.ConnectedCells.Clear();
            //stateMachine.Trigger_ConnectedCellsReset();
        }



        #region Handle Events

        private void SubscribeHandleEvents()
        {
            TouchManager2D.HandleClick += HandleClick;
            TouchManager2D.HandleMove += HandleMove;
            TouchManager2D.HandleUp += HandleUp;
        }

        private void UnsubscribeHandleEvents()
        {
            TouchManager2D.HandleClick -= HandleClick;
            TouchManager2D.HandleMove -= HandleMove;
            TouchManager2D.HandleUp -= HandleUp;
        }

        #endregion
    }
}