using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;


public class PowerUpSystem : MonoBehaviour
{
    public GameObject player;
    public PlayerShooting _pShooting;
    public float powerTime = 5f;
    private float elapsedTime = 0f;
    public bool havePower = false;


    [Header("UI")]
    public GameObject SliderGroup;
    private CanvasGroup SliderCG;
    private Slider sliderLeft;
    private Slider sliderRight;


    [Header("PowerButtons")]
    public GameObject PenetrationButtonGO;
    public GameObject BombButtonGO;
    public GameObject FasterButtonGO;
    public GameObject TripleShotButtonGO;
    public GameObject LightningButtonGO;

    private TextMeshProUGUI PenetrationTextAmount;
    private TextMeshProUGUI BombTextAmount;
    private TextMeshProUGUI FasterTextAmount;
    private TextMeshProUGUI TripleShotTextAmount;
    private TextMeshProUGUI LightningTextAmount;

    private GameObject PenetrationButtonDimGO;
    private GameObject BombButtonDimGO;
    private GameObject FasterButtonDimGO;
    private GameObject TripleShotButtonDimGO;
    private GameObject LightningButtonDimGO;

    private float[] powerUpAmount = { 0, 0, 0, 0, 0 };

    public float powerUPDelaytime;
    /// <summary>
    /// 0 - nothing 
    /// 1 - Penetration
    /// 2 - Bomb
    /// 3 - 3x Shot speed
    /// 4 - Triple shot
    /// 5 - Lightning shot
    /// </summary>

    public enum PowerUpType
    {
        None,
        Penetration,
        Bomb,
        FastShot,
        TripleShot,
        LightningShot
    }


    [Header("")]
    public PowerUpType currentPowerUp = PowerUpType.None;

    void Start()
    {
        PenetrationTextAmount = PenetrationButtonGO.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        BombTextAmount = BombButtonGO.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        FasterTextAmount = FasterButtonGO.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        TripleShotTextAmount = TripleShotButtonGO.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        LightningTextAmount = LightningButtonGO.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        PenetrationButtonDimGO = PenetrationButtonGO.transform.GetChild(2).gameObject;
        BombButtonDimGO = BombButtonGO.transform.GetChild(2).gameObject;
        FasterButtonDimGO = FasterButtonGO.transform.GetChild(2).gameObject;
        TripleShotButtonDimGO = TripleShotButtonGO.transform.GetChild(2).gameObject;
        LightningButtonDimGO = LightningButtonGO.transform.GetChild(2).gameObject;
       
        SliderCG = SliderGroup.GetComponent<CanvasGroup>();
        SliderCG.alpha = 0f;
        sliderLeft  = SliderGroup.transform.GetChild(0).GetComponent<Slider>();
        sliderRight = SliderGroup.transform.GetChild(1).GetComponent<Slider>();
        sliderLeft.maxValue  = powerTime;
        sliderRight.maxValue = powerTime;

        if (player != null)
            _pShooting = player.GetComponent<PlayerShooting>();
        else
            Debug.LogError("Player reference is not set in PowerUpSystem script.");
        UpdatePowerUpText();
    }

    void Update()
    {
        if (elapsedTime >= powerTime)
        {
            DisableCurrentPowerUp();
            UpdatePowerUpText();
        }
        else
        {
            elapsedTime += Time.deltaTime;

            sliderLeft.value = powerTime - elapsedTime;
            sliderRight.value = powerTime - elapsedTime;
        }
    }


    public void AddPowerUp(int power)
    {
        //havePower = true;
        //SliderCG.alpha = 1f;
        if (power != 0)
        {
            powerUpAmount[power - 1] += 1;
            UpdatePowerUpText();
        }
    }

    void DisableCurrentPowerUp()
    {
        havePower = false;
        SliderCG.alpha = 0f;
        switch (currentPowerUp)
        {
            case PowerUpType.None:
                break;
            case PowerUpType.Penetration:
                _pShooting.arrowPenetrate = false;
                break;
            case PowerUpType.Bomb:
                _pShooting.bombArrowBool = false;
                break;
            case PowerUpType.FastShot:
                _pShooting.fasterShooting = false;
                break;
            case PowerUpType.TripleShot:
                _pShooting.tripleArrow = false;
                break;
            case PowerUpType.LightningShot:
                _pShooting.lightningArrow = false;
                break;
                // Add cases for other power-ups if needed
        }

        currentPowerUp = PowerUpType.None;
    }


