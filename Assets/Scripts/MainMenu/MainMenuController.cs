using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    private void Start()
    {
        if (MainMenuTraker.firstMenuLoad)
        {
            MainMenuTraker.firstMenuLoad = false;
        }
        else
        {
            GameObject.FindGameObjectsWithTag("MainMenuManager")[0].GetComponent<MainMenu>().levelSelector = true;
        }
    }
}
