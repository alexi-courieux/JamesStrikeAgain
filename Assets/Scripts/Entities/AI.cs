using System.Collections;
using System.Collections.Generic;
using ashlight.james_strike_again.Animation;
using ashlight.james_strike_again.player;
using UnityEngine;

namespace ashlight.james_strike_again.entities
{
    public class AI : Entity
    {
        [SerializeField] private float fireRate;
        [SerializeField] private float range;
        [SerializeField] private Transform bulletOrigin;
        [SerializeField] private GameObject bullet;
        [SerializeField] private Transform targetPosition;

        private IAnimationHandler AnimationHandler { get; set; }
        private Rigidbody _rigidbody;
        private bool _canShoot;

        private void Awake()
        {
            AnimationHandler = GetComponent<IAnimationHandler>();
            _rigidbody = GetComponent<Rigidbody>();
            _canShoot = true;
        }

        // Start is called before the first frame update
        private void Start()
        {
            Player.Instance.OnDeath += Respawn;
        }

        // Update is called once per frame
        private void Update()
        {
            if (!IsPlayerInRange()) return;
            if (_canShoot) Shoot();
        }

        private void Shoot()
        {
            _canShoot = false;
            
            if (!Physics.Raycast(bulletOrigin.position, bulletOrigin.position - targetPosition.position)) return;
            
            Vector3 trueBulletPosition = new Vector3(bulletOrigin.position.x, bulletOrigin.position.y, transform.position.z);
            GameObject currentBullet = Instantiate(bullet, trueBulletPosition, Quaternion.identity);
            currentBullet.transform.LookAt(targetPosition.position);
            currentBullet.GetComponent<Rigidbody>().AddForce(currentBullet.transform.forward * 10, ForceMode.Impulse);
            Invoke(nameof(AllowShoot), fireRate);
        }

        private void AllowShoot()
        {
            _canShoot = true;
        }

        private void Respawn()
        {
            gameObject.SetActive(true);
            Health = 1;
        }


        private bool IsPlayerInRange ()
        {
            if (!(Vector3.Distance(targetPosition.position, transform.position) <= range)) return false;
            
            bool isAimingBehind = Mathf.Sign(targetPosition.position.x - transform.position.x) < 0;
            _rigidbody.MoveRotation(Quaternion.Euler(new Vector3(0, 90 * (isAimingBehind ? -1 : 1), 0)));
            return true;
        }

        public override void TakeDamage(float damage)
        {
            Health -= damage;
            if (Health <= 0)
                gameObject.SetActive(false);
        }

        private void OnAnimatorIK()
        {
            if (!IsPlayerInRange())
                return;

            // Define the hands as fully controlled by IK
            AnimationHandler.Animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
            AnimationHandler.Animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
            // Aim at target
            Vector3 aimTargetPosition = targetPosition.position;
            Vector3 position = transform.position;
            AnimationHandler.Animator.SetIKPosition(AvatarIKGoal.RightHand, new Vector3(aimTargetPosition.x, aimTargetPosition.y, position.z));
            AnimationHandler.Animator.SetIKPosition(AvatarIKGoal.LeftHand, new Vector3(aimTargetPosition.x, aimTargetPosition.y, position.z));
            // Look at target
            AnimationHandler.Animator.SetLookAtWeight(0.5f);
            AnimationHandler.Animator.SetLookAtPosition(aimTargetPosition);
        }
    }
}
