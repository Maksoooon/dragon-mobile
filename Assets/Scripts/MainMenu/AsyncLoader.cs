using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AsyncLoader : MonoBehaviour
{

    /// <summary>
    /// TEST
    /// 
    public int testlevel = 1;    

    /// </summary>
    public GameObject loadingScreen;
    public GameObject mainMenu;

    public GameObject sliderGO;
    private Image slider;
    private void Start()
    {
        loadingScreen.SetActive(false);
        slider = sliderGO.GetComponent<Image>();
    }
    public void LoadLevel(int level)
    {
        mainMenu.SetActive(false);
        loadingScreen.SetActive(true);

        StartCoroutine(LoadlevelASync(level));

    }

    IEnumerator LoadlevelASync(int level)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(level);
        while (!loadOperation.isDone)
        {
            slider.fillAmount = Mathf.Clamp01(loadOperation.progress / 0.9f);
            yield return null;
        }
    }
}
