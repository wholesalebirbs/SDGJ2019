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
        if (playerIsInsideDetectionRadius)
        {
            RaycastHit raycastHit;
            if(Physics.Raycast(this.transform.position, target.position - this.transform.position, out raycastHit))
            {
                Debug.DrawLine(this.transform.position, raycastHit.point, Color.red, 2.0f);
                if(raycastHit.distance <= _playerDetectionRadius && ReferenceEquals(raycastHit.transform.root.gameObject, Player.instance.transform.root.gameObject))
                {
                    isFollowingPlayer = true;
                    agent.stoppingDistance = this.stoppingDistance;
                    agent.SetDestination(target.position);
                }
                else
                {
                    if (isFollowingPlayer && agent.remainingDistance <= agent.stoppingDistance)
                    {
                        isFollowingPlayer = false;
                        StartCoroutine(SetDestinationToStartPositionAfterSeconds(waitTimeAfterLosingSightOfPlayer));
                    }
                }
            }
            else
            {
                if (isFollowingPlayer && agent.remainingDistance <= agent.stoppingDistance)
                {
                    isFollowingPlayer = false;
                    StartCoroutine(SetDestinationToStartPositionAfterSeconds(waitTimeAfterLosingSightOfPlayer));
                }
            }
        }
    }

    IEnumerator SetDestinationToStartPositionAfterSeconds(float time)
    {
        yield return new WaitForSeconds(time);

        if (!isFollowingPlayer)
        {
            agent.stoppingDistance = 0;
            agent.SetDestination(startPostion);
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
            agent.stoppingDistance = 0;
            playerIsInsideDetectionRadius = false;
            agent.SetDestination(startPostion);
        }
    }
}
