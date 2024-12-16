using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Splines;

public class DragonMovement : MonoBehaviour
{
    public float progress;
    public SplineContainer spline;
    public DragonSpawnerNEW dragonSpawnerScript;

    public GameObject[] childrenSegmentHolder;
    public Renderer[] SegmentHolderChildren;

    private Transform firstChild;

    public bool isHead = false;
    public float speed = 1f;
    public float objectLength;
    public float currentSpeed;
    public float fastSpeed;
    public float fastSpeedDistance;
    public float fastSpeedPercentage;
    public float backSpeed = -4f;
    public float _t;
    public float backTime;
    public float backTimeT;

    public float distanceTraveled;
    public float distancePercentage;
    public float splineLength;
    public float knotLength;
    public float health;
    public int position;
    public TextMeshPro healthText1;
    public float healthTextAlpha;
    public bool textHealthGoBlank = true;

    public bool returnToStart = false;
    public float backDistance;
    public float returnToStartSpeed = -1000f;
    public Color emissionColor;
    public Color currentEmissionColor;
    public GameObject DeathSpawnEffect;
    private TrailRenderer trailData;
    public Material ParticleMaterial;
    
    //new move logic

    public float elapsedBackTime = 0f;
    public float totalBackDistance = 0f;
    public float initialBackDistance = 0f;


    //public TextMeshPro healthText2;
    // <summary>
    // 0 - nothing 
    // 1 - Penetration
    // 2 - Bomb
    // 3 - 3x Shot speed
    // 4 - Triple shot
    // 5 - Lightning shot
    // </summary>

    public int powerUpOnDeath = 0;

    public bool isdead = false;
    public bool isGoingBack = false;
    public bool fast = true;

    private void Start()
    {
        spline = GameObject.FindWithTag("DragonPATHtag").GetComponent<SplineContainer>();
        splineLength = spline.CalculateLength();
        fastSpeedDistance = splineLength * fastSpeedPercentage + distanceTraveled - dragonSpawnerScript.dragonStartSpawn;
        healthText1.text = health.ToString();
        backTimeT = 0f;
        textHealthGoBlank = true;
        returnToStartSpeed = -100f;
        firstChild = gameObject.transform.GetChild(0);
        childrenSegmentHolder = new GameObject[firstChild.childCount];
        SegmentHolderChildren = new Renderer[firstChild.childCount];
        
        for (int i = 0; i < firstChild.childCount; i++)
        {
            childrenSegmentHolder[i] = firstChild.GetChild(i).gameObject;
            SegmentHolderChildren[i] = childrenSegmentHolder[i].gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>();
            SegmentHolderChildren[i].material.EnableKeyword("_EMISSION");
        }
        position = int.Parse(this.name);
    }
    
    private void Update()
    {

        if (!dragonSpawnerScript.isPaused)
        {
            
            if (isGoingBack && backTimeT > 0f)
            {
                elapsedBackTime += Time.deltaTime;

                
                progress = Mathf.Clamp01(elapsedBackTime / backTimeT);
                distanceTraveled = Mathf.Lerp(initialBackDistance, totalBackDistance, progress);

                
                if (progress == 1)
                {
                    isGoingBack = false;
                    backTimeT = 0;
                    elapsedBackTime = 0f;
                    totalBackDistance = 0;
                }
            }
            else if (returnToStart)
            {
                if (distanceTraveled > backDistance)
                {
                    currentSpeed = returnToStartSpeed;
                }
                else
                {
                    returnToStart = false;
                    currentSpeed = speed;
                }
            }
            else
            {
                if (distanceTraveled < fastSpeedDistance && fast)
                {
                    isGoingBack = false;
                    currentSpeed = fastSpeed;
                }
                else
                {
                    fast = false;
                    isGoingBack = false;
                    currentSpeed = speed;
                }
            }

            distanceTraveled += currentSpeed * Time.deltaTime;
            distancePercentage = distanceTraveled / splineLength;

            Vector3 currentPosition = spline.EvaluatePosition(distancePercentage);
            transform.position = currentPosition;

            Vector3 nextPosition = spline.EvaluatePosition(distancePercentage + 0.001f);
            Vector3 direction = nextPosition - currentPosition;
            transform.rotation = Quaternion.LookRotation(direction);
        }

        if (isdead)
        {
            HandleDeath();
        }

        if (textHealthGoBlank && healthText1.color.a > 0)
        {
            ChangeTextAlpha(healthText1.color.a - 0.02f);
        }
    }

