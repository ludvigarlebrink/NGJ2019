using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuzzSawBehaviour : MonoBehaviour
{
    public float rotSpeed = 700.0f;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, -rotSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Egg>())
        {
            EggArmy army = FindObjectOfType<EggArmy>();
            FindObjectOfType<UIBehaviour>().EggLost(other.GetComponent<Egg>().type);
            army.KillEgg(other.GetComponent<Egg>());
        }
    }
}
