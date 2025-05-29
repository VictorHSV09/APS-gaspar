using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Configura��o de Pontua��o")]
    public int score = 0;
    public Text scoreText; // Arraste o objeto Text aqui

    void Awake()
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

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Pontos: " + score;
        }
        else
        {
            Debug.LogError("ScoreText n�o atribu�do no GameManager!");
        }
    }
}