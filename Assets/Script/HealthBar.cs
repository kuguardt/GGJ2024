using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private int playerID = 0;
    [SerializeField] private Image[] healthPoints;

    private float healthTest = 88f;

    // Update is called once per frame
    void Update()
    {
        //HealthBarFilter();

        //if (Input.GetKeyDown(KeyCode.J))
        //    healthTest -= 1f;
    }

    public void HealthBarFilter(float HP)
    {
        for(int i = 0; i < healthPoints.Length; i++)
        {
            healthPoints[i].enabled = DisplayHealthPoint(HP, i, healthPoints[i]);
            SetHealthPointColor(HP, healthPoints[i]);
        }
    }

    bool DisplayHealthPoint(float currentHealth, int pointNumber, Image point)
    {
        if (currentHealth - (pointNumber * 10) <= 0)
        {
            return false;
        }
        else
        {
            if (currentHealth - (pointNumber * 10) < 10)
                point.color = new Color(point.color[0], point.color[1], point.color[2], (currentHealth - (pointNumber * 10)) / 10f);
            else
                point.color = new Color(point.color[0], point.color[1], point.color[2], 1f);

            return true;
        }
    }

    void SetHealthPointColor(float currentHealth, Image point)
    {
        if (currentHealth > 50f)
            point.color = new Color(118f / 255f, 1f, 102f / 255f, point.color[3]); // green: HP 51-100
        else if (currentHealth > 25f)
            point.color = new Color(253f / 255f, 255f / 255f, 0f, point.color[3]); // yellow: HP 26-50
        else
            point.color = new Color(1f, 0f, 0f, point.color[3]); // red
    }
}
