using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class SellingPlace : MonoBehaviour
{
    [SerializeField] int idSellingPlace;
    [SerializeField] List<Sprite> idBuyingPlaceSpriteList;
    [SerializeField] List<int> idBuyingPlacePriceList;
    [SerializeField] Image idBuyingPlaceImage;
    [SerializeField] ManageInventory inventory;
    [SerializeField] TMP_Text PriceText;

    [SerializeField] GameObject SoldText;
    [SerializeField] GameObject NothingToSellText;


    Vector3 spawnPlace;
    void Start()
    {
        inventory = FindObjectOfType<ManageInventory>();
        idBuyingPlaceImage.sprite = idBuyingPlaceSpriteList[idSellingPlace];
        PriceText.text = ("1 for " + idBuyingPlacePriceList[idSellingPlace]).ToString(); 
    }

    private void OnTriggerStay(Collider collision)
    {
        bool canSell = inventory.CheckIfHave(idSellingPlace) > 0;
        if ( canSell && collision.gameObject.tag == "Player")
        {
            Sell();
        }
        else
        {
            Debug.Log("Pas assez de ressources");
        }
    }

    void Sell()
    {
        inventory.Add(idSellingPlace, -1);
        inventory.Add(0, 2);
        Instantiate(SoldText, idBuyingPlaceImage.transform);
        Destroy(SoldText, 1f);
    }
}
