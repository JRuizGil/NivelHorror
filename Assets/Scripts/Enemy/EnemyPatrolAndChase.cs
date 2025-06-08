using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolAndChase : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float visionRange = 10f;
    public float visionAngle = 60f;
    public float chaseSpeed = 5f;
    public float patrolSpeed = 2f;

    private int currentPoint = 0;
    private NavMeshAgent agent;
    private GameObject player;
    private bool chasing = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        agent.speed = patrolSpeed;
        GoToNextPoint();
    }

    void Update()
    {
        if (PlayerInSight())
        {
            chasing = true;
            agent.speed = chaseSpeed;
            agent.SetDestination(player.transform.position);
        }
        else
        {
            if (chasing)
            {
                // Si estaba persiguiendo pero perdió de vista al jugador
                chasing = false;
                agent.speed = patrolSpeed;
                GoToNextPoint();
            }

            if (!agent.pathPending && agent.remainingDistance < 0.5f && !chasing)
            {
                GoToNextPoint();
            }
        }
    }

    void GoToNextPoint()
    {
        if (patrolPoints.Length == 0)
            return;

        agent.destination = patrolPoints[currentPoint].position;
        currentPoint = (currentPoint + 1) % patrolPoints.Length;
    }

    bool PlayerInSight()
    {
        Vector3 dirToPlayer = player.transform.position - transform.position;
        float angle = Vector3.Angle(transform.forward, dirToPlayer);

        if (dirToPlayer.magnitude < visionRange && angle < visionAngle / 2)
        {
            Ray ray = new Ray(transform.position + Vector3.up, dirToPlayer.normalized);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, visionRange))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    return true;
                }
            }
        }

        return false;
    }

    void OnDrawGizmosSelected()
    {
        // Dibuja el ángulo de visión
        Gizmos.color = Color.yellow;
        Vector3 rightBoundary = Quaternion.Euler(0, visionAngle / 2, 0) * transform.forward * visionRange;
        Vector3 leftBoundary = Quaternion.Euler(0, -visionAngle / 2, 0) * transform.forward * visionRange;
        Gizmos.DrawRay(transform.position, rightBoundary);
        Gizmos.DrawRay(transform.position, leftBoundary);
        Gizmos.DrawWireSphere(transform.position, visionRange);
    }
}
