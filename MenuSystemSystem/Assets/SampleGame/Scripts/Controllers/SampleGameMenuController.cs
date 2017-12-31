using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class SampleGameMenuController : MenuSystemController
{
    [Header("Levels")]
    public string[] levelNames;

    protected override void StandardFlow()
    {
        Debug.Log("Standard flow");
    }

    protected override bool InitialTutorialFlow()
    {
        Debug.Log("Is in tutorial mode?");
        if (isInTutorialMode)
        {
            Debug.Log("Do tutorial stuff");
            // then
            // isInTutorialMode = false;
        }

        return isInTutorialMode;
    }

    protected override bool FinishedMatchFlow()
    {
        //if (someMatchWasGoingOn)
        //{
        //    goto rewards screen or something
        //}

        return false;
    }

    public void GoToLevels()
    {
        menuSystem.GoToMenu(typeof(Menu_Levels), levelNames);
    }

    public void StartLevel(string level)
    {
        menuSystem.GoToMenu(typeof(Menu_Game), level);
    }

    public void FinshMatch()
    {
        GoToLevels();
    }
}
