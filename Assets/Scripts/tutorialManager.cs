using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class tutorialManager : MonoBehaviour
{
    private int currentStep;

    public GameObject text1;
    public GameObject text2;
    public GameObject text3;
    public GameObject text4;

    public Text next;
    public Button back;

    private GameObject myEventSystem;

    // Start is called before the first frame update
    void Start()
    {

        myEventSystem = GameObject.Find("EventSystem");
        
        currentStep = 1;

        back.interactable = false;

        text1.SetActive(true);
        text2.SetActive(false);
        text3.SetActive(false);
        text4.SetActive(false);
    }

    public void nextPressed()
    {
        if (currentStep == 1)
        {
            text1.SetActive(false);
            text2.SetActive(true);
            back.interactable = true;
        }

        else if (currentStep == 2)
        {
            text2.SetActive(false);
            text3.SetActive(true);
        }

        else if (currentStep == 3)
        {
            
            text3.SetActive(false);
            text4.SetActive(true);
            next.text = "End";
        }

        else if (currentStep == 4)
        {
            SceneManager.LoadScene("level select");
        }

        ++currentStep;

        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
    }

    public void backPressed()
    {
        if (currentStep == 2)
        {
            back.interactable = false;
            text2.SetActive(false);
            text1.SetActive(true);
        }

        else if (currentStep == 3)
        {
            text3.SetActive(false);
            text2.SetActive(true);
        }

        else if (currentStep == 4)
        {
            text4.SetActive(false);
            text3.SetActive(true);
            next.text = "Next";
        }
        --currentStep;

        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
    }
}
