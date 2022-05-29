using System.Collections;
using System.Collections.Generic;
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

        private GameObject player;


        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
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
            Debug.Log("AQueCoucou");
        }


        private bool IsPlayerInRange ()
        {
            if (Vector3.Distance(player.transform.position, transform.position) <= range)
            {
                Debug.Log("EstAPort�e");
                RaycastHit hit;
                Debug.DrawRay(transform.position, transform.forward, Color.red);
                //Le joueur est � port�e de l'IA on v�rifie si il y a un obstacle entre les deux
                if (Physics.Raycast(transform.position, transform.forward, out hit, float.MaxValue, playerMask))
                {
                    Debug.Log(hit);
                    return hit.collider.gameObject.CompareTag("Player");
                }
            }
            return false; 
        }

        public override void TakeDamage(float damage)
        {
            Health -= damage;
        }
    }
}
