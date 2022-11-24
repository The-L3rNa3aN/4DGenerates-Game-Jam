using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Grades earned")]
    private string Day1Grade;
    private string Day2Grade;
    private string Day3Grade;

    [Header("Buttons - Level Select")]
    [SerializeField] private Button day2;
    [SerializeField] private Button day3;

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
        Day1Grade = PlayerPrefs.GetString("day1_grade");
        Day2Grade = PlayerPrefs.GetString("day2_grade");
        Day3Grade = PlayerPrefs.GetString("day3_grade");

        DisableButtons();
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

    public void ResetButton()
    {
        PlayerPrefs.SetString("day1_grade", "none");
        PlayerPrefs.SetString("day2_grade", "none");
        PlayerPrefs.SetString("day3_grade", "none");

        day2.interactable = false;
        day3.interactable = false;
    }

    /// IN-GAME
    public void ResumeButton() => GameManager.instance.PauseGame();

    public void NextLevelButton()
    {
        int currentLevel = PlayerPrefs.GetInt("levelSelectValue");
        PlayerPrefs.SetInt("levelSelectValue", currentLevel++);
        Debug.Log("Level selected: " + currentLevel);
        SceneManager.LoadScene("123");
    }

    private void DisableButtons()                                   //Disables buttons in level select.
    {
        ///Buttons for levels 2 and 3 are disabled if their respective previous levels don't have a 
        ///grade higher or equal to C.
        
        if (StringToChar(Day1Grade) > 'C' || Day1Grade == "none")   //Enable / disable button day2.
            day2.interactable = false;
        else
            day2.interactable = true;

        if (StringToChar(Day2Grade) > 'C' || Day2Grade == "none")   //Enable / disable button day3.
            day3.interactable = false;
        else
            day3.interactable = true;
    }

    private char StringToChar(string str)                           //Converts any string to char. Obviously.
    {
        char[] c = str.ToCharArray();
        return c[0];
    }
}
