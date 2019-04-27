using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    public float Speed;

    private Transform destination;

    private Animator animator;
    private Rigidbody rb;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    public void AssignDestinationTransform(Transform _destination)
    {
        destination = _destination;
    }

    void FixedUpdate()
    {
        animator.SetFloat("Speed", new Vector2(rb.velocity.x, rb.velocity.z).sqrMagnitude);
        if (destination)
        {
            Vector3 force = (destination.position - transform.position).normalized * Speed;
            rb.AddForce(new Vector3(force.x, 0.0f, force.z), ForceMode.Force);
        }
    }
}
