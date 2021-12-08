using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantTutorial : Tutorial
{
    public WaterProgress waterProgress;
    public override bool StopTutorial()
    {
        if (waterProgress.canWater)
            return true;
        else
            return false;
    }
}
