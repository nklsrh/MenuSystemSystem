using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Menu_Game : SampleGameMenuGroup
{
    [Header("Buttons")]
    public Button btnEndmatch;
    public Text txtHeader;

    void OnEnable()
    {
        btnEndmatch.onClick.AddListener(Finish);
    }
    void OnDisable()
    {
        btnEndmatch.onClick.RemoveAllListeners();
    }

    public override void Open(params object[] parameters)
    {
        base.Open(parameters);

        txtHeader.text = (string)parameters[0];
    }

    public void Finish()
    {
        MenuController.menuSystem.GoToMenu(typeof(MenuPopup_Congrats));
    }
}
