using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemySightComponent : MonoBehaviour
{
    [SerializeField] private float viewDistance;
    [SerializeField] private float viewAngle;
    [SerializeField] private Transform player;
    [SerializeField] private Transform playerLastSeen;
    [SerializeField] private Light spotlight;
    [SerializeField] private LayerMask viewMask;
    [SerializeField] private Transform target;

    [Header("Waypoints")]
    [SerializeField] private GameObject currWaypoint;
    [SerializeField] private GameObject nextWaypoint;

    private Color neutralLight;
    private Color spottedLight;
    [HideInInspector] public bool reachedLastSeen;

    private bool setDecay = false;

    enum EnemyType
    {
        Guard,
        Roomba,
        Tracker,
    }

    [SerializeField] private EnemyType enemyType;

    private bool Guard = false;
    private bool Roomba = false;
    private bool Tracker = false;

    // Start is called before the first frame update
    void Start()
    {
        neutralLight = Color.yellow;
        spottedLight = Color.red;
        reachedLastSeen = true;

        switch (enemyType)
        {
            case EnemyType.Guard:
                Guard = true;
                break;
            case EnemyType.Roomba:
                Roomba = true;
                break;
            case EnemyType.Tracker:
                Tracker = true;
                break;
        }

        StartCoroutine(Detect());
    }

    bool CanSeePlayer()
    {
        if (Vector3.Distance(transform.position, player.position) < viewDistance)
        {
            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            float angleToPlayer = Vector3.Angle(transform.forward, dirToPlayer);
            if (angleToPlayer < viewAngle / 2f)
            {
                if (!Physics.Linecast(transform.position, player.position, viewMask))
                {
                    return true;
                }
            }
        }
        return false;
    }

    bool TooClose()
    {
        if (CanSeePlayer())
        {
            if (Vector3.Distance(transform.position, player.position) < viewDistance - 2f)
            {
                return true;
            }
        }
        return false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (CanSeePlayer())
        {
            spotlight.color = spottedLight;
            Detection.instance.isDecaying = false;
            setDecay = true;
        }
        else
        {
            spotlight.color = neutralLight;
            if (setDecay)
            {
                Detection.instance.isDecaying = true;
                setDecay = false;

                if (Guard)
                {
                    StartCoroutine(TrackOverflow());
                }
            }
        }

        if (Tracker)
        {
            if (TooClose())
            {
                Vector3 retreatDirection = (transform.position - player.position).normalized;
                target.position = transform.position + retreatDirection * 3f;
                gameObject.GetComponent<NavMeshAgent>().speed = 8f;
            }
            else
            {
                target.position = player.position;
                gameObject.GetComponent<NavMeshAgent>().speed = 3.5f;
            }
            return;
        }

        if (Roomba)
        {
            target.position = currWaypoint.transform.position;
            return;
        }

        if (Guard)
        {
            if (CanSeePlayer())
            {
                gameObject.GetComponent<NavMeshAgent>().speed = 8f;
                target.position = player.position;
                playerLastSeen.position = player.position;
                reachedLastSeen = false;
                return;
            }
            else if (!reachedLastSeen)
            {
                target.position = playerLastSeen.position;
                return;
            }
            else
            {
                gameObject.GetComponent<NavMeshAgent>().speed = 3.5f;
                target.position = currWaypoint.transform.position;
                return;
            }
        }
    }

    IEnumerator TrackOverflow()
    {
        for (int i = 0; i < (0.5f / 0.02f); i++)
        {
            playerLastSeen.position = player.position;
            yield return new WaitForSeconds(0.02f);
        }
    }

    IEnumerator Detect()
    {
        if (CanSeePlayer())
        {
            Detection.instance.IncreaseDetectionLevel();
        }
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(Detect());
    }

    public void ChangeWaypoint()
    {
        GameObject tempWaypoint;
        tempWaypoint = currWaypoint;
        currWaypoint = nextWaypoint;
        nextWaypoint = tempWaypoint;
    }
}
