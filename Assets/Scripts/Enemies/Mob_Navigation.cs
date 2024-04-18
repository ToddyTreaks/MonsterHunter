using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    [RequireComponent(typeof(DetectionRange))]
    public class MobNavigation : MonoBehaviour
    {

        #region Variables
        
        public Transform player;
        private NavMeshAgent _agent;
        private DetectionRange _detectionRange;
        private Animator _animator;
    
        public bool hasSpawned;
        private float _spawnTime = 3.5f;
        private bool _canAttack = true;
        [SerializeField] private float _attackCooldown;
        private float _attackCooldownTimer;
        
        [SerializeField] private Collider weaponCollider;
        
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int Attack = Animator.StringToHash("Attack");

        #endregion

        #region Start

        void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _detectionRange = GetComponent<DetectionRange>();
            _animator = GetComponent<Animator>();
            weaponCollider.enabled = false;
        
            _agent.enabled = false;
            hasSpawned = false;
        }

        #endregion

        #region Update

        void Update()
        {
            if (!hasSpawned)
                HasNotSpawned();
            
            else if (_detectionRange.IsPlayerDetected)
                PlayerDetected();
            
            else
                NoPlayerDetected();
            
            if (!_canAttack)
                ReloadTime();
        }
        
        private void HasNotSpawned()
        {
            Debug.Log("Not Spawned");
            if (_spawnTime <= 0)
            {
                _agent.enabled = true;
                Debug.Log("Spawned");
                hasSpawned = true;
            }
            else
            {
                _spawnTime -= Time.deltaTime;
            }
        }
        
        private void ReloadTime()
        {
            if (_attackCooldownTimer > 0)
                _attackCooldownTimer -= Time.deltaTime;
            else
                _canAttack = true;
        }

        #endregion
        
        #region PlayerDetected
    
        void PlayerDetected()
        {
            if (_detectionRange.IsPlayerInCloseAttackRange && _canAttack)
                PlayerInCloseRange();
            else
                PlayerNotInCloseRange();
        
        }

        public virtual void PlayerInCloseRange()
        {
            _canAttack = false;
            _attackCooldownTimer = _attackCooldown;
            weaponCollider.enabled = true;
            _animator.SetFloat(Speed, 0f);
            _agent.SetDestination(transform.position);
            _animator.SetTrigger(Attack);
        }

        private void PlayerNotInCloseRange()
        {
            weaponCollider.enabled = false;
            _animator.SetFloat(Speed, 1f);
            _agent.SetDestination(player.position);
        }
        
        
        
        #endregion
        
        #region NoPlayerDetected
    
        void NoPlayerDetected()
        {
            weaponCollider.enabled = false;
            _animator.SetFloat(Speed, _agent.velocity.magnitude / _agent.speed);
        }
        
        #endregion

        
    
    }
}
