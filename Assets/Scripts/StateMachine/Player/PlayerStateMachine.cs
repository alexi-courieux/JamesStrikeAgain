using System;
using UnityEngine;

namespace ashlight.james_strike_again.StateMachine
{
    public class PlayerStateMachine : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private int maxJumpCount = 1;
        [SerializeField] private float jumpHeight;
        [SerializeField] private Transform groundCheckOrigin;
        public PlayerBaseState CurrentState { get; set; }
        public Rigidbody Rigidbody { get; private set; }
        
        public IPlayerController PlayerController { get; private set; }
        public IAnimationHandler AnimationHandler { get; private set; }
        private PlayerStateFactory _states;
        public bool IsJumpTriggered { get; set; }
        public bool IsCrouching { get; set; }
        public int JumpCount { get; set; }
        public int MaxJumpCount => maxJumpCount;
        public float JumpHeight => jumpHeight;
        public float? LastJumpTime { get; set; }
        public float Speed => speed;
        public bool CanMoveFreelyWhileCrouched { get; set; }
        
        public Transform GroundCheckOrigin => groundCheckOrigin;


        private void Awake()
        {
            PlayerController = GetComponent<IPlayerController>();
            AnimationHandler = GetComponent<IAnimationHandler>();
            Rigidbody = GetComponent<Rigidbody>();

            _states = new PlayerStateFactory(this);
            CurrentState = _states.Grounded();
            CurrentState.EnterState();
        }
        private void Start()
        {
            RegisterPlayerControllerEvents();
        }

        private void RegisterPlayerControllerEvents() {
            PlayerController.OnJump += Jump;
            PlayerController.OnCrouch += Crouch;
            PlayerController.OnStopCrouch += StopCrouch;
            // PlayerController.OnDash += Dash;
            // PlayerController.OnShoot += Shoot;
        }

        private void Update()
        {
            CurrentState.UpdateStates();
        }

        private void Jump()
        {
            IsJumpTriggered = true;
        }
        
        private void Crouch()
        {
            IsCrouching = true;
        }
        private void StopCrouch()
        {
            IsCrouching = false;
        }
    }
}