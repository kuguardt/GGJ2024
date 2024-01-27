using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static int playerCount = 0;

    private int playerId;
    
    public float speed = 10.0f;

    private Vector2 movementInput;
    
    [SerializeField] List<Color> playerColors = new List<Color>(){Color.red, Color.blue, Color.green, Color.yellow};
    // Start is called before the first frame update
    
    void Awake()
    {
        playerId = playerCount;
        playerCount++;
        GetComponent<SpriteRenderer>().color = playerColors[playerId];
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(movementInput.x,movementInput.y,0) * (speed * Time.deltaTime));
        
    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }
}
