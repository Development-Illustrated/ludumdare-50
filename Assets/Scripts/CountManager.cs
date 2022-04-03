using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountManager : Singleton<CountManager>
{
    public enum CountType {
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
        hazardTxt = hazardCounter.GetComponent<Text>();
        populationTxt = populationCounter.GetComponent<Text>();
    }
    
    public void incrementCount(CountType type)
    {
        switch (type) {
            case CountType.Hazard:
                hazardCount += 1;
                hazardTxt.text = "Hazards: " + hazardCount;
                break;
            case CountType.Population:
                populationCount += 1;
                populationTxt.text = "Population: " + populationCount;
                break;
        }
    }
    
    public void decrementCount(CountType type)
    {
        switch (type) {
            case CountType.Hazard:
                hazardCount -= 1;
                hazardTxt.text = "Hazards: " + hazardCount;
                break;
            case CountType.Population:
                populationCount -= 1;
                populationTxt.text = "Population: " + populationCount;
                break;
        }
    }
}
