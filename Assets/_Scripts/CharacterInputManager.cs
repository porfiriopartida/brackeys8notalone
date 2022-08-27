using UnityEngine;

namespace _Scripts
{
    public class CharacterInputManager : MonoBehaviour
    {
        [Header("Camera")]
        public Camera _mainCamera;

        public GameObject target;

        public IMovable movable;
        public IJumper jumper;
        
        public Camera FindCamera()
        {
            return _mainCamera ? _mainCamera : Camera.main;
        }
        void Start()
        {
            _mainCamera = FindCamera();
            movable = target.GetComponent<IMovable>();
            jumper = target.GetComponent<IJumper>();
        }
        //TODO: Move to playerinput for new input system usage.
        void FixedUpdate()
        {
            // //camera forward and right vectors:
            var forward = _mainCamera.transform.forward;
            var right = _mainCamera.transform.right;
            forward.y = 0f;
            right.y = 0f;
            forward.Normalize();
            right.Normalize();
            // _direction = forward * vertical + right * horizontal;
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");
            var jmpTrigger = Input.GetButtonDown("Jump");
            //_direction = new Vector3(horizontal, 0, vertical).normalized;
            var direction = forward * vertical + horizontal * right;
            // Vector3 m_Input = new Vector3(horizontal, 0, vertical);
            var isMoving = direction != Vector3.zero;
            // Animator.SetBool(IsWalking, _isMoving);
            if (isMoving)
            {
                movable.Move(direction);
                var lookAt = direction.normalized;
                movable.LookAt(lookAt);
            }
            // && _controllerBehavior.isGrounded
            if (jmpTrigger)
            {
                jumper.Jump();
            }
        }
    }
}