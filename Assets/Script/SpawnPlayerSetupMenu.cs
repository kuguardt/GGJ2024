using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class SpawnPlayerSetupMenu : MonoBehaviour
{
    public GameObject playerSetupMenuPrefab;

    private GameObject rootMenu;
    public PlayerInput input;

    private int playerId = 0;

    [SerializeField]
    private Transform configSpawnPoint;

    private GameObject rootPlayerObj;

    [SerializeField]
    private GameObject playerPrefab;

    private void Awake()
    {
        rootMenu = GameObject.Find("MainLayout");
        if (rootMenu != null)
        {
            var menu = Instantiate(playerSetupMenuPrefab, rootMenu.transform);
            input.uiInputModule = menu.GetComponentInChildren<InputSystemUIInputModule>();
            menu.GetComponent<PlayerSetupMenuController>().setPlayerIndex(input.playerIndex);

            int controllerMode = 0;
            if(input.devices[0].description.manufacturer == "Sony Interactive Entertainment") controllerMode = 1;
            
            menu.GetComponent<PlayerSetupMenuController>().SetUI(controllerMode);
            playerId = input.playerIndex;

            SpawnPlayer(PlayerConfigurationManager.instance.GetPlayerConfigs()[playerId]);
        }
    }

    public void SpawnPlayer(PlayerConfiguration config)
    {
        rootPlayerObj = GameObject.Find("RootPlayers");
        if (rootPlayerObj != null)
        {
            var player = Instantiate(playerPrefab, configSpawnPoint.position, configSpawnPoint.rotation, rootPlayerObj.transform);
            player.GetComponent<PlayerController>().InitializePlayer(config);
            player.GetComponent<PlayerHealth>().SetMaxHealth(1000f);
        }
    }
}