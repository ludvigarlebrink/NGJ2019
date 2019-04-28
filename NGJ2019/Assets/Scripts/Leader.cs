using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leader : MonoBehaviour
{
    public float Speed = 200.0f;

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
        Vector3 vec = transform.forward * Input.GetAxis("Vertical");
        vec += transform.right * Input.GetAxis("Horizontal");
        vec = vec.normalized * Speed;
        rb.AddForce(vec, ForceMode.VelocityChange);
    }

    private void OnTriggerStay(Collider collision)
    {
        Egg collidedEgg = collision.gameObject.GetComponent<Egg>();
        if (collidedEgg && collidedEgg.specialityActivated && collidedEgg.type == Egg.Type.Green && collidedEgg.animationFinished)
        {
            // TODO change the movement of the egg
            float dist = Vector3.Distance(transform.position, collidedEgg.transform.position) / collidedEgg.GetComponent<CapsuleCollider>().radius;
            if (dist < 1.0f)
            {
                dist = 1 - dist;
                rb.AddForce(new Vector3(0.0f, Mathf.SmoothStep(0.0f, 20000.0f, dist), 0.0f));
            }
        }
    }
}
