namespace ashlight.james_strike_again.player
{
    public abstract class PlayerBaseState
    {
        protected readonly PlayerStateMachine Context;
        protected readonly PlayerStateFactory Factory;
        protected PlayerBaseState CurrentSuperState;
        private PlayerBaseState _currentSubState;

        protected PlayerBaseState(PlayerStateMachine context, PlayerStateFactory factory)
        {
            Context = context;
            Factory = factory;
        }
        public abstract string Name { get; }
        public abstract void EnterState();
        public abstract void UpdateState();
        public abstract void ExitState();
        public abstract void CheckSwitchStates();
        public abstract void InitializeSubState();

        public void UpdateStates()
        {
            UpdateState();
            _currentSubState?.UpdateState();
        }
        
        public void ExitStates()
        {
            ExitState();
            _currentSubState?.ExitState();
        }
        
        public void EnterStates()
        {
            EnterState();
            _currentSubState?.EnterState();
        }

        protected void SwitchState(PlayerBaseState newState)
        {
            // Exit the current states
            ExitStates();
            // Enter the new state
            newState.EnterStates();
            if (CurrentSuperState == null) // Est ce que ce state a un parent ?
            {
                // Aucun parent, on est donc en train de changer le "root state"
                Context.CurrentState = newState;
            }
            else
            {
                CurrentSuperState.SetSubState(newState);
            }
        }

        private void SetSuperState(PlayerBaseState newSuperState)
        {
            CurrentSuperState = newSuperState;
        }

        protected void SetSubState(PlayerBaseState newSubState)
        {
            _currentSubState = newSubState;
            newSubState.SetSuperState(this);
        }
    }
}