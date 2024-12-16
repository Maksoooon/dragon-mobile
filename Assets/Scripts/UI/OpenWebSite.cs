using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWebSite : MonoBehaviour
{
    public string websiteLink;


    public void OpenSite()
    {
        Application.OpenURL(websiteLink);

    }


}
