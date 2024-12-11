using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCatchComponent : MonoBehaviour
{
    [SerializeField] private Transform player;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform == player)
        {
            Detection.instance.Caught();
        }
    }
}
