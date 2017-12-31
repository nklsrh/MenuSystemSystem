using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class MenuSystemController : MonoBehaviour
{    
    public MenuSystem menuSystem;
    public MenuTopBar topBar;

    private int pendingEnergyCost;

    protected bool hasGameLoaded = false;
    protected bool isInTutorialMode = false;
    protected bool isSigningIn = false;

    void Start()
    {
        //Application.targetFrameRate = 50;

        foreach (MenuGroup m in transform.GetComponentsInChildren<MenuGroup>(true))
        {
            Debug.Log("SET MENU CONTOLLER: " + m.gameObject.name);
            m.SetMenuSystemControllerContext(this);
        }

        topBar.Setup();
        menuSystem.Setup();

        if (hasGameLoaded)
        {
            FinishedSetup();
        }
        else
        {
            Debug.Log("FIRST RUN");
            menuSystem.GoToMenu(typeof(Menu_Intro));
        }
    }

    public virtual void GoBack()
    {
        menuSystem.GoToPreviousMenu();
    }

    protected virtual void SetupFailed(string error)
    {
        isSigningIn = false;
        FinishedSetup();
    }

    protected virtual void FinishedSetup()
    {
        hasGameLoaded = true;
        isSigningIn = false;

        if (InitialTutorialFlow())
        {
            return;
        }

        if (FinishedMatchFlow())
        {
            return;
        }

        StandardFlow();
    }

    protected virtual void StandardFlow()
    {
    }

    protected virtual bool InitialTutorialFlow()
    {
        return false;
    }

    protected virtual bool FinishedMatchFlow()
    {
        return false;
    }

}
