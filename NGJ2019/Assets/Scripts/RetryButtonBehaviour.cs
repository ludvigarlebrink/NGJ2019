﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetryButtonBehaviour : MonoBehaviour
{
    public void OnButtonClicked()
    {
        Debug.Log("Retry Button Clicked!");
        Application.LoadLevel(Application.loadedLevel);
    }
}
