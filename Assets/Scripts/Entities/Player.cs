using UnityEngine;

namespace ashlight.james_strike_again.Entities
{
    public class Player : Entity
    {
        public static string PLAYER_TAG = "Player";
        public static Player Instance { get; private set; }
        private Vector3 _spawnPoint;

        private void Start()
        {
            Instance = this;
            _spawnPoint = transform.position;
        }

        private void Update()
        {
            Debug.Log(Health);
        }
        
        public override void TakeDamage(float damage)
        {
            Health -= damage;
            if (Health <= 0)
            {
                Respawn();
            }
        }

        private void Respawn()
        {
            transform.position = _spawnPoint;
            Health = MaxHealth;
        }
    }
}
