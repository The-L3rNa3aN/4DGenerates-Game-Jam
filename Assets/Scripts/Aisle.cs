using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aisle : MonoBehaviour
{
    public string product;

    private void OnTriggerEnter(Collider other)
    {
        var cart = other.GetComponent<CartAgent>();
        var shoppingList = other.GetComponent<ShoppingList>();

        if(shoppingList)
        {
            for(int i = 0; i < shoppingList.shopList.Count; i++)
            {
                if (shoppingList.shopList[i] == product)
                {
                    shoppingList.shopList.RemoveAt(i);
                }
            }
            shoppingList.IfListEmpty();
        }
    }
}
