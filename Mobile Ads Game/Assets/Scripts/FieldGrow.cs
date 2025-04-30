using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FieldGrow : MonoBehaviour
{
    public bool isHarvestable = false;
    [SerializeField] float growTime;
    [SerializeField] Image filledImage;
    [SerializeField] TMP_Text Counter;
    private float timeElapsed = 0f;
    [SerializeField] Animator animator;
    [SerializeField] ManageInventory inventory;

    private void Start()
    {
        StartCoroutine(Grow());
        inventory = FindObjectOfType<ManageInventory>();
    }
    void Update()
    {
        if (!isHarvestable)
        {
            timeElapsed += Time.deltaTime;
            filledImage.fillAmount = Mathf.Lerp(0, 1, timeElapsed / growTime);
            Counter.text = (Mathf.Round(growTime - timeElapsed)+1).ToString();
        }
        else
        {
            Counter.text = "Ready";
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (isHarvestable && collision.gameObject.tag == "Player")
        {

            Harvest();
        }
    }

    IEnumerator Grow()
    {
        Debug.Log("�a pousse");
        yield return new WaitForSeconds(growTime);
        isHarvestable = true;
        animator.SetTrigger("Grown");
    }

    void Harvest()
    {
        Debug.Log("Harvest");
        inventory.Add(1, 1);
        isHarvestable = false;
        timeElapsed = 0f;
        filledImage.fillAmount = 0;
        animator.SetTrigger("Degrown");
        StartCoroutine(Grow());
    }
}
