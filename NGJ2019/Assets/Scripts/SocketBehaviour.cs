using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocketBehaviour : MonoBehaviour
{

    [SerializeField]
    private int requiredNumber = 4;
    [SerializeField]
    private ElectricDoorBehaviour electricDoor;
    [SerializeField]
    private ParticleSystem particleOnCollision;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Egg>() && other.GetComponent<Egg>().type == Egg.Type.Blue)
        {
            if (requiredNumber > 0)
            {
                requiredNumber--;
                if (particleOnCollision)
                {
                    Instantiate(particleOnCollision, transform.position, Quaternion.identity);
                }
                //particleOnCollision.Play();

                FindObjectOfType<UIBehaviour>().EggLost(Egg.Type.Blue);
                EggArmy army = FindObjectOfType<EggArmy>();
                army.KillEgg(other.GetComponent<Egg>());
                GetComponent<AudioSource>().Play();
            }

            if (requiredNumber <= 0)
                electricDoor.OpenDoor();
        }
    }
}
