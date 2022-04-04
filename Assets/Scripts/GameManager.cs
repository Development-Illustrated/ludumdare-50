using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : Singleton<GameManager>
{
    public enum GameState
    {
        Menu,
        Play,
        Pause,
        Lose
    }
    public GameState currentState;

    [SerializeField]
    private string gameSceneName = "GameScene";
    [SerializeField] private string fakeSceneName = "FakeGameMainMenuScene";
    [SerializeField] GameObject blueEffect;

    private void Awake() 
    {
        SceneManager.LoadScene(fakeSceneName, LoadSceneMode.Additive);
        Debug.Log("Game manager starting");
        currentState = GameState.Menu;
        Time.timeScale = 1;
    }

    public void TogglePause()
    {
        if (currentState == GameManager.GameState.Pause)
        {
            ChangeState(GameState.Play);
        }

        else if (currentState == GameManager.GameState.Play)
        {
            ChangeState(GameState.Pause);
        }
    }

    public void ChangeState(GameState newState)
    {
        Debug.Log("GameManager: Request state change from " + currentState + " to " + newState);
        if (currentState == GameManager.GameState.Menu & newState == GameState.Play)
        {
            SceneManager.UnloadSceneAsync(fakeSceneName);
            blueEffect.SetActive(false);            
            SceneManager.LoadScene(gameSceneName, LoadSceneMode.Additive);
        }
        else if (newState == GameState.Menu)
        {
            Time.timeScale = 1;
            SceneManager.UnloadSceneAsync(gameSceneName);
            blueEffect.SetActive(true);
            SceneManager.LoadScene(fakeSceneName, LoadSceneMode.Additive);
        }
        else if (currentState == GameState.Play & newState == GameState.Pause)
        {
            Time.timeScale = 0;
        }
        else if (currentState == GameState.Pause & newState == GameState.Play)
        {
            Time.timeScale = 1;
        }
        else if(newState == GameState.Lose)
        {
            Time.timeScale = 0;
        }
        else if(currentState == GameState.Lose && newState == GameState.Play)
        {
            Time.timeScale = 1f;
            SceneManager.UnloadSceneAsync(gameSceneName);
            SceneManager.LoadScene(gameSceneName, LoadSceneMode.Additive);

        }
        else if(currentState == GameState.Lose && newState == GameState.Menu)
        {
            Time.timeScale = 1f;
            SceneManager.UnloadSceneAsync(gameSceneName);
            SceneManager.LoadScene(fakeSceneName, LoadSceneMode.Additive);

        }

        currentState = newState;
    }

    public void RestartState()
    {
        if (currentState == GameState.Pause)
        {
            SceneManager.UnloadSceneAsync(gameSceneName);
            SceneManager.LoadScene(gameSceneName, LoadSceneMode.Additive);
        }
    }
}
