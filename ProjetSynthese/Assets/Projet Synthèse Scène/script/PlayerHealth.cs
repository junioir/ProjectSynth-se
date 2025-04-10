using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth _Instance;
    public int _MaxHealth = 100;
    public int _CurrentHealth;

    [SerializeField] private HealthBar _HealthBar;
    [SerializeField] private MeshRenderer _Graphics;
    [SerializeField] private Image _InvincibilityIcon;
    [SerializeField] private GameObject _VxBouclier;
    [SerializeField] private float _InvincibilityFlashDelay = 0.2f;
    [SerializeField] private float _InvincibilityTimeAfterHit = 3f;
    [SerializeField] private bool _IsInvincible = false;
    private void Awake()
    {
        if (_Instance != null)
        {
            Debug.LogError("Il y a déjà une instance de PlayerHealth dans la scène !");
            return;
        }
        _Instance = this;
    }

    void Start()
    {
        _CurrentHealth = _MaxHealth;
        _HealthBar.SetMaxHealth(_MaxHealth);

        if (_InvincibilityIcon != null)
            _InvincibilityIcon.fillAmount = 0; // L'icône est vide au début
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)) // Test de dégâts
        {
            TakeDamage(50);
        }

        if (Input.GetKeyDown(KeyCode.Space)) // Activer manuellement l'invincibilité
        {
            ActivateInvincibility();
            _VxBouclier.SetActive(true);
        }
    }

    public void HealPlayer(int amount)
    {
        if ((_CurrentHealth + amount) > _MaxHealth)
        {
            _CurrentHealth = _MaxHealth;
        }
        else
        {
            _CurrentHealth += amount;
        }

        _HealthBar.SetHealth(_CurrentHealth);
    }

    public void TakeDamage(int damage)
    {
        if (!_IsInvincible)
        {
            _CurrentHealth -= damage;
            _HealthBar.SetHealth(_CurrentHealth);

            if (_CurrentHealth <= 0)
            {
                Die();
                return;
            }

            // ActivateInvincibility(); // Déclenche l'invincibilité après avoir subi des dégâts
        }
    }
    public void ActivateInvincibility()
    {
        if (!_IsInvincible)
        {
            _IsInvincible = true;
            StartCoroutine(Invincibility());
            StartCoroutine(InvincibilityIconUpdate());
        }
    }
    private IEnumerator Invincibility()
    {
        if (_Graphics == null)
        {
            Debug.LogError("MeshRenderer n'est pas assigné.");
            yield break;
        }

        Material material = _Graphics.material;

        while (_IsInvincible)
        {
            // Clignotement de l'invincibilité
            material.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(_InvincibilityFlashDelay);
            material.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(_InvincibilityFlashDelay);
        }

        material.color = new Color(1f, 1f, 1f, 1f); // Assurez-vous que le joueur redevient visible
    }

    private IEnumerator InvincibilityIconUpdate()
    {
        if (_InvincibilityIcon == null)
        {
            Debug.LogError("L'icône d'invincibilité n'est pas assignée.");
            yield break;
        }

        float timer = 0;
        _InvincibilityIcon.fillAmount = 1; // L'icône est complètement remplie au début

        while (timer < _InvincibilityTimeAfterHit)
        {
            timer += Time.deltaTime;
            _InvincibilityIcon.fillAmount = 1 - (timer / _InvincibilityTimeAfterHit); // Décrémentation du remplissage
            yield return null;
        }

        _InvincibilityIcon.fillAmount = 0; // L'icône est vide à la fin de l'invincibilité
        _IsInvincible = false;
    }
    private void Die()
    {
        Debug.Log("Le joueur a perdu.");
        player._Instance.enabled = false;
        player._Instance._animator.SetBool("die", true);
       
        GameOver._Instance.OnplayerDeath();
    }
}
