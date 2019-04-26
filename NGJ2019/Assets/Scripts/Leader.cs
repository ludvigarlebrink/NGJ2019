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
        rb.AddForce(0.0f, 0.0f, Speed * Input.GetAxis("Vertical"));
        rb.AddForce(Speed * Input.GetAxis("Horizontal"), 0.0f, 0.0f);

        RaycastHit hit;
        LayerMask mask = LayerMask.GetMask("Ground");
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.4f, mask))
        {
            if (Input.GetButtonDown("Jump"))
            {
                rb.AddExplosionForce(JumpForce, transform.position, JumpRadius);
            }
        }

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
