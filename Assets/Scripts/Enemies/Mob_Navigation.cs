using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(DetectionRange))]
public class Mob_Navigation : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    private DetectionRange detectionRange;
    private Animator animator;
    
    [SerializeField] private Collider _weaponCollider;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        detectionRange = GetComponent<DetectionRange>();
        animator = GetComponent<Animator>();
        _weaponCollider.enabled = false;
    }
    
    void Update()
    {
        agent.SetDestination(player.position);
        
        if (detectionRange.isPlayerDetected)
        {
            PlayerDetected();
        }
        else
        {
            NoPlayerDetected();
        }
    }
    
    void PlayerDetected()
    {
        if (detectionRange.isPlayerInCloseAttackRange)
        {
            _weaponCollider.enabled = true;
            animator.SetFloat("Speed", 0f);
            agent.SetDestination(transform.position);
            animator.SetTrigger("Attack"); 
            
            if (detectionRange._attackRange > 3f)
            {
                RotateToTarget();
            }
        }
        else
        {
            _weaponCollider.enabled = false;
            animator.SetFloat("Speed", 1f);
            agent.SetDestination(player.position);
        }
        
    }
    
    void NoPlayerDetected()
    {
        _weaponCollider.enabled = false;
        animator.SetFloat("Speed", 0f);
        agent.SetDestination(transform.position);
    }

    void RotateToTarget()
    {
        var targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, agent.angularSpeed * Time.deltaTime);
    }
}
