using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject[] gameOverObjects;
    [SerializeField] GameObject[] pauseObjects;
    [SerializeField] GameObject[] mainMenuObjects;
    [SerializeField] GameObject[] inGameObjects;

    void Start()
    {
        Debug.Log("UI manager starting");

        HideElements(gameOverObjects);
        HideElements(pauseObjects);
        HideElements(inGameObjects);
        ShowElements(mainMenuObjects);
    }

    public void StartGame()
    {
        HideElements(mainMenuObjects);
        HideElements(pauseObjects);
        HideElements(gameOverObjects);
        ShowElements(inGameObjects);

        GameManager.Instance.ChangeState(GameManager.GameState.Play);
    }

    public void RestartGame()
    {
        if (GameManager.Instance.currentState == GameManager.GameState.Pause)
            GameManager.Instance.RestartState();
        else
        {
            GameManager.Instance.ChangeState(GameManager.GameState.Play);
        }
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
        GameManager.Instance.ChangeState(GameManager.GameState.Menu);

        ShowElements(mainMenuObjects);
    }

    public void Pause(InputAction.CallbackContext obj)
    {
        Debug.Log("Ui manager: Pause Callback recieved");
        if (obj.performed)
        {
            if (GameManager.Instance.currentState == GameManager.GameState.Play)
            {
                ShowElements(pauseObjects);
                HideElements(inGameObjects);
            }
            else if (GameManager.Instance.currentState == GameManager.GameState.Pause)
            {
                HideElements(pauseObjects);
                ShowElements(inGameObjects);
            }
            GameManager.Instance.TogglePause();
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
