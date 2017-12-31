using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using DG.Tweening;

public class MenuGroup : MonoBehaviour
{
    [Header("Editable Settings")]
    public bool hideGameObject = false;
    public bool hideTopBar = false;

    [Header("Heirarchy Information")]
    public bool isPopupMenu = false;

    [Header("History")]
    public bool isRememberedInHistory = true;
    public UIBackButton backButton;
    public MenuGroup previousScreenFallback; // in case there's no history to call from
    protected System.Action onBackButtonClick;

    [Header("Transition/Animation")]
    protected List<Vector3> objectPos =  new List<Vector3>();
    protected Vector3 startingPosition;
    protected float distanceOfTravel = 12000;


    protected MenuSystemController menuSystemController;
    protected bool isOpen = false;
    protected bool isSetup = false;


    public virtual void Open(params object[] parameters)
    {
        if (hideGameObject)
        {
            gameObject.SetActive(true);
        }
        
        // Snap to middle of viewport before we animate
        transform.localPosition = Vector3.zero;

        int i = transform.childCount;

        foreach (Transform t in transform)
        {
            objectPos.Add(t.localPosition);

            // Start animating objects in
            t.Translate(Vector3.right * distanceOfTravel);
            t.transform.DOLocalMove(objectPos[objectPos.Count - 1], 0.2f + i * 0.08f).SetEase(Ease.OutExpo);

            i--;

            // Set Back Button if it doesnt exist
            if (backButton != null)
            {
                UIBackButton b = t.GetComponentInChildren<UIBackButton>(true);
                if (b != null)
                {
                    backButton = b;
                }
            }
        }

        Focus();

        isOpen = true;
        isSetup = true;

        if (backButton)
        {
            if (previousScreenFallback == null)
            {
                backButton.gameObject.SetActive(menuSystemController.menuSystem.ShouldBackButtonVisible);
            }
            //Back button exists so default to GoBack
            SetBackButtonFunction(menuSystemController.GoBack);
        }
    }

    public virtual void Focus()
    {
        if (hideTopBar)
        {
            menuSystemController.topBar.Hide();
        }
        else
        {
            menuSystemController.topBar.Show();
        }
    }

    protected virtual void SetBackButtonFunction(System.Action onClick)
    {
        onBackButtonClick = onClick;
        if (backButton != null)
        {
            backButton.button.onClick.RemoveAllListeners();
            backButton.button.onClick.AddListener(PressBackButton);
        }
    }

    protected virtual void PressBackButton()
    {
        if (onBackButtonClick != null)
        {
            onBackButtonClick.Invoke();
        }
    }

    internal virtual void Setup(bool hideGameObject)
    {
        this.hideGameObject = hideGameObject;

        if (hideGameObject)
            gameObject.SetActive(false);
    }

    protected virtual void Update()
    {
        if (isOpen)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PressBackButton();
            }
        }
    }

    public virtual void Close()
    {
        if (hideGameObject)
        {
            gameObject.SetActive(false);
        }
        transform.localPosition = startingPosition;
        isOpen = false;

        if (backButton != null)
        {
            backButton.GetComponent<Button>().onClick.RemoveAllListeners();
        }

        if (hideTopBar)
        {
            menuSystemController.topBar.Show();
        }
    }

    public MenuGroup GetMenuGroup()
    {
        startingPosition = transform.localPosition;
        return this;
    }

    internal void SetMenuSystemControllerContext(MenuSystemController menuSystemController)
    {
        this.menuSystemController = menuSystemController;
    }
}
