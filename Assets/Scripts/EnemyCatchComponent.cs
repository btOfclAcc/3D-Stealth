using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCatchComponent : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject loseText;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform == player)
        {
            loseText.SetActive(true);
        }
    }
}
