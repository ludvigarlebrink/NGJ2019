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
    private Formation activeFormation;
    private Formation lastFormation;
    public Egg eggie;
    private bool isSnakeFormation;
    private bool scaleX = false;
    private bool scaleZ = false;
    private bool unscaleX = false;
    private bool unscaleZ = false;

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
        eggie = Instantiate(SpeederEggPrefab, new Vector3(10, 0, 10), Quaternion.identity, transform).GetComponent<Egg>();
        eggie.standAlone = true;
        Eggs = new List<Egg>();
        currentCount = 36;
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
                GameObject eggGo = Instantiate(SpeederEggPrefab, transform);
                eggGo.transform.localPosition = new Vector3(w * Density, 0, l * Density);
                eggGo.GetComponent<Egg>().standAlone = false;
                Eggs.Add(eggGo.GetComponent<Egg>());
            }
        }

        LeaderEgg = Instantiate(LeaderEggPrefab, transform.position + new Vector3((sideLength / 2.0f) * Density, 0, (sideLength / 2.0f) * Density), Quaternion.identity);

        for (int i = 0; i < currentCount; ++i)
        {
            GameObject go = new GameObject("Destination" + i);
            go.transform.SetParent(LeaderEgg.transform);
            DestinationPoints.Add(go.transform);
        }
        isSnakeFormation = false;
        ChangeFormation(Formation.Block);
        lastFormation = Formation.Block;
    }

    public void Restart()
    {
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
                Eggs[index].transform.localPosition = new Vector3(w * Density, 0, l * Density);
                Eggs[index].gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
            }
        }
        LeaderEgg.transform.position = transform.position + new Vector3((sideLength / 2.0f) * Density, 0, (sideLength / 2.0f) * Density);
        LeaderEgg.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
        ChangeFormation(Formation.Block);
        lastFormation = Formation.Block;
    }

    public void AddEggie(Egg littleEggie)
    {
        ++currentCount;
        littleEggie.standAlone = false;
        Eggs.Add(littleEggie);

        GameObject go = new GameObject("Destination" + (currentCount - 1));
        go.transform.SetParent(LeaderEgg.transform);
        DestinationPoints.Add(go.transform);
        ChangeFormation(lastFormation);
    }

    void ChangeFormation(Formation formation)
    {
        activeFormation = formation;
        switch (formation)
        {
            case Formation.Block:
                int sideLength = Mathf.CeilToInt(Mathf.Sqrt(currentCount));
                float halfLength = (sideLength - 1) * 0.5f;
                for (int w = 0; w < sideLength; ++w)
                {
                    bool outOfBounds = false;
                    for (int l = 0; l < sideLength; ++l)
                    {
                        int index = w * sideLength + l;
                        if (index == currentCount)
                        {
                            outOfBounds = true;
                            break;
                        }
                        DestinationPoints[index].localPosition = new Vector3((w - halfLength) * Density, 0, (l - halfLength) * Density);
                    }
                    if (outOfBounds)
                    {
                        break;
                    }
                }
                for (int i = 0; i < currentCount; ++i)
                {
                    Eggs[i].AssignDestinationTransform(DestinationPoints[i]);
                }
                break;

            case Formation.Triangle:
                float lineDistance = Density * 0.866f;
                int countPerLine = 1;
                int counterInLine = 0;
                int lineCounter = 0;
                for (int i = 0; i < currentCount; ++i)
                {
                    DestinationPoints[i].localPosition = new Vector3((counterInLine - ((countPerLine - 1) * 0.5f)) * Density, 0, lineCounter * -lineDistance);
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

            case Formation.Snake:
                if (isSnakeFormation)
                {
                    // traverse back-to-front and take preceding egg last position
                    for (int i = DestinationPoints.Count - 1; i > 0; --i) // first egg will still follow the leader
                    {
                        Eggs[i].AssignDestinationTransform(Eggs[i - 1].transform);
                    }
                }
                else
                {
                    // init snake formation here
                    for (int i = 0; i < DestinationPoints.Count; ++i)
                    {
                        if (i != 0)
                        {
                            Eggs[i].transform.SetParent(Eggs[i - 1].transform);
                            DestinationPoints[i].localPosition = new Vector3(0, 0, Density);
                        }
                        else
                        {
                            DestinationPoints[i].localPosition = new Vector3(0, 0, Density);
                        }
                    }
                    for (int i = 0; i < currentCount; ++i)
                    {
                        Eggs[i].AssignDestinationTransform(DestinationPoints[i]);
                    }
                    //isSnakeFormation = true;
                }
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("ScaleX") && !Input.GetButton("RevertScale"))
        {
            LeaderEgg.transform.localScale += new Vector3(10.0f * Time.deltaTime, 0, 0.0f);
            if (LeaderEgg.transform.localScale.x > 5.0f)
            {
                LeaderEgg.transform.localScale = new Vector3(5.0f, LeaderEgg.transform.localScale.y, LeaderEgg.transform.localScale.z);
            }
        }
        else if (Input.GetButton("ScaleX") && Input.GetButton("RevertScale"))
        {
            LeaderEgg.transform.localScale -= new Vector3(10.0f * Time.deltaTime, 0, 0.0f);
            if (LeaderEgg.transform.localScale.x < 1.0f)
            {
                LeaderEgg.transform.localScale = new Vector3(1.0f, LeaderEgg.transform.localScale.y, LeaderEgg.transform.localScale.z);
            }
        }
        else if (Input.GetButton("ScaleZ") && !Input.GetButton("RevertScale"))
        {
            LeaderEgg.transform.localScale += new Vector3(0, 0, 10.0f * Time.deltaTime);
            if (LeaderEgg.transform.localScale.z > 5.0f)
            {
                LeaderEgg.transform.localScale = new Vector3(LeaderEgg.transform.localScale.x, LeaderEgg.transform.localScale.y, 5.0f);
            }
        }
        else if (Input.GetButton("ScaleZ") && Input.GetButton("RevertScale"))
        {
            LeaderEgg.transform.localScale -= new Vector3(0.0f, 0, 10.0f * Time.deltaTime);
            if (LeaderEgg.transform.localScale.z < 1.0f)
            {
                LeaderEgg.transform.localScale = new Vector3(LeaderEgg.transform.localScale.x, LeaderEgg.transform.localScale.y, 1.0f);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeFormation(Formation.Block);
            lastFormation = Formation.Block;
            isSnakeFormation = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeFormation(Formation.Triangle);
            lastFormation = Formation.Triangle;
            isSnakeFormation = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeFormation(Formation.Snake);
            lastFormation = Formation.Snake;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ChangeFormation(Formation.Dense);
            lastFormation = Formation.Dense;
            isSnakeFormation = false;
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

    public void KillEgg(Egg egg)
    {
        if (egg)
        {
            if (Eggs.Remove(egg))
            {
                Destroy(egg.gameObject);
                --currentCount;
                ChangeFormation(activeFormation);
            }
        }
    }
}
