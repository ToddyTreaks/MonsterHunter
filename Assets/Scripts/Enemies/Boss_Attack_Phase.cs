using System.Collections;
using Common;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies
{
    public class BossAttackPhase : MonoBehaviour
    {
        #region Variables

        private BossNavigation _bossNavigation;
        private DetectionRange _detectionRange;
        private Animator _animator;

        private float _attackCooldown = 2f;
        private float _attackCooldownTimer;

        private float _onGoingSpeedModifier = 1f;
        private float _savedAgentSpeed;
        private bool _isAgentStopped;
        
        private float _speedAgent;
    
        [SerializeField] private Collider kickCollider;
        [SerializeField] private Collider punchCollider;
        [SerializeField] private GameObject[] enemies;
        [SerializeField] private GameObject spellShort;
        [SerializeField] private GameObject spellHeavy;
        [SerializeField] private Transform spellSpawnPoint;
        
        
        private static readonly int Shoot = Animator.StringToHash("Shoot");
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Invocation = Animator.StringToHash("Invocation");
        private static readonly int IsAttacking = Animator.StringToHash("isAttacking");

        #endregion
        
        #region Initialisation
        
        void Start()
        {
            _bossNavigation = GetComponent<BossNavigation>(); 
            _detectionRange = GetComponent<DetectionRange>();
            _animator = GetComponent<Animator>();
        
            kickCollider.enabled = false;
            punchCollider.enabled = false;
            _isAgentStopped = false;
            _speedAgent = _bossNavigation.Agent.speed;
        }
        
        #endregion

        #region Update
        
        void Update()
        {
        
            if (_bossNavigation.IsFighting && _attackCooldownTimer <= 0)
            {
                kickCollider.enabled = false;
                punchCollider.enabled = false;
                StartCoroutine(NextAttack());
                _attackCooldownTimer = 10;
            }
            else if (_bossNavigation.IsFighting)
            {
                _attackCooldownTimer -= Time.deltaTime;
                if (!_detectionRange.IsPlayerInCloseAttackRange)
                    Utils.RotateToTarget(transform, _bossNavigation.player, 1f);
            }
            else if (!_bossNavigation.IsFighting)
            {
                kickCollider.enabled = false;
                punchCollider.enabled = false;
                StopCoroutine(NextAttack());
                _animator.SetBool(IsAttacking, false);
                _bossNavigation.Agent.speed = _speedAgent;
            }
        }
        
        #endregion

        #region AttackChoice
    
        private IEnumerator NextAttack()
        {
            _animator.SetBool(IsAttacking, false);
            ResetSpeedModification();
            yield return new WaitForSeconds(Random.Range(1, 5));
            _animator.SetBool(IsAttacking, true);
            AttackChoice();
            yield return null; 
        }
        
        private void AttackChoice()
        {
            if (_detectionRange.IsPlayerInCloseAttackRange)
                CorpACorpChoice();
            else
                DistanceChoice();
        }
    
        private void CorpACorpChoice()
        {
            int choice = Random.Range(0, 2);
            if (choice < 1) 
                MeleeAttack_Kick();
            else                
                MeleeAttack_Punch();
        }
    
        private void DistanceChoice()
        {
            int choice = Random.Range(0, 10);
            if (choice < 2) 
                InvocationChoice();
            else 
                SpellcastChoice();
            
        }
    
        private void InvocationChoice()
        {
            int choiceSummonType = Random.Range(0, 10);
            if (choiceSummonType < 8)
                Spellcast_Summon();
            else
                Spellcast_Raise();
        }
    
        private void SpellcastChoice()
        {
            int choiceAttackType = Random.Range(0, 10);
            if (choiceAttackType < 1)
                Spellcast_Shoot();
            else if (choiceAttackType < 10)
                Spellcast_ContinuousShoot();
            else
                Spellcast_HeavyShoot();
        }
    
        #endregion
    
        #region MeleeAttack
    
        // Kick
        private void MeleeAttack_Kick()
        {
            kickCollider.enabled = true;
            SpeedModification(0.3f);
            SetAttackCooldown(0.5f);
            _animator.SetFloat(Attack, 2);
        
        }
        // Punch
        private void MeleeAttack_Punch()
        {
            punchCollider.enabled = true;
            SpeedModification(0.3f);
            SetAttackCooldown(0.3f);
            _animator.SetFloat(Attack, 3);
        
        }
    
        #endregion
    
        #region Invocation
    
        //  Invock few mobs
        private void Spellcast_Raise()
        {
            StopAgent();
            SetAttackCooldown(2f);
            _animator.SetFloat(Attack, 0);
            _animator.SetFloat(Invocation, 0);
        
            var random = Random.Range(1,2);
        
            for (int i = 0; i < random; i++)
            {
                Spawn(transform);
            }
        }
        // Invock a swarm of mobs
        private void Spellcast_Summon()
        {
            StopAgent();
            SetAttackCooldown(3f);
            _animator.SetFloat(Attack, 0);
            _animator.SetFloat(Invocation, 1);
        
            var random = Random.Range(3,6);
        
            for (int i = 0; i < random; i++)
            {
                Spawn(transform);
            }
        }
    
        public void Spawn(Transform spawnPoint)
        {
            int randomEnemy = Random.Range(0, enemies.Length);
            var spawnpointNew = new Vector3(Random.Range(-4,4), 0, Random.Range(-4,4));
            Instantiate(enemies[randomEnemy], spawnPoint.position + spawnpointNew, Quaternion.identity);
        }
    
        #endregion
    
        #region Spellcast
        private void Spellcast_Shoot()
        {
            StopAgent();
            SetAttackCooldown(1f);
            _animator.SetFloat(Attack, 1);
            _animator.SetFloat(Shoot, 0);
            GameObject bullet = Instantiate(spellShort, spellSpawnPoint.position, Quaternion.identity);
            bullet.SetActive(true);
        }
        private void Spellcast_ContinuousShoot()
        {
            StopAgent();
            SetAttackCooldown(1f);
            _animator.SetFloat(Attack, 1);
            _animator.SetFloat(Shoot, 1);
            GameObject bullet = Instantiate(spellShort, spellSpawnPoint.position, Quaternion.identity);
            bullet.SetActive(true);
        }
        private void Spellcast_HeavyShoot()
        {
            StopAgent();
            SetAttackCooldown(3f);
            _animator.SetFloat(Attack, 1);
            _animator.SetFloat(Shoot, 2);
            GameObject bullet = Instantiate(spellHeavy, spellSpawnPoint.position, Quaternion.identity);
            bullet.SetActive(true);
        }
    
        #endregion
    
        #region SpeedModification 
    
        private void SpeedModification(float value)
        {
        
            ResetSpeedModification();
            _onGoingSpeedModifier = value;
            _bossNavigation.Agent.velocity *=  _onGoingSpeedModifier;
        }

        private void ResetSpeedModification()
        {
            if (_isAgentStopped)
                ResumeAgent();
            _bossNavigation.Agent.velocity /=  _onGoingSpeedModifier;
        }

        private void StopAgent()
        {
            _isAgentStopped = true;
            _savedAgentSpeed = _bossNavigation.Agent.speed;
            _bossNavigation.Agent.speed = 0.001f;
        }
    
        private void ResumeAgent()
        {
            _bossNavigation.Agent.speed = _savedAgentSpeed;
            _isAgentStopped = false;
        }

        #endregion
    
        #region AttackCooldown
    
        private void SetAttackCooldown(float value)
        {
            _attackCooldown = value;
            _attackCooldownTimer = _attackCooldown;
        }
    
        #endregion
        
    
    }
}
