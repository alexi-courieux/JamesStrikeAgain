namespace ashlight.james_strike_again.player
{
    public sealed class PlayerGroundedPlayerBaseState : PlayerBaseState
    {
        public PlayerGroundedPlayerBaseState(PlayerStateMachine context, PlayerStateFactory factory)
            : base(context, factory)
        {
            InitializeSubState();
        }
        public override void EnterState()
        {
            Context.AnimationHandler.SetParameter("grounded", true);
        }
        
        public override string Name => "Grounded";

        public override void InitializeSubState()
        {
            if (Context.Rigidbody.velocity.x == 0)
            {
                if (Context.IsCrouching)
                {
                    SetSubState(Factory.Crouch());
                }
                else
                {
                   SetSubState(Factory.Idle());
                }
            }
            else
            {
                SetSubState(Factory.Run());
            }
        }

        public override void UpdateState()
        {
            CheckSwitchStates();
        }

        public override void ExitState()
        {
            Context.AnimationHandler.SetParameter("grounded", false);
        }

        public override void CheckSwitchStates()
        {
            // Jump is pressed, switch to jump state
            if (Context.IsJumpTriggered)
            {
                SwitchState(Factory.Jump());
            }
        }
    }
}