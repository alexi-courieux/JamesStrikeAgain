using UnityEngine;

namespace ashlight.james_strike_again.Camera
{
    public class Tracker : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private bool trackX, trackY, trackZ;
        void Update()
        {
            // Follow the position of the specified target
            float x = trackX ? target.position.x : transform.position.x;
            float y = trackY ? target.position.y : transform.position.y;
            float z = trackZ ? target.position.z : transform.position.z;
            transform.position = new Vector3(x, y, z);
        }
    }
}
