using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelInitializer : MonoBehaviour
{
    [SerializeField]
    private Transform[] PlayerSpawns;

    [SerializeField]
    private GameObject playerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        var playerConfigs = PlayerConfigurationManager.instance.GetPlayerConfigs().ToArray();
        var playerConfigObjs = PlayerConfigurationManager.instance.GetPlayerConfigObjs().ToArray();
        for (int i = 0; i < playerConfigs.Length; i++)
        {
            var player = Instantiate(playerPrefab, PlayerSpawns[i].position, PlayerSpawns[i].rotation, gameObject.transform);
            player.GetComponent<PlayerController>().InitializePlayer(playerConfigObjs[i], playerConfigs[i]);
            players.Add(player);
        }

    }

    private List<GameObject> players = new List<GameObject>();
    public int AlivePlayerCount()
    {
        int count = 0;
        foreach (GameObject p in players)
        {
            if (p.GetComponent<PlayerHealth>().IsAlive)
            {
                count++;
            }
        }

        return count;
    }

    private Coroutine endGameCour = null;
    void Update()
    {
        if (endGameCour == null && AlivePlayerCount() == 1)
        {
            for (int i = 0; i < PlayerConfigurationManager.instance.PlayerCount; i++)
            {

                if (players[i].GetComponent<PlayerHealth>().IsAlive)
                {
                    PlayerConfigurationManager.instance.winPlayerIndex = i;
                }
            
            }
            endGameCour  = StartCoroutine(EndGame());
        }
    }
    
    IEnumerator EndGame()
    {
        
        yield return new WaitForSeconds(1.69f);
        Debug.Log("End Game");
        SceneManager.LoadScene("EndScene");
    }
}