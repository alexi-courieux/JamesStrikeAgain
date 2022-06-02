using UnityEngine;

namespace ashlight.james_strike_again.player
{
    public sealed class PlayerCrouchPlayerBaseState : PlayerBaseState

    {
        public PlayerCrouchPlayerBaseState(PlayerStateMachine context, PlayerStateFactory factory)
            : base(context, factory)
        {
            InitializeSubState();
        }

        public override string Name => "Crouch";

        public override void EnterState()
        {
            Context.AnimationHandler.SetParameter("crouch", true);
        }

        public override void UpdateState()
        {
            CheckSwitchStates();
            if(Context.CanMoveFreelyWhileCrouched || CurrentSuperState.Name.Equals("Jump")) HandleMove();
        }

        public override void ExitState()
        {
            Context.AnimationHandler.SetParameter("crouch", false);
        }

        public override void CheckSwitchStates()
        {
            if (Context.IsCrouching) return;
            if (Context.PlayerController.MovementDirection == 0) SwitchState(Factory.Idle());
            SwitchState(Factory.Run());
        }

        public override void InitializeSubState()
        {
        }
        
        private void HandleMove()
        {
            Vector3 movement = new Vector3(Context.PlayerController.MovementDirection * Context.Speed, 0, 0);
            Context.Rigidbody.MovePosition(Context.transform.position + movement);
        }
    }
}