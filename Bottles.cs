using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bottles : MonoBehaviour
{
    public GameObject[] bottlesList;
    private bool[] partFilled = new bool[3];
    public int currFilled, maxFill;
    public GameObject bottlesFullAlert;
    public PlayerController playerController;
    public bool bottlesFilling = false;

    void Start()
    {
        currFilled = 0;
        maxFill = 3;

        foreach (var bottle in bottlesList)
        {
            bottle.GetComponent<Slider>().maxValue = maxFill;
            bottle.GetComponent<Slider>().value = currFilled;
        }
        for (int i = 0; i < partFilled.Length; i++)
        {
            partFilled[i] = false;
        }
        bottlesFullAlert.SetActive(false);
    }
    private void Update()
    {
        PlayerController.collectedWater = currFilled;
        PlayerController.collectedWater = currFilled;
    }
    public void FillingBottles()
    {
        if (bottlesList[bottlesList.Length-1].GetComponent<Slider>().value < maxFill)
        {
            currFilled++;
            int increment = 1;
            for (int i = 0; i < bottlesList.Length; i++)
            {
                if (bottlesList[i].GetComponent<Slider>().value < maxFill)
                {
                    bottlesList[i].GetComponent<Slider>().value += increment;
                    increment = 0;
                    bottlesFilling = true;
                }
            }
        }
        else
        {
            StartCoroutine(TextActive(bottlesFullAlert));
        }
    }

    public void EmptyingBottles()
    {
        if (bottlesList[0].GetComponent<Slider>().value > 0)
        {
            currFilled--;
            int increment = 1;
            for (int i = bottlesList.Length-1; i > -1; i--)
            {
                if (bottlesList[i].GetComponent<Slider>().value > 0)
                {
                    bottlesList[i].GetComponent<Slider>().value -= increment;
                    increment = 0;
                }
            }
        }
    }

    IEnumerator TextActive( GameObject gameObject)
    {
        gameObject.SetActive(true);
        yield return new WaitForSeconds(4f);
        gameObject.SetActive(false);
    }
}
