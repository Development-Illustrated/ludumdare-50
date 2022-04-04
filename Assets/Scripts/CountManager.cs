using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountManager : MonoBehaviour
{
    public static CountManager Instance;
    public bool ignoreEndGame;

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
        HazardFixed,
        Death
    }


    [SerializeField]
    public GameObject hazardCounter;
    [SerializeField]
    public GameObject populationCounter;
    [SerializeField]
    public GameObject deadCounter;
    public GameObject[] scoreObjects;

    private Text hazardTxt;
    private Text populationTxt;
    private Text deadTxt;

    public int hazardCount;
    public int populationCount;
    public int deadCount;
    public int hazardFixedCount;

    void Start()
    {
        scoreObjects = GameObject.FindGameObjectsWithTag("Score");
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

    void Update()
    {
        foreach (GameObject score in scoreObjects)
        {
            Text txt = score.GetComponent<Text>();
            txt.text = "Score: " + CalculateScore();
        }
    }

    public int CalculateScore()
    {
        return (CountManager.Instance.populationCount * 100) +
            (CountManager.Instance.hazardFixedCount * 100) -
            (CountManager.Instance.deadCount * 50) -
            (CountManager.Instance.hazardCount * 50);
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
            case CountType.HazardFixed:
                hazardFixedCount += 1;
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


        if (deadCount > populationCount && !ignoreEndGame)
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
