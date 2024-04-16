using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(DetectionRange))]
[RequireComponent(typeof(HealthSystem))]
public class Mob_Navigation : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    private DetectionRange detectionRange;
    private Animator animator;
    
    public bool hasSpawned = false;
    private float spawnTime = 3.5f;
    
    [SerializeField] private Collider _weaponCollider;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        detectionRange = GetComponent<DetectionRange>();
        animator = GetComponent<Animator>();
        _weaponCollider.enabled = false;
        
        agent.enabled = false;
    }
    
    void Update()
    {
        if (!hasSpawned)
        {
            if (spawnTime <= 0)
            {
                hasSpawned = true;
                agent.enabled = true;
            }
            else
            {
                spawnTime -= Time.deltaTime;
            }
        }
        else if (detectionRange.isPlayerDetected)
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
                RotateToTarget(player);
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
        animator.SetFloat("Speed", agent.velocity.magnitude / agent.speed);
    }

    public void RotateToTarget(Transform objectif)
    {
        var targetRotation = Quaternion.LookRotation(objectif.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, agent.angularSpeed * Time.deltaTime);
    }
}
