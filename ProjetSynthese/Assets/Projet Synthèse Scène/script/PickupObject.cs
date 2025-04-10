using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObject : MonoBehaviour
{
    [SerializeField] private AudioClip _Sound;
    [SerializeField] private AudioSource _Audiosource;
    private void OnTriggerEnter(Collider collision)

    {
        if (collision.CompareTag("Player"))
        {

            AudioSource.PlayClipAtPoint(_Sound, transform.position);

            Inventory._Instance.Addcoin(1);

            Destroy(gameObject);
        }
    }

}
