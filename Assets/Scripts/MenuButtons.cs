using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour {

    public GameObject Button;
    public GameObject PressedButton;

    void Start()
    {
        
    }

    public void StartNewGame()
    {
        PressButton();
        Debug.Log("Starting new game!");
        SceneManager.LoadScene(GameManager.instance.FirstLevel);
    }

    public void LoadSave()
    {
        PressButton();
    }

    public void OpenSettings()
    {
        PressButton();
        Debug.Log("Open Settings");
        SceneManager.LoadScene(GameManager.instance.SettingsMenu);
    }

    public void LoadNextLevel()
    {
        Debug.Log("Loading next level");
        SceneManager.LoadScene(GameManager.instance.NextLevel);
    }

    public void LoadMainMenu()
    {
        Debug.Log("Load main menu");
        PressButton();
        SceneManager.LoadScene(GameManager.instance.MainMenu);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void PressButton()
    {
        Button.gameObject.SetActive(false);
        PressedButton.gameObject.SetActive(true);
    }
}
