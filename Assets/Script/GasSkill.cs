using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasSkill : MonoBehaviour
{
    [SerializeField] private float GasLifeTime = 1f;
    private float timePassed = 0f;

    [SerializeField] private float alphaStart = 0.5f;

    private SpriteRenderer sr;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
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
}
