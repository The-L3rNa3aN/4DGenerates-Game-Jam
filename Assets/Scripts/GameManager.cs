using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int cartNum;
    public int levelSelected;
    public static GameManager instance { get; private set; }

    [Header("Player Score")]
    public int score = 0;
    private char grade, oldGrade;

    [Header("Time")]
    public float gameTimer;         //Measured in seconds.
    public bool isTimerRunning;

    [Header("On Start Countdown")]
    public GameObject countdownPanel;
    public GameObject num3;
    public GameObject num2;
    public GameObject num1;

    [Header("Pause Menu")]
    public GameObject pauseMenu;
    public bool isPaused;

    [Header("Game Over Screen")]
    public GameObject endScreen;
    public Text gradeEarned;
    public GameObject newGradeNotif;
    public Button nextLevel;

    [Header("Other References")]
    private GameObject cameraManager;
    public GameObject[] cartAgents;

    private void Awake()
    {
        //Singleton
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;
    }

    private void Start()
    {
        cartNum = 1;
        InputManager.escInput += PauseGame;
        InputManager.num1Input += Alpha1Selected;
        InputManager.num2Input += Alpha2Selected;
        InputManager.num3Input += Alpha3Selected;

        cameraManager = FindObjectOfType<CameraManager>().gameObject;
        levelSelected = PlayerPrefs.GetInt("levelSelectValue");

        switch(levelSelected)                           //Set gameTimer based on the difficulty chosen.
        {
            case 1:
                gameTimer = 240f;
                oldGrade = StringToChar(PlayerPrefs.GetString("day1_grade"));
                break;

            case 2:
                gameTimer = 300f;
                oldGrade = StringToChar(PlayerPrefs.GetString("day2_grade"));
                break;

            case 3:
                gameTimer = 360f;
                oldGrade = StringToChar(PlayerPrefs.GetString("day3_grade"));
                break;
        }

        StartCoroutine(CoBeginCountdown());             //Start a "3-2-1" countdown for the player to prepare.
    }

    private void Update()
    {
        if (isTimerRunning) RunTimer();

        if(gameTimer <= 0f)
        {
            isTimerRunning = false;
            Debug.Log("Time's up!");

            foreach (GameObject cart in cartAgents)
                cart.GetComponent<CartAgent>().enabled = false;
            cameraManager.GetComponent<CameraManager>().enabled = false;

            GetGradeOnEnd();

            if(grade < oldGrade)
            {
                switch(levelSelected)
                {
                    case 1:
                        PlayerPrefs.SetString("day1_grade", grade.ToString());
                        break;

                    case 2:
                        PlayerPrefs.SetString("day2_grade", grade.ToString());
                        break;

                    case 3:
                        PlayerPrefs.SetString("day3_grade", grade.ToString());
                        break;
                }
                PlayerPrefs.Save();
            }
        }
    }

    private void OnGUI()
    {
        if(gameTimer <= 0f)
        {
            StartCoroutine(CoEndScreen());

            if (grade < oldGrade)
                newGradeNotif.gameObject.SetActive(true);

            if(StringToChar(PlayerPrefs.GetString("day1_grade")) <= 'C')
                nextLevel.interactable = true;

            if (levelSelected == 3)                             //The NextLevel button is disabled when the player completes the final level.
                nextLevel.gameObject.SetActive(false);

            gradeEarned.text = "Grade earned: " + grade;
        }
    }

    #region Key Presses: 1, 2, 3, Esc
    private void Alpha1Selected() => cartNum = 1;

    private void Alpha2Selected()
    {
        if (levelSelected >= 2)
            cartNum = 2;
    }

    private void Alpha3Selected()
    {
        if (levelSelected == 3)
            cartNum = 3;
    }

    public void PauseGame()
    {
        if(isPaused == false)
        {
            isPaused = true;
            cameraManager.GetComponent<CameraManager>().enabled = false;

            foreach (GameObject cart in cartAgents)
                cart.GetComponent<CartAgent>().enabled = false;

            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
        }
        else
        {
            isPaused = false;
            cameraManager.GetComponent<CameraManager>().enabled = true;

            foreach (GameObject cart in cartAgents)
                cart.GetComponent<CartAgent>().enabled = true;

            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
        }
    }
    #endregion

    public void AddScore(int add) => score += add;

    private void RunTimer() => gameTimer -= Time.deltaTime;

    private IEnumerator CoBeginCountdown()
    {
        foreach(GameObject cart in cartAgents)                      //Disable interactions such as the camera and the carts until the countdown is over.
            cart.GetComponent<CartAgent>().enabled = false;
        cameraManager.GetComponent<CameraManager>().enabled = false;

        countdownPanel.SetActive(true);
        num3.SetActive(true);
        Debug.Log("3...");

        yield return new WaitForSeconds(1f);
        num3.SetActive(false);
        num2.SetActive(true);
        Debug.Log("2...");

        yield return new WaitForSeconds(1f);
        num2.SetActive(false);
        num1.SetActive(true);
        Debug.Log("1...");
        
        yield return new WaitForSeconds(1f);
        num1.SetActive(false);
        countdownPanel.SetActive(false);

        foreach (GameObject cart in cartAgents)                     //Enable the camera and carts now that the countdown is over.
        {
            cart.GetComponent<CartAgent>().enabled = true;
            cart.GetComponent<CartAgent>().runTimer = true;         //Enable the timer for all carts. TimerRun() in CartAgent.cs will take care of the rest.
        }

        cameraManager.GetComponent<CameraManager>().enabled = true;

        isTimerRunning = true;                                      //Start the game timer.
    }

    private IEnumerator CoEndScreen()
    {
        yield return new WaitForSeconds(2f);
        endScreen.SetActive(true);
    }

    private void GetGradeOnEnd()
    {
        switch(levelSelected)
        {
            case 1:
                if (score >= 0 && score <= 10)
                    grade = 'F';
                else if (score >= 10 && score <= 30)
                    grade = 'D';
                else if (score >= 30 && score <= 50)
                    grade = 'C';
                else if (score >= 50 && score <= 70)
                    grade = 'B';
                else if (score >= 70 && score <= 90)
                    grade = 'A';
                break;

            case 2:
                if (score >= 0 && score <= 30)
                    grade = 'F';
                else if (score >= 30 && score <= 60)
                    grade = 'D';
                else if (score >= 60 && score <= 90)
                    grade = 'C';
                else if (score >= 90 && score <= 120)
                    grade = 'B';
                else if (score >= 120 && score <= 150)
                    grade = 'A';
                break;

            case 3:
                if (score >= 0 && score <= 50)
                    grade = 'F';
                else if (score >= 50 && score <= 100)
                    grade = 'D';
                else if (score >= 100 && score <= 150)
                    grade = 'C';
                else if (score >= 150 && score <= 200)
                    grade = 'B';
                else if (score >= 200 && score <= 250)
                    grade = 'A';
                break;
        }
    }

    private char StringToChar(string str)                           //Converts any string to char. Obviously.
    {
        char[] c = str.ToCharArray();
        return c[0];
    }
}
