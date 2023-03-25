using UnityEngine;

namespace cky.StateMachine.Base
{
    public abstract class BaseStateMachine : MonoBehaviour
    {
        private BaseState currentState;
        public string stateName;

        public void SwitchState(BaseState newState)
        {
            currentState?.Exit();
            currentState = newState;
            currentState.Enter();

            stateName = currentState.ToString();
        }

        private void Update() => Tick();

        protected virtual void Tick() => currentState?.Tick(Time.deltaTime);
    }
}