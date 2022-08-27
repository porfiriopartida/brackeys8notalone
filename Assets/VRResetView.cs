using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Management;

public class VRResetView : MonoBehaviour
{
    public InputActionReference toggleReference;
    // public XRInputSubsystem xrInput;
    // private XRGeneralSettings xrSettings;
    public Transform resetTransform;
    // public Transform xInfluence;
    // public Transform yInfluence;
    public Transform player;
    public Camera playerHead;
    private void Start()
    {
        // xrSettings = XRGeneralSettings.Instance;
        // var xrManager = xrSettings.Manager;
        // var xrLoader = xrManager.activeLoader;
        // xrInput = xrLoader.GetLoadedSubsystem<XRInputSubsystem>();
        TryResetView();
    }

    private void Awake()
    {
        toggleReference.action.started += ResetView;
    }
    private void OnDestroy()
    {
        toggleReference.action.started -= ResetView;
    }

    // Update is called once per frame
    [ContextMenu("Reset Position")]
    void ResetView(InputAction.CallbackContext context)
    {
        TryResetView();
    }

    public bool useX, useY, useZ;
    private void TryResetView()
    {
        var transform1 = playerHead.transform;
        var distanceDiff = resetTransform.position - transform1.position;
        player.position += distanceDiff;
        var rotation = transform1.rotation;
        var rotation1 = resetTransform.rotation;
        var rotationAngleX = rotation1.eulerAngles.x - rotation.eulerAngles.x;
        var rotationAngleY = rotation1.eulerAngles.y - rotation.eulerAngles.y;
        var rotationAngleZ = rotation1.eulerAngles.z - rotation.eulerAngles.z;
        player.Rotate(useX ? rotationAngleX:0, useY ? rotationAngleY:0, useZ ? rotationAngleZ:0);
    }
}
