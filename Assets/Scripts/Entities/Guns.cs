using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ashlight.james_strike_again
{
    public class Guns : MonoBehaviour
    {
        [SerializeField] private bool AddBulletSpread = true;
        [SerializeField] private Vector3 BulletSpreadVariance = new Vector3(0.1f, 0.1f, 0.1f);
        [SerializeField] private Transform BulletSpawnPoint;
        [SerializeField] private ParticleSystem ImpactParticuleSystem;
        [SerializeField] private TrailRenderer BulletTrail;
        [SerializeField] private float ShootDelay = 0.5f;
        [SerializeField] private LayerMask Mask;

        private float LastShootTime;

        public void Shoot()
        {
            Debug.Log("gun Shoot");
            if (LastShootTime + ShootDelay < Time.time)
            {
                Vector3 direction = GetDirection();
                if (Physics.Raycast(BulletSpawnPoint.position, direction, out RaycastHit hit, float.MaxValue, Mask))
                {
                    TrailRenderer trail = Instantiate(BulletTrail, BulletSpawnPoint.position, Quaternion.identity);
                    StartCoroutine(SpawnTrail(trail, hit));

                    LastShootTime = Time.time;
                }
            }
        }

        private Vector3 GetDirection()
        {
            Vector3 direction = transform.forward;

            if (AddBulletSpread)
            {
                direction += new Vector3(
                    Random.Range(-BulletSpreadVariance.x, BulletSpreadVariance.x),
                    Random.Range(-BulletSpreadVariance.y, BulletSpreadVariance.y),
                    0);

                direction.Normalize();
            }

            return direction;
        }

        private IEnumerator SpawnTrail(TrailRenderer Trail, RaycastHit Hit)
        {
            float time = 0;

            Vector3 startPosition = Trail.transform.position;

            while (time < 1)
            {
                Trail.transform.position = Vector3.Lerp(startPosition, Hit.point, time);
                time += Time.deltaTime / Trail.time;

                yield return null;
            }
            Trail.transform.position = Hit.point;
            Instantiate(ImpactParticuleSystem, Hit.point, Quaternion.LookRotation(Hit.normal));

            Destroy(Trail.gameObject, Trail.time);

        }

    }
}
