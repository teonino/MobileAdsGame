using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularMenuManager : MonoBehaviour
{
    public GameObject radialMenuUI;
    public List<CircularMenuOptions> options;
    public float highlightScale = 1.3f;
    public float minSelectDistance = 120f;

    private Vector2 centerScreenPos;
    private bool isActive = false;
    private CircularMenuOptions currentHighlighted = null;

    public int currentMenuID;
    public static CircularMenuManager Instance;

    private Stack<GameObject> menuStack = new Stack<GameObject>();

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        HideMenu();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ShowMenu(Input.mousePosition);
        }

        if (isActive)
        {
            UpdateSelection();

            if (Input.GetMouseButtonUp(1))
            {
                if (currentHighlighted != null)
                {
                    Vector2 mouseDir = (Vector2)Input.mousePosition - centerScreenPos;
                    if (mouseDir.magnitude >= minSelectDistance)
                    {
                        currentHighlighted.OnSelect();
                    }
                }

                HideMenu();
            }
        }
    }

    void ShowMenu(Vector2 position)
    {
        isActive = true;
        radialMenuUI.SetActive(true);
        radialMenuUI.transform.position = position;
        centerScreenPos = position;
    }

    public void HideMenu()
    {
        isActive = false;
        radialMenuUI.SetActive(false);
        if (currentHighlighted != null)
        {
            currentHighlighted.SetHighlight(false, 1f);
            currentHighlighted = null;
        }
    }

    void UpdateSelection()
    {
        Vector2 mouseDir = (Vector2)Input.mousePosition - centerScreenPos;
        if (mouseDir.magnitude < 30f) return;

        float angle = Mathf.Atan2(mouseDir.y, mouseDir.x) * Mathf.Rad2Deg;
        if (angle < 0) angle += 360f;

        int sector = Mathf.FloorToInt(angle / (360f / options.Count));

        for (int i = 0; i < options.Count; i++)
        {
            bool isSelected = (i == sector);
            options[i].SetHighlight(isSelected, highlightScale);

            if (isSelected)
                currentHighlighted = options[i];
        }
    }

    public void OpenMenu(GameObject menu, Vector2 position)
    {
        if (menuStack.Count > 0)
        {
            GameObject current = menuStack.Peek();
            current.SetActive(false);
        }

        menu.SetActive(true);
        menu.transform.position = position;
        menuStack.Push(menu);
    }

    public void CloseCurrentMenu()
    {
        if (menuStack.Count > 0)
        {
            GameObject closingMenu = menuStack.Pop();
            closingMenu.SetActive(false);

            if (menuStack.Count > 0)
            {
                GameObject previousMenu = menuStack.Peek();
                previousMenu.SetActive(true);
            }
        }
    }

    public void CloseAllMenus()
    {
        while (menuStack.Count > 0)
        {
            GameObject menu = menuStack.Pop();
            menu.SetActive(false);
        }
    }
}
