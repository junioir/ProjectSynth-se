using TMPro;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    public static TooltipSystem Instance;

    [SerializeField] private GameObject tooltipPanel;
    [SerializeField] private TextMeshProUGUI tooltipText;

    void Awake()
    {
        Instance = this;
        Hide();
    }

    public void Show(string content, Vector2 position)
    {
        Debug.Log("TOOLTIP SHOW: " + content); // ← AJOUTE CETTE LIGNE
        tooltipPanel.SetActive(true);
        tooltipText.text = content;
        tooltipPanel.transform.position = position;
    }


    public void Hide()
    {
        tooltipPanel.SetActive(false);


    }


}
