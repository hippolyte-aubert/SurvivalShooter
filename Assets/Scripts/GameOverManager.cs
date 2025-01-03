using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverScreen;
    public TextMeshProUGUI killText;
    public TextMeshProUGUI timerText;
    
    public static GameOverManager instance;
    
    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void GameOver()
    {
        GameManager.instance.isGameOver = true;
        gameOverScreen.SetActive(true);
        AudioManager.Instance.Play("GameOver");
        killText.text = GameManager.instance.score.ToString();
        timerText.text = GameManager.instance.timer.ToString("F2");
    }
    
    public void ReloadGame()
    {
        AudioManager.Instance.Play("Click");
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void QuitGame()
    {
        AudioManager.Instance.Play("Click");
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
}
