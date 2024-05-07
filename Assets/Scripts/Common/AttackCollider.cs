using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    [SerializeField] private float _damage = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<HealthSystem>(out var healthSystem))
        {
            healthSystem.Damage(_damage);
        }
    }
}