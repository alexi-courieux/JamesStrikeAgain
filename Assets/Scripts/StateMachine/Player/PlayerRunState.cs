using ashlight.james_strike_again.StateMachine;
using UnityEngine;

namespace ashlight.james_strike_again.StateMachine
{
    public sealed class PlayerRunPlayerBaseState : PlayerBaseState

    {
        public PlayerRunPlayerBaseState(PlayerStateMachine context, PlayerStateFactory factory) : base(context, factory)
        {
            InitializeSubState();
        }
        
        public override string Name => "Run";

        public override void EnterState()
        {
        }

        public override void UpdateState()
        {
            CheckSwitchStates();
            Context.AnimationHandler.SetParameter("speed", Context.PlayerController.MovementDirection * (Context.IsAimingBehind ? -1 : 1));
            HandleMove();
        }

        public override void ExitState()
        {
        }

        public override void CheckSwitchStates()
        {
            if(Context.IsCrouching) SwitchState(Factory.Crouch());
            if (Context.PlayerController.MovementDirection == 0) SwitchState(Factory.Idle());
        }

        public override void InitializeSubState()
        {
        }

        private void HandleMove()
        {
            Context.Rigidbody.MovePosition(Context.transform.position + new Vector3(Context.PlayerController.MovementDirection * Context.Speed, 0, 0));
        }
    }
}