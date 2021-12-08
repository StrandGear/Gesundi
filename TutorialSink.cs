using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSink : Tutorial
{
    public Bottles bottles;
    public override bool StopTutorial()
    {
        if (playerEntered && bottles.bottlesFilling)
            return true;
        else
            return false;
    }
}
