using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private GameManager gameManager;

    private void Start() => gameManager = FindObjectOfType<GameManager>();

    /// MAIN MENU, HOW TO PLAY and CREDITS
    public void PlayButton()
    {
        //For now.
        SceneManager.LoadScene("123");
    }

    public void CreditsButton() => SceneManager.LoadScene("Credits");

    public void HowToPlayButton() => SceneManager.LoadScene("HowToPlay");

    public void BackToMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }

    public void QuitGame() => Application.Quit();

    /// LEVEL SELECT
    /// Basically uses the same level, only that the difficulty is spiked with the availability of more carts.
    public void LevelSelectButtonClicked()      //All three buttons in Level Select use this method.
    {
        string clickedButtonName = EventSystem.current.currentSelectedGameObject.name;
        
        switch(clickedButtonName)
        {
            case "button_day1":
                PlayerPrefs.SetInt("levelSelectValue", 1);
                break;

            case "button_day2":
                PlayerPrefs.SetInt("levelSelectValue", 2);
                break;

            case "button_day3":
                PlayerPrefs.SetInt("levelSelectValue", 3);
                break;
        }

        Debug.Log("Level selected: " + PlayerPrefs.GetInt("levelSelectValue"));
        //PlayerPrefs.Save();
        SceneManager.LoadScene("123");
    }

    /// IN-GAME
    public void ResumeButton() => gameManager.PauseGame();
}
