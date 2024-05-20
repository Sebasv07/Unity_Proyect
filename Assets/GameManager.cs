using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private float startTime = -1f;
    private float score = 30000f;

    public float StartTime => startTime;
    public float Score => score;

    private bool gameStarted = false; // Variable para verificar si el juego ha comenzado

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartGame()
    {
        gameStarted = true; // Marcar que el juego ha comenzado
        startTime = Time.time;
        score = 30000f;
    }

    public void UpdateScore(float pointsToAdd)
    {
        score += pointsToAdd;
    }

    public void SaveData()
    {
        PlayerPrefs.SetFloat("StartTime", startTime);
        PlayerPrefs.SetFloat("Score", score);
        PlayerPrefs.Save();
    }

    public void LoadData()
    {
        startTime = PlayerPrefs.GetFloat("StartTime", -1f);
        score = PlayerPrefs.GetFloat("Score", 30000f);
    }

    public void ResetStartTime()
    {
        startTime = Time.time;
    }

    public void ResetScore()
    {
        score = 30000f;
    }

    public bool IsGameStarted()
    {
        return gameStarted;
    }
}
