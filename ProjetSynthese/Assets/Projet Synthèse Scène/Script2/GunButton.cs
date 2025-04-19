using UnityEngine;
using UnityEngine.UI;

public class GunButton : MonoBehaviour
{
    public Gun gunPrefab;
    public PlayerShooting playerShooting;
    public Text costText;

    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        costText.text = gunPrefab.cost.ToString();

        // Check if player can afford this gun
        UpdateButtonState();

        // Listen for resource changes
        GameManager.Instance.OnResourcesChanged += UpdateButtonState;
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnResourcesChanged -= UpdateButtonState;
        }
    }

    private void UpdateButtonState()
    {
        button.interactable = GameManager.Instance.playerResources >= gunPrefab.cost;
    }

    public void OnClick()
    {
        if (GameManager.Instance.playerResources >= gunPrefab.cost)
        {
            GameManager.Instance.AddResources(-gunPrefab.cost);

            // Check if player already has this gun
            for (int i = 0; i < playerShooting.guns.Length; i++)
            {
                if (playerShooting.guns[i].GetType() == gunPrefab.GetType())
                {
                    // Just switch to this gun
                    playerShooting.SwitchWeapon(i);
                    return;
                }
            }

            // Add new gun to player's arsenal
            Gun newGun = Instantiate(gunPrefab, playerShooting.transform);
            Gun[] newGuns = new Gun[playerShooting.guns.Length + 1];
            playerShooting.guns.CopyTo(newGuns, 0);
            newGuns[newGuns.Length - 1] = newGun;
            playerShooting.guns = newGuns;

            // Switch to new gun
            playerShooting.SwitchWeapon(newGuns.Length - 1);
        }
    }
}