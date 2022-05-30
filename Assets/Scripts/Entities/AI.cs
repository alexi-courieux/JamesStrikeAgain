using System.Collections;
using System.Collections.Generic;
using ashlight.james_strike_again.Animation;
using ashlight.james_strike_again.Entities;
using UnityEngine;

namespace ashlight.james_strike_again
{
    public class AI : Entity
    {
        [SerializeField] private float fireRate;
        [SerializeField] private float range;
        [SerializeField] private float damage;
        [SerializeField] private LayerMask playerMask;
        //[SerializeField] private float health;

        [SerializeField] private Collider collider;
        [SerializeField] private Transform bulletOrigin;
        [SerializeField] private GameObject bullet;
        [SerializeField] private Transform targetPosition;

        [SerializeField] private Transform gun;

        public IAnimationHandler AnimationHandler { get; private set; }
        private Rigidbody _rigidbody;

        private GameObject player;

        private void Awake()
        {
            AnimationHandler = GetComponent<IAnimationHandler>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            //player = Player.Instance();
        }

        // Update is called once per frame
        void Update()
        {

            if (IsPlayerInRange())
            {
                Shoot();
            }
        }

        private void Shoot()
        {
            if (Physics.Raycast(bulletOrigin.position, bulletOrigin.position - targetPosition.position))
            {
                Debug.Log("pioupiou");
            }

            Debug.Log("AQueCoucou");
        }


        private bool IsPlayerInRange ()
        {
            if (Vector3.Distance(targetPosition.position, transform.position) <= range)
            {
                Debug.Log("EstAPort�e");

                bool isAimingBehind = Mathf.Sign(targetPosition.position.x - transform.position.x) < 0;
                _rigidbody.MoveRotation(Quaternion.Euler(new Vector3(0, 90 * (isAimingBehind ? -1 : 1), 0)));

                return true;
            }
            return false; 
        }

        public override void TakeDamage(float damage)
        {
            Health -= damage;
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
