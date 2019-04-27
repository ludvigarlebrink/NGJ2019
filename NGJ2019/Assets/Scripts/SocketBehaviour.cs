using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocketBehaviour : MonoBehaviour
{

    [SerializeField]
    private int requiredNumber = 4;
    [SerializeField]
    private ElectricDoorBehaviour electricDoor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("fast"))
        {
            if (requiredNumber >0)
            {
                requiredNumber--;
                Destroy(other.gameObject);
            }

            if (requiredNumber <= 0)
                electricDoor.OpenDoor();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            requiredNumber--;
            if (requiredNumber <= 0)
            {
                electricDoor.OpenDoor();
            }
        }
    }

}
