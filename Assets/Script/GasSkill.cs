using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasSkill : MonoBehaviour
{
    [SerializeField] private float GasDamage = 10f;
    [SerializeField] private float GasForce = 10f;
    
    [SerializeField] private float GasLifeTime = 1f;
    private float timePassed = 0f;

    [SerializeField] private float alphaStart = 0.5f;

    private SpriteRenderer sr;

    private HashSet<GameObject> ignorePlayer = new HashSet<GameObject>();
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();


        Destroy(transform.GetChild(0).gameObject,3f);
        transform.DetachChildren();
    }

    // Update is called once per frame
    
    public void AddGasIgnore(GameObject player)
    {
        ignorePlayer.Add(player);
    }
    
    void Update()
    {
        timePassed += Time.deltaTime;
        timePassed = Math.Min(timePassed, GasLifeTime);
        float percent = timePassed / GasLifeTime;
        var color = sr.color;
        color.a = alphaStart * (1 - percent);
        sr.color = color;
        
        if (percent >= 1f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject obj = other.gameObject;
        if (other.CompareTag("Player") &&  !ignorePlayer.Contains(obj))
        {
            AddGasIgnore(obj);
            Debug.Log($"{other.name} is attacked by {gameObject.name}");
            obj.GetComponent<PlayerHealth>().DecreasePlayerHealth(GasDamage);
            
            Vector2 dir = (obj.transform.position - transform.position).normalized;
            obj.GetComponent<PlayerMovement>().GotAttacked(dir*GasForce);
        }
    }
}
