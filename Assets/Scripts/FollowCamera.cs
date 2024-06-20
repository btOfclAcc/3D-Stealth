using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{

    [SerializeField] private Transform leftCameraTarget;
    [SerializeField] private Transform rightCameraTarget;
    [SerializeField] private float positionSpeed;
    [SerializeField] private float rotationSpeed;

    private Transform cameraTarget;
    private Transform altCameraTarget;
    private Transform tempTarget;

    private void Start()
    {
        cameraTarget = rightCameraTarget;
        altCameraTarget = leftCameraTarget;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, cameraTarget.position, positionSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, cameraTarget.rotation, rotationSpeed);

        if (Input.GetKeyDown(KeyCode.C))
        {
            tempTarget = cameraTarget;
            cameraTarget = altCameraTarget;
            altCameraTarget = tempTarget;
        }
    }
}
