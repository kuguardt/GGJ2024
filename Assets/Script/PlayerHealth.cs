using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public bool IsAlive => currentHealth > 0f;
    [SerializeField] private float currentHealth = 100f;

    PlayerController playerController;

    private float idleDecreaseRate = 1f;
    private float decreaseRate;
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
        
        if (timer >= idleDecreaseRate && IsAlive)
        {
            currentHealth -= 1f;
            timer = 0f;
        }

        timer += Time.deltaTime;
        
        if (currentHealth <= 0f)
        {
            Death();
        }
        
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
        GetComponent<Animator>().Play("Death");
    }
}
