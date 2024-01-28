using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLevel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.StopSound("BGM1_Fanky_Crib");
        AudioManager.instance.PlaySound("BGM3_Thinking_Time");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        AudioManager.instance.StopSound("BGM3_Thinking_Time");
    }
}
