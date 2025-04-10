using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZoneMessage : MonoBehaviour
{
    [SerializeField] private GameObject messagePanel;
    [SerializeField] private float messageDuration = 2f;

    private bool hasTriggered = false;

    private void Start()
    {

        if (messagePanel != null)
        {
            messagePanel.SetActive(false);
        }
        else
        {
            Debug.LogError("Le messagePanel n'est pas assigné !");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Vérifie si le joueur entre dans la zone trigger
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true; // Marque que la zone a été déclenchée une fois
            ShowMessage();
        }
    }

    private void ShowMessage()
    {
        if (messagePanel != null)
        {
            messagePanel.SetActive(true);
            Invoke(nameof(HideMessage), messageDuration);
        }
    }
    private void HideMessage()
    {
        if (messagePanel != null)
        {
            messagePanel.SetActive(false);
        }
    }
}
