using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

/*Separate script for handling certain in-game UI (mostly for lesser headache).
Handles the following things: -
  * Displays the game timer, cart selected, score and the cart's respective list.
  * Functions which are used by the OnClick events for the main menu and resume
    buttons in the pause menu.*/

public class GameUIManager : MonoBehaviour
{
    public TextMeshPro t_gameTimerTMP;
    public TextMeshPro t_cartSelectedTMP;
    public TextMeshPro t_scoreTMP;
    public TextMeshPro[] t_listItemsTMP;                                //Assigned in editor.

    private void OnGUI()
    {
        t_gameTimerTMP.text = FormatTime(GameManager.instance.gameTimer);                    //Displays the game timer.

        t_cartSelectedTMP.text = "Cart selected: " + GameManager.instance.cartNum;           //Displays the cart number currently selected.

        t_scoreTMP.text = "Score: " + GameManager.instance.score;                            //Displays the score.

        foreach(TextMeshPro item in t_listItemsTMP) item.text = "";

        foreach (GameObject agent in GameManager.instance.cartAgents)
        {
            if (agent.GetComponent<CartAgent>().cartNumber == GameManager.instance.cartNum)
            {
                var list = agent.GetComponent<ShoppingList>().shopList;

                //Display the cart's respective shopping list.
                for (int i = 0; i < list.Count; i++)
                    t_listItemsTMP[i].text = " - " + list[i];              //Bug in the displayed items on text.
            }
        }
    }

    //PAUSE MENU.
    public void Button_MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void Button_Resume() => GameManager.instance.PauseGame();    //References for the pause panel are in GameManager.instance.cs

    //GAME OVER SCREEN.
    public void Button_NextLevel()
    {
        int currentLevel = PlayerPrefs.GetInt("levelSelectValue");
        PlayerPrefs.SetInt("levelSelectValue", ++currentLevel);
        Debug.Log("Level selected: " + currentLevel);
        SceneManager.LoadScene("123");
    }

    public string FormatTime(float value)
    {
        float minutes = Mathf.FloorToInt(value / 60);
        float seconds = Mathf.FloorToInt(value % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
