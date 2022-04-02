using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public InputAction playerInputs;
    [SerializeField] GameObject[] gameOverObjects;
    [SerializeField] GameObject[] pauseObjects;
    [SerializeField] GameObject[] mainMenuObjects;
    [SerializeField] GameObject[] inGameObjects;


    private bool isPaused;

    private void Awake()
    {
        playerInputs = new InputAction();
    }

    void Start()
    {
        isPaused = false;
        Time.timeScale = 1;
        HideElements(gameOverObjects);
        HideElements(pauseObjects);
        HideElements(inGameObjects);
        ShowElements(mainMenuObjects);
    }



    // ----- BUTTON CALLBACKS ----- //
    public void StartGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        HideElements(mainMenuObjects);
        HideElements(pauseObjects);
        HideElements(gameOverObjects);
        ShowElements(inGameObjects);

        SceneManager.LoadScene("GameScene", LoadSceneMode.Additive);
    }

    public void RestartGame()
    {
        SceneManager.UnloadSceneAsync("GameScene");
        StartGame();
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
        SceneManager.UnloadSceneAsync("GameScene");

        ShowElements(mainMenuObjects);
    }

    public void Pause(InputAction.CallbackContext obj)
    {
        if (obj.performed)
        {
            if (!isPaused)
            {
                Time.timeScale = 0;
                ShowElements(pauseObjects);
                HideElements(inGameObjects);
                isPaused = true;
            }
            else
            {
                Time.timeScale = 1;
                HideElements(pauseObjects);
                ShowElements(inGameObjects);
                isPaused = false;
            }
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void HideElements(GameObject[] objects)
    {
        foreach (GameObject obj in objects)
        {
            obj.SetActive(false);
        }
    }

    private void ShowElements(GameObject[] objects)
    {
        foreach (GameObject obj in objects)
        {
            obj.SetActive(true);
        }
    }
}
