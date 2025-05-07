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

      
        ResourceManager.Instance.OnResourceChanged += CheckResources;

        
        CheckResources();
    }

    private void OnDestroy()
    {
       
        if (ResourceManager.Instance != null)
            ResourceManager.Instance.OnResourceChanged -= CheckResources;
    }

    private void CheckResources()
    {
        int current = ResourceManager.Instance.GetCurrentResource();
        button.interactable = current >= itemCost;
    }
}
