using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryPlayer : MonoBehaviour
{
    [ColorUsageAttribute(true, true, 0f, 8f, 0.125f, 3f)] [SerializeField]
    List<Color> playerColors = new List<Color>() { Color.red, Color.blue, Color.green, Color.yellow };

    [SerializeField] private TextMeshProUGUI text;
    // Start is called before the first frame update
    int winI = 0;

    void Start()
    {
        winI = PlayerConfigurationManager.instance.winPlayerIndex;
        //winI = 2;
        GetComponent<SpriteRenderer>().material.color = playerColors[winI];

        GetComponent<Animator>().SetBool("isGrounded", true);

       // InvokeRepeating(nameof(swapColor), 0, 0.5f);

        int count = winI + 1;
        text.color= playerColors[winI];
        text.text = $"PLAYER {count}\n" +
                    $"STOLE THE STOOL!";
        
        Invoke(nameof(GoMenu), 10f);
    }

    private void swapColor()
    {
        winI = (winI + 1) % 4;
        GetComponent<SpriteRenderer>().material.color = playerColors[winI];

    }
    
    private void GoMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
