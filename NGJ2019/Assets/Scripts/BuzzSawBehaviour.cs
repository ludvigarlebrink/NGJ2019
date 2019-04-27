using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuzzSawBehaviour : MonoBehaviour
{
    public float rotSpeed = 700.0f;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, -rotSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Egg"))
        {
            Destroy(other.gameObject);
        }
    }
}
