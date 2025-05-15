using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class FireBullet : MonoBehaviour
{
    public XRRayInteractor leftHandInteractor;
    public InputActionProperty triggerAction;
    public GameObject ballePrefab; // Prefab de la sph�re
    public Transform spawnPoint; // Position de d�part de la sph�re
    public float forceTir = 10f; // Force appliqu�e � la sph�re

    void Update()
    {
        if (leftHandInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            if (hit.collider != null && hit.collider.gameObject.name == "Cylinder2")
            {
                if (triggerAction.action.WasPressedThisFrame()) // D�tection d'un appui
                {
                    TirerBalle(hit.collider.transform);
                }
            }
        }
    }

    void TirerBalle(Transform target)
    {
        if (ballePrefab == null || spawnPoint == null) return;

        // Instancier une sph�re
        GameObject balle = Instantiate(ballePrefab, spawnPoint.position, Quaternion.identity);

        // Ajouter un Rigidbody pour la physique (gravit�)
        Rigidbody rb = balle.GetComponent<Rigidbody>();
        if (rb == null) rb = balle.AddComponent<Rigidbody>();

        // D�finir la direction du tir (vers `Cylinder2`)
        Vector3 directionTir = (target.position - spawnPoint.position).normalized;

        // Appliquer une force pour lancer la balle
        rb.AddForce(directionTir * forceTir, ForceMode.Impulse);
    }
}
