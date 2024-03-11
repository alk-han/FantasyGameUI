using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void LoadGameButton()
    {
        SceneManager.LoadScene(3);
    }


    public void NewGameButton()
    {
        SceneManager.LoadScene(2);
    }


    public void SettingsButton()
    {
        SceneManager.LoadScene(1);
    }


    public void ExitButton()
    {
        Application.Quit();
    }
}
