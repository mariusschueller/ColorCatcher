using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundMusic : MonoBehaviour
{
    public static backgroundMusic instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // If there is already an instance of the background music object, destroy this instance to prevent duplicates
            Destroy(gameObject);
        }
    }
}
