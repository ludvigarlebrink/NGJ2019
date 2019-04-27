using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBehaviour : MonoBehaviour
{
    public float bubbleTimeout;

    public Text time;
    public Text[] eggsTexts;
    public GameObject[] bubbles;

    private IEnumerator[] bubblesTimeouts;
    private int[] score;

    // Start is called before the first frame update
    void Start()
    {

        foreach (GameObject bubble in bubbles)
        {
            bubble.SetActive(false);
        }

        score = new int[eggsTexts.Length];
        bubblesTimeouts = new IEnumerator[eggsTexts.Length];
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < eggsTexts.Length; i++)
        {
            if (score[i] > 99)
            {
                score[i] = 99;
            }

            eggsTexts[i].text = score[i].ToString();
        }

        int timeInt = (int)FindObjectOfType<Timer>().GetElapsedTime();
        time.text = timeInt.ToString();
    }

    public void EggCollected(Egg.Type type)
    {
        if (type >= 0)
        {
            score[(int)type]++;
            eggsTexts[(int)type].GetComponent<Animator>().SetTrigger("Active");

            if (score[(int)type] > 1)
            {
                for (int i = 0; i < bubbles.Length; i++)
                {
                    if (i != (int)type)
                    {
                        bubbles[i].SetActive(true);

                        if (bubblesTimeouts[i] != null)
                        {
                            StopCoroutine(bubblesTimeouts[i]);
                        }

                        bubblesTimeouts[i] = HideBubbleAfterDelay(bubbles[i]);
                        StartCoroutine(bubblesTimeouts[i]);
                    }
                }
            }
        }
    }

    IEnumerator HideBubbleAfterDelay(GameObject bubble)
    {
        yield return new WaitForSeconds(bubbleTimeout);

        bubble.SetActive(false);
    }
}
