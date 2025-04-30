using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlace : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] Camera buildCamera;
    [SerializeField] GameObject clicImage;

    void Start()
    {
        mainCamera.enabled = true;
        buildCamera.enabled = false;
        clicImage.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            clicImage.SetActive (true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        clicImage.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && Input.GetMouseButtonDown(1))
        {
            OpenBuildMenu();
        }
    }

    void OpenBuildMenu()
    {
        Debug.Log("Je construis");
    }
}