    private void HandleDeath()
    {
        for (int i = 0; i < position; i++)
        {
            try
            {
                dragonSpawnerScript.tailGOmovementSript[i].GoBackTimer();
            }
            catch { }
        }

        isdead = false;
        dragonSpawnerScript.tailLength--;
        if (dragonSpawnerScript.tailLength < 1)
        {
            dragonSpawnerScript.WinCondition();
        }
        dragonSpawnerScript.SpawnFireAtHead();
        Destroy(gameObject);
    }

    public void GoBackTimer()
    {

        backTimeT += backTime;
        
        
        if (isGoingBack)
        {
            totalBackDistance = initialBackDistance - ((backSpeed * backTimeT));
        }
        else
        {
            initialBackDistance = distanceTraveled;
            totalBackDistance = distanceTraveled - ((backSpeed * backTimeT));
        }
        
        //if (elapsedBackTime > 0)
        //{
        //    elapsedBackTime = 0;
        //}
        
        isGoingBack = true; 
    }
    public void setSpeedANDDistanceANDHealthANDFastspeed(float newSpeed, float newDistance, float newHealth, float newFastSpeed, float newFastSpeedPercentage, float newObjectLength, float newBackTime, DragonSpawnerNEW newScript)
    {
        speed = newSpeed;
        distanceTraveled = newDistance;
        health = newHealth;
        fastSpeed = newFastSpeed;
        fastSpeedPercentage = newFastSpeedPercentage;
        objectLength = newObjectLength;
        backTime = newBackTime;
        //backSpeed = 0.0255f + (newObjectLength - (newSpeed * newBackTime))/newBackTime; //no clue why have to add constant
        backSpeed = (newObjectLength - (newSpeed * newBackTime)) / newBackTime;
        //backSpeed = newSpeed * ((newObjectLength - distanceTraveled) / (newSpeed * newBackTime)); ;
        dragonSpawnerScript = newScript;
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Arrow"))
    //    {
    //        isdead = true;
    //    }
    //}

    public void dragonHit(float damage)
    {
        health -= damage;
        healthText1.text = health.ToString();

        textHealthGoBlank = false;
        if (!isHead) ChangeTextAlpha(1f);
        if (health < 1) isdead = true;

        for (int i = 0; i < firstChild.childCount; i++)
        {
            SegmentHolderChildren[i].material.SetColor("_EmissionColor", emissionColor);
        }
        StartCoroutine(SmoothColorChange());

        
    }


    public void ChangeTextAlpha(float alpha)
    {
        Color currentColor = healthText1.color;
        currentColor.a = alpha;
        healthText1.color = currentColor;
    }

    private void OnDestroy()
    {
      
        try
        {
            if (!dragonSpawnerScript.isPaused)  //particles appear on restart
            {
                dragonSpawnerScript.powerUPsys.StartPowerDelayRelay(powerUpOnDeath);
                if (powerUpOnDeath != 0)
                {

                    GameObject deathEffect = Instantiate(DeathSpawnEffect, transform.position, Quaternion.LookRotation(transform.position - dragonSpawnerScript.playerHolder.transform.position, Vector3.left));
                    ParticleSystemRenderer partSystem = deathEffect.transform.GetChild(0).gameObject.GetComponent<ParticleSystemRenderer>();

                    partSystem.material = ParticleMaterial;
                    partSystem.trailMaterial = ParticleMaterial;
                    Destroy(deathEffect, 10f);
                }
                else
                {
                    GameObject deathEffect = Instantiate(DeathSpawnEffect, transform.position, transform.rotation);
                    Destroy(deathEffect, 10f);

                }
            }
            
        }
        catch { }
            
            
    }

    public IEnumerator SmoothColorChange()
    {

        if (SegmentHolderChildren[0].material.GetColor("_EmissionColor").r > 0f)
        {
            currentEmissionColor = SegmentHolderChildren[0].material.GetColor("_EmissionColor");
            currentEmissionColor = new Color(currentEmissionColor.r - 0.031372552f, currentEmissionColor.g - 0.031372552f, currentEmissionColor.b - 0.031372552f);
            for (int i = 0; i < firstChild.childCount; i++)
            {
                SegmentHolderChildren[i].material.SetColor("_EmissionColor", currentEmissionColor);
            }
            yield return 0;
            StartCoroutine(SmoothColorChange());
        }
    }
    
}
