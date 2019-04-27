using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooterBehaviour : MonoBehaviour
{

    public GameObject projectile;

    public float spawnTime = 1;
    private float curSpawnTime;

    private void Start()
    {
        curSpawnTime = spawnTime;
    }

    private void Update()
    {
        curSpawnTime -= 1 * Time.deltaTime;
        if (curSpawnTime <= 0)
        {
            Spawn();
            curSpawnTime = spawnTime;
        }
    }

    void Spawn()
    {
        float randW = Random.Range(-5f, 5f);
        float randH = Random.Range(-3f, 3f);

        Instantiate(projectile, new Vector3(transform.position.x + randW, transform.position.y + randH, transform.position.z), Quaternion.identity);
    }
}
