using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float walkSpeed;
    [SerializeField] private float rotationSpeed;

    private Transform orientation;

    private float forwardInput;
    private float rightInput;

    private CapsuleCollider cachedCollider;
    private Rigidbody cachedRigidbody;

    private Vector2 turn;

    void Start()
    {
        cachedRigidbody = GetComponent<Rigidbody>();
        cachedCollider = GetComponent<CapsuleCollider>();
        if (cachedRigidbody == null || cachedCollider == null)
            Destroy(this);
    }

    void Update()
    {
        forwardInput = Input.GetAxis("Vertical");
        rightInput = Input.GetAxis("Horizontal");

        //Vector3 MoveVector = transform.TransformDirection(rightInput, 0, forwardInput) * walkSpeed;
        //cachedRigidbody.velocity = new Vector3(MoveVector.x, cachedRigidbody.velocity.y, MoveVector.z);

        //turn.x += Input.GetAxis("Mouse X");
        //transform.localRotation = Quaternion.Euler(0, turn.x, 0);
    }

    private void FixedUpdate()
    {
        Vector3 MoveVector = transform.TransformDirection(rightInput, 0, forwardInput) * walkSpeed;
        cachedRigidbody.velocity = new Vector3(MoveVector.x, cachedRigidbody.velocity.y, MoveVector.z);

        turn.x += Input.GetAxis("Mouse X");
        transform.localRotation = Quaternion.Euler(0, turn.x, 0);
    }
}
