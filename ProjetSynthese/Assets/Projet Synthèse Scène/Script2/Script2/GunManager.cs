using UnityEngine;
using TMPro;
using System;

public class GunManager : MonoBehaviour
{
    public static GunManager instance;

    public Gun[] allGuns;
    private Gun currentGun;

    public int money = 100;
    public TextMeshProUGUI ammoText;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        EquipGun(0); // commencer avec Gun1 (gratuit)
        

    }

    private void Update()

    {

        if (Input.GetButtonDown("Fire1") && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            currentGun?.Shoot();
        }

        if (Input.GetButtonDown("Fire1")) // CLIC GAUCHE
        {
            currentGun?.Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            TryReload();
        }

        UpdateAmmoUI();
    }


    public void EquipGun(int index)
    {
        for (int i = 0; i < allGuns.Length; i++)
            allGuns[i].gameObject.SetActive(i == index);

        currentGun = allGuns[index];
    }

    public void BuyGun(int index)
    {
        Gun gunToBuy = allGuns[index];
        if (money >= gunToBuy.price)
        {
            money -= gunToBuy.price;
            EquipGun(index);
        }
        else
        {
            Debug.Log("Pas assez d'argent");
        }
    }

    public void TryReload()
    {
        if (money >= 10)
        {
            money -= 10;
            currentGun?.Reload();
        }
        else
        {
            Debug.Log("Pas assez d'argent pour recharger");
        }
    }

    private void UpdateAmmoUI()
    {
        if (currentGun != null && ammoText != null)
        {
            Debug.Log($"[Ammo UI] Mise à jour : {currentGun.ammo} / {currentGun.maxAmmo}");
            ammoText.text = $"Ammo: {currentGun.ammo} / {currentGun.maxAmmo}";
        }
    }
}
