using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitComponent : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject winText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
        {
            winText.SetActive(true);
        }

    }
}
