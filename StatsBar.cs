using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   

public class StatsBar : MonoBehaviour
{
    public Slider task_slider;
    public Slider health_bar;
    //public PlayerController playerController;

    public void TaskBar( int points, int maxPoints = 20)
    {
        task_slider.maxValue = maxPoints;
        task_slider.value = points;
    }

    public void HealthBar( float health)
    {
        health_bar.value = health;
    }
}
