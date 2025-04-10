using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FireBall : MonoBehaviour
{
    [SerializeField] private GameObject _explosionVFX;
    [SerializeField] private float _Raduis;
    [SerializeField] private float _yoffset = 1.5f;
    [SerializeField] private float _speed;
    [SerializeField] private float _explosionDelay;
    [SerializeField] private int _damage;

    private Transform _target;
    private Rigidbody _Rigidbody;
    private Vector3 _Radius;
    private bool _hasExplosed;


    void Start()
    {
        _Rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 direction = ((_target.position + new Vector3(0, _yoffset, 0)) - transform.position).normalized;
        if (!_hasExplosed)
        {
            _Rigidbody.velocity = direction * _speed;
        }
    }
    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<heathanddefent>() != null && !_hasExplosed)
        {
            Explosion();
            Debug.Log("Explosion done");
        }
    }
    private void Explosion()
    {
        transform.localScale = Vector3.one * _Raduis * 2;
        _explosionVFX.SetActive(true);
        Collider[] hitCollider = Physics.OverlapSphere(transform.position, _Raduis);
        foreach (Collider collider in hitCollider)
        {
            heathanddefent health = collider.GetComponent<heathanddefent>();
            if (health != null)
            {
                health.ReceiveDamage(_damage);
            }
        }
        _hasExplosed = true;
        _Rigidbody.velocity = Vector3.zero;
        Destroy(gameObject, _explosionDelay);
    }
}



