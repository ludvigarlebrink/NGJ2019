using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThatAgainBehaviour : MonoBehaviour
{
    public Text that;
    public Text again;
    public GameObject targetLight;

    public float stepDuration = 0.3f;
    public float maxLightIntensity = 1.5f;
    public float lightIntensityIncreaseSpeed = 4f;
    public float lightIntensityDecreaseSpeed = -2f;

    private float currentLightIntensitySpeed = 0f;
    private Light lightComponent;

    // Start is called before the first frame update
    void Start()
    {
        that.gameObject.SetActive(false);
        again.gameObject.SetActive(false);

        lightComponent = targetLight.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            StartCoroutine(ShowHideText(0));
        }

        lightComponent.intensity += currentLightIntensitySpeed * Time.deltaTime;

        if (lightComponent.intensity > maxLightIntensity)
        {
            lightComponent.intensity = maxLightIntensity;
            currentLightIntensitySpeed = lightIntensityDecreaseSpeed;
        }

        if (lightComponent.intensity < 1f) lightComponent.intensity = 1f;
    }

    private IEnumerator ShowHideText(int stage)
    {
        switch(stage)
        {
            case 0:
                that.gameObject.SetActive(true);
                currentLightIntensitySpeed = lightIntensityIncreaseSpeed;
                break;
            case 1:
                that.gameObject.SetActive(false);
                again.gameObject.SetActive(true);
                currentLightIntensitySpeed = lightIntensityIncreaseSpeed;
                break;
            case 2:
                again.gameObject.SetActive(false);
                break;
        }

        yield return new WaitForSeconds(stepDuration);

        StartCoroutine(ShowHideText(stage + 1));
    }
}
