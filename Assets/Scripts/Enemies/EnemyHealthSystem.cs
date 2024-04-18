using UnityEngine;

namespace Enemies
{
    public class EnemyHealthSystem : HealthSystem
    {
        private Animator _animator;
        private static readonly int Death = Animator.StringToHash("Death");
        private static readonly int Hit = Animator.StringToHash("Hit");

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public override void OnDeath()
        {
            _animator.SetTrigger(Death);
        }

        public override void OnHit()
        {
            _animator.SetTrigger(Hit);
        }
    
    
    }
}
