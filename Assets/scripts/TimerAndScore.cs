using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimerAndScore : MonoBehaviour
{
    private bool isPaused = true;
    private float lastScoreUpdateTime;

    public Text timerText;
    public Text scoreText;
    private ScoreSender scoreSender;

    private float pausedStartTime;
   

    void Start()
    {
        scoreSender = GameObject.FindObjectOfType<ScoreSender>();

        SceneManager.sceneLoaded += OnSceneLoaded;

        // Comprobar si el juego ya ha comenzado antes de iniciar el temporizador
        if (GameManager.Instance.IsGameStarted())
        {
            ResumeTimerAndScore();
        }
    }

    void Update()
    {
        if (!isPaused)
        {
            UpdateTimer();
            UpdateScore();
        }
    }

    void UpdateTimer()
    {
        float elapsedTime = Time.time - GameManager.Instance.StartTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timerText.text = string.Format("Timer: {0:00}:{1:00}", minutes, seconds);

        Debug.Log("Elapsed Time: " + elapsedTime);
    }

    public void StartGame()
    {
        GameManager.Instance.StartGame();
        isPaused = false;
        lastScoreUpdateTime = Time.time;
        UpdateScoreText();
        Debug.Log("Game Started, Timer and Score Resumed");
    }

    public void PauseTimerAndScore()
    {
        isPaused = true;
        pausedStartTime = GameManager.Instance.StartTime; // Almacenar el tiempo de inicio al pausar
        Debug.Log("Timer and Score Paused");
    }


    public void ResumeTimerAndScore()
    {
        isPaused = false;
        // No se necesita un parámetro, simplemente llamamos al método sin argumentos
        GameManager.Instance.ResetStartTime();
        lastScoreUpdateTime = Time.time;
        UpdateScoreText();
        Debug.Log("Timer and Score Resumed");
    }

    void UpdateScore()
    {
        if (Time.time - lastScoreUpdateTime >= 30f)
        {
            GameManager.Instance.UpdateScore(-10f);
            UpdateScoreText();
            lastScoreUpdateTime = Time.time;
        }
    }

    void UpdateScoreText()
    {
        float score = Mathf.Max(GameManager.Instance.Score, 0f);
        scoreText.text = "Score: " + Mathf.RoundToInt(score).ToString();
    }

    void OnDestroy()
    {
        GameManager.Instance.SaveData();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (GameManager.Instance.IsGameStarted())
        {
            ResumeTimerAndScore();
        }
        else
        {
            PauseTimerAndScore();
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
   

    // Método para obtener el tiempo actual del temporizador
    public float GetCurrentTime()
    {
        if (isPaused)
        {
            return pausedStartTime;
        }
        else
        {
            return Time.time - GameManager.Instance.StartTime;
        }
    }

    // Método para obtener el puntaje actual
    public float GetScore()
    {
        return GameManager.Instance.Score;
    }


}
