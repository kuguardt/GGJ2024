using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poop : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
       GetComponent<Animator>().Play("Credit");
    }
}
