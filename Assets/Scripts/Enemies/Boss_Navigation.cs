using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(HealthSystem))]
public class Boss_Navigation : MonoBehaviour
{
    

    #region Variables

    internal float Speed;
    internal float Attack = 0f;
    internal float Invocation = 0f;
    internal float Shoot = 1f;
    internal bool IsAttacking = false;
    internal bool IsFighting = false;
    internal bool IsDead = false;
    
    [SerializeField] private float stoppingDistance = 2f;
    
    private float maxHealth = 100f;
    private HealthSystem _healthSystem;
    private DetectionRange _detectionRange;
    
    [SerializeField] internal Transform player;
    internal NavMeshAgent _agent;
    private Animator _animator;
    
    
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
        _animator = GetComponent<Animator>();
        SetWayPoints();
        _agent.stoppingDistance = stoppingDistance;
        Speed = _agent.speed;
    }
    
    #endregion

    #region Update

    void FixedUpdate()
    {
        FightStatusUpdate();
    }
    void Update()
    {
        
        _animator.SetFloat("Speed", _agent.velocity.magnitude);
    }

    #endregion
    
    #region FightStatus

    void FightStatusUpdate()
    {
        HasFightingStatusChanged();
        
        if (IsFighting)
            _agent.SetDestination(player.position);
        else
            OutOfFightUpdate();
    }
    
    void HasFightingStatusChanged()
    {
        if (_detectionRange.isPlayerDetected && !IsFighting)
        {
            GetInFight();
        }
        else if(!IsFighting)
        {
            
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
        _animator.SetBool("isFighting", false);
        IsFighting = false;
        Debug.Log("GotOutOfFight");
    }
    
    void OutOfFightUpdate()
    {
        if (Vector3.Distance(transform.position, randomWayPoint.position) < 5f)
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
    
    void GetInFight()
    {
        _animator.SetBool("isFighting", true);
        IsFighting = true;
        Debug.Log("GetInFight");
    }
    
    #endregion

    #region Death

    void Death()
    {
        IsDead = true;
    }

    #endregion
}