using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    public static PauseMenuManager instance;

    [SerializeField] private GameObject pauseMenu;
    //[SerializeField] private GameObject animator;

    private bool isDoorClose = false;

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

    }

    public void SetActivePauseMenu()
    {
        if (isDoorClose == false)
        {
            Time.timeScale = 0f;

            pauseMenu.SetActive(true);
            isDoorClose = true;
        }
        else if (isDoorClose == true)
        {
            Time.timeScale = 1f;

            pauseMenu.SetActive(false);
            isDoorClose = false;
        }
    }

    public void ResumeButton()
    {
        SetActivePauseMenu();
        Debug.Log("Resume");
    }
}
