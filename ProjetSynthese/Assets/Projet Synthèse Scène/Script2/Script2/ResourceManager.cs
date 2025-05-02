using UnityEngine;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;
    private int resources = 0;

    public TextMeshProUGUI resourceText;

    void Awake()
    {
        Instance = this;
    }

    public void AddResource(int amount)
    {
        resources += amount;
        UpdateUI();
    }

    public bool SpendResource(int amount)
    {
        if (resources >= amount)
        {
            resources -= amount;
            UpdateUI();
            return true;
        }
        return false;
    }

    private void UpdateUI()
    {
        if (resourceText != null)
            resourceText.text = "Ressources : " + resources;
    }
}
