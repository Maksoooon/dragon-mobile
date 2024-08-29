
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed;
    public float maxDistance;
    public float distance;
    public float _t;
    public float killTimeOut;
    public float killTime;
    public bool penetrate;
    public float arrowPenetrateAmount;
    public float PenetrateAmount = 0;
    public bool killing = true;
    public float damage;
    public bool bombArrowBool = false;
    public float bombDamage;
    public float bombRadius;
    public GameObject bombEffect;
    public GameObject arrowHitEffect;
    


    public bool lightningArrowbool = false;
    public GameObject lightningGO;
    public float lightningTime = 0.5f;
    public float lightningDamage;
    public int lightningAmount;
    public DragonSpawnerNEW dragonSpawner;
    public float distanceTraveled;

    [Header("Sound On Spawn")]
    public bool spawnOnDeath = false;
    public GameObject canvas;
    public PauseMenu _pauseMenu;
    public bool isMusic = false;
    public GameObject deathSpawnableGO;
    public AudioSource sound;

    public bool DestroyAfterTime = false;
    public float timeToDestroy;


    private void Start()
    {
        killTimeOut = 0.02f;
    }
    private void Update()
    {
        if (!dragonSpawner.isPaused)
        {
            _t += Time.deltaTime;
            transform.position += speed * Time.deltaTime * transform.forward;
            distance += speed * Time.deltaTime;

            if (distance > maxDistance)
            {
                Destroy(gameObject);
            }
            if (PenetrateAmount == arrowPenetrateAmount)
            {
                Destroy(gameObject);
            }
            if (_t > killTime + killTimeOut)
            {
                killing = true;
            }
        }
        

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dragon") && killing)
        {
            Vector3 collisionPoint = other.ClosestPoint(transform.position);
            Vector3 normal = (collisionPoint - transform.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(normal); 
            GameObject arrowHitEffectGO =  Instantiate(arrowHitEffect, collisionPoint, rotation);
            Destroy(arrowHitEffectGO,2f);

            killing = false;
            
            killTime = _t;
            other.gameObject.GetComponent<DragonMovement>().dragonHit(damage);
            if (!penetrate)
            {
                Destroy(gameObject);
            }
            else
            {
                PenetrateAmount++;
            }
        }
    }
    private void OnDestroy()
    {
        if (bombArrowBool)
        {
            GameObject explosionSphere = Instantiate(bombEffect, transform.position, transform.rotation);
            //explosionSphere.transform.localScale = new Vector3(bombRadius * 2, bombRadius * 2, bombRadius * 2);
            Destroy(explosionSphere, 1.0f);
            Collider[] colliders = Physics.OverlapSphere(transform.position, bombRadius);
            foreach (Collider hitCollider in colliders)
            {
                if (hitCollider.CompareTag("Dragon"))
                {
                    hitCollider.gameObject.GetComponent<DragonMovement>().dragonHit(bombDamage);
                    
                }
            }
        }
        if (lightningArrowbool)
        {
            int[] lightningStrikes = new int[lightningAmount]; // Initialize the array

            for (int i = 0; i < lightningAmount; i++)
            {
                float tempPercent = 0;
                while (tempPercent < distanceTraveled)
                {
                    try
                    {
                        int tempRandom = Random.Range(0, dragonSpawner.tailGO.Length);
                        tempPercent = dragonSpawner.tailGO[tempRandom].gameObject.GetComponent<DragonMovement>().distancePercentage;
                        lightningStrikes[i] = tempRandom;
                    }
                    catch
                    {

                    }
                    
                    
                }
            }
            for (int i = 0;i < lightningStrikes.Length; i++)
            {
                DragonMovement tempDragMove = dragonSpawner.tailGO[lightningStrikes[i]].gameObject.GetComponent<DragonMovement>();
                tempDragMove.dragonHit(lightningDamage);
                GameObject LightningStrikeGOTemp = Instantiate(lightningGO, tempDragMove.transform.position, Quaternion.identity);
                Destroy(LightningStrikeGOTemp, lightningTime);
            }
        }
    }
}
