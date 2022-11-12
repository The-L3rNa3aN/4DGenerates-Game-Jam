using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aisle : MonoBehaviour
{
    public string product;

    private void OnTriggerEnter(Collider other)
    {
        var cart = other.GetComponent<CartAgent>();

        if(cart) /*other.GetComponent<ShoppingList>()*/
        {
            /*Debug.Log(other.GetComponent<CartAgent>().cartNumber + ", " + product);
            var list = other.GetComponent<ShoppingList>();

            for(int i = 0; i < list.shopList.Count; i++)
            {
                if(list.shopList[i] == product)
                {
                    list.shopList.RemoveAt(i);
                }
            }*/
            Debug.Log(cart.cartNumber);
        }
    }
}
