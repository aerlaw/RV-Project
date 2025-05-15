using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrashSorting : MonoBehaviour
{
    public AudioClip successSound; // Son de réussite
    public AudioClip failureSound; // Son d’échec
    private AudioSource audioSource; // Source audio
    public Text scoreText; // Texte UI du score
    private int score = 0; // Score initial

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Récupération du composant AudioSource
        UpdateScoreUI(); // Affichage initial du score
    }

    private void OnTriggerEnter(Collider other)
    {
        string zoneTag = gameObject.tag; // Tag de la zone de la poubelle
        string trashTag = other.gameObject.tag; // Tag du déchet

        if ((zoneTag == "YellowTriggerZone" && trashTag == "YellowTrash") ||
            (zoneTag == "GreenTriggerZone" && trashTag == "GreenTrash") ||
            (zoneTag == "BrownTriggerZone" && trashTag == "BrownTrash"))
        {
            // Bonne poubelle => Son succès, +10 points
            PlaySound(successSound);
            score += 10;
        }
        else
        {
            // Mauvaise poubelle => Son d’erreur, -5 points
            PlaySound(failureSound);
            score -= 5;
        }

        UpdateScoreUI(); // Mise à jour de l'affichage du score
        Destroy(other.gameObject); // Suppression du déchet
    }

    void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    public int GetScore()
    {
        return score;
    }


    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
}
