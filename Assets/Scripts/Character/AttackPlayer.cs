using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : MonoBehaviour
{

    [SerializeField] private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<enemieHealthbdf>(out var enemie) && _animator.GetCurrentAnimatorStateInfo(1).IsName("Attack"))
        {
            enemie.ApplyDamage(100);
        }
        
    }
}
