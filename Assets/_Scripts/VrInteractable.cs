using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

namespace _Scripts
{
    public class VrInteractable : MonoBehaviour
    {
        public Transform Transform;
        public float target;
        public GameObject[] destroy;
        public InputActionReference interactAction;

        public float distance = 300f;
        public XRRayInteractor Interactor;
        public Transform rightHand;

        private void Start()
        {
            Interactor.hoverEntered.AddListener(hoverEntered);
            Interactor.selectEntered.AddListener(selectEntered);
        }

        private void hoverEntered(HoverEnterEventArgs arg0)
        {
            Debug.Log(arg0);
        }
        private void selectEntered(SelectEnterEventArgs arg0)
        {
            Debug.Log(arg0);
        }

        private void Awake()
        {
            interactAction.action.started += InteractAction;
        }
        private void OnDestroy()
        {
            interactAction.action.started -= InteractAction;
        
            Interactor.hoverEntered.RemoveAllListeners();
            Interactor.selectEntered.RemoveAllListeners();
        }
        void InteractAction(InputAction.CallbackContext context)
        {
            Debug.Log(context);
            var pos = rightHand.position;
            var rot = rightHand.rotation;
            var dir = rot * Vector3.forward;
            RaycastHit hit;
            if (Physics.Raycast(pos, dir, out hit, distance, LayerMask.GetMask("Interactable")))
            {//TODO: Migrate this code.
                if (hit.transform.gameObject == this.gameObject)
                {
                    Interact();
                }
            }
        }

        public GameObject indicator;

        private void Update()
        {
            CheckAim();
        }

        private void CheckAim()
        {
            var pos = rightHand.position;
            var rot = rightHand.rotation;
            var dir = rot * Vector3.forward;
            RaycastHit hit;
            if (Physics.Raycast(pos, dir, out hit, distance, LayerMask.GetMask("Interactable")))
            {
                indicator.SetActive(true);
                indicator.transform.position = hit.point;
            }
            else
            {
            
                indicator.SetActive(false);
            }
        }

        private void OnDrawGizmos()
        {
            var pos = rightHand.position;
            var rot = rightHand.rotation;
            var dir = rot * Vector3.forward;
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(pos, 1);
            RaycastHit hit;
            if (Physics.Raycast(pos, dir, out hit, distance, LayerMask.GetMask("Interactable")))
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawSphere(hit.point, 1);
            }
        }


        public void Interact()
        {
            var position = Transform.position;
            position = new Vector3(position.x, target, position.z);
            Transform.position = position;
            if (destroy != null && destroy.Length > 0)
            {
                for (int i = 0; i < destroy.Length; i++)
                {
                    Destroy(destroy[i]);
                }

                destroy = null;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log(other.name);
        }
    }
}
