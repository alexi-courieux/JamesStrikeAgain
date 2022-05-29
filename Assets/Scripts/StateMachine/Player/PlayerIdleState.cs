
using UnityEngine;

namespace ashlight.james_strike_again.StateMachine
{
    public sealed class PlayerIdlePlayerBaseState : PlayerBaseState

    {
        public PlayerIdlePlayerBaseState(PlayerStateMachine context, PlayerStateFactory factory)
            : base(context, factory)
        {
            InitializeSubState();
        }
        
        public override string Name => "Idle";

        public override void EnterState()
        {
            Context.AnimationHandler.SetParameter("speed", 0f);
        }

        public override void UpdateState()
        {
            CheckSwitchStates();
        }

        public override void ExitState()
        {
        }

        public override void CheckSwitchStates()
        {
            if(Context.IsCrouching) SwitchState(Factory.Crouch());
            if (Context.PlayerController.MovementDirection != 0) SwitchState(Factory.Run());
        }

        public override void InitializeSubState()
        {
        }
    }
}