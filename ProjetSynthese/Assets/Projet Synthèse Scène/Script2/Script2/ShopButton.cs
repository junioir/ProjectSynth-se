using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ShopButton : MonoBehaviour
{
    [SerializeField] private int itemCost = 10;
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();

        // 🔗 S'abonne à l'événement du ResourceManager
        ResourceManager.Instance.OnResourceChanged += CheckResources;

        // Vérifie une première fois à l'initialisation
        CheckResources();
    }

    private void OnDestroy()
    {
        // 🔌 Toujours se désabonner pour éviter les erreurs
        if (ResourceManager.Instance != null)
            ResourceManager.Instance.OnResourceChanged -= CheckResources;
    }

    private void CheckResources()
    {
        int current = ResourceManager.Instance.GetCurrentResource();
        button.interactable = current >= itemCost;
    }
}
