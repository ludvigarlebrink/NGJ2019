using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leader : MonoBehaviour
{
    public float Speed = 10.0f;

    public float JumpRadius = 15.0f;
    public float JumpForce = 800.0f;

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
        rb.AddForce(new Vector3(Input.GetAxis("Horizontal"), 0.0f,  Input.GetAxis("Vertical")).normalized * Speed);
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
