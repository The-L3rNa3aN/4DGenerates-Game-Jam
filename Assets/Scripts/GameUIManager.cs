using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//Separate script for handling the in-game UI (mostly for lesser headache).

public class GameUIManager : MonoBehaviour
{
    [Header("In Game UI - 123")]
    public Text t_gameTimer;
    public Text t_cartSelected;
    public Text[] t_listItems;              //Assigned in editor.

    [Header("GameManager references")]
    [SerializeField] private float gameTimer;
    [SerializeField] private int cartNum;
    [SerializeField] private GameObject[] agents;

    private void OnGUI()
    {
        gameTimer = GameManager.instance.gameTimer;
        cartNum = GameManager.instance.cartNum;
        agents = GameManager.instance.cartAgents;

        t_gameTimer.text = FormatTime(gameTimer);
        t_cartSelected.text = "Cart selected: " + cartNum.ToString();

        foreach (GameObject agent in agents)
        {
            if (agent.GetComponent<CartAgent>().cartNumber == cartNum)
            {
                var list = agent.GetComponent<ShoppingList>().shopList;

                //Display the cart's respective shopping list.
                for (int i = 0; i < list.Count; i++)
                {
                    t_listItems[i].text = list[i];
                }
            }
        }
    }

    public string FormatTime(float value)
    {
        float minutes = Mathf.FloorToInt(value / 60);
        float seconds = Mathf.FloorToInt(value % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
