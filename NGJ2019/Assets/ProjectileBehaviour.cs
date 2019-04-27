using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Egg"))
        {
            Destroy(other.gameObject);
        }
    }

    private void Awake()
    {
        float rand = Random.Range(12.0f, 18.0f);
        GetComponent<Rigidbody>().AddForce(transform.TransformDirection(Vector3.back) * rand, ForceMode.Impulse);
    }


}
