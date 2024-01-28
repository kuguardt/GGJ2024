using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public bool IsAlive => currentHealth > 0f;
    public bool IsFullHealth => currentHealth >= maxHeatlh;
    [SerializeField] private float currentHealth = 100f;

    [SerializeField]private float maxHeatlh = 100f;
    
    PlayerController playerController;

    private float idleDecreaseRate = 1f;
    private float decreaseRate;
    //private float attackDamage = 10f;

    private float decayValue = -1f;

    private float timer = 0f;
    private int playerID;

    public void SetMaxHealth(float health)
    {
        maxHeatlh = currentHealth = health;
        UpdateUI();
    }
    void Start()
    {
        decreaseRate = idleDecreaseRate;

        playerController = GetComponent<PlayerController>();
        playerID = playerController.GetPlayerID();

        HPBarManager.instance.SetActiveHealthBar(playerID);
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (timer >= idleDecreaseRate && IsAlive)
        {
            currentHealth += decayValue;
            timer = 0f;
        }

        timer += Time.deltaTime;

        if (currentHealth > maxHeatlh)
        {
            currentHealth = maxHeatlh;    
        }
        
        if (currentHealth <= 0f)
        {
            Death();
        }
        
        UpdateUI();
    }

    private void UpdateUI()
    {
        float healthPercent = (currentHealth/ maxHeatlh) * 100.0f;
        HPBarManager.instance.SetHealthBarUI(playerID, healthPercent);
    }

    public void SetDecayValue(float value)
    {
        decayValue = value;
    }

    public void SetDecreaseRate(float rate)
    {
        decreaseRate = rate;
    }

    public void IncreasePlayerHealth(float health)
    {
        currentHealth += health;
    }

    private Coroutine iFrameCour = null;

    IEnumerator iFrame(float health)
    {
        currentHealth -= health;
        yield return new WaitForSeconds(0.3f);
        iFrameCour = null;
    }

    public void DecreasePlayerHealth(float health)
    {
        if (iFrameCour == null)
        {
            iFrameCour = StartCoroutine(iFrame(health));
        }

    }

    public void Death()
    {
        currentHealth = 0f;
        GetComponent<Animator>().Play("Death");
        if(dieCour == null) dieCour = StartCoroutine(Die());
    }
    
    [SerializeField] float timeScaleIncreaseRate = 1f;

    private Coroutine dieCour = null;
    IEnumerator Die()
    {
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(0.15f);
        //Time.timeScale = 1f;
        yield return new WaitUntil(() =>
        {
            Time.timeScale += Time.unscaledDeltaTime * timeScaleIncreaseRate;
            if (Time.timeScale >= 1f)
            {
                Time.timeScale = 1f;
                return true;
            }
        
            return false;
        });
    }
}
