using UnityEngine;

public class GunManager : MonoBehaviour
{
    public static GunManager instance;

    public Gun[] allGuns;
    private Gun currentGun;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        EquipGun(0); // Commencer avec la 1ère arme
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1")) // clic gauche
        {
            currentGun?.Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            currentGun?.Reload();
        }
    }

    public void EquipGun(int index)
    {
        for (int i = 0; i < allGuns.Length; i++)
        {
            allGuns[i].gameObject.SetActive(i == index);
        }

        currentGun = allGuns[index];
    }
}
