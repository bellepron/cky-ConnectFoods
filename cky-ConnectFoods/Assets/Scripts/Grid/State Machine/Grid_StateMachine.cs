using cky.StateMachine.Base;
using System.Collections.Generic;
using System;
using UnityEngine;
using ConnectFoods.Enums;
using ConnectFoods.Item;
using ConnectFoods.Grid.Managers;
using ConnectFoods.Grid.States;
using cky.Reuseables.Level;
using ConnectFoods.Managers;
using System.Collections;


namespace ConnectFoods.Grid.StateMachine
{
    public class Grid_StateMachine : BaseStateMachine
    {
        private Action ControlTypeEvent;
        [field: SerializeField] public ControlTypes ControlType { get; private set; }

        public static Action<ICell> OnCellConnected;
        public static Action<List<ICell>> OnConnectedCellsUpdate;
        public static Action OnConnectedCellsReset;

        public ICell[,] Grid { get; private set; }
        public MatchType CollectedItemMatchType { get; set; }
        public ICell ClickedCell { get; set; }
        public List<ICell> ConnectedCells { get; private set; } = new List<ICell>();



        #region Managers
        private LevelSettings _levelSettings;
        public LevelSettings LevelSettings => _levelSettings != null ? _levelSettings : (_levelSettings = LevelManager.Instance.LevelSettings);
        public GridCreator GridCreator { get; private set; }
        public ItemCreator ItemCreator { get; private set; }
        public MatchManager MatchManager { get; private set; }
        public FallManager FallManager { get; private set; }
        public FillManager FillManager { get; private set; }
        public HintManager HintManager { get; private set; }
        public ShuffleManager ShuffleManager { get; private set; }
        public ExplosionManager ExplosionManager { get; private set; }


        #endregion



        #region States

        public Idle_State Idle_State => _idleState ?? (_idleState = new Idle_State(this));
        private Idle_State _idleState;
        public Clicking_State Clicking_State => _clickingState ?? (_clickingState = new Clicking_State(this));
        private Clicking_State _clickingState;
        public Connecting_State Connecting_State => _connectingState ?? (_connectingState = new Connecting_State(this));
        private Connecting_State _connectingState;
        public Swiping_State Swiping_State => _swipingState ?? (_swipingState = new Swiping_State(this));
        private Swiping_State _swipingState;
        public Explosion_State Explosion_State => _explosionState ?? (_explosionState = new Explosion_State(this));
        private Explosion_State _explosionState;
        public Shuffle_State Shuffle_State => _shuffleState ?? (_shuffleState = new Shuffle_State(this));

        private Shuffle_State _shuffleState;

        #endregion


        private void Start()
        {
            GridCreator = new GridCreator();
            Grid = GridCreator.Create();

            ItemCreator = new ItemCreator(Grid);
            ItemCreator.FillGridWithRandomRandomItems();

            MatchManager = new MatchManager(Grid);
            FallManager = new FallManager(Grid);
            FillManager = new FillManager(Grid, ItemCreator);
            HintManager = new HintManager(Grid, MatchManager);
            ShuffleManager = new ShuffleManager(Grid);
            ExplosionManager = new ExplosionManager(Grid);


            switch (ControlType)
            {
                case ControlTypes.Click:
                    ControlTypeEvent += ClickControl;
                    break;
                case ControlTypes.Connect:
                    ControlTypeEvent += ConnectControl;
                    break;
                case ControlTypes.Swipe:
                    ControlTypeEvent += SwipeControl;
                    break;
            }

            SwitchStateTo_ControlState();
        }



        protected override void Tick()
        {
            base.Tick();

            Fall();
            Fill();

            if (Input.GetKey(KeyCode.A))
                Fall();

            if (Input.GetKeyDown(KeyCode.F))
                Fill();

            if (Input.GetKeyDown(KeyCode.H))
                GiveHints();

            if (Input.GetKeyDown(KeyCode.S))
                SwitchStateTo_Shuffle();
        }



        #region Set Control Type

        public void ClickControl() => SwitchState(Clicking_State);
        public void ConnectControl() => SwitchState(Connecting_State);
        public void SwipeControl() => SwitchState(Swiping_State);

        #endregion



        #region Switch State

        public void SwitchStateTo_ControlState() => ControlTypeEvent?.Invoke();
        public void SwitchStateTo_IdleState() => SwitchState(Connecting_State);
        public void SwitchStateTo_Explosion() => SwitchState(Explosion_State);
        public void SwitchStateTo_Shuffle() => SwitchState(Shuffle_State);

        #endregion



        #region Special Functions

        public void Fall() => FallManager.Fall();

        public void Fill() => FillManager.Fill();

        public void GiveHints() => HintManager.GiveHint(LevelSettings.MinMatchLimit);

        public void ResetHints() => HintManager.ResetHints();

        public void Shuffle() => ShuffleManager.Shuffle();

        public void TryExplodeClickedCellWithNeighbours()
        {
            var cellsWillExplode = MatchManager.FindMatchedNeighbours(ClickedCell).ToArray();

            if (cellsWillExplode.Length >= _levelSettings.MinMatchLimit)
            {
                ExplosionManager.ExplodeClickedCellWithNeighbours(ClickedCell, cellsWillExplode);
            }
        }

        public void ExplodeConnectedCells()
        {
            ExplosionManager.ExplodeConnectedCells(ConnectedCells);
        }

        #endregion



        #region Event Triggers

        public void Trigger_CellConnected(ICell cell)
            => OnCellConnected?.Invoke(cell);

        public void Trigger_ConnectedCellsUpdate()
            => OnConnectedCellsUpdate?.Invoke(ConnectedCells);

        public void Trigger_ConnectedCellsReset()
            => OnConnectedCellsReset?.Invoke();

        #endregion



        #region Conditions

        public bool IsFallable() => FallManager.IsFallable;

        public bool IsThereMove()
            => HintManager.IsThereAnyHint(LevelSettings.MinMatchLimit) ? true : false;

        public bool IsLastConnectedCellNeighbours(ICell cell)
            => ConnectedCells.Count > 0 ? ConnectedCells[^1].Neighbours.Contains(cell) : false;

        public bool IsCollectedItemMatchType(ICell cell)
            => CollectedItemMatchType == cell.IItem?.MatchType ? true : false;

        #endregion
    }
}