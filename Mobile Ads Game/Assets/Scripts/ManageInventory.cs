using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageInventory : MonoBehaviour
{
    [SerializeField] Transform pickUpPosition;
    public int[] inventory;
    [SerializeField] GameObject moneyPrefab;
    //int totalStocks = 0;
    [SerializeField] UIManager UIManager;

    private void Start()
    {
        inventory = new int[2];
        // 0 for Money
        // 1 for Carrot
    }

    public void Add(int id, int amount)
    {
        inventory[id] = inventory[id] + amount;
        UIManager.UpdateUI(0);
        UIManager.UpdateUI(1);
    }

    public int CheckIfHave(int id)
    {
        return inventory[id];
    }


    /*public void UpdatePile(int idPile)
{
    for (int i= 0; i<inventory.Length; i++)
    {
        totalStocks += inventory[i];
    }
    UIManager.UpdateUI(0);
    if(pickUpPosition.childCount > inventory[0])
    {
        for(int i= 0; i < (pickUpPosition.childCount - inventory[0]); i++)
        {
            Destroy(pickUpPosition.GetChild(i).gameObject);
        }
    }
    else
    {
        for(int i  = 0; i < inventory[0]; i++)
        {
            Instantiate(moneyPrefab, new Vector3(pickUpPosition.position.x, (pickUpPosition.position.y + totalStocks * 0.2f) - 0.2f, pickUpPosition.position.z), pickUpPosition.rotation, pickUpPosition);
        }
    }
}*/
}
