using cky.Inputs;
using ConnectFoods.Grid.StateMachine;
using UnityEngine;

namespace ConnectFoods.Grid.States
{
    public class Connecting_State : Grid_BaseState
    {
        public Connecting_State(Grid_StateMachine stateMachine) : base(stateMachine) { }

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

        private void HandleClick(GameObject clickedGameObject)
        {
            if (clickedGameObject == null) return;

            if (clickedGameObject.TryGetComponent<ICell>(out var cell))
            {
                if (cell.IsFull)
                {
                    AddConnectedCell(cell);

                    stateMachine.CollectedItemMatchType = cell.IItem.MatchType;
                }
            }
        }

        private void HandleMove(GameObject objectTheCursorIsOn)
        {
            if (objectTheCursorIsOn == null) return;

            if (objectTheCursorIsOn.TryGetComponent<ICell>(out var cell))
            {
                if (stateMachine.ConnectedCells.Contains(cell) == false)
                {
                    if (stateMachine.IsLastConnectedCellNeighbours(cell))
                    {
                        if (stateMachine.IsCollectedItemMatchType(cell))
                        {
                            AddConnectedCell(cell);
                        }
                    }
                }
                else
                {
                    RemoveConnectedCells(cell);
                }
            }
        }

        private void HandleUp(GameObject obj)
        {
            var matchCount = stateMachine.ConnectedCells.Count;

            if (matchCount >= stateMachine.LevelSettings.MinMatchLimit)
            {
                stateMachine.SwitchStateTo_Explosion();
            }
            else
            {
                foreach (var cell in stateMachine.ConnectedCells)
                {
                    cell.DeSelected();
                }
            }

            stateMachine.ConnectedCells.Clear();
            stateMachine.Trigger_ConnectedCellsReset();
        }

        private void AddConnectedCell(ICell cell)
        {
            cell.Selected();
            stateMachine.ConnectedCells.Add(cell);

            stateMachine.Trigger_CellConnected(cell);
        }

        private void RemoveConnectedCells(ICell cell)
        {
            var connectedCells = stateMachine.ConnectedCells;
            var length = connectedCells.Count;
            if (length == 1) return;

            var cellIndex = connectedCells.IndexOf(cell);

            for (int i = length - 1; i > cellIndex; i--)
            {
                connectedCells[i].DeSelected();
                connectedCells.RemoveAt(i);
            }

            stateMachine.Trigger_ConnectedCellsUpdate();
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