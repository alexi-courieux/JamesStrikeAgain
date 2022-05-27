using System;
using UnityEngine;

namespace ashlight.james_strike_again {
    public enum PlayerAnimationParameter {
        ForwardBackwardMovement,
        LeftRightMovement,
        IsRunning,
        IsChargingHeavyAttack,
        HeavyAttack,
        HeavyAttackComboCount,
        IsFinalHeavyAttack,
        Attack,
        AttackComboCount,
        IsFinalAttack
    }

    public class PlayerAnimationHandler : MonoBehaviour, IPlayerAnimationHandler {
        [SerializeField] private Transform rightHandSlot;
        [SerializeField] private Transform leftHandSlot;
        [SerializeField] private Animator animator;

        public Transform RightHandSlot
        {
            get => rightHandSlot;
            private set => leftHandSlot = value;
        }

        public Transform LeftHandSlot
        {
            get => leftHandSlot;
            private set => leftHandSlot = value;
        }

        public void SetParameter(string parameterName, object parameterValue = null) {
            switch (parameterValue) {
                case null: {
                    animator.SetTrigger(parameterName);
                    break;
                }
                case int i: {
                    animator.SetInteger(parameterName, i);
                    break;
                }
                case float f: {
                    animator.SetFloat(parameterName, f);
                    break;
                }
                case bool b: {
                    animator.SetBool(parameterName, b);
                    break;
                }
                default:
                    throw new Exception($"Value type not supported : {parameterValue.GetType()}");
            }
        }

        public void SetController(AnimatorOverrideController animatorOverrideController) {
            animator.runtimeAnimatorController = animatorOverrideController;
        }
    }
}