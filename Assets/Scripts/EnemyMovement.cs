using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform[] waypoints;
    public float detectRange = 10f;
    public float attackRange = 2f;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 5f;
    public float rotationSpeed = 5f;
    public Material normalMaterial;
    public Material detectedMaterial;

    private int currentWaypoint = 0;
    //private NavMeshAgent _agent;
    private Transform _player;
    private Renderer _renderer;
    private bool isChasing = false;

    void Start()
    {
        //_agent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _renderer = GetComponent<Renderer>();
        GoToNextWaypoint();
    }

    void Update()
    {
        if (CanSeePlayer())
        {
            _renderer.material = detectedMaterial;
            isChasing = true;
            ChasePlayer();

        }
        else if (isChasing)
        {
            isChasing = false;
            GoToNextWaypoint();
        }
        else
        {
            _renderer.material = normalMaterial;
            //Patrol();
        }
    }

    bool CanSeePlayer()
    {
        bool canSee = false;
        Vector3 direction = _player.position - transform.position;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, detectRange))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                canSee = true;
            }
        }
        return canSee;
    }

    void ChasePlayer()
    {
        //_agent.speed = chaseSpeed;
        //_agent.SetDestination(_player.position);
        if (Vector3.Distance(transform.position, _player.position) <= attackRange)
        {
            AttackPlayer();
        }
    }

    void AttackPlayer()
    {
        // Insert attack code here
    }

    //void Patrol()
    //{
    //    _agent.speed = patrolSpeed;
    //    if (_agent.remainingDistance < _agent.stoppingDistance)
    //    {
    //        GoToNextWaypoint();
    //    }
    //}

    void GoToNextWaypoint()
    {
        //_agent.SetDestination(waypoints[currentWaypoint].position);
        currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
