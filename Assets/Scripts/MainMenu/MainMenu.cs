using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    public bool levelSelector = false;

    public GameObject dragon;
    public GameObject archer;
    public RectTransform levelSelectBlock;
    public RectTransform startBlock;
    //public GameObject MainMenuGO;
    //private CanvasGroup MainMenuCG;


    [Header("Scene1")]
    public Transform dragonScene1Pos;
    public Transform archerScene1Pos;
    public RectTransform StartBlockScene1Pos;
    public RectTransform LevelBlockScene1Pos;

    [Header("Scene2")]
    public Transform dragonScene2Pos;
    public Transform archerScene2Pos;
    public RectTransform StartBlockScene2Pos;
    public RectTransform LevelBlockScene2Pos;

    [Header("Time")]
    public float transitionTime;
    //private float transitionTimer = 1f;
    public bool transitionLock = true;
    private bool isTransisioning = false;
    private Vector3 velocity = Vector3.zero;
    private Vector3 velocity2 = Vector3.zero;
    private Vector3 startBlockVelocity = Vector3.zero;
    private Vector3 levelBlockVelocity = Vector3.zero;

    private void Start()
    {
        //MainMenuCG = MainMenuGO.GetComponent<CanvasGroup>();
        if (levelSelector)
        {
            
            dragon.transform.position = dragonScene2Pos.position;
            archer.transform.position = archerScene2Pos.position;
            levelSelectBlock.anchoredPosition3D = LevelBlockScene2Pos.anchoredPosition3D;
            startBlock.anchoredPosition3D = StartBlockScene2Pos.anchoredPosition3D;
        }
    }
    public void Update()
    {
        if (levelSelector)
        {
            if (!isTransisioning)
            {
                //transitionTimer += Time.deltaTime / transitionTime;
                dragon.transform.position = Vector3.SmoothDamp(dragon.transform.position, dragonScene2Pos.position, ref velocity, transitionTime);
                archer.transform.position = Vector3.SmoothDamp(archer.transform.position, archerScene2Pos.position, ref velocity2, transitionTime);
                levelSelectBlock.anchoredPosition3D = Vector3.SmoothDamp(levelSelectBlock.anchoredPosition3D, LevelBlockScene2Pos.anchoredPosition3D, ref levelBlockVelocity, transitionTime);
                startBlock.anchoredPosition3D = Vector3.SmoothDamp(startBlock.anchoredPosition3D, StartBlockScene2Pos.anchoredPosition3D, ref startBlockVelocity, transitionTime);
                //MainMenuCG.alpha = Mathf.Lerp(1f, 0f, transitionTimer);

            }
            else
            {
                //transitionTimer += Time.deltaTime / transitionTime;
                dragon.transform.position = Vector3.SmoothDamp(dragon.transform.position, dragonScene1Pos.position, ref velocity, transitionTime);
                archer.transform.position = Vector3.SmoothDamp(archer.transform.position, archerScene1Pos.position, ref velocity2, transitionTime);
                levelSelectBlock.anchoredPosition3D = Vector3.SmoothDamp(levelSelectBlock.anchoredPosition3D, LevelBlockScene1Pos.anchoredPosition3D, ref levelBlockVelocity, transitionTime);
                startBlock.anchoredPosition3D = Vector3.SmoothDamp(startBlock.anchoredPosition3D, StartBlockScene1Pos.anchoredPosition3D, ref startBlockVelocity, transitionTime);
                //MainMenuCG.alpha = Mathf.Lerp(0f, 1f, transitionTimer);

            }
        }
        else
        {
            if (isTransisioning)
            {
                //transitionTimer += Time.deltaTime / transitionTime;
                dragon.transform.position = Vector3.SmoothDamp(dragon.transform.position, dragonScene2Pos.position, ref velocity, transitionTime);
                archer.transform.position = Vector3.SmoothDamp(archer.transform.position, archerScene2Pos.position, ref velocity2, transitionTime);
                levelSelectBlock.anchoredPosition3D = Vector3.SmoothDamp(levelSelectBlock.anchoredPosition3D, LevelBlockScene2Pos.anchoredPosition3D, ref levelBlockVelocity, transitionTime);
                startBlock.anchoredPosition3D = Vector3.SmoothDamp(startBlock.anchoredPosition3D, StartBlockScene2Pos.anchoredPosition3D, ref startBlockVelocity, transitionTime);
                //MainMenuCG.alpha = Mathf.Lerp(1f, 0f, transitionTimer);

            }
            else
            {
                //transitionTimer += Time.deltaTime / transitionTime;
                dragon.transform.position = Vector3.SmoothDamp(dragon.transform.position, dragonScene1Pos.position, ref velocity, transitionTime);
                archer.transform.position = Vector3.SmoothDamp(archer.transform.position, archerScene1Pos.position, ref velocity2, transitionTime);
                levelSelectBlock.anchoredPosition3D = Vector3.SmoothDamp(levelSelectBlock.anchoredPosition3D, LevelBlockScene1Pos.anchoredPosition3D, ref levelBlockVelocity, transitionTime);
                startBlock.anchoredPosition3D = Vector3.SmoothDamp(startBlock.anchoredPosition3D, StartBlockScene1Pos.anchoredPosition3D, ref startBlockVelocity, transitionTime);
                //MainMenuCG.alpha = Mathf.Lerp(0f, 1f, transitionTimer);

            }   
        }
        
    }
    public void Scene1to2()
    {
        if (transitionLock)
        {
            isTransisioning = !isTransisioning;
            transitionLock = false;
        }
        StartCoroutine(LockForTransitionTime());
        //transitionTimer = 0f;
    }
    public void Scene2to1()
    {
        if (transitionLock)
        {
            isTransisioning = !isTransisioning;
            transitionLock = false;
        }
        StartCoroutine(LockForTransitionTime());
        //transitionTimer = 0f;
    }
    private IEnumerator LockForTransitionTime()
    {
        yield return new WaitForSeconds(transitionTime);
        transitionLock = true;
    }

}
