using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool isEggSelected = false;
    private SpringJoint invisSpring = null;

    void Start()
    {
        GameObject invisObject = new GameObject();
        invisSpring = invisObject.AddComponent<SpringJoint>();
        invisSpring.spring = 50.0f;
        invisSpring.damper = 20.0f;
    }

    void Update()
    {
        if (isEggSelected)
        {
            if (Input.GetMouseButtonUp(0))
            {
                invisSpring.connectedBody = null;
                isEggSelected = false;
            }

            LayerMask mask = LayerMask.GetMask("Ground");
            RaycastHit hitInfo = new RaycastHit();
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, mask))
            {
                invisSpring.transform.position = hitInfo.point;
            }
        }
        else
        {
            RaycastHit hitInfo = new RaycastHit();
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo))
            {
                Egg egg = hitInfo.transform.GetComponent<Egg>();
                if (egg)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        invisSpring.transform.position = egg.transform.position;
                        invisSpring.connectedBody = egg.GetComponent<Rigidbody>();
                        isEggSelected = true;
                    }
                }
            }
        }
    }
}
