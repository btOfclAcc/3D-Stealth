using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerLastSeenComponent : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<EnemySightComponent>() != null)
        other.gameObject.GetComponent<EnemySightComponent>().reachedLastSeen = true;
    }
}
