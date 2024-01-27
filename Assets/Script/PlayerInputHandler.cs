using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerConfiguration playerConfig;
    private PlayerMovement playerMovement;

    private InputMap controls;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        controls = new InputMap();
    }

    public void InitializePlayer(PlayerConfiguration config)
    {
        playerConfig = config;
        config.Input.onActionTriggered += Input_onActionTriggered;
    }

    private void Input_onActionTriggered(CallbackContext obj)
    {
        if (obj.action.name == controls.Gameplay.Movement.name)
        {
            OnMove(obj);
        }
    }

    public void OnMove(CallbackContext context)
    {
        if (playerMovement != null)
            playerMovement.OnMove(context);
    }

}