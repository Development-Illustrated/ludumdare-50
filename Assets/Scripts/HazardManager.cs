using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HazardManager : Singleton<HazardManager>
{
    [SerializeField]
    public GameObject hazardCounter;
    private Text hazardTxt;
    public int hazardCount;

    void Start()
    {
        hazardCount = 0;
        hazardTxt = hazardCounter.GetComponent<Text>();
    }
    
    public void incrementHazardCount()
    {
        hazardCount += 1;
        hazardTxt.text = "Hazards: " + hazardCount;
    }
    
    public void decrementHazardCount()
    {
        hazardCount -= 1;
        hazardTxt.text = "Hazards: " + hazardCount;
    }
}
