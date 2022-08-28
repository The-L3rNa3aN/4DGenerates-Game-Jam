using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int cartNum;
    public GameObject pauseMenu;

    private void Start()
    {
        cartNum = 1;
    }

    private void Update()
    {
        PauseGame();

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            cartNum = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            cartNum = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            cartNum = 3;
        }
    }

    private void PauseGame()
    {
        /*if(Input.GetKeyDown(KeyCode.Escape) && pauseMenu.activeSelf == false)
        {
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
        }*/

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
