using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountManager : MonoBehaviour
{
    public static CountManager Instance;

    private void Awake()
    {
        if (CountManager.Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public enum CountType
    {
        Population,
        Hazard,
        Death
    }


    [SerializeField]
    public GameObject hazardCounter;
    [SerializeField]
    public GameObject populationCounter;
    [SerializeField]
    public GameObject deadCounter;

    private Text hazardTxt;
    private Text populationTxt;
    private Text deadTxt;

    public int hazardCount;
    public int populationCount;
    public int deadCount;



    void Start()
    {
        hazardCount = 0;
        populationCount = 0;
        deadCount = 0;
        if (hazardCounter)
            hazardTxt = hazardCounter.GetComponent<Text>();

        if (populationCounter)
            populationTxt = populationCounter.GetComponent<Text>();

        if (deadCounter)
            deadTxt = deadCounter.GetComponent<Text>();
    }

    public void incrementCount(CountType type)
    {
        Debug.Log("Count manager incrementing count: " + type);
        switch (type)
        {
            case CountType.Hazard:
                hazardCount += 1;
                updateText(hazardTxt, hazardCount);
                break;
            case CountType.Population:
                populationCount += 1;
                updateText(populationTxt, populationCount);
                break;
            case CountType.Death:
                deadCount += 1;
                updateText(deadTxt, deadCount);
                break;
        }


        if (deadCount > populationCount)
        {
            GameManager.Instance.ChangeState(GameManager.GameState.Lose);
        }

    }

    public void decrementCount(CountType type)
    {
        switch (type)
        {
            case CountType.Hazard:
                hazardCount -= 1;
                updateText(hazardTxt, hazardCount);
                break;
            case CountType.Population:
                populationCount -= 1;
                updateText(populationTxt, populationCount);
                break;
        }
    }

    private void updateText(Text field, int text)
    {
        if (field != null)
            field.text = text.ToString();
    }

    public void clearCounts()
    {
        hazardCount = 0;
        populationCount = 0;
        deadCount = 0;
    }
}
