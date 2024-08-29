using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    public static bool GameisPaused = false;

    [Header("GameObjects")]
    //public GameObject MainCameraHolder;
    public GameObject pauseButtonGO;
    public GameObject pauseMenuUI;
    public GameObject settingsMenuUI;
    public GameObject joystickGO;
    public GameObject goBackButtonGO;
    public GameObject dragonSpawner;
    public GameObject playerHolder;
    public GameObject winScreen;
    public GameObject loseScreen1;
    public GameObject loseScreen2;
    public GameObject miniGameHolder;


    [HideInInspector] public JoyStickSimple _joystickScript;
    [HideInInspector] public DragonSpawnerNEW _dragonSpawnerScript;
    [HideInInspector] public PlayerAimRightLeft _playerAimRightLeft;
    [HideInInspector] public Minigame _miniGameScript;
    [HideInInspector] public CameraToggle _cameraToggle;
    [HideInInspector] public RewardedAdLoader _adLoaderScript;


    [Header("UI GameObjects")]
    public GameObject XMult;
    public GameObject YMult;
    public GameObject YToggle;
    public GameObject MusicToggle;
    public GameObject SoundToggle;

    [Header("UI Components")]
    public Slider XMultSlider;
    public Slider YMultSlider;
    public Toggle YToggleToggle;
    public Toggle MusicToggleToggle;
    public Toggle SoundToggleToggle;

    //private float defaultSoundVolume = 0.2f;
    private float volume;

    public bool musicBool;
    public bool soundBool;
    //public int pressAmount = 0;

    void Start()
    {
        pauseButtonGO.SetActive(false);
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(false);
        goBackButtonGO.gameObject.SetActive(false);
        winScreen.gameObject.SetActive(false);
        loseScreen1.gameObject.SetActive(false);
        loseScreen2.gameObject.SetActive(false);
        miniGameHolder.gameObject.SetActive(false);
        _dragonSpawnerScript = dragonSpawner.GetComponent<DragonSpawnerNEW>();
        _joystickScript = joystickGO.GetComponent<JoyStickSimple>();
        _playerAimRightLeft = playerHolder.GetComponent<PlayerAimRightLeft>();
        _miniGameScript = gameObject.GetComponent<Minigame>();
        _cameraToggle = gameObject.GetComponent<CameraToggle>();
        XMultSlider = XMult.GetComponent<Slider>();
        YMultSlider = YMult.GetComponent<Slider>();
        YToggleToggle = YToggle.GetComponent<Toggle>();
        MusicToggleToggle = MusicToggle.GetComponent<Toggle>();
        SoundToggleToggle = SoundToggle.GetComponent<Toggle>();
        _miniGameScript = miniGameHolder.transform.GetChild(0).GetComponent<Minigame>();
        _adLoaderScript = gameObject.GetComponent<RewardedAdLoader>();
        GetSettings();
    }

    public void Pause()
    {
        GameisPaused = true;
        pauseMenuUI.gameObject.SetActive(true);
        settingsMenuUI.gameObject.SetActive(false);
        pauseButtonGO.gameObject.SetActive(false);
        goBackButtonGO.gameObject.SetActive(false);
        _dragonSpawnerScript.isPaused = GameisPaused;
        _joystickScript.isPaused = GameisPaused;
        SetSettings();
    }

    public void OpenSettings()
    {
        pauseMenuUI.gameObject.SetActive(false);
        settingsMenuUI.gameObject.SetActive(true);
        goBackButtonGO.gameObject.SetActive(true);
    }

    public void Resume()
    {
        GameisPaused = false;
        pauseMenuUI.gameObject.SetActive(false);
        settingsMenuUI.gameObject.SetActive(false);
        pauseButtonGO.gameObject.SetActive(true);
        goBackButtonGO.gameObject.SetActive(false);
        winScreen.gameObject.SetActive(false);
        loseScreen1.gameObject.SetActive(false);
        loseScreen2.gameObject.SetActive(false);
        miniGameHolder.gameObject.SetActive(false);
        _dragonSpawnerScript.isPaused = GameisPaused;
        _joystickScript.isPaused = GameisPaused;
        GetSettings();
    }

    public void WinScreen()
    {
        GameisPaused = true;
        pauseMenuUI.gameObject.SetActive(false);
        settingsMenuUI.gameObject.SetActive(false);
        pauseButtonGO.gameObject.SetActive(false);
        goBackButtonGO.gameObject.SetActive(false);
        winScreen.gameObject.SetActive(true);
        _dragonSpawnerScript.isPaused = GameisPaused;
        _joystickScript.isPaused = GameisPaused;
    }
    public void LoseScreen1()
    {
        GameisPaused = true;
        pauseMenuUI.gameObject.SetActive(false);
        settingsMenuUI.gameObject.SetActive(false);
        pauseButtonGO.gameObject.SetActive(false);
        goBackButtonGO.gameObject.SetActive(false);
        loseScreen1.gameObject.SetActive(true);
        loseScreen1.gameObject.transform.GetChild(3).GetComponent<ADTimer>().StartTimer();
        loseScreen2.gameObject.SetActive(false);
        _dragonSpawnerScript.isPaused = GameisPaused;
        _joystickScript.isPaused = GameisPaused;
    }
    public void LoseScreen2()
    {
        GameisPaused = true;
        pauseMenuUI.gameObject.SetActive(false);
        settingsMenuUI.gameObject.SetActive(false);
        pauseButtonGO.gameObject.SetActive(false);
        goBackButtonGO.gameObject.SetActive(false);
        loseScreen1.gameObject.SetActive(false);
        loseScreen2.gameObject.SetActive(true);
        _dragonSpawnerScript.isPaused = GameisPaused;
        _joystickScript.isPaused = GameisPaused;
    }
    public void StartMiniGame()
    {
        _cameraToggle.CameraToggler();
        _dragonSpawnerScript.loseProtectTime = 10000000;
        GameisPaused = true;
        pauseMenuUI.gameObject.SetActive(false);
        settingsMenuUI.gameObject.SetActive(false);
        pauseButtonGO.gameObject.SetActive(false);
        goBackButtonGO.gameObject.SetActive(false);
        winScreen.gameObject.SetActive(false);
        loseScreen1.gameObject.SetActive(false);
        loseScreen2.gameObject.SetActive(false);
        miniGameHolder.gameObject.SetActive(true);

    }
    public void MusicToggler(Toggle toggle)
    {
        musicBool = toggle.isOn;
    }
    public void SoundToggler(Toggle toggle)
    {
        soundBool = toggle.isOn;
    }
    public void SetSettings()
    {
        PlayerPrefs.SetFloat("XMult", XMultSlider.value);
        PlayerPrefs.SetFloat("YMult", YMultSlider.value);
        PlayerPrefs.SetFloat("YInvert", YToggleToggle.isOn ? 1 : 0);
        PlayerPrefs.SetFloat("Mute", volume);

        PlayerPrefs.SetFloat("musicBool", musicBool ? 1 : 0);
        PlayerPrefs.SetFloat("soundBool", soundBool ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void GetSettings()
    {
        XMultSlider.value = PlayerPrefs.GetFloat("XMult", 0.5f);
        YMultSlider.value = PlayerPrefs.GetFloat("YMult", 0.25f);
        YToggleToggle.isOn = PlayerPrefs.GetFloat("YInvert") == 1 ? true : false;

        musicBool = PlayerPrefs.GetFloat("musicBool", 1) == 1 ? true : false;
        soundBool = PlayerPrefs.GetFloat("soundBool", 1) == 1 ? true : false;
        _playerAimRightLeft.multiplierX = XMultSlider.value;
        _playerAimRightLeft.multiplierY = YMultSlider.value;
        _playerAimRightLeft.invertY = YToggleToggle.isOn;

        MusicToggleToggle.isOn = musicBool;
        SoundToggleToggle.isOn = soundBool;
    }

    //public void MiniGameShoot()
    //{
    //    pressAmount += 1;
    //    if (pressAmount > 1)
    //    {
    //        gameObject.GetComponent<LevelPicker>().LEVEL();
    //    }
    //    _adLoaderScript.LoadAd();
    //    _adLoaderScript.adWatched = false;
    //}

}
