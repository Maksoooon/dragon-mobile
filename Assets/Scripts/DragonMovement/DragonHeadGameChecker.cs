using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DragonHeadGameChecker : MonoBehaviour
{
    public DragonMovement dragonMovementScript;
    public DragonSpawnerNEW _dragonSpawnerScript;
    public float endGame;

    void Start()
    {
        dragonMovementScript = gameObject.GetComponent<DragonMovement>();
        _dragonSpawnerScript = dragonMovementScript._dragonSpawnerScript;
        endGame = _dragonSpawnerScript.endPercentage;
    }

    // Update is called once per frame
    void Update()
    {
        if (dragonMovementScript.distancePercentage > endGame & _dragonSpawnerScript._t > _dragonSpawnerScript.loseProtectTime)
        {
            _dragonSpawnerScript.LoseCondition();
            
        }
    }
   
}
