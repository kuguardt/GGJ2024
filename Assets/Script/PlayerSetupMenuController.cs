using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSetupMenuController : MonoBehaviour
{
    private int playerIndex;

    [SerializeField]
    private TextMeshProUGUI titleText;
    [SerializeField]
    private GameObject readyPanel;
    
    [SerializeField]
    private GameObject inputGroup;
    [SerializeField]
    private Image promptImage;
    [SerializeField]
    private Button readyButton;
    [SerializeField]
    private TextMeshProUGUI readyButtonText;
    
    [SerializeField]
    private TextMeshProUGUI ready;
    
    [SerializeField] List<Sprite> buttonIcons = new List<Sprite>();
    
    private float ignoreInputTime = 1.5f;
    private bool inputEnabled;
    
    [ColorUsageAttribute(true, true, 0f, 8f, 0.125f, 3f)] [SerializeField]
    List<Color> playerColors = new List<Color>() { Color.red, Color.blue, Color.green, Color.yellow };

    public void setPlayerIndex(int pi)
    {
        playerIndex = pi;
        titleText.SetText("Player " + (pi + 1).ToString());
        ignoreInputTime = Time.time + ignoreInputTime;
        titleText.color = playerColors[playerIndex];
    }

    public void SetUI(int type)
    {
        promptImage.sprite = buttonIcons[type];
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > ignoreInputTime)
        {
            inputEnabled = true;
        }
    }

    public void SelectColor(string color)
    {
        if (!inputEnabled) { return; }

        PlayerConfigurationManager.instance.SetPlayerColor(playerIndex, color);
        readyPanel.SetActive(true);
        readyButton.interactable = true;
        readyButton.Select();

    }

    public void ReadyPlayer()
    {
        if (!inputEnabled) { return; }

        PlayerConfigurationManager.instance.ReadyPlayer(playerIndex);
        inputGroup.SetActive(false);
        
        ready.gameObject.SetActive(true);
    }
}