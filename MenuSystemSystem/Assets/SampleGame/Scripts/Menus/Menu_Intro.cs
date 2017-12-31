using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Menu_Intro : SampleGameMenuGroup
{
    private float waitTilChange = 2.0f;

    public override void Open(params object[] parameters)
    {
        base.Open(parameters);

        waitTilChange = 2.0f;
    }

    protected override void Update()
    {
        base.Update();

        if (isOpen)
        {
            if (waitTilChange < 0)
            {
                MenuController.GoToLevels();
            }
            else
            {
                waitTilChange -= Time.deltaTime;
            }
        }
    }
}
