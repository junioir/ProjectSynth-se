using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heathanddefent : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    [SerializeField] private int _health = 20;
    [SerializeField] private AnimationController _controller;
    //[SerializeField] private Animator _animator;
    public void ReceiveDamage(int damage)
    {
        _health -= damage;

        Debug.Log("health remaning is:" + _health);

        if (_health == 0)
        {
            _controller.SetIsDying();
            Destroy(gameObject);
            // _animator.SetBool("die", true);
            Debug.Log("enemy mort");
        }

    }

}