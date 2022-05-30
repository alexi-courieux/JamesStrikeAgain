using ashlight.james_strike_again.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ashlight.james_strike_again
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float lifeTime;
        [SerializeField] private Collider collider;
        [SerializeField] private float damage;
        private bool isFromPlayer;

        private void OnTriggerEnter(Collider other)
        {
            Entity entity = other.transform.GetComponent<Entity>();

            if (IsFromPlayer)
                entity?.TakeDamage(damage);
            else if (other.CompareTag(Player.PLAYER_TAG))
                entity?.TakeDamage(damage);

            Destroy(gameObject);
        }

        private void Start()
        {
            StartCoroutine(DestroyAfterLifeTime());
        }

        private IEnumerator DestroyAfterLifeTime()
        {
            yield return new WaitForSeconds(lifeTime);
            Destroy(gameObject);
        }

        public bool IsFromPlayer { get; set; }
    }
}
