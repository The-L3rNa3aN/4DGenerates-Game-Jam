using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// Used only in the MainMenu_3D scene. Also handles mouse over events.

public class ClickableObject : MonoBehaviour
{
    public UnityEvent MouseDownFunc;
    public UnityEvent MouseOverFunc;

    [Header("ThreeDCameraManager position references")]
    private Vector3 menu;
    private Vector3 levelSelect;
    private Vector3 howToPlay;
    private Vector3 credits;
    private Vector3 quit;

    [Header("3D Text on hover")]
    public TextMeshPro mm_text;
    public TextMeshPro ls_text;

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
        menu = ThreeDCameraManager.instance.menu.position;
        levelSelect = ThreeDCameraManager.instance.levelSelect.position;
        howToPlay = ThreeDCameraManager.instance.howToPlay.position;
        credits = ThreeDCameraManager.instance.credits.position;
        quit = ThreeDCameraManager.instance.quit.position;

        Day1Grade = PlayerPrefs.GetString("day1_grade");
        Day2Grade = PlayerPrefs.GetString("day2_grade");
        Day3Grade = PlayerPrefs.GetString("day3_grade");
    }

    private void OnMouseDown() => MouseDownFunc.Invoke();
    private void OnMouseOver() => MouseOverFunc.Invoke();

    private void OnMouseExit()
    {
        //I hate them fucking errors flooding the console.
        if(mm_text != null) mm_text.gameObject.SetActive(false);
        if(ls_text != null) ls_text.gameObject.SetActive(false);
    }

    #region MAIN MENU FUNCTIONS
    public void MDPlay()
    {
        Debug.Log("Navigating to PLAY");
        ThreeDCameraManager.instance.MoveCamera(menu, levelSelect);
    }
    public void MOPlay()
    {
        mm_text.gameObject.SetActive(true);
        mm_text.text = "Play the game";
    }

    public void MDHowToPlay()
    {
        Debug.Log("Navigating to HOW TO PLAY");
        ThreeDCameraManager.instance.MoveCamera(menu, howToPlay);
    }
    public void MOHowToPlay()
    {
        mm_text.gameObject.SetActive(true);
        mm_text.text = "All the know-how for playing";
    }

    public void MDCredits()
    {
        Debug.Log("Navigating to CREDITS");
        ThreeDCameraManager.instance.MoveCamera(menu, credits);
    }
    public void MOCredits()
    {
        mm_text.gameObject.SetActive(true);
        mm_text.text = "Learn the ones behind the game (and maybe your mom)";
    }

    public void MDQuit()
    {
        Debug.Log("Navigating to QUIT");
        ThreeDCameraManager.instance.MoveCamera(menu, quit);
    }
    public void MOQuit()
    {
        mm_text.gameObject.SetActive(true);
        mm_text.text = "Quit the game and go touch grass";
    }
    #endregion

    #region LEVEL SELECT FUNCTIONS
    public void MDDay1()
    {
        PlayerPrefs.SetInt("levelSelectValue", 1);
        Debug.Log("Level selected: " + PlayerPrefs.GetInt("levelSelectValue"));
        SceneManager.LoadScene("123");
    }
    public void MODay1()
    {
        ls_text.gameObject.SetActive(true);
        ls_text.text = "Grade achieved: " + PlayerPrefs.GetString("day1_grade");
    }

    public void MDDay2()
    {
        if(StringToChar(Day1Grade) <= 'C')
        {
            PlayerPrefs.SetInt("levelSelectValue", 2);
            Debug.Log("Level selected: " + PlayerPrefs.GetInt("levelSelectValue"));
            SceneManager.LoadScene("123");
        }
    }
    public void MODay2()
    {
        ls_text.gameObject.SetActive(true);
        ls_text.text = "Grade achieved: " + PlayerPrefs.GetString("day2_grade");
    }

    public void MDDay3()
    {
        if (StringToChar(Day2Grade) <= 'C')
        {
            PlayerPrefs.SetInt("levelSelectValue", 3);
            Debug.Log("Level selected: " + PlayerPrefs.GetInt("levelSelectValue"));
            SceneManager.LoadScene("123");
        }
    }
    public void MODay3()
    {
        ls_text.gameObject.SetActive(true);
        ls_text.text = "Grade achieved: " + PlayerPrefs.GetString("day3_grade");
    }
    #endregion

    public void Yes()
    {
        PlayerPrefs.Save();
        Application.Quit();
    }

    public void Back()                                              //Used for the back button and the "no" option before quitting.
    {
        Vector3 currentPos = ThreeDCameraManager.instance.transform.position;
        ThreeDCameraManager.instance.MoveCamera(currentPos, menu);
    }

    private char StringToChar(string str)                           //Converts any string to char. Obviously.
    {
        char[] c = str.ToCharArray();
        return c[0];
    }
}
