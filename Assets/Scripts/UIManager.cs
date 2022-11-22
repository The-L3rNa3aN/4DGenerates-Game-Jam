using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private GameManager gameManager;

    [Header("Grades earned")]
    private string Day1Grade;
    private string Day2Grade;
    private string Day3Grade;

    private void Awake()
    {
        string grade1 = PlayerPrefs.GetString("day1_grade");
        string grade2 = PlayerPrefs.GetString("day2_grade");
        string grade3 = PlayerPrefs.GetString("day3_grade");

        if (grade1 == "")
            PlayerPrefs.SetString("day1_grade", "none");

        if (grade2 == "")
            PlayerPrefs.SetString("day2_grade", "none");

        if (grade3 == "")
            PlayerPrefs.SetString("day3_grade", "none");
    }

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        Day1Grade = PlayerPrefs.GetString("day1_grade");
        Day2Grade = PlayerPrefs.GetString("day2_grade");
        Day3Grade = PlayerPrefs.GetString("day3_grade");
    }

    /// MAIN MENU, HOW TO PLAY and CREDITS
    public void PlayButton() => SceneManager.LoadScene("LevelSelect");

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
