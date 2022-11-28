using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoppingList : MonoBehaviour
{
    public List<string> shopList = new List<string>();

    private List<string> listOfObjects = new List<string>
    {
        "Grapes",
        "Banana",
        "Onion",
        "Orange",
        "Apple",
        "Yogurt",
        "Bread",
        "Coke",
        "Milk",
        "Soap Bottle",
        "Fringles",
        "Water Bottle"
    };

    public int listCount;
    private int temp;
    private string cartName;
    [SerializeField] private bool isEmpty;              //Needed to prevent the shopping list from having the function called with many trigger colliders present.

    private CartAgent cartAgent;

    private void Start()
    {
        RandomShoppingList();
        cartName = transform.name;
        cartAgent = GetComponent<CartAgent>();
    }

    public void RandomShoppingList()
    {
        shopList.Clear();
        listCount = Random.Range(3, 5);
        temp = 1;

        foreach(string obj in listOfObjects)
        {
            if(temp < listCount)
            {
                int randomItem = Random.Range(0, listOfObjects.Count - 1);
                shopList.Add(listOfObjects[randomItem]);
                temp++;
            }
        }
    }

    public void IfListEmpty()
    {
        if(shopList.Count == 0 && isEmpty == false)
        {
            StartCoroutine(CoListEmpty());
            isEmpty = true;
        }
    }

    private IEnumerator CoListEmpty()
    {
        yield return new WaitForSeconds(3.5f);
        Debug.Log(cartName + " is given a new shopping list.");
        RandomShoppingList();
        cartAgent.TimerReset();
        isEmpty = false;
    }
}
