using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    private float curSpawnTime = 5.0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Egg>())
        {
            EggArmy army = FindObjectOfType<EggArmy>();
            army.KillEgg(other.GetComponent<Egg>());
        }
    }

    private void Awake()
    {
        float rand = Random.Range(12.0f, 18.0f);
        GetComponent<Rigidbody>().AddForce(transform.TransformDirection(Vector3.back) * rand, ForceMode.Impulse);
    }

    void Update()
    {
        curSpawnTime -= 1 * Time.deltaTime;
        if (curSpawnTime <= 0)
        {
            Destroy(gameObject);
        }
    }


}
