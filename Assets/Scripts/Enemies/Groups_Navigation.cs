using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Groups_Navigation : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private GameObject[] _enemies;
    
    private bool _isOnMove = false;
    private bool _waypointReached = false;

    [SerializeField] private float timeBetweenRoaming = 10f;


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
            var mobNav = enemy.GetComponent<Mob_Navigation>();
            if (!enemy.GetComponent<DetectionRange>().isPlayerDetected && mobNav.hasSpawned)
            {
                Vector3 ecart = new Vector3(Random.Range(-3, 3), 0, Random.Range(-3, 3));
                Vector3 randomAroundWayPoint =  randomWayPoint.position + ecart;
                enemy.GetComponent<NavMeshAgent>().SetDestination(randomAroundWayPoint);
                
                Transform randomAroundWayPointTransform = new GameObject().transform;
                mobNav.RotateToTarget(randomAroundWayPointTransform);
            }
            
        }
        yield return new WaitForSeconds(timeBetweenRoaming);
        _isOnMove = false;
    }
    
    
}
