using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggArmy : MonoBehaviour
{
    public float JumpForce = 50.0f;
    public GameObject SpeederEgg;
    public GameObject LeaderEgg;
    public Egg[,] Eggs;
    private const int maxSize = 5;
    public float Density = 1;

    public enum ConnectionType
    {
        Grid,
        XGrid,
        Snake,
        Dense
    }

    // Start is called before the first frame update
    void Start()
    {
        Eggs = new Egg[maxSize, maxSize];
        for (int w = 0; w < maxSize; ++w)
        {
            for (int l = 0; l < maxSize; ++l)
            {
                if (l == Mathf.Floor(maxSize / 2) && w == Mathf.Floor(maxSize / 2))
                {
                    Eggs[w, l] = Instantiate(LeaderEgg, new Vector3(w * Density, 0, l * Density), Quaternion.identity, transform).GetComponent<Egg>();
                }
                else
                {
                    Eggs[w, l] = Instantiate(SpeederEgg, new Vector3(w * Density, 0, l * Density), Quaternion.identity, transform).GetComponent<Egg>();
                }
            }
        }
        ConnectJoints(ConnectionType.Grid);
    }

    void ConnectJoints(ConnectionType type)
    {
        for (int w = 0; w < maxSize; ++w)
        {
            for (int l = 0; l < maxSize; ++l)
            {
                if (Eggs[w, l])
                {
                    switch (type)
                    {
                        case ConnectionType.Grid:
                            for (int w0 = w + 1; w0 < maxSize; ++w0)
                            {
                                if (Eggs[w0, l])
                                {
                                    Vector3 tmpPosition = Eggs[w0, l].transform.position;
                                    Eggs[w0, l].transform.position = Eggs[w, l].transform.position + new Vector3(Density, 0, 0);
                                    MakeJoint(Eggs[w, l], Eggs[w0, l]);
                                    Eggs[w0, l].transform.position = tmpPosition;
                                    break;
                                }
                            }
                            for (int l0 = l + 1; l0 < maxSize; ++l0)
                            {
                                if (Eggs[w, l0])
                                {
                                    Vector3 tmpPosition = Eggs[w, l0].transform.position;
                                    Eggs[w, l0].transform.position = Eggs[w, l].transform.position + new Vector3(0, 0, Density);
                                    MakeJoint(Eggs[w, l], Eggs[w, l0]);
                                    Eggs[w, l0].transform.position = tmpPosition;
                                    break;
                                }
                            }
                            break;

                        case ConnectionType.XGrid:
                            for (int w0 = w + 1; w0 < maxSize; ++w0)
                            {
                                if (Eggs[w0, l])
                                {
                                    Vector3 tmpPosition = Eggs[w0, l].transform.position;
                                    Eggs[w0, l].transform.position = Eggs[w, l].transform.position + new Vector3(Density, 0, 0);
                                    MakeJoint(Eggs[w, l], Eggs[w0, l]);
                                    Eggs[w0, l].transform.position = tmpPosition;
                                    for (int l0 = l + 1; l0 < maxSize; ++l0)
                                    {
                                        if (Eggs[w0, l0])
                                        {
                                            tmpPosition = Eggs[w0, l0].transform.position;
                                            Eggs[w0, l0].transform.position = Eggs[w, l].transform.position + new Vector3(Density, 0, Density);
                                            MakeJoint(Eggs[w, l], Eggs[w0, l0]);
                                            Eggs[w0, l0].transform.position = tmpPosition;
                                            break;
                                        }
                                    }
                                    break;
                                }
                            }
                            for (int l0 = l + 1; l0 < maxSize; ++l0)
                            {
                                if (Eggs[w, l0])
                                {
                                    Vector3 tmpPosition = Eggs[w, l0].transform.position;
                                    Eggs[w, l0].transform.position = Eggs[w, l].transform.position + new Vector3(0, 0, Density);
                                    MakeJoint(Eggs[w, l], Eggs[w, l0]);
                                    Eggs[w, l0].transform.position = tmpPosition;
                                    for (int w0 = l - 1; w0 > 0; --w0)
                                    {
                                        if (Eggs[w0, l0])
                                        {
                                            tmpPosition = Eggs[w0, l0].transform.position;
                                            Eggs[w0, l0].transform.position = Eggs[w, l].transform.position + new Vector3(-Density, 0, Density);
                                            MakeJoint(Eggs[w, l], Eggs[w0, l0]);
                                            Eggs[w0, l0].transform.position = tmpPosition;
                                            break;
                                        }
                                    }
                                    break;
                                }
                            }
                            break;

                        case ConnectionType.Snake:
                            int startW = w + 1;
                            for (int l0 = l; l0 < maxSize; ++l0)
                            {
                                for (int w0 = startW; w0 < maxSize; ++w0)
                                {
                                    if (Eggs[w0, l0])
                                    {
                                        Vector3 tmpPosition = Eggs[w0, l0].transform.position;
                                        Eggs[w0, l0].transform.position = Eggs[w, l].transform.position + new Vector3(Density, 0, 0);
                                        MakeJoint(Eggs[w, l], Eggs[w0, l0]);
                                        Eggs[w0, l0].transform.position = tmpPosition;
                                        l0 = maxSize;
                                        break;
                                    }
                                }
                                startW = 0;
                            }
                            break;

                        case ConnectionType.Dense:
                            for (int w0 = w + 1; w0 < maxSize; ++w0)
                            {
                                if (Eggs[w0, l])
                                {
                                    Vector3 tmpPosition = Eggs[w0, l].transform.position;
                                    Eggs[w0, l].transform.position = Eggs[w, l].transform.position + new Vector3(Density, 0, 0);
                                    MakeJoint(Eggs[w, l], Eggs[w0, l]);
                                    Eggs[w0, l].transform.position = tmpPosition;
                                }
                            }
                            for (int l0 = l + 1; l0 < maxSize; ++l0)
                            {
                                if (Eggs[w, l0])
                                {
                                    Vector3 tmpPosition = Eggs[w, l0].transform.position;
                                    Eggs[w, l0].transform.position = Eggs[w, l].transform.position + new Vector3(0, 0, Density);
                                    MakeJoint(Eggs[w, l], Eggs[w, l0]);
                                    Eggs[w, l0].transform.position = tmpPosition;
                                }
                            }
                            break;

                        default:
                            break;
                    }
                    
                }
            }
        }
    }

    private void MakeJoint(Egg egg1, Egg egg2)
    {
        SpringJoint springJoint = egg1.gameObject.AddComponent<SpringJoint>();
        springJoint.connectedBody = egg2.GetComponent<Rigidbody>();
        springJoint.damper = (egg1.Damper + egg2.Damper) / 2.0f;
        springJoint.spring = (egg1.Spring + egg2.Spring) / 2.0f;
        springJoint.enableCollision = true;
        springJoint.maxDistance = 0.002f;
        springJoint.minDistance = 0.0f;
        springJoint.tolerance = 0.1f;
        springJoint.enablePreprocessing = false;
    }

    void BreakJoints()
    {
        for (int w = 0; w < maxSize; ++w)
        {
            for (int l = 0; l < maxSize; ++l)
            {
                if (Eggs[w, l])
                {
                    foreach(SpringJoint joint in Eggs[w, l].GetComponents<SpringJoint>())
                    {
                        Destroy(joint);
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            BreakJoints();
            ConnectJoints(ConnectionType.Grid);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            BreakJoints();
            ConnectJoints(ConnectionType.Snake);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            BreakJoints();
            ConnectJoints(ConnectionType.XGrid);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            BreakJoints();
            ConnectJoints(ConnectionType.Dense);
        }
    }

    private void FixedUpdate()
    {
        foreach (Egg e in Eggs)
        {
            RaycastHit hit;
            LayerMask mask = LayerMask.GetMask("Ground");
            if (Physics.Raycast(e.transform.position, Vector3.down, out hit, 0.4f, mask))
            {
                if (Input.GetButtonDown("Jump"))
                {
                    e.GetComponent<Rigidbody>().AddForce(Vector3.up * JumpForce);
                }
            }
        }
    }
}
