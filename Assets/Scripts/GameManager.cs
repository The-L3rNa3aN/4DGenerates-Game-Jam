using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int cartNum;
    public int levelSelected;

    [Header("Pause Menu")]
    public GameObject pauseMenu;
    public bool isPaused;

    [Header("Other References")]
    private GameObject cameraManager;
    [SerializeField] private GameObject[] cartAgents;

    [Header("Player Score")]
    public int score = 0;

    private void Start()
    {
        cartNum = 1;
        InputManager.escInput += PauseGame;
        InputManager.num1Input += Alpha1Selected;
        InputManager.num2Input += Alpha2Selected;
        InputManager.num3Input += Alpha3Selected;

        cameraManager = FindObjectOfType<CameraManager>().gameObject;
        levelSelected = PlayerPrefs.GetInt("levelSelectValue");
    }

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

    public void AddScore(int add) => score += add;
}
