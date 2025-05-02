using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitTrigger : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;

    private bool gameOver = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!gameOver && other.CompareTag("Enemy"))
        {
            gameOver = true;
            Debug.Log("GAME OVER – An enemy end the game");
            gameOverPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f; // Pause le jeu
        }
    }

    public void ReloadScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
