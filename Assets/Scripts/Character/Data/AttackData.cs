using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackData : ScriptableObject
{
    [Range(10, 1000)] [Tooltip("Quick attack damage")]
    public int DamageQuickAttaque = 50;

    [Range(10, 1000)]
    [Tooltip("Long attack damage")]
    public int DamageLongAttaque = 50;
}
