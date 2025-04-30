using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Build : MonoBehaviour
{
    [SerializeField] Sprite constructionImage;
    [SerializeField] SpriteRenderer constructionImageRenderer;
    public int cost;
    [SerializeField] TMP_Text constructionPrice;
    [SerializeField] ManageInventory manageInventory;
    SphereCollider sphereCollider;
    [SerializeField] UIManager uiManager;
    [SerializedDictionary("ID String", "Prefab")] public SerializedDictionary<string, GameObject> buildings;
    [SerializeField] string type;

    void Start()
    {
        constructionImageRenderer.sprite = constructionImage;
        constructionPrice.text = cost.ToString();
        sphereCollider = gameObject.GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (gameObject.tag == "Construction" && collider.gameObject.tag == "Player")
        {
            BuildSite();
            StartCoroutine(WaitforCollideAgain());
        }
    }
    void BuildSite()
    {
        if(cost>0 && manageInventory.inventory[0]>0)
        {
            cost -= 1;
            manageInventory.inventory[0] -= 1;
            constructionPrice.text = cost.ToString();
            manageInventory.Add(0, 0);
            
        }
        else if(cost <=0)
        {
            Debug.Log("Built");
            Construct(type, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("PLUS DE SOUS");
        }
    }

    IEnumerator WaitforCollideAgain()
    {
        sphereCollider.enabled = false;
        yield return new WaitForSeconds(0.5f);
        sphereCollider.enabled = true;
    }

    GameObject Construct(string key, Vector3 position, Quaternion rotation)
    {
        if (buildings.TryGetValue(key, out GameObject prefab))
        {
            return Instantiate(prefab, position, rotation);
        }
        else
        {
            Debug.Log("Clé non trouvée");
            return null;
        }
    }
}
