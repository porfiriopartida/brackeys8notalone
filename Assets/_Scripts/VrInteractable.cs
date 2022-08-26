using UnityEngine;

public class VrInteractable : MonoBehaviour
{
    public Transform Transform;
    public float target = -15.44f;
    public void Interact()
    {
        Transform.position = new Vector3(Transform.position.x, target, Transform.position.z);
    }
}
