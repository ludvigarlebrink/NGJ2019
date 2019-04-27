using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorExplosion : MonoBehaviour
{
    [SerializeField]
    private GameObject piece1;
    [SerializeField]
    private GameObject piece2;
    [SerializeField]
    private GameObject piece3;
    [SerializeField]
    private GameObject piece4;
    [SerializeField]
    private GameObject piece5;

    void Awake()
    {
        piece1.GetComponent<Rigidbody>().AddForce(-piece1.transform.TransformDirection(Vector3.up) * 30, ForceMode.Impulse);                        
        piece2.GetComponent<Rigidbody>().AddForce(-piece2.transform.TransformDirection(Vector3.up) * 40, ForceMode.Impulse);                        
        piece3.GetComponent<Rigidbody>().AddForce(-piece3.transform.TransformDirection(Vector3.up) * 50, ForceMode.Impulse);                        
        piece4.GetComponent<Rigidbody>().AddForce(-piece4.transform.TransformDirection(Vector3.up) * 40, ForceMode.Impulse);
        piece5.GetComponent<Rigidbody>().AddForce(-piece5.transform.TransformDirection(Vector3.up) * 20, ForceMode.Impulse);
    }                                             
}
