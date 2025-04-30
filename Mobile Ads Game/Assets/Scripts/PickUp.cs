using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] ManageInventory manageInventory;

    private void Start()
    {
        manageInventory = FindObjectOfType<ManageInventory>();
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (gameObject.tag == "Money" && collider.gameObject.tag == "Player")
        {
            manageInventory.Add(0,1);
            Destroy(gameObject);
        }
    }
}
