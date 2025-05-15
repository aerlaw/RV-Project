using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class RaycastScale : MonoBehaviour
{
    public XRRayInteractor leftHandInteractor;
    public InputActionProperty triggerAction;
    public InputActionProperty joystickAction;
    public GameObject playerObject; 
    public float scaleSpeed = 0.5f;

    private bool isScaling = false;
    private static bool isGrabbing = false;

    private ActionBasedContinuousMoveProvider moveProvider;

    void Start()
    {
        if (playerObject != null)
        {
            moveProvider = playerObject.GetComponent<ActionBasedContinuousMoveProvider>();
        }
    }

    void Update()
    {
        
        if (isGrabbing) return;

        if (leftHandInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            if (hit.collider != null && hit.collider.gameObject.name == "Cylinder1")
            {
                if (triggerAction.action.IsPressed())
                {
                    isScaling = true;
                    if (moveProvider != null) moveProvider.enabled = false;
                }
            }
        }

        if (isScaling && triggerAction.action.IsPressed()) 
        {
            Vector2 joystickValue = joystickAction.action.ReadValue<Vector2>();

            if (joystickValue.y > 0.1f) 
            {
                transform.localScale += Vector3.one * scaleSpeed * Time.deltaTime;
            }
            else if (joystickValue.y < -0.1f) 
            {
                transform.localScale -= Vector3.one * scaleSpeed * Time.deltaTime;
                transform.localScale = Vector3.Max(transform.localScale, Vector3.one * 0.1f); 
            }
        }

       
        if (isScaling && !triggerAction.action.IsPressed())
        {
            isScaling = false;
            if (moveProvider != null) moveProvider.enabled = true; 
        }
    }
}
