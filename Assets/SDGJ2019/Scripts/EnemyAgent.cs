using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAgent : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform target;

    [Range(0, 10)]
    public float stoppingDistance;

    [SerializeField, Range(1,10)]
    private float _playerDetectionRadius = 5;
    public float playerDetectionRadius { get { return _playerDetectionRadius; } set { SetPlayerDetectionRadius(value); } }

    private SphereCollider playerDetectionCollider;

    private Vector3 startPostion;

    private bool isFollowingPlayer = false;

    private void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();

        target = Player.instance.transform;

        startPostion = this.transform.position;
        CreatePlayerDetectionCollider();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isFollowingPlayer)
        {
            agent.SetDestination(target.position);
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
            agent.stoppingDistance = this.stoppingDistance;
            isFollowingPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (ReferenceEquals(other.transform.root.gameObject, Player.instance.transform.root.gameObject))
        {
            //Player exited trigger
            agent.stoppingDistance = 0;
            isFollowingPlayer = false;
            agent.SetDestination(startPostion);
        }
    }
}
