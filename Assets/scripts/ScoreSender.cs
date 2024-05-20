using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ScoreSender : MonoBehaviour
{
    private List<ScoreData> scoreDataList = new List<ScoreData>();

    // Estructura para almacenar los datos del puntaje
    private struct ScoreData
    {
        public int score;
        public string dateRegistrer;
    }

    // Método para agregar datos de puntaje a la lista
    public void AddScoreData(int score)
    {
        ScoreData data;
        data.score = score;
        data.dateRegistrer = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"); // Formato ISO 8601
        scoreDataList.Add(data);
        Debug.Log("Score Data Added: " + score);
    }

    // Método para finalizar el juego y enviar todos los datos de puntaje a la API
    public void FinishGameAndSendScores()
    {
        if (!IsConnectedToInternet())
        {
            Debug.LogError("No hay conexión a Internet. No se pueden enviar los datos de puntaje.");
            return;
        }

        if (scoreDataList.Count == 0)
        {
            Debug.Log("No score data to send.");
            return;
        }

        Debug.Log("Sending all scores to API...");

        // Obtener el ID de usuario almacenado en PlayerPrefs
        int userId = PlayerPrefs.GetInt("userId");

        // URL de la API para insertar puntajes
        string url = "http://www.guayabagame.somee.com/api/Score";

        foreach (var scoreData in scoreDataList)
        {
            // Crear un objeto JSON con los datos a enviar
            string jsonData = "{\"scoreId\":0,\"userId\":" + userId + ",\"score\":" + scoreData.score + ",\"dateRegistrer\":\"" + scoreData.dateRegistrer + "\",\"game\":0,\"active\":true}";

            // Enviar la solicitud HTTP POST con los datos JSON
            StartCoroutine(PostRequest(url, jsonData));
        }

        // Limpiar la lista después de enviar los datos
        scoreDataList.Clear();
    }

    // Método para enviar una solicitud HTTP POST
    private IEnumerator PostRequest(string url, string jsonData)
    {
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error al enviar el puntaje: " + request.error);
        }
        else
        {
            Debug.Log("Puntaje enviado exitosamente: " + jsonData);
        }
    }

    // Método para verificar si hay conexión a Internet
    private bool IsConnectedToInternet()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
