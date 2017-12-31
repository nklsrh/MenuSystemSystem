using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MenuSystem : MonoBehaviour 
{
    private List<MenuGroup> menus;

    private MenuGroup currentMenu
    {
        get
        {
            return menuStack.Count > 0 ? menuStack[menuStack.Count - 1] : null;
        }
    }
    private MenuGroup prevMenu
    {
        get
        {
            return menuStack.Count > 1 ? menuStack[menuStack.Count - 2] : null;
        }
    }
    private List<MenuGroup> menuStack = new List<MenuGroup>();

    public bool setupOnStart = true;
    public bool hideGameObject = false;

    public bool ShouldBackButtonVisible
    {
        get
        {
            return prevMenu != null && currentMenu != null;
        }
    }

    void Start()
    {
        if (setupOnStart)
        {
            Setup();
        }
    }

	public void Setup () 
    {
        menus = new List<MenuGroup>();
        int i = 0;
        foreach (MenuGroup m in transform.GetComponentsInChildren<MenuGroup>(true))
        {
            if (m)
            {
                m.Setup(hideGameObject);
                m.transform.localPosition = new Vector3(15000, 0, 0);       // move everything far away from viewport
                menus.Add(m.GetMenuGroup());
            }
            i++;
        }
    }


    internal void GoToMenu(string p, params object[] parameters)
    {
        foreach (MenuGroup g in menus)
        {
            if (g.name.ToLower().Contains(p.ToLower()))
            {
                GoToMenu(g, parameters);
                break;
            }
        }
    }

    internal void GoToMenu(System.Type type, params object[] parameters)
    {
        foreach (MenuGroup g in menus)
        {
            if (g.GetType() == type)
            {
                GoToMenu(g, parameters);
                break;
            }
        }
    }
    
    public void GoToMenu(MenuGroup menu, params object[] parameters)
    {
        if (currentMenu != null && currentMenu == menu)
        {
            return;
        }

        if (currentMenu != null && !menu.isPopupMenu)
        {
            currentMenu.Close();
            if (!currentMenu.isRememberedInHistory)
            {
                menuStack.RemoveAt(menuStack.Count - 1);
            }
        }

        menuStack.Add(menu);
        if (menu.isPopupMenu)
        {
            menu.transform.SetAsLastSibling();
        }
        menu.Open(parameters);
    }

    public void ClosePopupMenu()
    {
        currentMenu.Close();
        menuStack.RemoveAt(menuStack.Count - 1);
    }

    public void GoToPreviousMenu()
    {
        if (currentMenu.isPopupMenu)
        {
            Debug.Log("Close popup menu");
            ClosePopupMenu();
            currentMenu.Focus();
        }
        else if (currentMenu != null)
        {
            if (prevMenu != null || currentMenu.previousScreenFallback != null)
            {
                MenuGroup prev = (prevMenu == null ? currentMenu.previousScreenFallback : prevMenu);
                currentMenu.Close();
                menuStack.RemoveAt(menuStack.Count - 1);

                if (currentMenu == null)
                {
                    menuStack.Add(prev);
                    Debug.LogError("Had to use previousScreenFallback " + currentMenu.name);
                }
                currentMenu.Open();
            }
            else
            {
                Debug.LogError("prevmenu is missing and there's no fallback for " + currentMenu.name);
            }
        }
    }

    public void Trigger()
    {
        gameObject.SetActive(true);

        if (menus == null)
            Start();

        GoToMenu(menus[0]);
    }


    public void CloseSystem()
    {
        GoToMenu(menus[0]);
        gameObject.SetActive(false);
    }
}
