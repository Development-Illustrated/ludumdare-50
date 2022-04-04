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
        Lose
    }
    public GameState currentState;

    public bool isPaused;

    [SerializeField]
    private string gameSceneName = "PlaytestScene";
    [SerializeField] private string fakeSceneName = "FakeGameMainMenuScene";
    [SerializeField] GameObject blueEffect;
    [SerializeField] MusicManager musicManager;

    private void Awake() 
    {
        isPaused = false;
        SceneManager.LoadScene(fakeSceneName, LoadSceneMode.Additive);
        Debug.Log("Game manager starting");
        currentState = GameState.Menu;
        Time.timeScale = 1;
    }

    public void TogglePause()
    {
        if (currentState != GameState.Menu)
        {
            if(isPaused)
            {
                UnpauseGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ChangeState(GameState newState)
    {
        Debug.Log("GameManager: Request state change from " + currentState + " to " + newState);
        if (currentState == GameManager.GameState.Menu & newState == GameState.Play)
        {
            LoadGame();
        }
        else if (newState == GameState.Menu)
        {
            LoadMainMenu();
            UnpauseGame();
        }
        else if(newState == GameState.Lose)
        {
            PauseGame();
            musicManager.ResetCurrentTrack();
        }
        else if(currentState == GameState.Lose && newState == GameState.Play)
        {
            RestartGame();
        }
        else if(currentState == GameState.Play && newState == GameState.Play)
        {
            RestartGame();
        }

        currentState = newState;
    }

    private void PauseGame()
    {
        musicManager.TogglePause();
        Time.timeScale = 0;
        isPaused = true;
    }

    private void UnpauseGame()
    {
        musicManager.TogglePause();
        Time.timeScale = 1;
        isPaused = false;
    }

    private void LoadMainMenu()
    {
        musicManager.PlayMainMenuMusic();
        SceneManager.UnloadSceneAsync(gameSceneName);
        blueEffect.SetActive(true);
        SceneManager.LoadScene(fakeSceneName, LoadSceneMode.Additive);
    }

    private void LoadGame()
    {
        musicManager.PlayGameMusic();
        SceneManager.UnloadSceneAsync(fakeSceneName);
        blueEffect.SetActive(false);            
        SceneManager.LoadScene(gameSceneName, LoadSceneMode.Additive);
    }

    public void RestartGame()
    {
        UnpauseGame();
        StartCoroutine(LoadSyncOperation());
        musicManager.PlayGameMusic();
    }

    IEnumerator LoadSyncOperation()
    {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(gameSceneName);
        while (ao.progress < 1f)
        {
            yield return new WaitForEndOfFrame();
        } 

        SceneManager.LoadScene(gameSceneName, LoadSceneMode.Additive);
    }

}
