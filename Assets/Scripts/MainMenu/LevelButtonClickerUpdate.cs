using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelButtonClickerUpdate : MonoBehaviour
{

    public GameObject asyncGO;
    private AsyncLoader _asyncLoader;
    public TextMeshProUGUI text;
    void Start()
    {
        _asyncLoader = asyncGO.GetComponent<AsyncLoader>();
        text = this.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    public void loadLevelBasedOnText()
    {
        _asyncLoader.LoadLevel(int.Parse(text.text));
        Debug.Log(text.text);
    }
}
