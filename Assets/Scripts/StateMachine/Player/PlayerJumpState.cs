using UnityEngine;

namespace ashlight.james_strike_again.StateMachine
{
    public sealed class PlayerJumpPlayerBaseState : PlayerBaseState
    {
        public PlayerJumpPlayerBaseState(PlayerStateMachine context, PlayerStateFactory factory)
            : base(context, factory)
        {
            InitializeSubState();
        }
        
        public override string Name => "Jump";

        public override void EnterState()
        {
            HandleJump();
        }

        public override void UpdateState()
        {
            if(Context.IsJumpTriggered) HandleJump();
            CheckSwitchStates();
        }

        public override void ExitState()
        {
            Context.JumpCount = 0;
            Context.LastJumpTime = null;
        }

        public override void CheckSwitchStates()
        {
            // The player jump since less than x seconds, we block the state
            if (Context.LastJumpTime == null || Time.time - Context.LastJumpTime < 0.1f) return;
            // The player touched the ground, switch to grounded state 
            if (Physics.Raycast(Context.GroundCheckOrigin.position, Vector3.down, 0.1f))
            {
                SwitchState(Factory.Grounded());
            }
        }

        public override void InitializeSubState()
        {
            if (Context.Rigidbody.velocity.x == 0)
            {
                if (Context.IsCrouching) SetSubState(Factory.Crouch());
                else SetSubState(Factory.Idle());
            }
            else
            {
                SetSubState(Factory.Run());
            }
        }

        private void HandleJump()
        {
            Context.IsJumpTriggered = false;
            if (Context.LastJumpTime != null && Time.time - Context.LastJumpTime < 0.1f) return;
            
            if (Context.JumpCount >= Context.MaxJumpCount) return; // Can we jump again ?
            Context.JumpCount++;
            Vector3 velocity = Context.Rigidbody.velocity;
            velocity = new Vector3(velocity.x, 0, velocity.z);
            Context.Rigidbody.velocity = velocity;
            Context.Rigidbody.AddForce(Vector3.up * Context.JumpHeight, ForceMode.VelocityChange);
            Context.LastJumpTime = Time.time;
            Context.AnimationHandler.SetParameter("jump");
        }
    }
}