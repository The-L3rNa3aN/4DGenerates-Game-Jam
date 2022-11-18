using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int cartNum;

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
    }

    private void Alpha1Selected() => cartNum = 1;
    private void Alpha2Selected() => cartNum = 2;
    private void Alpha3Selected() => cartNum = 3;

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
}