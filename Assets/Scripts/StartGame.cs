using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField] private string sceneName;

    public void PlayButton()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGameButton()
    {
        Application.Quit();
    }


}
