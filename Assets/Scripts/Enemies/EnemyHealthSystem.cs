using System.Collections;
using UnityEngine;

namespace Enemies
{
    public class EnemyHealthSystem : HealthSystem
    {
        #region Variables
        
        private Animator _animator;
        private static readonly int Death = Animator.StringToHash("Death");
        private static readonly int Hit = Animator.StringToHash("Hit");

        #endregion
        
        #region Initialization
        
        private void Start()
        {
            _animator = GetComponent<Animator>();
        }
        
        #endregion
        
        #region Overrides

        public override void OnDeath()
        {
            _animator.SetTrigger(Death);
            StartCoroutine("DeathMob");
        }

        IEnumerator DeathMob()
        {
            yield return new WaitForSeconds(5f);
            Destroy(gameObject);
        }

        public override void OnHit()
        {
            _animator.SetTrigger(Hit);
        }
        
        #endregion
        
    }
}
