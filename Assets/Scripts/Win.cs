using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ashlight.james_strike_again
{
    public class Win : MonoBehaviour
    {
        [SerializeField] private GameObject winText;
        private void Start()
        {
            winText.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            winText.SetActive(true);
        }
    }
}
