using System;
using ashlight.james_strike_again.Animation;
using ashlight.james_strike_again.Controller;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ashlight.james_strike_again.StateMachine
{
    public class PlayerStateMachine : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private int maxJumpCount = 1;
        [SerializeField] private float jumpHeight;
        [SerializeField] private Transform groundCheckOrigin;
        [SerializeField] private LayerMask mouseAimMask;
        [SerializeField] private Transform neck;
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
        public bool IsAimingBehind { get; private set; }
        private UnityEngine.Camera _camera;
        private Vector3 aimTarget;
        
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
            _camera = UnityEngine.Camera.main;
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
            HandleRotation();
            HandleAimPointerPosition();
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

        private void HandleRotation()
        {
            IsAimingBehind = Mathf.Sign(aimTarget.x - transform.position.x) < 0;
            Rigidbody.MoveRotation(Quaternion.Euler(new Vector3(0, 90 * (IsAimingBehind ? -1 : 1), 0)));
        }

        private void HandleAimPointerPosition()
        {
            Ray ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, mouseAimMask))
            {
                aimTarget = hit.point;
            }
        }

        private void OnAnimatorIK()
        {
            // Define the hands as fully controlled by IK
            AnimationHandler.Animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
            AnimationHandler.Animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
            // Aim at target
            Vector3 aimTargetPosition = aimTarget;
            Vector3 position = transform.position;
            AnimationHandler.Animator.SetIKPosition(AvatarIKGoal.RightHand, new Vector3(aimTargetPosition.x, aimTargetPosition.y, position.z));
            AnimationHandler.Animator.SetIKPosition(AvatarIKGoal.LeftHand, new Vector3(aimTargetPosition.x, aimTargetPosition.y, position.z));
            // Look at target
            AnimationHandler.Animator.SetLookAtWeight(0.5f);
            AnimationHandler.Animator.SetLookAtPosition(aimTargetPosition);
        }
    }
}