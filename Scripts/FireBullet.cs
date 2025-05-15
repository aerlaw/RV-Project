using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class FireBullet : MonoBehaviour
{
    public XRRayInteractor leftHandInteractor;
    public InputActionProperty triggerAction;
    public GameObject ballePrefab; // Prefab de la sphère
    public Transform spawnPoint; // Position de départ de la sphère
    public float forceTir = 10f; // Force appliquée à la sphère

    void Update()
    {
        if (leftHandInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            if (hit.collider != null && hit.collider.gameObject.name == "Cylinder2")
            {
                if (triggerAction.action.WasPressedThisFrame()) // Détection d'un appui
                {
                    TirerBalle(hit.collider.transform);
                }
            }
        }
    }

    void TirerBalle(Transform target)
    {
        if (ballePrefab == null || spawnPoint == null) return;

        // Instancier une sphère
        GameObject balle = Instantiate(ballePrefab, spawnPoint.position, Quaternion.identity);

        // Ajouter un Rigidbody pour la physique (gravité)
        Rigidbody rb = balle.GetComponent<Rigidbody>();
        if (rb == null) rb = balle.AddComponent<Rigidbody>();

        // Définir la direction du tir (vers `Cylinder2`)
        Vector3 directionTir = (target.position - spawnPoint.position).normalized;

        // Appliquer une force pour lancer la balle
        rb.AddForce(directionTir * forceTir, ForceMode.Impulse);
    }
}
