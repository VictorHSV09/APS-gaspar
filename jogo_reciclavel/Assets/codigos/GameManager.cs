using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("UI References")]
    [SerializeField] private Text scoreText;
    [SerializeField] private Text timerText;

    [Header("Game Settings")]
    public float gameDuration = 60f;
    public string endSceneName = "fim";

    private float currentTime;
    private bool gameActive;
    private int score;
    private string gameSceneName = "jogo"; // Nome da sua cena de jogo principal

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded; // Importante!
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    // Chamado toda vez que uma cena é carregada
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == gameSceneName)
        {
            InitializeGame();
            FindUIReferences();
        }
    }

    void InitializeGame()
    {
        currentTime = gameDuration;
        score = 0;
        gameActive = true;
        Time.timeScale = 1f;
    }

    void FindUIReferences()
    {
        // Busca dinâmica dos elementos UI
        scoreText = GameObject.Find("ScoreText")?.GetComponent<Text>();
        timerText = GameObject.Find("TimerText")?.GetComponent<Text>();

        UpdateScoreDisplay();
        UpdateTimerDisplay();
    }

    void Update()
    {
        if (gameActive)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerDisplay();

            if (currentTime <= 0)
            {
                EndGame();
            }
        }
    }

    public void EndGame()
    {
        if (!gameActive) return;

        gameActive = false;
        SceneManager.LoadScene(endSceneName);
    }

    public void AddScore(int points)
    {
        if (!gameActive) return;

        score += points;
        UpdateScoreDisplay();
    }

    void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            timerText.text = $"Tempo: {Mathf.RoundToInt(currentTime)}";
        }
    }

    void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Pontos: {score}";
        }
    }

    public int GetFinalScore()
    {
        return score;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("menu");
    }
}