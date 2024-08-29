using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JoyStickSimple : MonoBehaviour
{
    public float halfScreenWidth;
    public float xDiff = 0f;
    public float yDiff = 0f;
    public float xDiffLast = 0f;
    public float yDiffLast = 0f;
    public GameObject emptyStayRight;
    public GameObject emptyMovingRight;
    public Transform emptyStayRightTransform;
    public Transform emptyMovingRightTransform;
    public bool isPaused = false;
    public float movementSpeed;

    private bool isDragging = false;


    private void Start()
    {
        
    }

    void Update()
    {
        ///*
        if (Input.touchCount > 0 && !isPaused)
        {
            // Iterate through each touch
            for (int i = 0; i < Input.touchCount; i++)
            {
                
                Touch touch = Input.GetTouch(i);
                if (i == 0)
                {
                    switch (touch.phase)
                    {
                        case TouchPhase.Began:
                            // Handle touch began
                            emptyStayRight.transform.position = touch.position;
                            break;
                        case TouchPhase.Moved:
                            // Handle touch moved
                            //emptyMovingRight.transform.position = touch.position - new Vector2(xDiffLast, yDiffLast);
                            Vector2 targetPosition = touch.position - new Vector2(xDiffLast, yDiffLast);
                            emptyMovingRight.transform.position = Vector2.Lerp(emptyMovingRight.transform.position, targetPosition, Time.deltaTime * movementSpeed);
                            xDiff = emptyStayRight.transform.position.x - emptyMovingRight.transform.position.x;
                            yDiff = emptyStayRight.transform.position.y - emptyMovingRight.transform.position.y;

                            break;
                        case TouchPhase.Stationary:
                            // Handle touch stationary
                            break;
                        case TouchPhase.Ended:
                            // Handle touch ended
                            xDiffLast = xDiff;
                            yDiffLast = Mathf.Clamp(yDiff, -120, 120);
                            break;
                        case TouchPhase.Canceled:
                            // Handle touch canceled
                            break;
                    }
                }
                


            }

        }
        //*/

        /*
        if (!isPaused)
        {
            if (Input.GetMouseButtonDown(0)) 
            {

                emptyStayRight.transform.position = Input.mousePosition;
                isDragging = true;
            }
            else if (Input.GetMouseButton(0) && isDragging) 
            {

                Vector2 targetPosition = (Vector2)Input.mousePosition - new Vector2(xDiffLast, yDiffLast);
                emptyMovingRight.transform.position = Vector2.Lerp(emptyMovingRight.transform.position, targetPosition, Time.deltaTime * movementSpeed);
                xDiff = emptyStayRight.transform.position.x - emptyMovingRight.transform.position.x;
                yDiff = emptyStayRight.transform.position.y - emptyMovingRight.transform.position.y;
            }
            else if (Input.GetMouseButtonUp(0) && isDragging) 
            {

                xDiffLast = xDiff;
                yDiffLast = Mathf.Clamp(yDiff, -120, 120);
                isDragging = false;
            }
        }
        */

    }


}
