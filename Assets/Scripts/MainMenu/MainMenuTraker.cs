using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuTraker : MonoBehaviour
{
    public static bool firstMenuLoad = true;

    private void Awake()
    {
        // Ensures this object persists across scenes
        DontDestroyOnLoad(gameObject);
    }
}
