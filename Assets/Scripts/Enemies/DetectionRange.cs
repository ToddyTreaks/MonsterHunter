using UnityEngine;

namespace Enemies
{
    public class DetectionRange : MonoBehaviour
    {
        #region Variables
    
        [SerializeField] private float aggroRange;
        [SerializeField] internal float attackRange;
        [SerializeField] private float lostAggroRange;
        [SerializeField] private LayerMask playerLayer;
    
        internal bool IsPlayerDetected;
        internal bool IsPlayerInCloseAttackRange;

        #endregion

        #region Start
                
        void Start()
        {
            IsPlayerDetected = false;
            IsPlayerInCloseAttackRange = false;
        }
        
        
        #endregion
    
        #region Update

        void Update()
        {
            if (Physics.CheckSphere(transform.position, aggroRange, playerLayer))
            {
                if (!IsPlayerDetected)
                {
                    IsPlayerDetected = true;
                }
            }
        
            if (Physics.CheckSphere(transform.position, attackRange, playerLayer))
            {
                if (!IsPlayerInCloseAttackRange)
                {
                    IsPlayerInCloseAttackRange = true;
                }
            }
        
            if (!Physics.CheckSphere(transform.position, attackRange, playerLayer))
            {
                if (IsPlayerInCloseAttackRange)
                {
                    IsPlayerInCloseAttackRange = false;
                }
            
            }
        
            if (!Physics.CheckSphere(transform.position, lostAggroRange, playerLayer))
            {
                if (IsPlayerDetected)
                {
                    IsPlayerDetected = false;
                }
            }
        }
    
        #endregion
    }
}
