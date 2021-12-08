using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustbinTutorial : Tutorial
{
    public AbsorbItem absorbItem;

    public override bool StopTutorial()
    {
        if (absorbItem.garbageInInventory && Input.GetKey(KeyCode.E) && playerEntered)
            return true;
        else
            return false;
    }

}
