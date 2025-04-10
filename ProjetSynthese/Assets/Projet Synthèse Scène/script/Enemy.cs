using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _life;
    [SerializeField] private float _damage;
    [SerializeField] private float _mouvmentSpeed;
    [SerializeField] private bool _isMelee;

    private MeshRenderer _meshrenderer;
    private player _player;
    private Rigidbody _rb;
    private Vector3 _direction;
    private float _distanceToPlayer;
    private float _stopingDistance = 0.9f;
    private float _attackReset = 1.2f;
    private float _timer;
    private bool _attacking = false;
    private bool _dying = false;


    void Start()
    {
        _rb = this.gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (_life >= 0)
        {
            _timer += Time.deltaTime;
            GameObject[] checking;
            checking = GameObject.FindGameObjectsWithTag("Player");
            if (checking.Length != 0)
            {
                _player = checking[0].GetComponent<player>();
                _direction = _player.transform.position - _rb.transform.position;
                _distanceToPlayer = _direction.magnitude;
                _rb.transform.LookAt(_player.transform.position);
                if (_distanceToPlayer > _stopingDistance && !_attacking)
                {
                    _rb.velocity = _direction.normalized * _mouvmentSpeed;
                    _animator.Play("run");
                }
                else if (_distanceToPlayer <= _stopingDistance && !_attacking && _timer > _attackReset)
                {
                    StartCoroutine(AttackDealay());
                    _timer = 0f;
                }
                else
                {
                    _rb.velocity = Vector3.zero;
                }
            }
        }
        else if (_life <= 0 && !_dying)
        {
            _dying = true;
            _rb.velocity = Vector3.zero;
            _animator.Play("die");
        }
    }

    private IEnumerator AttackDealay()
    {
        _rb.velocity = Vector3.zero;
        _animator.Play("attack");
        _attacking = true;
        yield return new WaitForSeconds(0.45f);
        _player.ReceiveDamage(_damage);
        _attacking = false;
    }

    public void ReceiveDamage(float damage)
    {
        _life = _life - damage;
        Debug.Log($"Name:{gameObject.name} Life: {_life} Damage Taken :{damage}");
    }

    private void Awake()
    {
        _meshrenderer = GetComponent<MeshRenderer>();
    }

    internal void ChangeColor(Color color)
    {
        _meshrenderer.material.color = color;
    }

    
}
