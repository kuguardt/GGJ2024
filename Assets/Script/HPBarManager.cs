using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class HPBarManager : MonoBehaviour
{
    public static HPBarManager instance;

    [SerializeField] private GameObject[] playerHealthBar;

    private void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else
            instance = this;
    }

    private void Start()
    {
        foreach(GameObject healthBar in playerHealthBar)
            healthBar.SetActive(false);
    }

    public void SetActiveHealthBar(int playerID)
    {
        playerHealthBar[playerID].SetActive(true);
    }
    public void SetHealthBarUI(int playerID, float HP)
    {
        playerHealthBar[playerID].GetComponent<HealthBar>().HealthBarFilter(HP);
    }
}
