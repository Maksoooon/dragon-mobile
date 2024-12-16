using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiableButtonOnPress : MonoBehaviour
{
    public GameObject buttonGO;
    private Button button;
    private void Start()
    {
        button = buttonGO.GetComponent<Button>();
    }
    public void DisableButton()
    {
        button.interactable = false;
    }
    public void EnableButton()
    {
        button.interactable = true;
    }
}
