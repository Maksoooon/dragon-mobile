using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAimRightLeft : MonoBehaviour
{
    public GameObject joystickGO;
    //public TextMeshProUGUI angleText;
    //public TextMeshProUGUI angleText1;
    public Transform player;
    public JoyStickSimple joystickSimple;
    public float multiplierX;
    public float multiplierY;
    public bool invertY = true;
    public float rotationAngleX;
    public float rotationAngleY;
    public float maxAngle = 30f;
    public float minAngle = -10f;

    public float smoothingFactor = 5f;
    private void Start()
    {
        joystickSimple = joystickGO.GetComponent<JoyStickSimple>();

    }

    private void Update()
    {
        RotatePlayerTowardsJoystick();

    }
    private void RotatePlayerTowardsJoystick()
    {
        rotationAngleX = joystickSimple.xDiff * multiplierX * -1;
        rotationAngleY = invertY ? joystickSimple.yDiff * multiplierY : joystickSimple.yDiff * multiplierY * -1;
        

        rotationAngleY = Mathf.Clamp(rotationAngleY, minAngle, maxAngle);

        Quaternion targetRotation = Quaternion.Euler(-rotationAngleY, rotationAngleX, 0);

        player.rotation = Quaternion.Slerp(player.rotation, targetRotation, Time.deltaTime * smoothingFactor);

    }


    public void ToggleInvert(Toggle toggle)
    {
        if (toggle.isOn)
        {
            invertY = true;

        }
        else
        {
            invertY = false;
        }
    }
}