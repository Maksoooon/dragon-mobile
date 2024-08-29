using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class LightningEffect : MonoBehaviour
{
    private GameObject lightningGO;
    private Renderer LightningRenderer;
    public int flashTimes;
    //public float startingSize = 1f;
    //public float flashSize;
    public Color startingColor;
    public Color flashColor;
    public float offTime;
    public float onTime;
    //private Vector3 normalSize;

    void Start()
    {
        lightningGO = this.transform.GetChild(0).gameObject;
        LightningRenderer = lightningGO.GetComponent<Renderer>();
        LightningRenderer.material.SetColor("_Color", startingColor);
        //normalSize = lightningGO.transform.localScale;
        StartCoroutine(LightningFlash());
    }

    public IEnumerator LightningFlash()
    {
        for (int i = 0; i < flashTimes; i++)
        {
            //lightningGO.transform.localScale = normalSize * startingSize;

            LightningRenderer.material.SetColor("_Color", startingColor);
            yield return new WaitForSeconds(onTime);
            //lightningGO.SetActive(false);
            LightningRenderer.material.SetColor("_Color", flashColor);
            yield return new WaitForSeconds(offTime);

            LightningRenderer.material.SetColor("_Color", startingColor);
            //lightningGO.transform.localScale = normalSize * flashSize;

            //lightningGO.SetActive(true);


        }
    }
}
