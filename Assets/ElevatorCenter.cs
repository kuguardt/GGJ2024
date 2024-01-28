using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorCenter : MonoBehaviour
{
    public GameObject toilet1;
    public GameObject toilet2;
    // 1 = lower floor
    public bool isFloor1 = false;
    public GameObject signOn1;
    public GameObject signOff1;
    public GameObject signOn2;
    public GameObject signOff2;
    public Animator elevator1anim;
    public Animator elevator2anim;

    private void Start()
    {
        ChangeFloorElevator();
    }

    [ContextMenu("ChangeFloorElevator")]
    public void ChangeFloorElevator()
    {
        if (isFloor1)
        {
            elevator1anim.Play("close");
            elevator2anim.Play("open");

            signOn1.SetActive(false);
            signOff1.SetActive(true);

            signOn2.SetActive(true);
            signOff2.SetActive(false);

            toilet1.SetActive(false);
            toilet2.SetActive(true);
            isFloor1 = false;
        }
        else
        {
            elevator1anim.Play("open");
            elevator2anim.Play("close");

            signOn1.SetActive(true);
            signOff1.SetActive(false);

            signOn2.SetActive(false);
            signOff2.SetActive(true);

            toilet1.SetActive(true);
            toilet2.SetActive(false);
            isFloor1 = true;
        }
    }

    public void Change()
    {
        if (isFloor1)
        {

        }
        else
        {

        }
    }
}
