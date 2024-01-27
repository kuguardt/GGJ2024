using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static int playerCount = 0;

    [SerializeField]
    private int playerId;
    
    [SerializeField] List<Color> playerColors = new List<Color>(){Color.red, Color.blue, Color.green, Color.yellow};
    // Start is called before the first frame update
    
    void Awake()
    {
        playerId = playerCount;
        playerCount++;
        GetComponent<SpriteRenderer>().color = playerColors[playerId];
    }

    public void SetPlayerId(int id)
    {
        playerId = id;
    }

}
