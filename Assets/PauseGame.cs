using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{

    private bool isPause = false;
    private CanvasGroup menu;

    private void Start()
    {
        menu = GetComponent<CanvasGroup>();

        menu.alpha = 0;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (isPause == false)
            {
                Pause();
                
            }
            else
            {
                Resume();
                
            }
        }
    }

    void Pause()
    {
        Time.timeScale = 0;
        isPause = true;
        menu.alpha = 1;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        isPause = false;
        menu.alpha = 0;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("menu");
    }
}
