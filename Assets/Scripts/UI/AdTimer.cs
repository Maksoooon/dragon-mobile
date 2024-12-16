using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ADTimer : MonoBehaviour
{
    public GameObject CanvasGO;
    private PauseMenu _PauseMenu;
    public Image UIFill;

    public float adTime;    
    
    public float timeRemaining;
    public Color startColor;
    public Color endColor;
    //private Color currentColor;
    
    // Start is called before the first frame update
    void Start()
    {
        _PauseMenu = CanvasGO.GetComponent<PauseMenu>();
        UIFill = transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();
    }

    private void Update()
    {
        if (timeRemaining > 0)
        {
            UIFill.fillAmount = Mathf.InverseLerp(0, adTime, timeRemaining);
            UIFill.color = NicerColorLerp(endColor, startColor, timeRemaining / adTime);
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            //_PauseMenu.LoseScreen2();
        }
    }
    public void StartTimer()
    {
        timeRemaining = adTime;
    }
    Color NicerColorLerp(Color A, Color B, float t)
    {
        return new Color(Mathf.Sqrt(A.r * A.r * (1 - t) + t * B.r * B.r), Mathf.Sqrt(A.g * A.g * (1 - t) + t * B.g * B.g), Mathf.Sqrt(A.b * A.b * (1 - t) + t * B.b * B.b));
    }

    public void SkipTime()
    {
        timeRemaining = 0;
    }
}
