using UnityEngine;

namespace _Scripts
{
    public interface IMovable
    {
        void Move(Vector3 direction);
        void LookAt(Vector3 lookAt);
    }
}