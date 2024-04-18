using System.Collections;
using UnityEngine;

namespace Spells
{
    public class Spell : MonoBehaviour
    {
        #region Variables
        
        private Animator _animator;
        private float _damage;
        private float _speed;
        private float _spellDuration = 4f;
    
        private static readonly int Launch = Animator.StringToHash("Launch");
        private static readonly int Hit = Animator.StringToHash("Hit");

        #endregion
        
        #region Start

        void Start()
        {
            _animator = GetComponent<Animator>();
            
        }
        
        #endregion
        
        #region Setters
    
        public void SetDamage(float damage)
        {
            _damage = damage;
        }
        
        public void SetSpeed(float speed)
        {
            _speed = speed;
        }
        
        #endregion
        
        #region SpellLifeCycle
    
        public void LaunchSpell()
        {
            _animator.SetTrigger(Launch);
            StartCoroutine(SpellMovement());
        }
        
        IEnumerator SpellMovement()
        {
            while (true)
            {
                transform.Translate(Vector3.forward * (_speed * Time.deltaTime));
                yield return null;
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent<HealthSystem>(out var enemyHealthSystem))
            {
                enemyHealthSystem.Damage(_damage);
            }
            StartCoroutine(HitAndDestroy());
        }
        
        IEnumerator HitAndDestroy()
        {
            _animator.SetTrigger(Hit);
            Destroy(gameObject);
            yield return null;
        }
        
        #endregion
    }
}
