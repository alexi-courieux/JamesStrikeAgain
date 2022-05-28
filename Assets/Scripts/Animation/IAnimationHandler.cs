using UnityEngine;

namespace ashlight.james_strike_again {
    public interface IAnimationHandler {
        public void SetParameter(string parameter, object parameterValue = null);
        public void SetController(AnimatorOverrideController animatorController);
    }
}