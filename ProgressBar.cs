using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Slider progress_slider;
    public Gradient gradient;
    public Image fill;

    private void Update()
    {
        if (progress_slider.value == progress_slider.maxValue)
            fill.color = gradient.Evaluate(1f);
    }
    public void TaskProgressBar(int points, int maxVal)
    {
        progress_slider.maxValue = maxVal;
        progress_slider.value = points;
        fill.color = gradient.Evaluate(progress_slider.normalizedValue);
    }
}
