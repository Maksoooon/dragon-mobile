using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    [Header("Count Down Time")]
    public float timeToCount;
    public float _t;
    public TextMeshProUGUI _textMeshPro;
    private DragonSpawnerNEW _spawnDragon;
    private GameObject _statingPoint;
    private bool spawn = true;
    public bool uiClose;
    // Start is called before the first frame update
    void Start()
    {
        _statingPoint = GameObject.Find("DragonSpawner");
        _spawnDragon = _statingPoint.GetComponent<DragonSpawnerNEW>();
        _spawnDragon.isPaused = true;
        
        _t = timeToCount;
        uiClose = true;


    }

    // Update is called once per frame
    void Update()
    {
        
        
        if(_t > 1.1)
        {
            _t -= Time.deltaTime;
            _textMeshPro.SetText(Convert.ToString((int)_t));
            _spawnDragon.isPaused = true;
            _textMeshPro.gameObject.SetActive(true);
        }
        else if (uiClose)
        {
            if (spawn)
            {
                spawn = false;
                _spawnDragon.SpawnDragon();
                _textMeshPro.gameObject.SetActive(false);
                _spawnDragon.isPaused = false;
                _spawnDragon.pausemenuScrip._joystickScript.isPaused = false;
                _spawnDragon.pausemenuScrip.pauseButtonGO.SetActive(true);
                _spawnDragon.pausemenuScrip.powerUpButton.SetActive(true);
                gameObject.transform.GetComponent<RewardedAdLoader>().LoadAd();
            }
            else
            {
                _textMeshPro.gameObject.SetActive(false);
                _spawnDragon.isPaused = false;
                _spawnDragon.pausemenuScrip._joystickScript.isPaused = false;
                //_spawnDragon.pausemenuScrip.pauseButtonGO.SetActive(true);
            }
        }
        else
        {
            _textMeshPro.gameObject.SetActive(false);
        }
    }
}
