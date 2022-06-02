using System;
using UnityEngine;

namespace ashlight.james_strike_again.entities
{
    public abstract class Entity : MonoBehaviour
    {
        public event Action OnHealthChanged;
        public event Action OnDeath;
        private bool _isDead;
        [SerializeField] private float health;
        public float MaxHealth { get; set; }
        public float Health { 
            get { return health; } 
            protected set {
                health = value;
                OnHealthChanged?.Invoke();
                if (health <= 0 && !_isDead)
                {
                    OnDeath?.Invoke();
                    _isDead = true;
                }
                else
                {
                    _isDead = false;
                }
            } }

        // Start is called before the first frame update
        void Start()
        {
            MaxHealth = Health;
        }

        public abstract void TakeDamage(float damage);
    }
}
