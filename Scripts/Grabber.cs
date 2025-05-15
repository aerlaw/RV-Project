using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class Grabber : MonoBehaviour
{
    public XRRayInteractor leftHandInteractor;
    public InputActionProperty triggerAction;
    public InputActionProperty joystickAction;
    public GameObject playerObject;

    public float adjustSpeed = 0.5f;
    private GameObject grabbedObject = null;
    private float grabDistance = 0f;
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
        if (isGrabbing && grabbedObject == null)
        {
            isGrabbing = false;
            if (moveProvider != null) moveProvider.enabled = true;
        }

        if (leftHandInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            if (hit.collider != null && IsGrabbableTag(hit.collider.tag))
            {
                if (triggerAction.action.WasPressedThisFrame() && !isGrabbing)
                {
                    if (moveProvider != null) moveProvider.enabled = false;
                    grabbedObject = hit.collider.gameObject;
                    grabDistance = Vector3.Distance(leftHandInteractor.transform.position, grabbedObject.transform.position);
                    isGrabbing = true;
                }
            }
        }

        if (isGrabbing && grabbedObject != null)
        {
            if (triggerAction.action.IsPressed())
            {
                // Déplacement et rotation de l’objet vers la main
                grabbedObject.transform.position = leftHandInteractor.transform.position + leftHandInteractor.transform.forward * grabDistance;
                grabbedObject.transform.rotation = leftHandInteractor.transform.rotation;

                // Ajustement de la distance avec le joystick
                Vector2 joystickValue = joystickAction.action.ReadValue<Vector2>();

                if (joystickValue.y > 0.1f)
                {
                    grabDistance += adjustSpeed * Time.deltaTime;
                }
                else if (joystickValue.y < -0.1f)
                {
                    grabDistance -= adjustSpeed * Time.deltaTime;
                    grabDistance = Mathf.Max(0.1f, grabDistance);
                }
            }
            else
            {
                // Relâchement de l’objet
                if (moveProvider != null) moveProvider.enabled = true;
                isGrabbing = false;
                grabbedObject = null;
            }
        }
    }

    // Vérifie si l’objet touché est un déchet grabbable
    private bool IsGrabbableTag(string tag)
    {
        return tag == "BrownTrash" || tag == "YellowTrash" || tag == "GreenTrash";
    }
}
