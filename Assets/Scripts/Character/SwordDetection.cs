using Enemies;
using UnityEngine;

public class SwordDetection : MonoBehaviour
{
    [SerializeField] Animator _animator;

    public static int Damage = 0;

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<EnemyHealthSystem>(out var enemie) && !_animator.GetCurrentAnimatorStateInfo(1).IsName("empty"))
        {
            Debug.Log("hit");
            enemie.Damage(Damage);
        }
    }
}
