using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int cartNum;
    public int levelSelected;

    [Header("Player Score")]
    public int score = 0;

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

    [Header("Other References")]
    private GameObject cameraManager;
    [SerializeField] private GameObject[] cartAgents;

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
                break;

            case 2:
                gameTimer = 300f;
                break;

            case 3:
                gameTimer = 360f;
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
            cart.GetComponent<CartAgent>().enabled = true;
        cameraManager.GetComponent<CameraManager>().enabled = true;

        isTimerRunning = true;                                      //Start the game timer.
    }
}
