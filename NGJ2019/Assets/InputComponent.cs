using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputComponent : MonoBehaviour
{
    public float bubbleTimeout;

    public Text[] eggsTexts;
    public GameObject[] bubbles;

    private IEnumerator[] bubblesTimeouts;
    private int[] score;

    // Start is called before the first frame update
    void Start()
    {
        
        foreach(GameObject bubble in bubbles)
        {
            bubble.SetActive(false);
        }

        score = new int[eggsTexts.Length];
        bubblesTimeouts = new IEnumerator[eggsTexts.Length];
    }

    // Update is called once per frame
    void Update()
    {
        int eggIndex = -1;

        if (Input.GetKeyDown(KeyCode.A))
        {
            eggIndex = 0;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            eggIndex = 1;
        }

        if(eggIndex >= 0)
        {
            score[eggIndex]++;
            eggsTexts[eggIndex].GetComponent<Animator>().SetTrigger("Active");

            if(score[eggIndex] > 1)
            {
                for(int i = 0; i < bubbles.Length; i++)
                {
                    if(i != eggIndex)
                    {
                        bubbles[i].SetActive(true);

                        if(bubblesTimeouts[i] != null)
                        {
                            StopCoroutine(bubblesTimeouts[i]);
                        }

                        bubblesTimeouts[i] = HideBubbleAfterDelay(bubbles[i]);  
                        StartCoroutine(bubblesTimeouts[i]);
                    }
                }
            }
        }

        for (int i = 0; i < eggsTexts.Length; i++)
        {
            if(score[i] > 99)
            {
                score[i] = 99;
            }

            eggsTexts[i].text = score[i].ToString();
        }
    }

    IEnumerator HideBubbleAfterDelay(GameObject bubble)
    {
        yield return new WaitForSeconds(bubbleTimeout);

        bubble.SetActive(false);
    }
}
