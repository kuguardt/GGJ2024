using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Script;
using static UnityEngine.InputSystem.InputAction;

public class PlayerController : MonoBehaviour
{
    //public static int playerCount = 0;
    [SerializeField] private int playerId = 0;

    private PlayerConfiguration playerConfig;
    public GameObject playerConfigObj;

    private PlayerMovement playerMovement;
    private PlayerAttack playerAttack;
    private PlayerSkill playerSkill;
    private PlayerInteract playerInteract;
    private PlayerHealth playerHealth;
    

    private PlayerInputMap controls;

    private bool inConfigRoom = false;

    [ColorUsageAttribute(true, true, 0f, 8f, 0.125f, 3f)] [SerializeField]
    List<Color> playerColors = new List<Color>() { Color.red, Color.blue, Color.green, Color.yellow };

    // Start is called before the first frame update

    void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
        playerMovement = GetComponent<PlayerMovement>();
        playerAttack = GetComponent<PlayerAttack>();
        playerSkill = GetComponent<PlayerSkill>();
        playerInteract = GetComponent<PlayerInteract>();
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
        gameObject.name = "Player" + playerId;

        config.Input.onActionTriggered += Input_onActionTriggered;
    }

    private void Input_onActionTriggered(CallbackContext obj)
    {
        if (obj.action.name == controls.Gameplay.Pause.name)
        {
            PauseMenuManager.instance.SetActivePauseMenu();
            Debug.Log("Open Pause Menu");
        }

        if (!playerHealth.IsAlive)
        {
            playerMovement.movementInput = Vector2.zero;
            return;
        }
        
        if (obj.action.name == controls.Gameplay.Interact.name)
        {
            if (inConfigRoom)
            {

                return;
            }
            OnInteract(obj);
        }
        if (obj.action.name == controls.Gameplay.Skill.name)
        {
            OnSkill(obj);
        }
        
        if (playerInteract._isInteracting)
        {
            playerMovement.movementInput = Vector2.zero;
            return;
        }

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
    }

    public void InitializePlayer(PlayerConfiguration config)
    {
        playerConfig = config;
        playerId = playerConfig.PlayerIndex;
        GetComponent<SpriteRenderer>().material.color = playerColors[playerId];
        config.Input.onActionTriggered += Input_onActionTriggered;
    }

    public void SetInConfigurationRoom(bool fact)
    {
        inConfigRoom = fact;
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

    public void OnInteract(CallbackContext context)
    {
        if (playerInteract != null)
            playerInteract.OnInteract(context);
    }
}