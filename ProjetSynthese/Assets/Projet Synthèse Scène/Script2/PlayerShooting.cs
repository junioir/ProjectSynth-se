using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Gun[] guns;
    private int currentGunIndex = 0;

    private void Start()
    {
        // Activate first gun and deactivate others
        for (int i = 0; i < guns.Length; i++)
        {
            guns[i].gameObject.SetActive(i == currentGunIndex);
        }

        if (guns.Length > 0)
        {
            guns[currentGunIndex].OnSelect();
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (guns.Length > 0)
            {
                guns[currentGunIndex].Shoot();
            }
        }

        // Switch weapons with number keys
        if (Input.GetKeyDown(KeyCode.Alpha1) && guns.Length >= 1)
        {
            SwitchWeapon(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && guns.Length >= 2)
        {
            SwitchWeapon(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && guns.Length >= 3)
        {
            SwitchWeapon(2);
        }
    }

    public void SwitchWeapon(int index)
    {
        if (index < 0 || index >= guns.Length) return;

        guns[currentGunIndex].gameObject.SetActive(false);
        currentGunIndex = index;
        guns[currentGunIndex].gameObject.SetActive(true);
        guns[currentGunIndex].OnSelect();
    }
}