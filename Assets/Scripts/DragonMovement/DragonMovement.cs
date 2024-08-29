using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Splines;
using static UnityEngine.GraphicsBuffer;

public class DragonMovement : MonoBehaviour
{
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

            distanceTraveled += currentSpeed * Time.deltaTime;
            distancePercentage = distanceTraveled / splineLength;
            

            if (!(backTimeT > 0f) && !returnToStart)
            {
                if ((distanceTraveled < fastSpeedDistance) && fast)
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
            else if (distanceTraveled > backDistance && returnToStart)
            {
                currentSpeed = returnToStartSpeed;
            }
            else if(distanceTraveled < backDistance && returnToStart)
            {
                currentSpeed = speed;
                returnToStart = false;

            }
            else
            {
                backTimeT -= Time.deltaTime;
                isGoingBack = true;
                currentSpeed = -backSpeed;
                returnToStart = false;
            }
            
            //goto distence perentage 
            Vector3 currentPosition = spline.EvaluatePosition(distancePercentage);
            transform.position = currentPosition;
            


            //look direction
            Vector3 nextPosition = spline.EvaluatePosition(distancePercentage + 0.001f);
            Vector3 direction = nextPosition - currentPosition;
            transform.rotation = Quaternion.LookRotation(direction);


            


            if (isdead)
            {
                for (int i = 0; i < position; i++)
                {
                    try
                    {
                        dragonSpawnerScript.tailGO[i].GetComponent<DragonMovement>().backTimeT += backTime;
                    }
                    catch
                    {

                    }

                }
                isdead = false;
                dragonSpawnerScript.tailLength -= 1;
                if (dragonSpawnerScript.tailLength < 1)
                {
                    dragonSpawnerScript.WinCondition();
                }
                dragonSpawnerScript.SpawnFireAtHead();
                Destroy(gameObject);
            }
            
        }
        //ChangeTextAlpha(healthTextAlpha);
        if (textHealthGoBlank && healthText1.color.a > 0) { ChangeTextAlpha(healthText1.color.a - 0.02f); }


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
        backSpeed = 0.0255f + (newObjectLength - (newSpeed * newBackTime))/newBackTime; //no clue why have to add constant
        dragonSpawnerScript = newScript;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Arrow"))
        {
            isdead = true;
        }
    }
    
    public void dragonHit(float damage)
    {
        health -= damage;
        healthText1.text = health.ToString();

        textHealthGoBlank = false;
        if (!isHead) ChangeTextAlpha(1f);
        if (health < 1)
        {
            isdead = true;
        }
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
       dragonSpawnerScript.powerUPsys.StartPowerDelayRelay(powerUpOnDeath);

        try
        {
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
