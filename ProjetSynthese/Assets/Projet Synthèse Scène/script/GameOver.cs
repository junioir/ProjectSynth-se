using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject _GameOverUi;
    public static GameOver _Instance;
    private void Awake()
    {
        if (_Instance != null)
        {
            Debug.Log("The is more  instance Gameover in the scene");
            return;
        }
        _Instance = this;
    }
    public void OnplayerDeath()
    {
        _GameOverUi.SetActive(true);
    }

    public void RetryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        _GameOverUi.SetActive(false);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

}
