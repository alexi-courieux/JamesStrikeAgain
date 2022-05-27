using System;
using UnityEngine;

namespace ashlight.james_strike_again
{
    public class Player : MonoBehaviour
    {
        private IPlayerController _playerController;
        private IPlayerAnimationHandler _animationHandler;
        private Rigidbody _rb;
        
        [SerializeField] private float speed;

        private void Awake()
        {
            _playerController = GetComponent<IPlayerController>();
            _animationHandler = GetComponent<IPlayerAnimationHandler>();
            _rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            RegisterPlayerControllerEvents();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void RegisterPlayerControllerEvents() {
            // _playerController.OnJump += Jump;
            // _playerController.OnCrouch += Crouch;
            // _playerController.OnStopCrouch += StopCrouch;
            // _playerController.OnDash += Dash;
            // _playerController.OnShoot += Shoot;
        }

        private void Move()
        {
            float direction = _playerController.MovementDirection;
            if (direction > 0)
            {
                _rb.velocity = new Vector3(0, _rb.velocity.y) + transform.forward * direction * speed * direction; // forward
            }
            else
            {
                _rb.velocity = new Vector3(0, _rb.velocity.y) + -transform.forward * speed * Mathf.Abs(direction); // backward
            }
        }
    }
}
