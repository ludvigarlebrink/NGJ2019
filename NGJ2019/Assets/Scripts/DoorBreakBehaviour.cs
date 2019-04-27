using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBreakBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject doorPieces;

    public void DoorBreak()
    {
        Instantiate(doorPieces, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            DoorBreak();
        }
    }
}
