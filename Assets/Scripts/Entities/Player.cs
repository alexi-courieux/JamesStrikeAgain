using ashlight.james_strike_again.entities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ashlight.james_strike_again.player
{
    public class Player : Entity
    {
        public static string PLAYER_TAG = "Player";
        public static Player Instance { get; private set; }
        private Vector3 _spawnPoint;
        private PlayerStateMachine _stateMachine;

        private void Awake()
        {
            Instance = this;
            _stateMachine = GetComponent<PlayerStateMachine>();
        }

        private void Start()
        {
            _spawnPoint = transform.position;
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

        public void MajorUpgrade()
        {
            if (!_stateMachine.CanMoveFreelyWhileCrouched) _stateMachine.CanMoveFreelyWhileCrouched = true;
            else _stateMachine.MaxJumpCount++;
        }
        
        public void MinorUpgrade()
        {
            float random = Random.Range(0, 100);
            switch (random)
            {
                case < 30:
                    _stateMachine.Speed += 0.02f;
                    break;
                case < 60:
                    _stateMachine.JumpHeight += 1;
                    break;
                case < 90:
                    _stateMachine.FireRate -= 0.1f;
                    break;
                case <= 100:
                    MaxHealth++;
                    Health = MaxHealth;
                    break;
            }
        }
    }
}
