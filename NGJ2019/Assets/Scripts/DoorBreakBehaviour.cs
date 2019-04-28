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
            if (egg.GetComponent<Egg>())
            {
                FindObjectOfType<UIBehaviour>().EggLost(Egg.Type.Black);
                EggArmy army = FindObjectOfType<EggArmy>();
                army.KillEgg(egg.GetComponent<Egg>());
            }
            DoorBreak();
        }
    }

    private void Update()
    {
    }
}
