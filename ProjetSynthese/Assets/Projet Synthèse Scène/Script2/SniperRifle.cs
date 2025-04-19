using UnityEngine;

public class SniperRifle : Gun
{
    [Header("Sniper Specific")]
    public float zoomFOV = 20f;
    public float normalFOV = 60f;
    public GameObject scopeOverlay;
    public int criticalDamage = 100;
    public float criticalChance = 0.3f;

    private bool isZoomed;

    protected override void Update()
    {
        base.Update();

        if (Input.GetMouseButtonDown(1)) // Right click to zoom
        {
            ToggleZoom();
        }
    }

    private void ToggleZoom()
    {
        isZoomed = !isZoomed;
        Camera.main.fieldOfView = isZoomed ? zoomFOV : normalFOV;

        if (scopeOverlay != null)
        {
            scopeOverlay.SetActive(isZoomed);
        }
    }

    protected override void PerformShot()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Damage logic with critical chance
            int finalDamage = damage;
            if (Random.value < criticalChance)
            {
                finalDamage = criticalDamage;
                // Show critical hit effect
            }

            Enemy enemy = hit.collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(finalDamage);
            }
        }
    }
}