using UnityEngine;

public class SwordDetection : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private bool hasAttack = false;
    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<enemieHealthbdf>(out var enemie) && _animator.GetCurrentAnimatorStateInfo(1).IsName("Attack"))
        {
            enemie.ApplyDamage(100);
        }
    }
}
