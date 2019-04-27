using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform XRotator;
    public Transform YRotator;
    public Transform Arm;
    Transform target;

    public float XRotationSpeed = 5.0f;
    public float YRotationSpeed = 5.0f;

    void Start()
    {
        Cursor.visible = false;
    }

    private void OnDestroy()
    {
        Cursor.visible = true;
    }

    void LateUpdate()
    {
        if (!target)
        {
            Leader leader = FindObjectOfType<Leader>();
            if (!leader)
            {
                return;
            }

            target = leader.transform;
        }

        Arm.transform.position = target.transform.position;
        YRotator.Rotate(Vector3.up * Input.GetAxis("Mouse X") * YRotationSpeed);
        XRotator.Rotate(Vector3.right * Input.GetAxis("Mouse Y") * XRotationSpeed);
        if (XRotator.localEulerAngles.x > 80.0f)
        {
            XRotator.localRotation = Quaternion.Euler(80.0f, 0.0f, 0.0f);
        }
        else if (XRotator.localEulerAngles.x < 10.0f)
        {
            XRotator.localRotation = Quaternion.Euler(10.0f, 0.0f, 0.0f);
        }
        target.transform.rotation = YRotator.rotation;
    }
}
