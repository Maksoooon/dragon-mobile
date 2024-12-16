using System.Collections;
using TMPro;
using UnityEngine;
//using UnityEngine.UIElements;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("level Block")]
    public int levelAmount;
    public int currentPage = 0; //used later to save page
    public GameObject[] levelbuttonGO;
    //public Button[] _levelbuttonArr;
    public GameObject nextLevelBlockButton;
    public GameObject asyncGO;
    //private AsyncLoader _asyncLoader;


    [Header("CanvasGO")]
    public bool levelSelector = false;

    public GameObject dragon;
    public GameObject archer;
    public RectTransform levelSelectBlock;
    public RectTransform startBlock;
    public RectTransform gradientBlock;
    //public GameObject MainMenuGO;
    //private CanvasGroup MainMenuCG;


    [Header("Scene1")]
    public Transform dragonScene1Pos;
    public Transform archerScene1Pos;
    public RectTransform StartBlockScene1Pos;
    public RectTransform LevelBlockScene1Pos;
    public RectTransform GradientBlockScene1Pos;

    [Header("Scene2")]
    public Transform dragonScene2Pos;
    public Transform archerScene2Pos;
    public Transform gradientScene2Pos;
    public RectTransform StartBlockScene2Pos;
    public RectTransform LevelBlockScene2Pos;
    public RectTransform GradientBlockScene2Pos;

    [Header("Time")]
    public float transitionTime;
    //private float transitionTimer = 1f;
    public bool transitionLock = true;
    private bool isTransisioning = false;
    private Vector3 velocity = Vector3.zero;
    private Vector3 velocity2 = Vector3.zero;
    private Vector3 startBlockVelocity = Vector3.zero;
    private Vector3 levelBlockVelocity = Vector3.zero;
    private Vector3 GradientBlockVelocity = Vector3.zero;

    [Header("SettingsUI")]
    public GameObject settingsMenuUI;

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

    private float volume;

    public bool musicBool;
    public bool soundBool;
    public bool yinvertBool;


    private void Start()
    {

        //MainMenuCG = MainMenuGO.GetComponent<CanvasGroup>();
        LevelTextUpdate();
        if (levelSelector)
        {
            
            dragon.transform.position = dragonScene2Pos.position;
            archer.transform.position = archerScene2Pos.position;
            levelSelectBlock.anchoredPosition3D = LevelBlockScene2Pos.anchoredPosition3D;
            startBlock.anchoredPosition3D = StartBlockScene2Pos.anchoredPosition3D;
            gradientBlock.anchoredPosition3D = GradientBlockScene2Pos.anchoredPosition3D;
        }
        //for (int i = 0; i < levelbuttonGO.Length; i++)
        //{
        //    _levelbuttonArr[i] = levelbuttonGO[i].GetComponent<Button>();
        //}
        ////_asyncLoader = asyncGO.GetComponent<AsyncLoader>()
        settingsMenuUI.SetActive(false);
        XMultSlider = XMult.GetComponent<Slider>();
        YMultSlider = YMult.GetComponent<Slider>();
        YToggleToggle = YToggle.GetComponent<Toggle>();
        MusicToggleToggle = MusicToggle.GetComponent<Toggle>();
        SoundToggleToggle = SoundToggle.GetComponent<Toggle>();
        GetSettings();
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
                gradientBlock.anchoredPosition3D = Vector3.SmoothDamp(gradientBlock.anchoredPosition3D, GradientBlockScene2Pos.anchoredPosition3D, ref GradientBlockVelocity, transitionTime);
                startBlock.anchoredPosition3D = Vector3.SmoothDamp(startBlock.anchoredPosition3D, StartBlockScene2Pos.anchoredPosition3D, ref startBlockVelocity, transitionTime);
                //MainMenuCG.alpha = Mathf.Lerp(1f, 0f, transitionTimer);

            }
            else
            {
                //transitionTimer += Time.deltaTime / transitionTime;
                dragon.transform.position = Vector3.SmoothDamp(dragon.transform.position, dragonScene1Pos.position, ref velocity, transitionTime);
                archer.transform.position = Vector3.SmoothDamp(archer.transform.position, archerScene1Pos.position, ref velocity2, transitionTime);
                levelSelectBlock.anchoredPosition3D = Vector3.SmoothDamp(levelSelectBlock.anchoredPosition3D, LevelBlockScene1Pos.anchoredPosition3D, ref levelBlockVelocity, transitionTime);
                gradientBlock.anchoredPosition3D = Vector3.SmoothDamp(gradientBlock.anchoredPosition3D, GradientBlockScene1Pos.anchoredPosition3D, ref GradientBlockVelocity, transitionTime);
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
                gradientBlock.anchoredPosition3D = Vector3.SmoothDamp(gradientBlock.anchoredPosition3D, GradientBlockScene2Pos.anchoredPosition3D, ref GradientBlockVelocity, transitionTime);
                startBlock.anchoredPosition3D = Vector3.SmoothDamp(startBlock.anchoredPosition3D, StartBlockScene2Pos.anchoredPosition3D, ref startBlockVelocity, transitionTime);
                //MainMenuCG.alpha = Mathf.Lerp(1f, 0f, transitionTimer);

            }
            else
            {
                //transitionTimer += Time.deltaTime / transitionTime;
                dragon.transform.position = Vector3.SmoothDamp(dragon.transform.position, dragonScene1Pos.position, ref velocity, transitionTime);
                archer.transform.position = Vector3.SmoothDamp(archer.transform.position, archerScene1Pos.position, ref velocity2, transitionTime);
                levelSelectBlock.anchoredPosition3D = Vector3.SmoothDamp(levelSelectBlock.anchoredPosition3D, LevelBlockScene1Pos.anchoredPosition3D, ref levelBlockVelocity, transitionTime);
                gradientBlock.anchoredPosition3D = Vector3.SmoothDamp(gradientBlock.anchoredPosition3D, GradientBlockScene1Pos.anchoredPosition3D, ref GradientBlockVelocity, transitionTime);
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

    public void NextLevelBlock()
    {
        if (levelAmount > currentPage * 8)
        {
            currentPage++;
            LevelTextUpdate();
            if (levelAmount < (currentPage + 1) * 8)
            {
                nextLevelBlockButton.SetActive(false);
            }

        }
    }
    public void BackLevelBlock()
    {
        if (currentPage == 0)
        {
            Scene2to1();
        }
        else {
            currentPage--;
            LevelTextUpdate();
            nextLevelBlockButton.SetActive(true);
        }
    }
    public void LevelTextUpdate()
    {
        for (int i = 0; i < 8; i++)
        {

            if (((currentPage) * 8) + i + 1 <= levelAmount)
            {
                
                levelbuttonGO[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ((currentPage * 8) + i + 1).ToString();
                levelbuttonGO[i].SetActive(true);
                //_levelbuttonArr[i].clicked -= () => buttonclicked(i-8);
                //_levelbuttonArr[i].clicked += () => buttonclicked(i);
                
            }
            else
            {
                levelbuttonGO[i].SetActive(false);
            }

        }
    }
    public void OpenSettings()
    {
        if (!isTransisioning)
        {
            GetSettings();
            settingsMenuUI.SetActive(true);
        }
        

    }
    public void CloseSettings()
    {
        SetSettings();
        settingsMenuUI.SetActive(false);

    }
    public void MusicToggler(Toggle toggle)
    {
        musicBool = toggle.isOn;
    }
    public void SoundToggler(Toggle toggle)
    {
        soundBool = toggle.isOn;
    }
    public void YinvertToggler(Toggle toggle)
    {
        yinvertBool = toggle.isOn;
    }
    public void SetSettings()
    {
        PlayerPrefs.SetFloat("XMult", XMultSlider.value);
        PlayerPrefs.SetFloat("YMult", YMultSlider.value);
        PlayerPrefs.SetFloat("YInvert", yinvertBool ? 1 : 0);
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
        //_playerAimRightLeft.multiplierX = XMultSlider.value;
        //_playerAimRightLeft.multiplierY = YMultSlider.value;
        //_playerAimRightLeft.invertY = YToggleToggle.isOn;
        //_playerAimRightLeft.joystickSimple.yDiffmax[0] = _playerAimRightLeft.maxAngle / _playerAimRightLeft.multiplierY;


        MusicToggleToggle.isOn = musicBool;
        SoundToggleToggle.isOn = soundBool;
    }
    //private void buttonclicked(int i)
    //{
    //    asyncGO.GetComponent<AsyncLoader>().LoadLevel(i);
    //}
}
