using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuAction : MonoBehaviour
{
    [SerializeField] private GameObject optionMenu;
    [SerializeField] private GameObject creditMenu;

    private void Start()
    {
        AudioManager.instance.PlaySound("BGM1_Fanky_Crib");
    }

    public void ButtonPlay()
    {
        SceneManager.LoadScene("ConfigurationRoom");
    }
    public void ButtonOption()
    {
        optionMenu.SetActive(true);
    }
    public void ButtonCloseOption()
    {
        optionMenu.SetActive(false);
    }
    public void ButtonCredit()
    {
        creditMenu.SetActive(true);
    }
    public void ButtonCloseCredit()
    {
        creditMenu.SetActive(false);
    }
    public void ButtonExit()
    {
        Application.Quit();
    }
}
