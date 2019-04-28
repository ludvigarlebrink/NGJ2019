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
        GetComponent<AudioSource>().Play();
        StartCoroutine(DestroyAfterTimeout());       
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

    IEnumerator DestroyAfterTimeout()
    {
        //gameObject.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
