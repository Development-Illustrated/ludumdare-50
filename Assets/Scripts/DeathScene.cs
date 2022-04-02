using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScene : MonoBehaviour
{
    public void RestartGame() {
        SceneManager.LoadScene("SampleScene");
    }

    public void ReturnToMenu() {
        SceneManager.LoadScene("MenuScene");
    }
}
