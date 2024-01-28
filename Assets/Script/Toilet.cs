using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toilet : MonoBehaviour
{
    public Outline toiletoutline;

    public bool isOccupied = false;

    private void OnEnable()
    {
        isOccupied = false;
    }

    private void Update()
    {
        if (isOccupied == true)
        {
            toiletoutline.enabled = false;
        }
        else
        {
            toiletoutline.enabled = true;
        }
    }
}
