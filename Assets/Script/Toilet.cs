using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toilet : MonoBehaviour
{
    public bool isOccupied = false;

    private void OnEnable()
    {
        isOccupied = false;
    }
}
