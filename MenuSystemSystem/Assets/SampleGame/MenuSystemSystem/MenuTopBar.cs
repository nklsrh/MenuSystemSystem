using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class MenuTopBar : MonoBehaviour
{
    public MenuSystemController menuSystemController;
    public MenuSystem menuSystem;

    // Stub: add currency buttons and stuff here

    public void Setup()
    {
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
