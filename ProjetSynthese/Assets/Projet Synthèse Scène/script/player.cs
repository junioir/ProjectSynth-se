using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class player : MonoBehaviour
{
    public Animator _animator;
    public static player _Instance;

    [SerializeField] private AnimationController _controller;
    [SerializeField] private AudioClip _WalkSound;
    [SerializeField] private AudioClip _AttackSound;
    [SerializeField] private AudioSource _AudioSource;
    [SerializeField] private float _movementSpeed = 5f;
    [SerializeField] private float _stopingDistance = 0.75f;
   // [SerializeField] private float _attackcoolDown = 1.5f;
    [SerializeField] private float _life;
    [SerializeField] private int _damage = 5;

    private Camera _camera;
    private Rigidbody _rigidbody;
    private heathanddefent _Currentenemy;
    private Vector3 _targetposition;
    private bool _attackIsattive;


    private void Awake()
    {
        if (_Instance != null)
        {
            Debug.Log("The is more player movement in the scene");
            return;
        }
        _Instance = this;
    }
    void Start()
    {
        _camera = Camera.main;
        _rigidbody = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray;
            RaycastHit hit;
            ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                heathanddefent enemy = hit.collider.GetComponent<heathanddefent>();

                if (enemy != null)
                {
                    _Currentenemy = enemy;
                    _attackIsattive = true;

                }

                else
                {
                    _Currentenemy = null;
                    _targetposition = hit.point;
                    transform.LookAt(_targetposition);
                }
            }

        }
        if (_Currentenemy != null)
        {
            _targetposition = _Currentenemy.transform.position;
            transform.LookAt(_Currentenemy.transform.position);
        }

        float distance = (transform.position - _targetposition).magnitude;
        Vector3 direction = (_targetposition - transform.position).normalized;

        if (distance > _stopingDistance)
        {
            _rigidbody.velocity = _movementSpeed * direction;
            _controller.SetIsWalking();
            PlaySound(_WalkSound);
        }
        else
        {
            _rigidbody.velocity = Vector3.zero;
            _controller.SetIsNotWalking();
        }

        if (_attackIsattive && distance < _stopingDistance && _Currentenemy != null)
        {
            attack();
        }
    }
    public void attack()

    {
        _controller.SetIsAttacking();
        PlaySound(_AttackSound);
        _attackIsattive = false;
        _Currentenemy.ReceiveDamage(_damage);

    }
    public void ReceiveDamage(float damage)
    {
        _life -= damage;
        Debug.Log("health remaning is:" + _life);
    }
    public float Getlife()
    {
        return _life;
    }
    private void PlaySound(AudioClip clip)
    {
        if (clip != null && _AudioSource != null)
        {
            _AudioSource.PlayOneShot(clip);
        }
    }

}

