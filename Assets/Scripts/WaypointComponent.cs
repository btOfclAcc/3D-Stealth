using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointComponent : MonoBehaviour
{
    [SerializeField] private GameObject guard;
    [SerializeField] private EnemySightComponent guardSight;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == guard)
        {
            guardSight.ChangeWaypoint();
        }
    }
}
