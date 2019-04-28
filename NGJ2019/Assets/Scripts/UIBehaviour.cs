using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Formation { Line, Triangle, Square };

public class UIBehaviour : MonoBehaviour
{
    public float bubbleTimeout;

    public Text time;
    public Text[] eggsTexts;
    public GameObject[] bubbles;

    //Abilities
    public bool stretchAvailable;
    public bool chargeAvailable;
    public bool electricityAvailable;
    public bool extraAvailable;

    public GameObject buttonA;
    public GameObject buttonX;
    public GameObject buttonB;
    public GameObject buttonY;

    //Formation
    public Formation formation;

    public GameObject lineFormation;
    public GameObject triangleFormation;
    public GameObject squareFormation;

    //Others
    private bool[] abilitiesAvailable;
    private GameObject[] abilitiesButtons;

    private IEnumerator[] bubblesTimeouts;
    private int[] score;

    // Start is called before the first frame update
    void Start()
    {
        abilitiesAvailable = new bool[] { stretchAvailable, chargeAvailable, electricityAvailable, extraAvailable };
        abilitiesButtons = new GameObject[] { buttonA,  buttonX, buttonB, buttonY};

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
        
        UpdateAbilityButtons();
        UpdateFormationIcon();
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

    public void EggLost(Egg.Type type)
    {
        if ((int)type >= 0)
        {
            score[(int)type]--;
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

    private void UpdateAbilityButtons()
    {
        Image image;
        Color tempColor;

        for(int i = 0; i < 4; i++)
        {
            GameObject button = abilitiesButtons[i];
            bool available = abilitiesAvailable[i];

            image = button.GetComponent<Image>();
            tempColor = image.color;
            tempColor.a = available ? 1f : 0.2f;
            image.color = tempColor;

            if(button.transform.childCount > 0) {
                image = button.transform.GetChild(0).GetComponent<Image>();
                tempColor = image.color;
                tempColor.a = available ? 1f : 0.2f;
                image.color = tempColor;
            }
        }
    }

    private void UpdateFormationIcon()
    {
        lineFormation.SetActive(formation == Formation.Line);
        triangleFormation.SetActive(formation == Formation.Triangle);
        squareFormation.SetActive(formation == Formation.Square);
    }
}
