using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggArmy : MonoBehaviour
{
    public float JumpForce = 50.0f;
    public GameObject SpeederEggPrefab;
    public GameObject LeaderEggPrefab;
    private GameObject LeaderEgg;
    public List<Transform> DestinationPoints;
    public List<Egg> Eggs;
    private int currentCount = 0;
    public float Density = 1;

    public enum Formation
    {
        Block,
        Triangle,
        Snake,
        Dense
    }

    // Start is called before the first frame update
    void Start()
    {
        Eggs = new List<Egg>();
        currentCount = 10;
        int sideLength = Mathf.CeilToInt(Mathf.Sqrt(currentCount));
        for (int w = 0; w < sideLength; ++w)
        {
            for (int l = 0; l < sideLength; ++l)
            {
                int index = w * sideLength + l;
                if (index >= currentCount)
                {
                    w = sideLength;
                    break;
                }
                GameObject eggGo = Instantiate(SpeederEggPrefab, new Vector3(w * Density, 0, l * Density), Quaternion.identity, transform);
                Eggs.Add(eggGo.GetComponent<Egg>());
            }
        }

        LeaderEgg = Instantiate(LeaderEggPrefab, new Vector3((sideLength / 2.0f) * Density, 0, (sideLength / 2.0f) * Density), Quaternion.identity);

        for (int i = 0; i < currentCount; ++i)
        {
            GameObject go = new GameObject("Destination" + i);
            go.transform.SetParent(LeaderEgg.transform);
            DestinationPoints.Add(go.transform);
        }
        ChangeFormation(Formation.Block);
    }

    void ChangeFormation(Formation formation)
    {
        switch (formation)
        {
            case Formation.Block:
                int sideLength = Mathf.CeilToInt(Mathf.Sqrt(currentCount));
                float halfLength = (sideLength - 1) * 0.5f;
                for (int w = 0; w < sideLength; ++w)
                {
                    for (int l = 0; l < sideLength; ++l)
                    {
                        int index = w * sideLength + l;
                        if (index >= currentCount)
                        {
                            w = sideLength;
                            break;
                        }
                        DestinationPoints[index].localPosition = new Vector3((w - halfLength) * Density, 0, (l - halfLength) * Density);
                    }
                }
                break;

            case Formation.Triangle:
                float lineDistance = Density * 0.866f;
                int countPerLine = 1;
                int counterInLine = 0;
                int lineCounter = 0;
                for (int i = 0; i < currentCount; ++i)
                {
                    DestinationPoints[i].localPosition = new Vector3((counterInLine - ((countPerLine - 1)* 0.5f)) * Density, 0, lineCounter * -lineDistance);
                    ++counterInLine;
                    if (counterInLine >= countPerLine)
                    {
                        countPerLine++;
                        lineCounter++;
                        counterInLine = 0;
                        if (countPerLine > currentCount - (i + 1))
                        {
                            countPerLine = currentCount - (i + 1);
                        }
                    }
                }
                break;

            case Formation.Dense:
                for (int i = 0; i < currentCount; ++i)
                {
                    DestinationPoints[i].localPosition = Vector3.zero;
                }
                break;
        }

        for (int i = 0; i < currentCount; ++i)
        {
            Eggs[i].AssignDestinationTransform(DestinationPoints[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeFormation(Formation.Block);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeFormation(Formation.Triangle);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeFormation(Formation.Snake);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ChangeFormation(Formation.Dense);
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
                    e.GetComponent<Rigidbody>().AddForce(Vector3.up * JumpForce, ForceMode.Acceleration);
                }
            }
        }
    }
}
