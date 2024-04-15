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
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        detectionRange = GetComponent<DetectionRange>();
        animator = GetComponent<Animator>();
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
            animator.SetFloat("Speed", 1f);
            agent.SetDestination(player.position);
        }
        
    }
    
    void NoPlayerDetected()
    {
        animator.SetFloat("Speed", 0f);
        agent.SetDestination(transform.position);
    }

    void RotateToTarget()
    {
        var targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, agent.angularSpeed * Time.deltaTime);
    }
}
