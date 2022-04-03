using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountManager : Singleton<CountManager>
{
    public enum CountType
    {
        Population,
        Hazard
    }

    [SerializeField]
    public GameObject hazardCounter;
    [SerializeField]
    public GameObject populationCounter;

    private Text hazardTxt;
    private Text populationTxt;

    public int hazardCount;
    public int populationCount;

    void Start()
    {
        hazardCount = 0;
        populationCount = 0;
        if (hazardCounter)
            hazardTxt = hazardCounter.GetComponent<Text>();

        if (populationCounter)
            populationTxt = populationCounter.GetComponent<Text>();
    }

    public void incrementCount(CountType type)
    {
        switch (type)
        {
            case CountType.Hazard:
                hazardCount += 1;
                updateText(hazardTxt, "Hazards: " + hazardCount);
                break;
            case CountType.Population:
                populationCount += 1;
                updateText(populationTxt, "Population: " + populationCount);
                break;
        }
    }

    public void decrementCount(CountType type)
    {
        switch (type)
        {
            case CountType.Hazard:
                hazardCount -= 1;
                updateText(hazardTxt, "Hazards: " + hazardCount);
                break;
            case CountType.Population:
                populationCount -= 1;
                updateText(populationTxt, "Population: " + populationCount);
                break;
        }
    }

    private void updateText(Text field, string text)
    {
        if (field != null)
            field.text = text;
    }
}
