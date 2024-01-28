using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerController : MonoBehaviour
{
    //public static int playerCount = 0;
    [SerializeField]
    private int playerId = 0;

    private PlayerConfiguration playerConfig;
    public GameObject playerConfigObj;

    private PlayerMovement playerMovement;
    private Script.PlayerAttack playerAttack;
    private Script.PlayerSkill playerSkill;
    private Script.PlayerInteract playerInteract;

    private PlayerInputMap controls;
    
    [ColorUsageAttribute(true,true,0f,8f,0.125f,3f)]

    [SerializeField] List<Color> playerColors = new List<Color>(){Color.red, Color.blue, Color.green, Color.yellow};

    // Start is called before the first frame update
    
    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerAttack = GetComponent<Script.PlayerAttack>();
        playerSkill = GetComponent<Script.PlayerSkill>();
        playerInteract = GetComponent<Script.PlayerInteract>();
        controls = new PlayerInputMap();
    }

    public int GetPlayerID()
    {
        return playerId;
    }

    public void InitializePlayer(GameObject configObj, PlayerConfiguration config)
    {
        playerConfig = config;
        playerId = playerConfig.PlayerIndex;
        GetComponent<SpriteRenderer>().material.color = playerColors[playerId];
        playerConfigObj = configObj;

        config.Input.onActionTriggered += Input_onActionTriggered;
    }

    private void Input_onActionTriggered(CallbackContext obj)
    {
        if (obj.action.name == controls.Gameplay.Movement.name)
        {
            OnMove(obj);
        }
        if (obj.action.name == controls.Gameplay.Jump.name)
        {
            OnJump(obj);
        }
        if (obj.action.name == controls.Gameplay.Dash.name)
        {
            OnDash(obj);
        }
        if (obj.action.name == controls.Gameplay.Attack.name)
        {
            OnAttack(obj);
        }
        if (obj.action.name == controls.Gameplay.Skill.name)
        {
            OnSkill(obj);
        }
    }

    public void InitializePlayer(PlayerConfiguration config)
    {
        playerConfig = config;
        playerId = playerConfig.PlayerIndex;
        GetComponent<SpriteRenderer>().material.color = playerColors[playerId];
        playerConfig.Input.onActionTriggered += Input_onActionTriggered;
    }

    public void OnMove(CallbackContext context)
    {
        if (playerMovement != null)
            playerMovement.OnMove(context);
    }

    public void OnJump(CallbackContext context)
    {
        if (playerMovement != null)
            playerMovement.OnJump(context);
    }

    public void OnDash(CallbackContext context)
    {
        if (playerMovement != null)
            playerMovement.OnDash(context);
    }

    public void OnSkill(CallbackContext context)
    {
        if (playerSkill != null)
            playerSkill.OnSkill(context);
    }

    public void OnAttack(CallbackContext context)
    {
        if (playerAttack != null)
            playerAttack.OnAttack(context);
    }
}
