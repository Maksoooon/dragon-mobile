using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{

    public Image loadingBar;

    private void Start()
    {
        StartCoroutine(LoadNextLevel());
    }
    IEnumerator LoadNextLevel()
    {
        AsyncOperation loadLevel = SceneManager.LoadSceneAsync("Main");

        while (!loadLevel.isDone)
        {
            loadingBar.fillAmount = Mathf.Clamp01(loadLevel.progress/ .9f);
            yield return null;
        }
    }
}
