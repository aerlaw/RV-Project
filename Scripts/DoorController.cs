using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorController : MonoBehaviour
{
    public XRRayInteractor leftHandInteractor;
    
    public InputActionProperty buttonTriggerAction;

    private bool etatF = true;
    private bool etatO = false;

    void OnEnable()
    {
        buttonTriggerAction.action.performed += OnButtonTriggeredPressed;
    }

    void OnDisable()
    {
        buttonTriggerAction.action.performed -= OnButtonTriggeredPressed;
    }

    private void OnButtonTriggeredPressed(InputAction.CallbackContext context)
    {
        if (leftHandInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            Animator anim = hit.transform.GetComponent<Animator>();
            if (etatF)
            {
                anim.Play("DoorOpen");
                etatF = false;
                etatO = true;
            } else if (etatO)
            {
                anim.Play("DoorClosed");
                etatF = true;
                etatO = false;
            }
        }
    }
}
