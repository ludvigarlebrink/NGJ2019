using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leader : MonoBehaviour
{
    public float Speed = 10.0f;
    public float JumpRadius = 15.0f;
    public float JumpForce = 800.0f;

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
}
