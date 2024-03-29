using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerConfigurationManager : MonoBehaviour
{
    public int PlayerCount => playerConfigs.Count;
    public int winPlayerIndex = -1;
    public List<PlayerConfiguration> playerConfigs;
    public List<GameObject> playerConfigObjs;

    public bool inConfigRoom = false;

    public static PlayerConfigurationManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Create PlayerConfigurationManager.");
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            playerConfigs = new List<PlayerConfiguration>();
            playerConfigObjs = new List<GameObject>();
        }
    }

    [ContextMenu("ResetConfigs")]
    public void ResetConfigs()
    {
        foreach (GameObject obj in playerConfigObjs)
        {
            Destroy(obj);
        }

        playerConfigs = new List<PlayerConfiguration>();
        playerConfigObjs = new List<GameObject>();
    }

    public void SetPlayerColor(int index, string color)
    {
        playerConfigs[index].Color = color;
    }

    public void ReadyPlayer(int index)
    {
        playerConfigs[index].IsReady = true;
        if (playerConfigs.All(p => p.IsReady == true))
        {
            inConfigRoom = false;
            SceneManager.LoadScene("Mainlevel");
        }
    }

    public List<GameObject> GetPlayerConfigObjs()
    {
        return playerConfigObjs;
    }

    public List<PlayerConfiguration> GetPlayerConfigs()
    {
        return playerConfigs;
    }

    public void HandlePlayerJoin(PlayerInput pi)
    {
        Debug.Log("player joined " + pi.playerIndex);

        pi.transform.SetParent(transform);

        if (!playerConfigs.Any(p => p.PlayerIndex == pi.playerIndex))
        {
            playerConfigs.Add(new PlayerConfiguration(pi));
            playerConfigObjs.Add(pi.gameObject);
        }
    }
}

public class PlayerConfiguration
{
    public PlayerConfiguration(PlayerInput pi)
    {
        PlayerIndex = pi.playerIndex;
        Input = pi;
    }
    public PlayerInput Input { get; set; }
    public int PlayerIndex { get; set; }
    public bool IsReady { get; set; }
    public string Color { get; set; }
}
