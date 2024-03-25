using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionRange : MonoBehaviour
{
    [SerializeField] private float _aggroRange = 10f;
    [SerializeField] private float _attackRange = 2f;
    [SerializeField] private float _lostAggroRange = 20f;
    
    [SerializeField] private LayerMask _playerLayer;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
