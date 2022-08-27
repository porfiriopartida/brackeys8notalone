using UnityEditor;
using UnityEngine;

namespace _Scripts
{
    [CustomEditor(typeof(VrInteractable))]
    public class VrInteractableEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var interactable = (VrInteractable) target;
            if (GUILayout.Button("Interact"))
            {
                interactable.Interact();
            }
        }
    }
}
