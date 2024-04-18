using System;
using System.Collections;
using UnityEngine;

namespace Spells
{
    public class Spell : MonoBehaviour
    {
        #region Variables
        
        private Animator _animator;
        [SerializeField] float _damage = 10f;
        [SerializeField] float _speed = 5f;
        [SerializeField] float _spellDuration = 4f;
        private bool _isLaunched;
    
        private static readonly int Hit = Animator.StringToHash("Hit");

        #endregion
        
        #region Start

        void Start()
        {
            _animator = GetComponent<Animator>();
            _isLaunched = false;
        }
        
        #endregion

        private void Update()
        {
            if (_isLaunched)
            {
                transform.Translate(Vector3.forward * (_speed * Time.deltaTime));
            }
        }

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
            _isLaunched = true;
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
            yield return new WaitForSeconds(1f);
            Destroy(gameObject);
            yield return null;
        }
        
        #endregion
    }
}
