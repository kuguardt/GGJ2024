using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float currentHealth = 100f;

    PlayerController playerController;

    private float idleDecreaseRate = 1f;
    private float decreaseRate;

    [SerializeField] private float decayValue = -1f;
    //private float attackDamage = 10f;

    private float timer = 0f;
    private int playerID;

    void Start()
    {
        decreaseRate = idleDecreaseRate;

        playerController = GetComponent<PlayerController>();
        playerID = playerController.GetPlayerID();

        HPBarManager.instance.SetActiveHealthBar(playerID);
        HPBarManager.instance.SetHealthBarUI(playerID, currentHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= idleDecreaseRate)
        {
            currentHealth += decayValue;
            timer = 0f;
        }

        timer += Time.deltaTime;
        HPBarManager.instance.SetHealthBarUI(playerID, currentHealth);
    }

    public void SetDecreaseRate(int rate)
    {
        decreaseRate = rate;
    }

    public void IncreasePlayerHealth(int health)
    {
        currentHealth += health;
    }

    public void DecreasePlayerHealth(int health)
    {
        currentHealth -= health;
    }

    public void Death()
    {
        currentHealth = 0f;
    }
}
