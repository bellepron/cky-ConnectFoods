using cky.StateMachine.Base;

namespace ConnectFoods.Grid.StateMachine
{
    public abstract class Grid_BaseState : BaseState
    {
        protected Grid_StateMachine stateMachine;

        public Grid_BaseState(Grid_StateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }
    }
}