using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Outline = cakeslice.Outline;

/// Used in MainMenu_3D and 123. Also handles mouse over events.

public class ClickableObject : MonoBehaviour
{
    public bool isClickable;

    public UnityEvent MouseDownFunc;
    public UnityEvent MouseOverFunc;

    [Header("3D Text on hover")]
    public TextMeshPro mm_text;
    public TextMeshPro ls_text;

    [Header("Grades earned")]
    private string Day1Grade;
    private string Day2Grade;
    private string Day3Grade;

    [Header("123 Aisle Names On Hover")]
    public Text aisleName;

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
        if(SceneManager.GetActiveScene().name == "MainMenu_3D")
        {
            Day1Grade = PlayerPrefs.GetString("day1_grade");
            Day2Grade = PlayerPrefs.GetString("day2_grade");
            Day3Grade = PlayerPrefs.GetString("day3_grade");
        }
    }

    private void OnMouseDown()
    {
        if (isClickable) MouseDownFunc.Invoke();
    }
    private void OnMouseOver() => MouseOverFunc.Invoke();

    private void OnMouseExit()
    {
        //I hate them fucking errors flooding the console.
        if (mm_text != null) mm_text.gameObject.SetActive(false);
        if (ls_text != null) ls_text.gameObject.SetActive(false);
        if (aisleName != null) aisleName.gameObject.SetActive(false);

        if(SceneManager.GetActiveScene().name == "123")
        {
            Outline[] _outlineObjects = GameManager.instance.outlineObjects;
            foreach (Outline o in _outlineObjects)
                o.eraseRenderer = true;
        }
    }

    #region MAIN MENU FUNCTIONS
    public void OnClickMainMenu(int id)
    {
        switch(id)
        {
            case 1:
                Debug.Log("Navigating to PLAY");
                ThreeDCameraManager.instance.MoveCamera(ThreeDCameraManager.instance.menu.position, ThreeDCameraManager.instance.levelSelect.position);
                break;

            case 2:
                Debug.Log("Navigating to HOW TO PLAY");
                ThreeDCameraManager.instance.MoveCamera(ThreeDCameraManager.instance.menu.position, ThreeDCameraManager.instance.howToPlay.position);
                break;

            case 3:
                Debug.Log("Navigating to CREDITS");
                ThreeDCameraManager.instance.MoveCamera(ThreeDCameraManager.instance.menu.position, ThreeDCameraManager.instance.credits.position);
                break;

            case 4:
                Debug.Log("Navigating to QUIT");
                ThreeDCameraManager.instance.MoveCamera(ThreeDCameraManager.instance.menu.position, ThreeDCameraManager.instance.quit.position);
                break;
        }
    }

    public void MainMenuMouseOver(int id)
    {
        mm_text.gameObject.SetActive(true);
        switch (id)
        {
            case 1:
                mm_text.text = "Play the game";
                break;

            case 2:
                mm_text.text = "All the know-how for playing";
                break;

            case 3:
                mm_text.text = "Learn the ones behind the game (and maybe your mom)";
                break;

            case 4:
                mm_text.text = "Quit the game and go touch grass";
                break;
        }
    }
    #endregion

    #region LEVEL SELECT FUNCTIONS
    public void OnClickLevelSelected(int levelNumber)
    {
        char c = default;
        string grade = default;
        LoadingScreen loadingScreen = ThreeDCameraManager.instance.loadingScreen;
        if(levelNumber != 1)
        {
            if (levelNumber == 2)
            {
                grade = Day1Grade;
                c = StringToChar(grade);
            }
            else
            {
                grade = Day2Grade;
                c = StringToChar(grade);
            }

            if (grade == "none" || c <= 'C')
            {
                PlayerPrefs.SetInt("levelSelectValue", levelNumber);
                Debug.Log("Level selected: " + PlayerPrefs.GetInt("levelSelectValue"));
                ThreeDCameraManager.instance.SnapCamera();
                loadingScreen.LoadScene(3);
            }
            else
            {
                switch (c)
                {
                    case '2':
                        Debug.Log("Requires 'C' or higher in Day 1");
                        break;

                    case '3':
                        Debug.Log("Requires 'C' or higher in Day 2");
                        break;
                }
            }
        }
        else
        {
            PlayerPrefs.SetInt("levelSelectValue", levelNumber);
            Debug.Log("Level selected: " + PlayerPrefs.GetInt("levelSelectValue"));
            ThreeDCameraManager.instance.SnapCamera();
            loadingScreen.LoadScene(3);
        }
    }

    public void LevelSelectMouseOver(int level)
    {
        ls_text.gameObject.SetActive(true);
        switch (level)
        {
            case 1:
                ls_text.text = "Grade achieved: " + PlayerPrefs.GetString("day1_grade");
                break;

            case 2:
                ls_text.text = "Grade achieved: " + PlayerPrefs.GetString("day2_grade");
                break;

            case 3:
                ls_text.text = "Grade achieved: " + PlayerPrefs.GetString("day3_grade");
                break;
        }
    }

    public void MDReset()
    {
        PlayerPrefs.SetString("day1_grade", "none");
        PlayerPrefs.SetString("day2_grade", "none");
        PlayerPrefs.SetString("day3_grade", "none");

        Day1Grade = PlayerPrefs.GetString("day1_grade");
        Day2Grade = PlayerPrefs.GetString("day2_grade");
        Day3Grade = PlayerPrefs.GetString("day3_grade");
    }
    public void MOReset()
    {
        ls_text.gameObject.SetActive(true);
        ls_text.text = "Reset your progress, if you dare";
    }
    #endregion

    #region 123 HOVER
    public void OnHoverAisle()
    {
        if(SceneManager.GetActiveScene().name == "123")
        {
            Outline outline = gameObject.GetComponent<Outline>();
            aisleName.gameObject.SetActive(true);

            switch (gameObject.name)
            {
                case "cube_apples":
                    outline.eraseRenderer = false;
                    aisleName.text = "Apples";
                    break;

                case "cube_bananas":
                    outline.eraseRenderer = false;
                    aisleName.text = "Bananas";
                    break;

                case "cube_bread":
                    outline.eraseRenderer = false;
                    aisleName.text = "Bread";
                    break;

                case "cube_coke":
                    outline.eraseRenderer = false;
                    aisleName.text = "Coke";
                    break;

                case "cube_fringles":
                    outline.eraseRenderer = false;
                    aisleName.text = "Fringles";
                    break;

                case "cube_grapes":
                    outline.eraseRenderer = false;
                    aisleName.text = "Grapes";
                    break;

                case "cube_milk":
                    outline.eraseRenderer = false;
                    aisleName.text = "Milk";
                    break;

                case "cube_onions":
                    outline.eraseRenderer = false;
                    aisleName.text = "Onions";
                    break;

                case "cube_oranges":
                    outline.eraseRenderer = false;
                    aisleName.text = "Oranges";
                    break;

                case "cube_soap":
                    outline.eraseRenderer = false;
                    aisleName.text = "Soap Bottles";
                    break;

                case "cube_water":
                    outline.eraseRenderer = false;
                    aisleName.text = "Water Bottles";
                    break;

                case "cube_yogurt":
                    outline.eraseRenderer = false;
                    aisleName.text = "Yogurt";
                    break;
            }
        }
    }
    #endregion

    #region 123 PAUSE MENU
    public void Resume_MouseDown() => GameManager.instance.PauseGame();

    public void MainMenu_MouseDown()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu_3D");
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
        ThreeDCameraManager.instance.MoveCamera(currentPos, ThreeDCameraManager.instance.menu.position);
    }

    private char StringToChar(string str)                           //Converts any string to char. Obviously.
    {
        char[] c = str.ToCharArray();
        return c[0];
    }
}
