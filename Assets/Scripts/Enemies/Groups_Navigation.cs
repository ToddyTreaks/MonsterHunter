using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    public class GroupsNavigation : MonoBehaviour
    {
        #region Variables

        [SerializeField] private Transform[] waypoints;
        [SerializeField] private GameObject[] enemies;
        [SerializeField] private float timeToNextRoaming;

        private bool _isOnMove;

        private float _timeBetweenRoaming;

        #endregion

        #region Start

        private void Start()
        {
            _isOnMove = false;

            _timeBetweenRoaming = 0f;
        }

        #endregion

        #region Update

        void Update()
        {
            if (!_isOnMove)
            {
                StartCoroutine(MoveToWaypoint());
            }
        }

        #endregion

        #region Movement

        private IEnumerator MoveToWaypoint()
        {
            _isOnMove = true;
            Transform randomWayPoint = waypoints[Random.Range(0, waypoints.Length)];
            foreach (var enemy in enemies)
            {
                if (enemy == null)
                    continue;

                if (!enemy.GetComponent<DetectionRange>().IsPlayerDetected &&
                    enemy.GetComponent<MobNavigation>().hasSpawned)
                {
                    Vector3 ecart = new Vector3(Random.Range(-3, 3), 0, Random.Range(-3, 3));
                    Vector3 randomAroundWayPoint = randomWayPoint.position + ecart;
                    enemy.GetComponent<NavMeshAgent>().SetDestination(randomAroundWayPoint);
                    _timeBetweenRoaming = timeToNextRoaming;
                }
                else
                {
                    _timeBetweenRoaming = 0f;
                }
            }

            yield return new WaitForSeconds(_timeBetweenRoaming);

            _isOnMove = false;
        }

        #endregion
    }
}