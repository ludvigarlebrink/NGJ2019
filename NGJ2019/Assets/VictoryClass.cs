using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryClass : MonoBehaviour
{
    public CanvasGroup victoryCanvas;
    public GameObject camera;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Egg") && !other.gameObject.name.Equals("Prf_LeaderEgg"))
        {
            camera.GetComponent<CameraController>().enabled = false;
            camera.GetComponent<GameOverCameraBehaviour>().enabled = true;
            victoryCanvas.blocksRaycasts = true;
            victoryCanvas.interactable = true;
            victoryCanvas.alpha = 1.0f;
            Cursor.visible = true;
        }
    }
}
