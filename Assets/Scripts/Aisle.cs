using UnityEngine;

public class Aisle : MonoBehaviour
{
    public string product;

    private void OnTriggerEnter(Collider other)
    {
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
