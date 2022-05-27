using System;
using UnityEngine;

namespace ashlight.james_strike_again {
    public interface IPlayerController {
        // system
        public event Action OnPause;
        // movement
        public float MovementDirection { get; }
        public event Action OnJump;
        public event Action OnDash;
        public event Action OnCrouch;
        public event Action OnStopCrouch;
        // combat
        public event Action OnShoot;

        public PlayerInputActions PlayerInputActions { get; }
    }
}