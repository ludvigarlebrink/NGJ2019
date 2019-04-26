using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    public int GridPosX = 0;
    public int GridPosY = 0;
    public SpringJoint[] Joints = new SpringJoint[4];
    public Egg[] Eggs = new Egg[4];

    public float Damper = 2.0f;
    public float Spring = 10.0f;

    private Animator animator;
    private Rigidbody rb;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        animator.SetFloat("Speed", new Vector2(rb.velocity.x, rb.velocity.z).sqrMagnitude);
    }
}
