using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialFridge : Tutorial
{
    public AbsorbItem absorbItem;

    public override bool StopTutorial()
    {
        if (absorbItem.canUseFridge)
            return true;
        else 
            return false;
    }
}
