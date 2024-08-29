using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnSpawn : MonoBehaviour
{

    public AudioSource sound;
    public bool spawnOnDeath = false;
    public GameObject canvas;
    public PauseMenu _pauseMenu;
    public bool isMusic = false;
    public GameObject deathSpawnableGO;

    private float _t = 0f;
    public bool DestroyAfterTime = false;
    public float timeToDestroy;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.FindGameObjectsWithTag("Canvas")[0];
        _pauseMenu = canvas.GetComponent<PauseMenu>();
        sound = this.gameObject.GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (DestroyAfterTime)
        {
            _t += Time.deltaTime;
            if (timeToDestroy < _t)
            {
                Destroy(this.gameObject);
            }
        }
        if (isMusic)
        {
            if (!_pauseMenu.musicBool)
            {
                sound.mute = true;
            }
            else
            {
                sound.mute = false;
            }
        }
        else
        {
            if (!_pauseMenu.soundBool)
            {
                sound.mute = true;
            }
            else
            {
                sound.mute = false;
            }
        }
    }
    private void OnDestroy()
    {
        if (spawnOnDeath)
        {
            GameObject hitsound = Instantiate(deathSpawnableGO);
        }
    }
}
