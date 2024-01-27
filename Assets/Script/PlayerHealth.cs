using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float currentHealth = 100f;

    private float idleDecreaseRate = 3f;
    private float decreaseRate;
    //private float attackDamage = 10f;

    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        decreaseRate = idleDecreaseRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= idleDecreaseRate)
        {
            currentHealth -= 1f;
            timer = 0f;
        }

        timer += Time.deltaTime;
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
