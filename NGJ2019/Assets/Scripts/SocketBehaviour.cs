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
        if (other.gameObject.name.Contains("fast"))
        {
            if (requiredNumber > 0)
            {
                requiredNumber--;
                if (particleOnCollision)
                {
                    Instantiate(particleOnCollision, transform.position, Quaternion.identity);
                }
                //particleOnCollision.Play();
                Destroy(other.gameObject);
                FindObjectOfType<UIBehaviour>().EggLost(Egg.Type.Blue);
            }

            if (requiredNumber <= 0)
                electricDoor.OpenDoor();
        }
    }
}
