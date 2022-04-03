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

    void Start()
    {
        Debug.Log("Game manager starting");
        currentState = GameState.Menu;
        Time.timeScale = 1;
    }

    void Update()
    {

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
            SceneManager.LoadScene(gameSceneName, LoadSceneMode.Additive);
            currentState = newState;
        }
        else if (newState == GameState.Menu)
        {
            Time.timeScale = 1;
            GL.Clear(true, true, Color.black, 1f);
            SceneManager.UnloadSceneAsync(gameSceneName);
            currentState = newState;


        }
        else if (currentState == GameState.Play & newState == GameState.Pause)
        {
            Time.timeScale = 0;
            currentState = newState;
        }
        else if (currentState == GameState.Pause & newState == GameState.Play)
        {
            Time.timeScale = 1;
            currentState = newState;
        }



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
