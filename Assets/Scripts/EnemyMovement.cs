using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform[] waypoints;
    public float detectRange = 10f;
    public float attackRange = 2f;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 5f;
    public float rotationSpeed = 10f;
    public Material normalMaterial;
    public Material detectedMaterial;
    public AudioClip moveSound;

    private int currentWaypoint = 0;
    private NavMeshAgent _agent;
    private GameObject _player;
    private Renderer _renderer;
    private AudioSource _audioSource;
    private bool isChasing = false;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _renderer = GetComponent<Renderer>();
        _audioSource = GetComponent<AudioSource>();
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
            Patrol();
        }
    }

    bool CanSeePlayer()
    {
        bool canSee = false;
        Vector3 direction = _player.transform.position - transform.position;
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
        _agent.speed = chaseSpeed;
        _agent.SetDestination(_player.transform.position);
        if (Vector3.Distance(transform.position, _player.transform.position) <= attackRange)
        {
            _agent.speed = 0;
        }
        transform.rotation = Quaternion.LookRotation(_agent.velocity.normalized);
        PlayMoveSound();
    }

    void Patrol()
    {
        _agent.speed = patrolSpeed;
        if (_agent.remainingDistance < _agent.stoppingDistance)
        {
            GoToNextWaypoint();
        }
        transform.rotation = Quaternion.LookRotation(_agent.velocity.normalized);
        PlayMoveSound();
    }

    void GoToNextWaypoint()
    {
        _agent.SetDestination(waypoints[currentWaypoint].position);
        currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        PlayMoveSound();
    }

    void PlayMoveSound()
    {
        if (_audioSource.isPlaying) return;
        _audioSource.clip = moveSound;
        _audioSource.loop = true;
        _audioSource.Play();
    }
}
