using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggArmy : MonoBehaviour
{
    public GameObject EggPrefab;
    public Egg[,] Eggs;
    private const int maxSize = 16;
    public float Density = 1;

    // Start is called before the first frame update
    void Start()
    {
        Eggs = new Egg[maxSize, maxSize];
        for (int w = 0; w < maxSize; ++w)
        {
            for (int l = 0; l < maxSize; ++l)
            {
                Eggs[w, l] = Instantiate(EggPrefab, new Vector3(w * Density, 0, l * Density), Quaternion.identity, transform).GetComponent<Egg>();
            }
        }
        ConnectJoints();
    }

    void ConnectJoints()
    {
        for (int w = 0; w < maxSize; ++w)
        {
            for (int l = 0; l < maxSize; ++l)
            {
                if (Eggs[w, l])
                {
                    for (int w0 = w + 1; w0 < maxSize; ++w0)
                    {
                        if (Eggs[w0, l])
                        {
                            SpringJoint springJoint = Eggs[w, l].gameObject.AddComponent<SpringJoint>();
                            springJoint.connectedBody = Eggs[w0, l].GetComponent<Rigidbody>();
                            springJoint.damper = (Eggs[w, l].Damper + Eggs[w0, l].Damper) / 2.0f;
                            springJoint.spring = (Eggs[w, l].Spring + Eggs[w0, l].Spring) / 2.0f;
                            springJoint.enableCollision = true;
                            springJoint.maxDistance = 2.0f;
                            break;
                        }
                    }

                    for (int l0 = l + 1; l0 < maxSize; ++l0)
                    {
                        if (Eggs[w, l0])
                        {
                            SpringJoint springJoint = Eggs[w, l].gameObject.AddComponent<SpringJoint>();
                            springJoint.connectedBody = Eggs[w, l0].GetComponent<Rigidbody>();
                            springJoint.damper = (Eggs[w, l].Damper + Eggs[w, l0].Damper) / 2.0f;
                            springJoint.spring = (Eggs[w, l].Spring + Eggs[w, l0].Spring) / 2.0f;
                            springJoint.enableCollision = true;
                            springJoint.maxDistance = 2.0f;
                            break;
                        }
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
