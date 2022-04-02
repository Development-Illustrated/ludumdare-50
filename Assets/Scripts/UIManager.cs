using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    GameObject[] gameOverObjects;
    GameObject[] pauseObjects;
    GameObject[] mainMenuObjects;
    GameObject[] inGameObjects;
    // PlayerController playerController;

    void Start()
    {
        Time.timeScale = 1;
        gameOverObjects = GameObject.FindGameObjectsWithTag("ShowOnGameOver");
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnGamePause");
        mainMenuObjects = GameObject.FindGameObjectsWithTag("ShowOnMainMenu");
        inGameObjects = GameObject.FindGameObjectsWithTag("ShowInGame");

        HideElements(gameOverObjects);
        HideElements(pauseObjects);
        HideElements(inGameObjects);

        // Set player controller?
        // if (Application.loadedLevel == "MainLevel") {
        //     // playerController = new PlayerController();
        // }
        Debug.Log(Time.timeScale);
    }
    
    void Update () {
		if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
		{
			if(Time.timeScale == 1)
			{
				Time.timeScale = 0;
                ShowElements(pauseObjects);
                HideElements(inGameObjects);
			} else if (Time.timeScale == 0){
				Time.timeScale = 1;
                HideElements(pauseObjects);
                ShowElements(inGameObjects);
			}
		}

        // if player is dead
		// if (false) {
        //     GameOver();
		// }
	}

    public void StartGame()
    {
        HideElements(mainMenuObjects);
        HideElements(pauseObjects);
        HideElements(gameOverObjects);
        ShowElements(inGameObjects);

        // Load game
    }

    public void GameOver()
    {
        ShowElements(gameOverObjects);
        HideElements(inGameObjects);
    }

    public void QuitLevel()
    {
        HideElements(gameOverObjects);
        HideElements(pauseObjects);
        HideElements(inGameObjects);

        ShowElements(mainMenuObjects);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void HideElements(GameObject[] objects)
    {
		foreach(GameObject obj in objects){
			obj.SetActive(false);
		}
    }

    private void ShowElements(GameObject[] objects)
    {
		foreach(GameObject obj in objects){
			obj.SetActive(true);
		}
    }
}
