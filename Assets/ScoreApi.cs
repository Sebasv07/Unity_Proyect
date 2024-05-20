using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class ScoreApi : MonoBehaviour
{
    public List<Text> scoreIDTexts;
    public List<Text> usuarioTexts;
    public List<Text> scoreTexts;
    public List<Text> fechaTexts;
    public List<Text> partidaTexts;

    private List<Score> scores = new List<Score>();

    void Start()
    {
        StartCoroutine(GetScoresFromAPI());
    }

    IEnumerator GetScoresFromAPI()
    {
        string url = "http://www.guayabagame.somee.com/api/Score"; // URL de tu API

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error en la solicitud: " + webRequest.error);
            }
            else
            {
                string jsonResponse = webRequest.downloadHandler.text;
                ProcessScores(jsonResponse);
            }
        }
    }

    void ProcessScores(string jsonResponse)
    {
        scores = JsonUtility.FromJson<ScoreList>("{\"Score\":" + jsonResponse + "}").scores;
        DisplayScores();
    }

    void DisplayScores()
    {
        for (int i = 0; i < scores.Count && i < scoreIDTexts.Count; i++)
        {
            scoreIDTexts[i].text = scores[i].Score_ID.ToString();
            usuarioTexts[i].text = scores[i].Usuario;
            scoreTexts[i].text = scores[i].ScoreUser.ToString();
            fechaTexts[i].text = scores[i].Fecha;
            partidaTexts[i].text = scores[i].Partida;
        }
    }
}

[System.Serializable]
public class Score
{
    public int Score_ID;
    public string Usuario;
    public int ScoreUser;
    public string Fecha;
    public string Partida;
}

[System.Serializable]
public class ScoreList
{
    public List<Score> scores;
}
