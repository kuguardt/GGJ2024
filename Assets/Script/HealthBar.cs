using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private int playerID = 0;
    [SerializeField] private Image[] healthPoints;

    private float healthTest = 88f;

    // TO-DO: get player health

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerController.playerCount <= playerID)
        {
            this.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        HealthBarFilter();
    }

    void HealthBarFilter()
    {
        for(int i = 0; i < healthPoints.Length; i++)
        {
            healthPoints[i].enabled = DisplayHealthPoint(healthTest, i, healthPoints[i]);
            SetHealthPointColor(healthTest, healthPoints[i]);
        }
    }

    bool DisplayHealthPoint(float currentHealth, int pointNumber, Image point)
    {
        if (currentHealth - (pointNumber * 10) <= 0)
        {
            return false;
        }
        else if (currentHealth - (pointNumber * 10) < -10)
        {
            point.color = new Color(point.color[0], point.color[1], point.color[2], 0.1f);
            return true;
        }
        else
            return true;
        //return ((pointNumber * 10) <= currentHealth);
    }

    void SetHealthPointColor(float currentHealth, Image point)
    {
        if (currentHealth > 50f)
            point.color = new Color(118f / 255f, 1f, 102f / 255f);
        else if (currentHealth > 25f)
            point.color = new Color(253f / 255f, 255f / 255f, 0f);
        else
            point.color = Color.red;
    }

    void SetHealthPointAlpha()
    {

    }
}
