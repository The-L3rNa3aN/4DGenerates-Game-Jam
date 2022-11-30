using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/*Separate script for handling certain in-game UI (mostly for lesser headache).
Handles the following things: -
  * Displays the game timer, cart selected, score and the cart's respective list.
  * Functions which are used by the OnClick events for the main menu and resume
    buttons in the pause menu.*/

public class GameUIManager : MonoBehaviour
{
    [Header("In Game UI")]
    public Text t_gameTimer;
    public Text t_cartSelected;
    public Text t_score;
    public Text[] t_listItems;                                           //Assigned in editor.

    [Header("GameManager references")]
    [SerializeField] private float gameTimer;
    [SerializeField] private int cartNum;
    [SerializeField] private GameObject[] agents;
    [SerializeField] private int score;

    private void Update()
    {
        gameTimer = GameManager.instance.gameTimer;
        cartNum = GameManager.instance.cartNum;
        agents = GameManager.instance.cartAgents;
        score = GameManager.instance.score;
    }

    private void OnGUI()
    {
        t_gameTimer.text = FormatTime(gameTimer);                       //Displays the game timer.
        t_cartSelected.text = "Cart selected: " + cartNum;              //Displays the cart number currently selected.
        t_score.text = "Score: " + score;                               //Displays the score.

        foreach(Text item in t_listItems) item.text = "";

        foreach (GameObject agent in agents)
        {
            if (agent.GetComponent<CartAgent>().cartNumber == cartNum)
            {
                var list = agent.GetComponent<ShoppingList>().shopList;

                //Display the cart's respective shopping list.
                for (int i = 0; i < list.Count; i++)
                {
                    t_listItems[i].text = " - " + list[i];              //Bug in the displayed items on text.
                }
            }
        }
    }

    //PAUSE MENU.
    public void Button_MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void Button_Resume() => GameManager.instance.PauseGame();    //References for the pause panel are in GameManager.cs

    //GAME OVER SCREEN.
    public void Button_NextLevel()
    {
        int currentLevel = PlayerPrefs.GetInt("levelSelectValue");
        PlayerPrefs.SetInt("levelSelectValue", currentLevel++);
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
