using UnityEngine;
using UnityEngine.EventSystems;

public class BuyGunButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [TextArea]
    public string tooltipContent;

    public void OnPointerEnter(PointerEventData eventData)

    {
        Debug.Log("SURVOL détecté");
        TooltipSystem.Instance.Show(tooltipContent, Input.mousePosition);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.Instance.Hide();
    }
}
