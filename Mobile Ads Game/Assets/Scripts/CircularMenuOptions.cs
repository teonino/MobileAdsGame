using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularMenuOptions : MonoBehaviour
{
    public string optionName;
    public GameObject subMenuPrefab;
    public GameObject parentMenu;
    public bool isBackButton = false;

    public void SetHighlight(bool active, float scale)
    {
        transform.localScale = active ? Vector3.one * scale : Vector3.one;
    }

    public void OnSelect()
    {
        if (isBackButton)
        {
            CircularMenuManager.Instance.CloseCurrentMenu();
            return;
        }

        if (subMenuPrefab != null)
        {
            Vector2 position = (parentMenu != null)
                ? parentMenu.transform.position
                : Input.mousePosition;

            CircularMenuManager.Instance.OpenMenu(subMenuPrefab, position);

            if (parentMenu != null)
                parentMenu.SetActive(false);
        }
        else
        {
            Debug.Log("Option sélectionnée : " + optionName);
            CircularMenuManager.Instance.CloseAllMenus();
        }
    }
}
