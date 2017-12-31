using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MenuPopup_Congrats : SampleGameMenuGroup
{
    [Header("Buttons")]
    public Button btnEndmatch;

    void OnEnable()
    {
        btnEndmatch.onClick.AddListener(Finish);
    }
    void OnDisable()
    {
        btnEndmatch.onClick.RemoveAllListeners();
    }

    public void Finish()
    {
        MenuController.menuSystem.ClosePopupMenu();

        MenuController.FinshMatch();
    }
}
