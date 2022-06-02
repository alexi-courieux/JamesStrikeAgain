using System.Collections;
using ashlight.james_strike_again.entities;
using ashlight.james_strike_again.player;
using UnityEngine;

namespace ashlight.james_strike_again
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float lifeTime;
        [SerializeField] private float damage;
        [SerializeField] private bool isFromPlayer;

        private void OnTriggerEnter(Collider other)
        {
            Entity entity = other.transform.GetComponent<Entity>();

            bool isPlayer = other.CompareTag(Player.PLAYER_TAG);
            if ((isFromPlayer && !isPlayer) || (!isFromPlayer && isPlayer))
            {
                entity?.TakeDamage(damage);
            }
            if (isFromPlayer && isPlayer) return;
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

    }
}
