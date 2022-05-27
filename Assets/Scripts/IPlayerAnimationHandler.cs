using UnityEngine;

namespace ashlight.james_strike_again {
    public interface IPlayerAnimationHandler : IAnimationHandler {
        public Transform RightHandSlot { get; }
        public Transform LeftHandSlot { get; }
    }
}