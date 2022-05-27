using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ashlight.james_strike_again {
    public class UnityNewInputSystemPlayerController : MonoBehaviour, IPlayerController {

        public event Action OnPause;
        public float MovementDirection { get; private set; }
        public event Action OnJump;
        public event Action OnDash;
        public event Action OnCrouch;
        public event Action OnStopCrouch;
        public event Action OnShoot;

        public PlayerInputActions PlayerInputActions { get; private set; }

        private bool
            _isCrouchTriggerPressed,
            _isShootTriggerPressed;

        private void Start()
        {
            PlayerInputActions = new PlayerInputActions();
            PlayerInputActions.Player.Enable();

            InitialiseUniversalEvents();
            InitialisePlayerEvents();
        }

        private void Update()
        {
            MovementDirection = PlayerInputActions.Player.Move.ReadValue<Vector2>().x;
            
        }

        private void InitialiseUniversalEvents() {
            PlayerInputActions.Universal.Enable();
            PlayerInputActions.Universal.Pause.performed += Pause;
        }
        
        private void InitialisePlayerEvents() {
            PlayerInputActions.Player.Enable();
            PlayerInputActions.Player.Jump.performed += Jump;
            PlayerInputActions.Player.Dash.performed += Dash;
            PlayerInputActions.Player.Crouch.performed += Crouch;
            PlayerInputActions.Player.Shoot.performed += Shoot;
        }

        private void Pause(InputAction.CallbackContext context) {
            OnPause?.Invoke();
        }
        private void Jump(InputAction.CallbackContext context) {
            OnJump?.Invoke();
        }
        private void Dash(InputAction.CallbackContext context) {
            OnDash?.Invoke();
        }
        private void Crouch(InputAction.CallbackContext context) {
            _isCrouchTriggerPressed = !_isCrouchTriggerPressed;
            if (_isCrouchTriggerPressed)
                OnCrouch?.Invoke();
            else
                OnStopCrouch?.Invoke();
        }
        private void Shoot(InputAction.CallbackContext context) {
            OnShoot?.Invoke();
        }
    }
}