using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionRange : MonoBehaviour
{
    [SerializeField] internal float _aggroRange = 10f;
    [SerializeField] internal float _attackRange = 2f;
    [SerializeField] internal float _lostAggroRange = 20f;
    
    [SerializeField] private LayerMask _playerLayer;
    
    internal bool isPlayerDetected = false;
    internal bool isPlayerInCloseAttackRange = false;


    void Update()
    {
        if (Physics.CheckSphere(transform.position, _aggroRange, _playerLayer))
        {
            if (!isPlayerDetected)
            {
                isPlayerDetected = true;
            }

        }
        
        if (Physics.CheckSphere(transform.position, _attackRange, _playerLayer))
        {
            if (!isPlayerInCloseAttackRange)
            {
                isPlayerInCloseAttackRange = true;
            }
        }
        
        if (!Physics.CheckSphere(transform.position, _attackRange, _playerLayer))
        {
            if (isPlayerInCloseAttackRange)
            {
                isPlayerInCloseAttackRange = false;
            }
            
        }
        
        if (!Physics.CheckSphere(transform.position, _lostAggroRange, _playerLayer))
        {
            if (isPlayerDetected)
            {
                isPlayerDetected = false;
            }
        }
    }
}