    public void UsePenetrate()
    {
        if (powerUpAmount[0] > 0 && (currentPowerUp == PowerUpType.None))
        {
            powerUpAmount[0] -= 1;
            _pShooting.arrowPenetrate = true;
            currentPowerUp = (PowerUpType)1;
            OnUsePower();
        }
        UpdatePowerUpText();
    }
    public void UseBomb()
    {
        if (powerUpAmount[1] > 0 && (currentPowerUp == PowerUpType.None))
        {
            powerUpAmount[1] -= 1;
            _pShooting.bombArrowBool = true;
            currentPowerUp = (PowerUpType)2;
            OnUsePower();
        }
        UpdatePowerUpText();
    }
    public void UseFaster()
    {
        if (powerUpAmount[2] > 0 && (currentPowerUp == PowerUpType.None))
        {
            powerUpAmount[2] -= 1;
            _pShooting.fasterShooting = true;
            currentPowerUp = (PowerUpType)3;
            OnUsePower();
        }
        UpdatePowerUpText();
    }
    public void UseTripleShot()
    {
        if (powerUpAmount[3] > 0 && (currentPowerUp == PowerUpType.None))
        {
            powerUpAmount[3] -= 1;
            _pShooting.tripleArrow = true;
            currentPowerUp = (PowerUpType)4;
            OnUsePower();
        }
        UpdatePowerUpText();
    }
    public void UseLightning()
    {
        if (powerUpAmount[4] > 0 && (currentPowerUp == PowerUpType.None))
        {
            powerUpAmount[4] -= 1;
            _pShooting.lightningArrow = true;
            currentPowerUp = (PowerUpType)5;
            OnUsePower();
        }
        UpdatePowerUpText();
    }

    void OnUsePower()
    {
        havePower = true;
        SliderCG.alpha = 1f;
        elapsedTime = 0;
    }

    void UpdatePowerUpText()
    {
        
        if (powerUpAmount[0] > 0 && (currentPowerUp == PowerUpType.None))
        {
            PenetrationButtonDimGO.SetActive(false);
            PenetrationTextAmount.text = $"{powerUpAmount[0]}x";
        }
        else if(powerUpAmount[0] > 0)
        {
            PenetrationButtonDimGO.SetActive(true);
            PenetrationTextAmount.text = $"{powerUpAmount[0]}x";
        }
        else
        {
            PenetrationButtonDimGO.SetActive(true);
            PenetrationTextAmount.text = "";
        }

        if (powerUpAmount[1] > 0 && (currentPowerUp == PowerUpType.None))
        {
            BombButtonDimGO.SetActive(false);
            BombTextAmount.text = $"{powerUpAmount[1]}x";
        }
        else if(powerUpAmount[1] > 0)
        {
            BombButtonDimGO.SetActive(true);
            BombTextAmount.text = $"{powerUpAmount[1]}x";
        }
        else
        {
            BombButtonDimGO.SetActive(true);
            BombTextAmount.text = "";
        }

        if (powerUpAmount[2] > 0 && (currentPowerUp == PowerUpType.None))
        {
            FasterButtonDimGO.SetActive(false);
            FasterTextAmount.text = $"{powerUpAmount[2]}x";
        }
        else if(powerUpAmount[2] > 0)
        {
            FasterButtonDimGO.SetActive(true);
            FasterTextAmount.text = $"{powerUpAmount[2]}x";
        }
        else
        {
            FasterButtonDimGO.SetActive(true);
            FasterTextAmount.text = "";
        }

        if (powerUpAmount[3] > 0 && (currentPowerUp == PowerUpType.None))
        {
            TripleShotButtonDimGO.SetActive(false);
            TripleShotTextAmount.text = $"{powerUpAmount[3]}x";
        }
        else if(powerUpAmount[3] > 0)
        {
            TripleShotButtonDimGO.SetActive(true);
            TripleShotTextAmount.text = $"{powerUpAmount[3]}x";
        }
        else
        {
            TripleShotButtonDimGO.SetActive(true);
            TripleShotTextAmount.text = "";
        }

        if (powerUpAmount[4] > 0 && (currentPowerUp == PowerUpType.None))
        {
            LightningButtonDimGO.SetActive(false);
            LightningTextAmount.text = $"{powerUpAmount[4]}x";
        }
        else if(powerUpAmount[4] > 0)
        {
            LightningButtonDimGO.SetActive(true);
            LightningTextAmount.text = $"{powerUpAmount[4]}x";
        }
        else
        {
            LightningButtonDimGO.SetActive(true);
            LightningTextAmount.text = "";
        }

    }

    public IEnumerator AddPowerUPAfterDelay(int power)
    {
        yield return new WaitForSeconds(powerUPDelaytime);
        AddPowerUp(power);
    }
    public void StartPowerDelayRelay(int power)
    {
        StartCoroutine(AddPowerUPAfterDelay(power));
    }

}
