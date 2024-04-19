using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Enemies
{
    public class BossNavigation : EnemyNavigation
    {
    

        #region Variables

        
        internal bool IsFighting;
        [SerializeField] private float stoppingDistance;
        [SerializeField]   float maxHealth = 100f;
    
        private HealthSystem _healthSystem;
        private DetectionRange _detectionRange;
    
        [SerializeField] internal Transform player;
        internal NavMeshAgent Agent;
        private Animator _animator;
    
        [SerializeField] private Transform[] waypoints;
        private Transform _randomWayPoint;
        
        
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int Fighting = Animator.StringToHash("isFighting");

        #endregion
    
        #region Initialization

        private void Awake()
        {
            _healthSystem = GetComponent<HealthSystem>();
            _healthSystem.SetMaxLife(maxHealth);
            _detectionRange = GetComponent<DetectionRange>();
            Agent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
            
            Agent.stoppingDistance = stoppingDistance;
            IsFighting = false;
        
        }
    
        void Start()
        {
            SetWayPoints();
        }
    
        #endregion

        #region Update

        void FixedUpdate()
        {
            if (!_isDead)
                FightStatusUpdate();
        }
        void Update()
        {
            _animator.SetFloat(Speed, Agent.velocity.magnitude);
        }

        #endregion
    
        #region FightStatus

        void FightStatusUpdate()
        {
            HasFightingStatusChanged();
        
            if (IsFighting)
                Agent.SetDestination(player.position);
            else
                OutOfFightUpdate();
        }
    
        void HasFightingStatusChanged()
        {
            if (_detectionRange.IsPlayerDetected && !IsFighting)
            {
                GetInFight();
            }
            else if(!IsFighting)
            {
            
            }
            else if(!_detectionRange.IsPlayerDetected && IsFighting)
            {
                GotOutOfFight();
            }
        }
    
        #endregion
    
        #region OutOfFight
    
        void GotOutOfFight()
        {
            _animator.SetBool(Fighting, false);
            IsFighting = false;
        }
    
        void OutOfFightUpdate()
        {
            if (Vector3.Distance(transform.position, _randomWayPoint.position) < 5f)
            {
                SetWayPoints();
                ToWayPoints();
            }
            else
            {
                ToWayPoints();
            }
        }
    
        void SetWayPoints()
        {
            _randomWayPoint = waypoints[Random.Range(0, waypoints.Length)];
        }
    
        void ToWayPoints()
        {
            Agent.SetDestination(_randomWayPoint.position);
        }
    
    
        #endregion
    
        #region InFight
    
        void GetInFight()
        {
            _animator.SetBool(Fighting, true);
            IsFighting = true;
        }
    
        #endregion

    }
}