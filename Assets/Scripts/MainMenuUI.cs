using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");

        if (!PlayerPrefs.HasKey("Coins")) PlayerPrefs.SetInt("Coins", 0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
