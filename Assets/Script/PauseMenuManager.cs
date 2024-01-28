using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public static PauseMenuManager instance;

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject button;
    [SerializeField] private GameObject options;
    //[SerializeField] private GameObject animator;

    private bool isDoorClose = false;
    private bool isEnable = false;

    private void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else
            instance = this;
    }

    void Start()
    {
        pauseMenu.SetActive(false);
        //animator.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetActivePauseMenu();
        }
    }

    public void SetActivePauseMenu()
    {
        if (isEnable == false)
        {
            Time.timeScale = 0f;

            pauseMenu.SetActive(true);
            button.SetActive(true);
            options.SetActive(false);

            isEnable = true;
        }
        else if (isEnable == true)
        {
            Time.timeScale = 1f;

            pauseMenu.SetActive(false);
            isEnable = false;
        }
    }

    // Pause Menu Button =====================================
    public void ResumeButton()
    {
        SetActivePauseMenu();
        AudioManager.instance.PlaySound("Click");
    }

    public void EnableOptions()
    {
        options.SetActive(true);
        button.SetActive(false);

        AudioManager.instance.PlaySound("Click");
    }

    public void BackToPauseMenu()
    {
        options.SetActive(false);
        button.SetActive(true);

        AudioManager.instance.PlaySound("Click");
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        AudioManager.instance.PlaySound("Click");
    }
}
