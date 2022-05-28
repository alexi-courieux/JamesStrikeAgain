using System;
using ashlight.james_strike_again.StateMachine;
using UnityEngine;

namespace ashlight.james_strike_again
{
    public class Player : Entity
    {
        public override void TakeDamage(float damage)
        {
            Health -= damage;
        }
    }
}
