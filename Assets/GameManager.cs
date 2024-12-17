using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [Header("Game score")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    [HideInInspector] public int score = 0;
    [HideInInspector] public float timer = 0;
    [HideInInspector]public bool isGameOver = false;
    
    [Header("Pause Menu")]
    public GameObject pauseMenu;
    [HideInInspector] public bool isPaused = false;
    
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
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreText.text = "0";
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver) timer += Time.deltaTime;
        timerText.text = timer.ToString("F2");
    }
    
    public void AddKillCount()
    {
        score++;
        scoreText.text = score.ToString();
    }
    
    public void PauseGame()
    {
        isPaused = !isPaused;
        pauseMenu.SetActive(isPaused);
        Time.timeScale = isPaused ? 0 : 1;
        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isPaused;
    }
}
