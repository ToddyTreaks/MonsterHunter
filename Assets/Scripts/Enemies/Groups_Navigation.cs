using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Groups_Navigation : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private GameObject[] _enemies;
    
    private bool _isOnMove = false;
    private float spawnTime = 3.5f; 
    
    
    
    
    private bool _waypointReached = false;
    [SerializeField] private float timeToNextRoaming;
    private float timeBetweenRoaming;


    void Update()
    {
        if (!_isOnMove)
        {
            StartCoroutine(MoveToWaypoint());
        }
        
    }
    
    private IEnumerator MoveToWaypoint()
    {
        _isOnMove = true;
        Transform randomWayPoint = _waypoints[UnityEngine.Random.Range(0, _waypoints.Length)];
        foreach (var enemy in _enemies)
        {
            if (!enemy.GetComponent<DetectionRange>().isPlayerDetected && enemy.GetComponent<Mob_Navigation>().hasSpawned)
            {
                Vector3 ecart = new Vector3(Random.Range(-3, 3), 0, Random.Range(-3, 3));
                Vector3 randomAroundWayPoint =  randomWayPoint.position + ecart;
                enemy.GetComponent<NavMeshAgent>().SetDestination(randomAroundWayPoint);
                timeBetweenRoaming = timeToNextRoaming;
            }
            else
            {
                timeBetweenRoaming = 0f;
            }

            
        }
        
        yield return new WaitForSeconds(timeBetweenRoaming);
        
        _isOnMove = false;
    }
    
    
}
