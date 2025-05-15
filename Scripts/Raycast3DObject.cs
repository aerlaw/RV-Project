using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class Raycast3DObject : MonoBehaviour
{
    public XRRayInteractor leftHandInteractor;
    public InputActionProperty triggerAction;

    // Update is called once per frame
    void Update()
    {
        if (leftHandInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            if (triggerAction.action.IsPressed())
            {
                if (hit.collider != null && hit.collider.gameObject.name == "CubeTest")
                {
                    Color randomColor = new Color(Random.value, Random.value, Random.value);
                    hit.collider.gameObject.GetComponent<Renderer>().material.color = randomColor;
                }
            }
        }
    }
}
