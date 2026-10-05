using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public void OnPlayerDeath()
    {
        gameObject.SetActive(true);
    }

    public void OnRestartClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
