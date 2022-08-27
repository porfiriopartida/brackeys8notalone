using UnityEngine;

namespace _Scripts
{
    public class OnTriggerImpulse : MonoBehaviour
    {
        public float force;
        public LayerMask LayerMask;
        private void OnTriggerEnter(Collider other)
        {
            if (LayerMask != (LayerMask | (1 << other.gameObject.layer))) return;
            var rb = other.GetComponent<Rigidbody>();
            if (rb == null) return;
            var pos = transform.position;
            var otherPos = other.transform.position;
        
            // var direction = otherPos - pos;
            var direction = new Vector3(0, 0, (pos.z - otherPos.z) > 0 ? -1:1);
            rb.AddForce(direction.normalized * force,  ForceMode.Impulse);
        }
    }
}
