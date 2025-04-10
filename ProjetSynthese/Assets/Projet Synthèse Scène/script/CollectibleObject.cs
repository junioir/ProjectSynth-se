using UnityEngine;

public class CollectibleObject : MonoBehaviour
{
   [SerializeField] private GameObject victoryPanel;
    private static int _collectedCount = 0;
    private static int _totalCollectibles = 3;
    private void Start()
    {
        
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(false);
        }
        else
        {
            Debug.LogError("Le panneau de victoire (victoryPanel) n'est pas assigné !");
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            _collectedCount++;
            Destroy(gameObject);
            if (_collectedCount >= _totalCollectibles)
            {
                DisplayVictoryPanel();
            }
        }
    }

    private void DisplayVictoryPanel()
    {
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true);
        }
    }
}
