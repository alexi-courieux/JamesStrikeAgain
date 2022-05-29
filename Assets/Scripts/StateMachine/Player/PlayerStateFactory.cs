namespace ashlight.james_strike_again.StateMachine
{
    public class PlayerStateFactory
    {
        private readonly PlayerStateMachine _context;

        public PlayerStateFactory(PlayerStateMachine currentContext)
        {
            _context = currentContext;
        }

        public PlayerBaseState Idle()
        {
            return new PlayerIdlePlayerBaseState(_context, this);
        }
        public PlayerBaseState Run()
        {
            return new PlayerRunPlayerBaseState(_context, this);
        }
        public PlayerBaseState Jump()
        {
            return new PlayerJumpPlayerBaseState(_context, this);
        }
        public PlayerBaseState Grounded()
        {
            return new PlayerGroundedPlayerBaseState(_context, this);
        }
        public PlayerBaseState Crouch()
        {
            return new PlayerCrouchPlayerBaseState(_context, this);
        }
    }
}
