 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishwasherTutorial : Tutorial
{
    public AbsorbItem absorbItem;

    public override bool StopTutorial()
    {
        if (absorbItem.forkPut)
            return true;
        else 
            return false;
    }
}
