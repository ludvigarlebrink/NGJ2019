using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricDoorBehaviour : MonoBehaviour
{

    public GameObject doorTarget;

    public float speed = 2.0f;
    private bool opening = false;

    public void OpenDoor()
    {
        opening = true;
    }

    private void Update()
    {
        if (opening)
        {
            transform.position = Vector3.MoveTowards(transform.position, doorTarget.transform.position, Time.deltaTime * speed);
            if (transform.position == doorTarget.transform.position)
            {
                opening = false;
            }
        }
    }
}
