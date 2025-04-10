using System.Collections;

using UnityEngine;

public class frieBall : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private FireBall _Fireball;
    [SerializeField] private AudioClip _PowerSound;
    [SerializeField] private AudioSource _AudioSource;
    [SerializeField] private Transform _characterhand;
    [SerializeField] private float _coolDownDelay;
    [SerializeField] private float _animationDelay = 0.5f;
    private float _Timer;
    void Update()
    {
        _Timer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Mouse1) && _Timer > _coolDownDelay)
        {
            Ray ray;
            RaycastHit hit;
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {

                heathanddefent health = hit.collider.GetComponent<heathanddefent>();
                if (health != null)
                {
                    _animator.transform.LookAt(health.transform.position);

                    StartCoroutine(SendFireBall(health.transform));
                }
            }


        }
    }
    public float GetCoolDownRatio()
    {
        return 1 - (_Timer / _coolDownDelay);
    }
    private IEnumerator SendFireBall(Transform target)

    {
        _animator.SetBool("IsAttacking", true);
        yield return new WaitForSeconds(_animationDelay);
        PlaySound(_PowerSound);
        FireBall newfireBall = Instantiate(_Fireball, _characterhand.position, Quaternion.identity);
        newfireBall.SetTarget(target);
        yield return new WaitForSeconds(_animationDelay);
        _animator.SetBool("IsAttacking", false);
        _Timer = 0;
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && _AudioSource != null)
        {
            _AudioSource.PlayOneShot(clip);
        }
    }

}