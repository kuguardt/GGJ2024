using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadPlayerDisplay : MonoBehaviour
{
    [SerializeField] private List<GameObject> deadPlayerDisplays;

    [ColorUsageAttribute(true, true, 0f, 8f, 0.125f, 3f)] [SerializeField]
    List<Color> playerColors = new List<Color>() { Color.red, Color.blue, Color.green, Color.yellow };
    private void Start()
    {
        int winI = PlayerConfigurationManager.instance.winPlayerIndex;
        
        playerColors.RemoveAt(winI);
        
        for (int i = 0; i < PlayerConfigurationManager.instance.PlayerCount - 1; i++)
        {
            deadPlayerDisplays[i].SetActive(true);
            deadPlayerDisplays[i].GetComponent<SpriteRenderer>().material.color = playerColors[i];
        }
    }
}
