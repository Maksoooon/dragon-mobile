using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;

public class DragonHeadSegment : MonoBehaviour
{
    public GameObject dragonParent;
    public DragonMovement _dragonMovement;
    public SplineContainer spline;


    //public Transform basePosition;
    public float distanceTraveled;
    public float distancePercentage;
    public float splineLength;
    public float sizeMult;
    //private Vector3 previousPosition;
    //private Vector3 targetPosition;
    //private Quaternion targetRotation;
    //
    //private Quaternion previousRotation;


    public float sideToSideMultiplier = 1f;
    public float wavePeriod = 1f;

    public float offset;

    void Start()
    {

        //basePosition = this.transform;
        //dragonParent = this.gameObject.transform.parent.gameObject.transform.parent.gameObject;
        _dragonMovement = dragonParent.GetComponent<DragonMovement>();
        spline = _dragonMovement.spline;
        
        splineLength = _dragonMovement.splineLength;
}


    void Update()
    {
    
        distanceTraveled = _dragonMovement.distanceTraveled + offset;
        distancePercentage = distanceTraveled / splineLength;
        Vector3 currentPosition = spline.EvaluatePosition(distancePercentage);
        transform.position = currentPosition + (transform.up * Mathf.Sin(distanceTraveled * wavePeriod) * sideToSideMultiplier);


        //look direction
        Vector3 nextPosition = (Vector3)spline.EvaluatePosition(distancePercentage + 0.001f) + (transform.up * Mathf.Sin((distanceTraveled + 0.5f) * wavePeriod) * sideToSideMultiplier);
        Vector3 direction = nextPosition - transform.position;
        transform.rotation = Quaternion.LookRotation(direction);
    }
    
}
