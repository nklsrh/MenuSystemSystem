using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class SampleGameMenuGroup : MenuGroup
{
    public SampleGameMenuController MenuController
    {
        get
        {
            return (SampleGameMenuController)menuSystemController;
        }
    }
}
