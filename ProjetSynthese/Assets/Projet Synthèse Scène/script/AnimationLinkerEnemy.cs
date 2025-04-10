
using UnityEngine;

public class AnimationLinkerEnemy : MonoBehaviour
{
    public void ResetAttack()
    {
        GetComponentInParent<EnemyMelee>().ResetAttack();
    }
}
