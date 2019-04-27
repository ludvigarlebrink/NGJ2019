using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverCameraBehaviour : MonoBehaviour
{
    public float cameraSpeed = 20;
    public Vector3 cameraDistance = new Vector3(40, 60, 40);
    public Vector3 cameraLookAt = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        transform.Translate(cameraLookAt + cameraDistance);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(cameraLookAt);
        transform.Translate(Vector3.right * cameraSpeed * Time.deltaTime);
    }
}
