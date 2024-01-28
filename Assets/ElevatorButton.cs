using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorButton : MonoBehaviour
{
    public ElevatorButton otherElevatorButton;

    public ElevatorCenter elevatorCenter;

    public GameObject redbutton;
    public GameObject greenbutton;

    public bool isActive = true;

    public bool isFloor1 = false;

    private void Start()
    {
        if (isFloor1)
        {
            SetButtonActive(true);
            otherElevatorButton.SetButtonActive(false);
        }
    }

    public void Press()
    {
        if (!isActive)
        {
            return;
        }

        redbutton.SetActive(false);
        greenbutton.SetActive(true);
        isActive = false;

        elevatorCenter.ChangeFloorElevator();

        otherElevatorButton.SetButtonActive(true);
    }

    public void SetButtonActive(bool fact)
    {
        isActive = fact;
        redbutton.SetActive(fact);
        greenbutton.SetActive(!fact);
    }
}
