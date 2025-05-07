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

       
        button.onClick.AddListener(AttemptPurchase);

        
        ResourceManager.Instance.OnResourceChanged += CheckResources;

        CheckResources();
    }

    private void OnDestroy()
    {
        if (ResourceManager.Instance != null)
            ResourceManager.Instance.OnResourceChanged -= CheckResources;

        button.onClick.RemoveListener(AttemptPurchase);
    }

    private void CheckResources()
    {
        int current = ResourceManager.Instance.GetCurrentResource();
        button.interactable = current >= itemCost;
    }

    private void AttemptPurchase()
    {
        bool success = ResourceManager.Instance.SpendResource(itemCost);

        if (success)
        {
            Debug.Log("Achat réussi pour " + itemCost + " ressources !");
            
        }
        else
        {
            Debug.Log("Pas assez de ressources !");
        }
    }
}
