using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public ThatAgainBehaviour thatAgain;

    private float time;
    EggArmy eggArmy;
    // Start is called before the first frame update
    void Start()
    {
        time = 40.0f;
        eggArmy = FindObjectOfType<EggArmy>();
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;

        if (time <= 0)
        {
            eggArmy.Restart();
            thatAgain.ActivateCanvas();
            time = 40.0f;
        }
    }

    public float GetElapsedTime()
    {
        return time;
    }
}
