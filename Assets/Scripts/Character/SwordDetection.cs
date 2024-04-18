using Enemies;
using UnityEngine;

public class SwordDetection : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private bool hasAttack = false;
    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<EnemyHealthSystem>(out var enemie) && _animator.GetCurrentAnimatorStateInfo(1).IsName("Attack"))
        {
            Debug.Log("Hit");
            enemie.Damage(100);
        }
    }
}
