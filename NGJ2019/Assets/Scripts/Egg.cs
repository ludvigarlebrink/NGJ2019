using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    public int GridPosX = 0;
    public int GridPosY = 0;
    public SpringJoint[] Joints = new SpringJoint[4];
    public Egg[] Eggs = new Egg[4];

    public float Damper = 2.0f;
    public float Spring = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
