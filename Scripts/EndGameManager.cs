using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameManager : MonoBehaviour
{
    public Text endGameText; // Texte UI pour afficher le message de fin
    private bool gameEnded = false; // Pour �viter d'afficher plusieurs fois le message

    void Update()
    {
        if (!gameEnded && AllTrashCollected())
        {
            gameEnded = true;
            DisplayEndMessage();
        }
    }

    bool AllTrashCollected()
    {
        // V�rifie s'il reste des d�chets en jeu
        return GameObject.FindGameObjectsWithTag("YellowTrash").Length == 0 &&
               GameObject.FindGameObjectsWithTag("GreenTrash").Length == 0 &&
               GameObject.FindGameObjectsWithTag("BrownTrash").Length == 0;
    }

    void DisplayEndMessage()
    {
        int finalScore = FindObjectOfType<TrashSorting>().GetScore(); // R�cup�re le score final

        if (endGameText != null)
        {
            endGameText.text = (finalScore >= 0 ? "Bravo, vous avez r�ussi !" : "Vous avez �chou�.")
                             + "\nScore: " + finalScore;
        }
    }

}
