using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlastisBehaviour : MonoBehaviour
{
    public float rotSpeed = 60.0f;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, rotSpeed * Time.deltaTime);
    }
}

