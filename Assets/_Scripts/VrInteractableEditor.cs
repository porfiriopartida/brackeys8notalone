using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(VrInteractable))]
public class VrInteractableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        DrawDefaultInspector();
        var interactable = (VrInteractable) target;
        if (GUILayout.Button("Interact"))
        {
            interactable.Interact();
        }
    }
}
