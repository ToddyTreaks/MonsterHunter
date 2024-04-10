using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Boss_Attack_Phase : MonoBehaviour
{
    private Boss_Navigation _bossNavigation;
    private DetectionRange _detectionRange;
    private Animator _animator;

    private float _attackCooldown = 2f;
    private float _attackCooldownTimer = 0f;

    private float _onGoingSpeedModifier = 1f;
    private float _savedAgentSpeed;
    private bool _isAgentStopped = false;
    
    void Start()
    {
        _bossNavigation = GetComponent<Boss_Navigation>(); 
        _detectionRange = GetComponent<DetectionRange>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        
        if (_bossNavigation.IsFighting && _attackCooldownTimer <= 0)
        {
            Debug.Log("AttackCoroutine Launched");
            StartCoroutine(NextAttack());
            _attackCooldownTimer = 10;
        }
        else if (_bossNavigation.IsFighting)
        {
            _attackCooldownTimer -= Time.deltaTime;
        }
        else if (!_bossNavigation.IsFighting)
        {
            StopCoroutine(NextAttack());
            _animator.SetBool("isAttacking", false);
            _bossNavigation._agent.speed = _bossNavigation.Speed;
        }
    }
    
    private IEnumerator NextAttack()
    {
        _animator.SetBool("isAttacking", false);
        ResetSpeedModification();
        yield return new WaitForSeconds(Random.Range(1, 5));
        _animator.SetBool("isAttacking", true);
        AttackChoice();
        yield return null; 
    }

    #region AttackChoice
    
    private void AttackChoice()
    {
        Debug.Log("AttackChoice");
        if (_detectionRange.isPlayerInCloseAttackRange)
            CorpACorpChoice();
        else
            DistanceChoice();
    }
    
    private void CorpACorpChoice()
    {
        int choice = Random.Range(0, 2);
        if (choice < 1) 
            MeleeAttack_Kick();
        else                
            MeleeAttack_Punch();
    }
    
    private void DistanceChoice()
    {
        int choice = Random.Range(0, 10);
        if (choice < 1)
            InvocationChoice();
        else 
            SpellcastChoice();
            
    }
    
    private void InvocationChoice()
    {
        int choiceSummonType = Random.Range(0, 10);
        if (choiceSummonType < 8)
            Spellcast_Summon();
        else
            Spellcast_Raise();
    }
    
    private void SpellcastChoice()
    {
        int choiceAttackType = Random.Range(0, 10);
        if (choiceAttackType < 1)
            Spellcast_Shoot();
        else if (choiceAttackType < 10)
            Spellcast_ContinuousShoot();
        else
            Spellcast_HeavyShoot();
    }
    
    #endregion
    
    #region MeleeAttack
    
    // Kick
    private void MeleeAttack_Kick()
    {
        SpeedModification(0.3f);
        SetAttackCooldown(0.5f);
        _animator.SetFloat("Attack", 2);
        
    }
    // Punch
    private void MeleeAttack_Punch()
    {
        SpeedModification(0.3f);
        SetAttackCooldown(0.3f);
        _animator.SetFloat("Attack", 3);
        
    }
    
    #endregion
    
    #region Invocation
    
    //  Invock few mobs
    private void Spellcast_Raise()
    {
        StopAgent();
        SetAttackCooldown(2f);
        _animator.SetFloat("Attack", 0);
        _animator.SetFloat("Invocation", 0);
    }
    // Invock a swarm of mobs
    private void Spellcast_Summon()
    {
        StopAgent();
        SetAttackCooldown(3f);
        _animator.SetFloat("Attack", 0);
        _animator.SetFloat("Invocation", 1);
    }
    
    #endregion
    
    #region Spellcast
    private void Spellcast_Shoot()
    {
        StopAgent();
        SetAttackCooldown(1f);
        _animator.SetFloat("Attack", 1);
        _animator.SetFloat("Shoot", 0);
    }
    private void Spellcast_ContinuousShoot()
    {
        StopAgent();
        SetAttackCooldown(1f);
        _animator.SetFloat("Attack", 1);
        _animator.SetFloat("Shoot", 1);
    }
    private void Spellcast_HeavyShoot()
    {
        StopAgent();
        SetAttackCooldown(3f);
        _animator.SetFloat("Attack", 1);
        _animator.SetFloat("Shoot", 2);
    }
    
    #endregion
    
    #region SpeedModification 
    
    private void SpeedModification(float value)
    {
        
        ResetSpeedModification();
        _onGoingSpeedModifier = value;
        _bossNavigation._agent.velocity *=  _onGoingSpeedModifier;
        Debug.Log("Speed Modified");
    }

    private void ResetSpeedModification()
    {
        if (_isAgentStopped)
            ResumeAgent();
        _bossNavigation._agent.velocity /=  _onGoingSpeedModifier;
    }

    private void StopAgent()
    {
        _isAgentStopped = true;
        _savedAgentSpeed = _bossNavigation._agent.speed;
        _bossNavigation._agent.speed = 0.001f;
    }
    
    private void ResumeAgent()
    {
        _bossNavigation._agent.speed = _savedAgentSpeed;
        _isAgentStopped = false;
        Debug.Log("Agent Resumed");
    }

    #endregion
    
    #region AttackCooldown
    
    private void SetAttackCooldown(float value)
    {
        _attackCooldown = value;
        _attackCooldownTimer = _attackCooldown;
    }
    
    #endregion
    
}
