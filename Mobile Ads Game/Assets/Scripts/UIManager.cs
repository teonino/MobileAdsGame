using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] ManageInventory inventory;
    [SerializeField] List<TMP_Text> UIs;
    public void UpdateUI(int id)
    {
        UIs[id].text = inventory.inventory[id].ToString();
    }
}
