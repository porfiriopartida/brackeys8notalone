using UnityEngine;

namespace _Scripts
{
    public class OnTriggerAnim : MonoBehaviour
    {
        public Animator animator;
        public string state;
        public LayerMask LayerMask;
        private void OnTriggerEnter(Collider other)
        {
            if (LayerMask == (LayerMask | (1 << other.gameObject.layer))) {
                animator.Play(state);
            }
        }
    }
}
