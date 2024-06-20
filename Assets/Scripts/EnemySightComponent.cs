using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySightComponent : MonoBehaviour
{
    [SerializeField] private float viewDistance;
    [SerializeField] private float viewAngle;
    [SerializeField] private Transform player;
    [SerializeField] private Transform playerLastSeen;
    [SerializeField] private Light spotlight;
    [SerializeField] private LayerMask viewMask;
    [SerializeField] private Transform target;

    [SerializeField] private GameObject currWaypoint;
    [SerializeField] private GameObject nextWaypoint;

    private GameObject tempWaypoint;
    private Color neutralLight;
    private Color spottedLight;
    public bool reachedLastSeen;

    // Start is called before the first frame update
    void Start()
    {
        neutralLight = Color.yellow;
        spottedLight = Color.red;
        reachedLastSeen = true;
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

    // Update is called once per frame
    void Update()
    {
        if (CanSeePlayer())
        {
            spotlight.color = spottedLight;
            target.position = player.position;
            playerLastSeen.position = player.position;
            reachedLastSeen = false;
        }
        else if (!reachedLastSeen)
        {
            return;
        }
        else
        {
            spotlight.color = neutralLight;
            target.position = currWaypoint.transform.position;
        }
    }

    public void changeWaypoint()
    {
        tempWaypoint = currWaypoint;
        currWaypoint = nextWaypoint;
        nextWaypoint = tempWaypoint;
    }
}
