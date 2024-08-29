using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

public class MoveUIElementAlongSpline : MonoBehaviour
{

    public GameObject canvasGO;
    public GameObject dragonSpawnerNew;

    [HideInInspector] public PauseMenu _pauseMenu;
    [HideInInspector] public CameraToggle _cameraToggle;
    [HideInInspector] public DragonSpawnerNEW _dragonSpawnerNew;


    public SplineContainer splineContainer; 
    public RectTransform uiElement;         
    public RectTransform dragonRectTransform;
    public float duration = 5.0f;           

    public float timeElapsed = 0.0f;
    public float t;
    public float dragonPos;

    public float errorRate = 0.1f;
    private void Start()
    {

        _pauseMenu = canvasGO.GetComponent<PauseMenu>();
        _cameraToggle = canvasGO.GetComponent<CameraToggle>();
        _dragonSpawnerNew = dragonSpawnerNew.GetComponent<DragonSpawnerNEW>();
        dragonPos = UnityEngine.Random.Range(0, 1f);
        float3 splinePosition = splineContainer.EvaluatePosition(dragonPos);
        Vector3 uiPosition = new Vector3(splinePosition.x, splinePosition.y, splinePosition.z);

        dragonRectTransform.position = uiPosition;
    }

    void Update()
    { 
        timeElapsed += Time.deltaTime;
        t = (timeElapsed / duration);
        if (t > 1.0f)
        {
            timeElapsed = 0;
        }


        float3 splinePosition = splineContainer.EvaluatePosition(t);
        Vector3 uiPosition = new Vector3(splinePosition.x, splinePosition.y, splinePosition.z);


        uiElement.position = uiPosition;

    }

    public void CheckCrosshair()
    {
        
        if ((dragonPos + errorRate) >= t && (dragonPos - errorRate) <= t)
        {
            //_pauseMenu.pressAmount = 0;
            _pauseMenu.Resume();
            _cameraToggle.CameraToggler();
            _dragonSpawnerNew.MiniGameWin();
        }
        else { _pauseMenu.LoseScreen2(); }
        
    }
}
