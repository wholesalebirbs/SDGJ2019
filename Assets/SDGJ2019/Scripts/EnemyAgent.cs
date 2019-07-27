﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAgent : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform target;

    [Range(0, 10)]
    public float stoppingDistance = 1.25f;
    [Range(0, 10)]
    public float waitTimeAfterLosingSightOfPlayer = 2.5f;

    [SerializeField, Range(1,10)]
    private float _playerDetectionRadius = 5;
    public float playerDetectionRadius { get { return _playerDetectionRadius; } set { SetPlayerDetectionRadius(value); } }

    private SphereCollider playerDetectionCollider;

    private Vector3 startPostion;

    private bool playerIsInsideDetectionRadius = false;
    private bool isFollowingPlayer = false;

    public Path path;
    private Transform currentPathNode;

    [Range(0,10)]
    public float stunDuration = 1.5f;
    private bool isStunned;

    [Range(1,1000)]
    public float damageDealt = 35;

    [Range(0.1f, 5)]
    public float attackDelay = 0.5f;

    private bool isAttacking;

    private void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 0.25f;

        target = Player.instance.transform;

        startPostion = this.transform.position;
        CreatePlayerDetectionCollider();
        if(path != null)
        {
            currentPathNode = path.GetNextPathNode();
            agent.SetDestination(currentPathNode.position);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isStunned || isAttacking)
            return;
        if (playerIsInsideDetectionRadius)
        {
            RaycastHit raycastHit;
            if(Physics.Raycast(this.transform.position, target.position - this.transform.position, out raycastHit))
            {
                if(raycastHit.distance <= _playerDetectionRadius && ReferenceEquals(raycastHit.transform.root.gameObject, Player.instance.transform.root.gameObject))
                {
                    isFollowingPlayer = true;
                    agent.stoppingDistance = this.stoppingDistance;
                    agent.SetDestination(target.position);

                    if (!isAttacking && agent.remainingDistance <= agent.stoppingDistance)
                    {
                        isAttacking = true;
                        StartCoroutine(BeginAttack());
                    }
                }
                else
                {
                    if (isFollowingPlayer && agent.remainingDistance <= agent.stoppingDistance)
                    {
                        isFollowingPlayer = false;
                        StartCoroutine(ReturnToPositionAfterSeconds(waitTimeAfterLosingSightOfPlayer));
                    }
                }
            }
            else
            {
                if (isFollowingPlayer && agent.remainingDistance <= agent.stoppingDistance)
                {
                    isFollowingPlayer = false;
                    StartCoroutine(ReturnToPositionAfterSeconds(waitTimeAfterLosingSightOfPlayer));
                }
            }
        }

        if (path != null && !isFollowingPlayer)
        {
            if(agent.remainingDistance <= agent.stoppingDistance)
            {
                currentPathNode = path.GetNextPathNode();
                agent.SetDestination(currentPathNode.position);
            }
        }
    }

    IEnumerator BeginAttack()
    {
        yield return new WaitForSeconds(attackDelay);

        Attack();
    }

    private void Attack()
    {
        for (int i = 0; i < 6; i++)
        {
            Debug.DrawLine(this.transform.position + this.transform.forward * 0.25f + Vector3.up * 0.5f, this.transform.position + Quaternion.Euler(0, ((90f / 6f) * i) - 45, 0) * this.transform.forward + Vector3.up * 0.5f, Color.red, 2);
            RaycastHit raycastHit;
            if (Physics.Raycast(this.transform.position + this.transform.forward * 0.25f + Vector3.up * 0.5f, Quaternion.Euler(0, ((90f / 6f) * i) - 45, 0) * this.transform.forward, out raycastHit, 1.5f))
            {
                Player player = raycastHit.transform.root.gameObject.GetComponentInChildren<Player>();
                if (player != null)
                {
                    player.ApplyDamage(damageDealt);
                    break;
                }
            }
        }

        isAttacking = false;
    }

    IEnumerator ReturnToPositionAfterSeconds(float time)
    {
        yield return new WaitForSeconds(time);

        if (!isFollowingPlayer)
        {
            agent.stoppingDistance = 0.25f;
            if (path != null)
            {
                agent.SetDestination(path.GetClosestPathNode(this.transform.position).position);
            }
            else
            {
                agent.SetDestination(startPostion);
            }
        }
    }

    private void SetPlayerDetectionRadius(float radius)
    {
        radius = Mathf.Clamp(radius, 0, 10);

        _playerDetectionRadius = radius;

        CreatePlayerDetectionCollider();
    }

    private void CreatePlayerDetectionCollider()
    {
        if (playerDetectionCollider == null)
        {
            playerDetectionCollider = this.gameObject.AddComponent<SphereCollider>();
            playerDetectionCollider.isTrigger = true;
        }

        playerDetectionCollider.radius = _playerDetectionRadius;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(ReferenceEquals(other.transform.root.gameObject, Player.instance.transform.root.gameObject))
        {
            //Player detected
            playerIsInsideDetectionRadius = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (ReferenceEquals(other.transform.root.gameObject, Player.instance.transform.root.gameObject))
        {
            //Player exited trigger
            playerIsInsideDetectionRadius = false;
            isFollowingPlayer = false;
            StartCoroutine(ReturnToPositionAfterSeconds(0));
        }
    }

    public void Stun()
    {
        if (this.isStunned)
            return;
        Debug.Log("I'be been stunned!");
        this.isStunned = true;
        agent.isStopped = true;
        StartCoroutine(RemoveStunEffectAfterTime(stunDuration));
    }

    IEnumerator RemoveStunEffectAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        this.isStunned = false;
        agent.isStopped = false;
    }
}
