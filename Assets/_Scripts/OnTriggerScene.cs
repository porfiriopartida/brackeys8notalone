using LopapaGames.Common.Core;
using UnityEngine;

namespace _Scripts
{
    public class OnTriggerScene : MonoBehaviour
    {
        public string sceneName;
        public LayerMask LayerMask;
        private void OnTriggerEnter(Collider other)
        {
            if (LayerMask != (LayerMask | (1 << other.gameObject.layer))) return;
        
            UnityService.GetInstance().LoadScene(sceneName);
        }
    }
}
