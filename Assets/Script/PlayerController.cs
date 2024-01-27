using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static int playerCount = 0;

    private int playerId;
    
    [ColorUsageAttribute(true,true,0f,8f,0.125f,3f)]

    [SerializeField] List<Color> playerColors = new List<Color>(){Color.red, Color.blue, Color.green, Color.yellow};

    // Start is called before the first frame update
    
    void Awake()
    {
        playerId = playerCount;
        playerCount++;
        gameObject.name = $"Player{playerId}";
        GetComponent<SpriteRenderer>().material.color = playerColors[playerId];
    }

    private void Update()
    {
        
    }

}
