using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;

public class DragonSegment : MonoBehaviour
{
    public GameObject dragonParent;
    public DragonMovement _dragonMovement;
    public SplineContainer spline;

    public float distanceTraveled;
    public float distancePercentage;
    public float splineLength;
    public float multi;
    public float sizeMult;
    //private Vector3 previousPosition;
    //private Vector3 targetPosition;
    //private Quaternion targetRotation;
    //
    //private Quaternion previousRotation;


    public float sideToSideMultiplier = 1f;
    public float wavePeriod = 1f;

    void Start()
    {
        
        //dragonParent = this.gameObject.transform.parent.gameObject.transform.parent.gameObject;
        _dragonMovement = dragonParent.GetComponent<DragonMovement>();
        spline = _dragonMovement.spline;
        switch (transform.GetSiblingIndex())
        {
            case 0:
                multi = 2;
                break;
            case 1:
                multi = 1;
                break;
            case 2:
                multi = 0;
                break;
            case 3:
                multi = -1;
                break;
            case 4:
                multi = -2;
                break;
            
            default:
                break;
        }
        splineLength = _dragonMovement.splineLength;
    }


    void Update()
    {
    
        distanceTraveled = _dragonMovement.distanceTraveled + 0.50f * multi;
        distancePercentage = distanceTraveled / splineLength;
        Vector3 currentPosition = spline.EvaluatePosition(distancePercentage);
        transform.position = currentPosition + (transform.up * Mathf.Sin(distanceTraveled * wavePeriod) * sideToSideMultiplier);


        //look direction
        Vector3 nextPosition = (Vector3)spline.EvaluatePosition(distancePercentage + 0.001f) + (transform.up * Mathf.Sin((distanceTraveled + 0.5f) * wavePeriod) * sideToSideMultiplier);
        Vector3 direction = nextPosition - transform.position;
        transform.rotation = Quaternion.LookRotation(direction);
    }
    
}
