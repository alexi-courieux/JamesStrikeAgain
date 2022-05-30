using System;
using System.Collections;
using System.Collections.Generic;
using ashlight.james_strike_again.Entities;
using UnityEngine;

namespace ashlight.james_strike_again
{
    public class Trap : MonoBehaviour
    {
        [SerializeField] private float damage = 1;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Player.PLAYER_TAG))
            {
                Player.Instance.TakeDamage(damage);
            }
        }
    }
}
