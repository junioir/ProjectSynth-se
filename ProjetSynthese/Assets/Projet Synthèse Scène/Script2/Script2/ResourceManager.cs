using UnityEngine;
using TMPro;
using System; 

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;

    private int resources = 0;

    [SerializeField] private TextMeshProUGUI resourceText;

    
    public event Action OnResourceChanged;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Il y a déjà un ResourceManager dans la scène !");
            return;
        }

        Instance = this;
    }


    public void AddResource(int amount)
    {
        resources += amount;
        UpdateUI();
        OnResourceChanged?.Invoke(); 
    }

    public bool SpendResource(int amount)
    {
        if (resources >= amount)
        {
            resources -= amount;
            UpdateUI();
            OnResourceChanged?.Invoke(); 
            return true;
        }

        return false;
    }

    public int GetCurrentResource() => resources;

    private void UpdateUI()
    {
        if (resourceText != null)
            resourceText.text = "Ressources : " + resources;
    }

    

}
