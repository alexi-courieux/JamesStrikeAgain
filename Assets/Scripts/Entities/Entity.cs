using System;
using UnityEngine;

namespace ashlight.james_strike_again.Entities
{
    public abstract class Entity : MonoBehaviour
    {
        public event Action OnHealthChanged;
        public event Action OnDeath;
        [SerializeField] private float health;

        public float Health { 
            get { return health; } 
            protected set {
                health = value;
                OnHealthChanged.Invoke();
                if (health <= 0)
                {
                    OnDeath.Invoke();
                }
            } }

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public abstract void TakeDamage(float damage);
    }
}
