using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leader : MonoBehaviour
{
    public float Speed = 10.0f;

    Rigidbody rb = null;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
            rb.AddForce(0.0f, 0.0f, Speed * Input.GetAxis("Vertical"));
            rb.AddForce(Speed * Input.GetAxis("Horizontal"), 0.0f, 0.0f);
    }
}
