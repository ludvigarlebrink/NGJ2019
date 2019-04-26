using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leader : MonoBehaviour
{
    public float Speed = 0.125f;
    public float SpeedModifier = 1.0f;

    Rigidbody rb = null;

    // Camera parameters
    public float cameraSmoothSpeed;
    public Vector3 cameraOffset;
    private Camera MainCamera;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        MainCamera = Camera.main;
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        rb.AddForce(0.0f, 0.0f, Speed * Input.GetAxis("Vertical") * SpeedModifier);
        rb.AddForce(Speed * Input.GetAxis("Horizontal") * SpeedModifier, 0.0f, 0.0f);
    }

    private void LateUpdate()
    {
        CameraFollow();
    }

    private void CameraFollow()
    {
        Vector3 desiredPosition = transform.position + cameraOffset;
        Vector3 smoothedPosition = Vector3.Lerp(MainCamera.transform.position, desiredPosition, cameraSmoothSpeed * Time.deltaTime * 60);

        MainCamera.transform.position = smoothedPosition;
        MainCamera.transform.LookAt(transform);
    }
}
