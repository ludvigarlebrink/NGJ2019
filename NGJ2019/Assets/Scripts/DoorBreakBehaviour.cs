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

    private void OnCollisionEnter(Collision collision)
    {
        Egg egg = collision.gameObject.GetComponent<Egg>();
        if (egg && egg.type == Egg.Type.Black && egg.specialityActivated)
        {
            Destroy(egg.gameObject);
            DoorBreak();
        }
    }

    private void Update()
    {
    }
}
