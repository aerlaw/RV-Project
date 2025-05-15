using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class MoveTrashButton : MonoBehaviour
{
    public XRRayInteractor leftHandInteractor; // Interacteur XR pour le raycast
    public InputActionProperty triggerAction; // Gâchette du contrôleur

    public Animator TrashbinYellow;
    public Animator TrashbinGreen;
    public Animator TrashbinBrown;

    private bool isYellowOpen = false;
    private bool isGreenOpen = false;
    private bool isBrownOpen = false;

    void Update()
    {
        if (leftHandInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            if (hit.collider != null)
            {
                string hitTag = hit.collider.gameObject.tag; // Récupère le tag du bouton touché

                if (hitTag == "YellowButton" && triggerAction.action.WasPressedThisFrame())
                {
                    if (TrashbinYellow != null)
                    {
                        if (!isYellowOpen)
                            TrashbinYellow.Play("OpenLidYellowTrash");
                        else
                            TrashbinYellow.Play("CloseLidYellowTrash");

                        isYellowOpen = !isYellowOpen;
                    }
                }
                else if (hitTag == "GreenButton" && triggerAction.action.WasPressedThisFrame())
                {
                    if (TrashbinGreen != null)
                    {
                        if (!isGreenOpen)
                            TrashbinGreen.Play("OpenLidGreenTrash");
                        else
                            TrashbinGreen.Play("CloseLidGreenTrash");

                        isGreenOpen = !isGreenOpen;
                    }
                }
                else if (hitTag == "BrownButton" && triggerAction.action.WasPressedThisFrame())
                {
                    if (TrashbinBrown != null)
                    {
                        if (!isBrownOpen)
                            TrashbinBrown.Play("OpenLidBrownTrash");
                        else
                            TrashbinBrown.Play("CloseLidBrownTrash");

                        isBrownOpen = !isBrownOpen;
                    }
                }
            }
        }
    }
}
