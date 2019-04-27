using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private const float timeInterval = 10000.0f;
    private float time;
    EggArmy eggArmy;
    // Start is called before the first frame update
    void Start()
    {
        time = 0.0f;
        eggArmy = FindObjectOfType<EggArmy>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time >= timeInterval)
        {
            eggArmy.Restart();
            time = 0.0f;
        }
    }

    public float GetElapsedTime()
    {
        return time;
    }
}
