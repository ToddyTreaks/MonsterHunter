using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionRange : MonoBehaviour
{
    [SerializeField] private float _aggroRange = 10f;
    [SerializeField] private float _attackRange = 2f;
    [SerializeField] private float _lostAggroRange = 20f;
    
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
                Debug.Log("Player Detected");
            }

        }
        
        if (Physics.CheckSphere(transform.position, _attackRange, _playerLayer))
        {
            if (!isPlayerInCloseAttackRange)
            {
                isPlayerInCloseAttackRange = true;
                Debug.Log("Player in range");
            }
        }
        
        if (!Physics.CheckSphere(transform.position, _attackRange, _playerLayer))
        {
            if (isPlayerInCloseAttackRange)
            {
                isPlayerInCloseAttackRange = false;
                Debug.Log("Player not in range anymore");
            }
            
        }
        
        if (!Physics.CheckSphere(transform.position, _lostAggroRange, _playerLayer))
        {
            if (isPlayerDetected)
            {
                isPlayerDetected = false;
                Debug.Log("Player Lost");
            }
        }
    }
}
