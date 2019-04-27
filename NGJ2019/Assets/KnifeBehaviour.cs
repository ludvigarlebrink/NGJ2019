using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeBehaviour : MonoBehaviour
{
    public GameObject point1;
    public GameObject point2;
    public GameObject knife;
    public float speed = 20.0f;

    bool change = true;
    bool start = false;

    private float curSpawnTime;

    private void Start()
    {
        float rand = Random.Range(0.1f, 2.0f);
        curSpawnTime = rand;
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            if (change)
            {
                knife.transform.position = Vector3.MoveTowards(knife.transform.position, point1.transform.position, Time.deltaTime * speed);
                if (knife.transform.position == point1.transform.position)
                    change = !change;
            }
            else
            {
                knife.transform.position = Vector3.MoveTowards(knife.transform.position, point2.transform.position, Time.deltaTime * speed);
                if (knife.transform.position == point2.transform.position)
                    change = !change;
            }
        }
        else
        {
            curSpawnTime -= 1 * Time.deltaTime;
            if (curSpawnTime <= 0)
            {
                start = true;
            }
        }
    }
}
