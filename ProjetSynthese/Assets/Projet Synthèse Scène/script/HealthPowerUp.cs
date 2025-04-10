using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerUp : MonoBehaviour
{
    [SerializeField] private int _Healthpoint;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (PlayerHealth._Instance._CurrentHealth != PlayerHealth._Instance._MaxHealth)
            {
                PlayerHealth._Instance.HealPlayer(_Healthpoint);
                Destroy(gameObject);
            }

        }
    }

}
