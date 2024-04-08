using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Boss_Navigation : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent _agent;
    private BossAnimatorControl _bossAnimatorControl;

    #region Variables

    internal float Speed = 0f;
    internal float Attack = 0f;
    internal float Invocation = 0f;
    internal float Shoot = 1f;
    internal bool IsAttacking = false;
    internal bool IsFighting = false;
    internal bool IsDead = false;
    
    private float maxHealth = 100f;
    private HealthSystem _healthSystem;
    private DetectionRange _detectionRange;
    
    
    [SerializeField] private Transform[] _waypoints;
    private Transform randomWayPoint;
    
    #endregion
    
    #region Initialization

    private void Awake()
    {
        _healthSystem = GetComponent<HealthSystem>();
        _healthSystem.SetMaxLife(maxHealth);
        
    }
    
    void Start()
    {
        _detectionRange = GetComponent<DetectionRange>();
        _agent = GetComponent<NavMeshAgent>();
        _bossAnimatorControl = GetComponent<BossAnimatorControl>();
        SetWayPoints();
    }
    
    #endregion

    #region Update

    void FixedUpdate()
    {
        FightStatusUpdate();
    }
    void Update()
    {
        Speed = _agent.velocity.magnitude;
        _bossAnimatorControl.UpdateAnimation();
    }

    #endregion
    
    #region FightStatus

    void FightStatusUpdate()
    {
        HasFightingStatusChanged();
        
        if (IsFighting) 
            InFightUpdate();
        else 
            OutOfFightUpdate();
    }
    
    void HasFightingStatusChanged()
    {
        if (_detectionRange.isPlayerDetected && !IsFighting)
        {
            GetInFight();
        }
        else if(!_detectionRange.isPlayerDetected && IsFighting)
        {
            GotOutOfFight();
        }
    }
    
    #endregion
    
    #region OutOfFight
    
    void GotOutOfFight()
    {
        IsFighting = false;
    }
    
    void OutOfFightUpdate()
    {
        if (Vector3.Distance(transform.position, randomWayPoint.position) < 1f)
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
        randomWayPoint = _waypoints[Random.Range(0, _waypoints.Length)];
    }
    
    void ToWayPoints()
    {
        _agent.SetDestination(randomWayPoint.position);
    }
    
    
    #endregion
    
    #region InFight
    void InFightUpdate()
    {
        _agent.SetDestination(player.position);
    }
    
    void GetInFight()
    {
        IsFighting = true;
    }
    
    #endregion

    #region Death

    void Death()
    {
        IsDead = true;
    }

    #endregion
}