using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Splines;

public class DragonSpawnerNEW : MonoBehaviour
{
    //public NewDragonMove newDragonMove;
    //private SplineContainer spline;
    [Header("Body")]
    public int[] bodyVars;

    DragonSpawnerNEW spawner;
    public GameObject playerHolder;
    public GameObject canvas;

    [HideInInspector] public PowerUpSystem powerUPsys;
    [HideInInspector] public PauseMenu pausemenuScrip;
    
    [Header("Dragon Body Parts")]
    public GameObject dragonHead;
    //public GameObject dragonBody;
    public GameObject dragonTail;
    //public bool spawnVars;
    public int varEveryn = 5;
    public GameObject[] dragonTailVariations;
    
    [Header("Body Part Length")]
    public float tailPartLength;
    public float bodyPartLength;
    public float headPartLength;

    

    [Header("Dragon Parts")]
    public float dragonSpeed;
    public float dragonFastSpeed;
    public float dragonFastPercentage;
    public float dragonStartSpawn;
    public float backTime;

    [Header("EndGame")]
    public float endPercentage;

    [Header("Health")]
    public float headHealth;
    public float bodyHealth;
    public float tailHealth;

    public int fullAlphaAmount;
    public int alphaGradientAmount;
    

    //[Header("Length")]
    public int tailLength;
    [HideInInspector] public int tailLengthPermenent;
    private float dragonDistance = 0;

    [Header("GameObjects")]
    public GameObject fireFX;
    public GameObject[] tailGO;


    public float _t = 0;
    public float loseProtect = 5f;
    public float fireSpawn = 1f;
    public float fireSpawnTime;
    public float loseProtectTime;
    public bool isPaused = false;

    private void Start()
    {
        tailLength = bodyVars.Length;
        tailGO = new GameObject[bodyVars.Length + 1];
        spawner = gameObject.GetComponent<DragonSpawnerNEW>();
        powerUPsys = playerHolder.GetComponent<PowerUpSystem>();
        pausemenuScrip = canvas.GetComponent<PauseMenu>();
        tailLengthPermenent = bodyVars.Length;
        dragonDistance = dragonStartSpawn;
    }
    private void Update()
    {
        _t += Time.deltaTime;
    }
    public void SpawnDragon()
    {
        GameObject tail =  Instantiate(dragonTail);
        tail.GetComponent<DragonMovement>().setSpeedANDDistanceANDHealthANDFastspeed(dragonSpeed, dragonDistance, tailHealth, dragonFastSpeed, dragonFastPercentage, bodyPartLength, backTime, spawner);
        tail.name = bodyVars.Length.ToString();
        dragonDistance += tailPartLength;
        tailGO[bodyVars.Length] = tail;


        for (int i = bodyVars.Length-1; i > 0; i--)
        {
            GameObject body = Instantiate(dragonTailVariations[bodyVars[i]]);
            body.GetComponent<DragonMovement>().setSpeedANDDistanceANDHealthANDFastspeed(dragonSpeed, dragonDistance, bodyHealth, dragonFastSpeed, dragonFastPercentage, bodyPartLength, backTime, spawner);
            body.name = i.ToString();
            dragonDistance += bodyPartLength;
            tailGO[i] = body;

        }
        
        
        GameObject head = Instantiate(dragonHead);
        head.GetComponent<DragonMovement>().setSpeedANDDistanceANDHealthANDFastspeed(dragonSpeed, dragonDistance, headHealth, dragonFastSpeed, dragonFastPercentage, bodyPartLength, backTime, spawner);
        head.GetComponent<DragonMovement>().ChangeTextAlpha(0);
        head.GetComponent<DragonMovement>().isHead = true;
        head.name = 0.ToString();
        tailGO[0] = head;
        dragonDistance += headPartLength;

        float reduceAlphaBy = 1f / (float)alphaGradientAmount;
        float InitialAlpha = ((float)fullAlphaAmount + (float)alphaGradientAmount) * reduceAlphaBy;
        
        for (int i = 1 ; i < bodyVars.Length +1; i++)
        {
            //Debug.Log(InitialAlpha);
            tailGO[i].GetComponent<DragonMovement>().ChangeTextAlpha(InitialAlpha);
            InitialAlpha -= reduceAlphaBy;
        }

    }

    public void WinCondition()
    {
        pausemenuScrip.WinScreen();
    }

    public void LoseCondition()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        if (!isPaused)
        {

            pausemenuScrip.LoseScreen1();
        }
    }

    
    public void MiniGameWin()
    {
        loseProtectTime = _t + loseProtect;
        DragonMovement movementHead = tailGO[0].GetComponent<DragonMovement>();
        float backDistance =  movementHead.distanceTraveled - movementHead.fastSpeedDistance;
        pausemenuScrip.pauseButtonGO.SetActive(false);
        
        for (int i = 0; i < tailLengthPermenent + 1; i++)
        {
            try
            {
                DragonMovement movement = tailGO[i].GetComponent<DragonMovement>();
                movement.backDistance = movement.distanceTraveled - backDistance;
                movement.returnToStart = true;
                //Debug.Log($"{i}  " + $"{movement.distanceTraveled - backDistance}");
            }
            catch {  }
            
            //movement.backDistance = distanceToFastSpeed;
        }
        StartCoroutine(PauseForNSeconds(1.5f));
    }

    public IEnumerator PauseForNSeconds(float waitTime)
    {
        isPaused = false;
        pausemenuScrip._joystickScript.isPaused = true;
        yield return new WaitForSeconds(waitTime);
        //isPaused = true;
        pausemenuScrip._joystickScript.isPaused = false;
        pausemenuScrip._adLoaderScript.LoadAd();
        pausemenuScrip.pauseButtonGO.SetActive(true);
    }

    public void SpawnFireAtHead()
    {
        if (fireSpawn + _t > fireSpawnTime)
        {
            fireSpawnTime = fireSpawn + _t;
            GameObject fireFXGO = Instantiate(fireFX, tailGO[0].transform, false);
            Destroy( fireFXGO, 2f);
        }

    }



}
