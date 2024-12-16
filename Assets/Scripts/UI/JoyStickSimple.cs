using UnityEngine;
using static UnityEngine.GraphicsBuffer;

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
    public float[] yDiffmax = {};
    //private bool isDragging = false;
    private int? joystickFingerId = null;

    void Update()
    {
        
        if (!isPaused)
        {
            if (Input.touchCount > 0)
            {
                for (int i = 0; i < Input.touchCount; i++)
                {
                    Touch touch = Input.GetTouch(i);

                    if (i == 0)
                    {
                        if (joystickFingerId == null || joystickFingerId == touch.fingerId)
                        {
                            switch (touch.phase)
                            {
                                case TouchPhase.Began:
                                    if (joystickFingerId == null && !isPaused)
                                    {
                                        joystickFingerId = touch.fingerId;
                                        emptyStayRight.transform.position = touch.position;
                                    }
                                    break;
                                case TouchPhase.Moved:
                                    if (joystickFingerId == touch.fingerId && !isPaused)
                                    {
                                        Vector2 targetPosition = touch.position - new Vector2(xDiffLast, yDiffLast);
                                        emptyMovingRight.transform.position = Vector2.Lerp(emptyMovingRight.transform.position, targetPosition, Time.deltaTime * movementSpeed);
                                        xDiff = emptyStayRight.transform.position.x - emptyMovingRight.transform.position.x;
                                        yDiff = emptyStayRight.transform.position.y - emptyMovingRight.transform.position.y;

                                        UpdateEmptyStayRightPosition();



                                    }
                                    break;
                                case TouchPhase.Stationary:
                                    break;
                                case TouchPhase.Ended:
                                case TouchPhase.Canceled:
                                    if (joystickFingerId == touch.fingerId && !isPaused)
                                    {
                                        joystickFingerId = null;
                                        xDiffLast = xDiff;
                                        yDiffLast = Mathf.Clamp(yDiff, -120, 120);
                                    }
                                    break;
                            }
                        }
                        
                    }
                }
            }
        }


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

    public void UpdateEmptyStayRightPosition()
    {
        Vector3 offset = emptyStayRight.transform.position - emptyMovingRight.transform.position;

        offset.y = Mathf.Clamp(offset.y, -yDiffmax[0], yDiffmax[1]);

        emptyStayRight.transform.position = emptyMovingRight.transform.position + offset;
    }
}
