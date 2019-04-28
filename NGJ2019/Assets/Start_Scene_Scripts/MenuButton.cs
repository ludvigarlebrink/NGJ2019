using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    [SerializeField] MenuButtonController menuButtonController;
    [SerializeField] Animator animator;
    [SerializeField] AnimatorFunctions animatorFunctions;
    [SerializeField] int thisIndex;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(menuButtonController.index == thisIndex)
        {
            animator.SetBool("selected", true);
            if(Input.GetAxis ("Submit") == 1)
            {
                animator.SetBool("pressed", true);
            }else if (animator.GetBool("pressed"))
            {
                animator.SetBool("pressed", false);
                animatorFunctions.disableOnce = true;

                if (thisIndex == 0)
                {
                    Debug.Log("Load game!");
                    SceneManager.LoadSceneAsync("OrestisScene", LoadSceneMode.Single);
                }
                else if (thisIndex == 1)
                {
                    Debug.Log("QUIT!");
                    Application.Quit();
                }
            }
        }else
        {
            animator.SetBool("selected", false);
        }
    }
}
