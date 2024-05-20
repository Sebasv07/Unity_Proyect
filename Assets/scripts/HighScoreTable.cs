using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HighScoreTable : MonoBehaviour
{
    [SerializeField]
    private Button ButtonAtras;

    private Transform entryContainer;
    private Transform entryTemplate;

    private void Awake()
    {
        entryContainer = transform.Find("HigshScoreContainer");
        entryTemplate = entryContainer.Find("HighScoreEntryTem");

        entryTemplate.gameObject.SetActive(false);

        // Obtener los datos del usuario logueado
        string username = PlayerPrefs.GetString("nameUser");
        int userId = PlayerPrefs.GetInt("userId");

        // Si no hay usuario logueado, mostrar un mensaje de error
        if (string.IsNullOrEmpty(username) || userId == 0)
        {
            Debug.LogError("Error: No se detecta usuario logueado");
            return;
        }

        // Iniciar la corrutina para verificar la conexión y obtener los datos de la API
        StartCoroutine(CheckConnectionAndFetchData(userId));
    }

    private IEnumerator CheckConnectionAndFetchData(int userId)
    {
        string url = "http://www.guayabagame.somee.com/api/Score";

        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error al conectar con la URL: " + request.error);
            // Puedes mostrar un mensaje de error en la UI si lo deseas
        }
        else
        {
            // Si la conexión es exitosa, procesar los datos
            StartCoroutine(GetHighScoresFromAPI(url, userId));
        }
    }

    private IEnumerator GetHighScoresFromAPI(string url, int userId)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            // Parsear la respuesta JSON
            string json = request.downloadHandler.text;
            List<HighScoreEntry> allHighScoreEntries = JsonUtility.FromJson<HighScoreEntryList>("{\"highScoreEntries\":" + json + "}").highScoreEntries;

            // Filtrar los puntajes por el ID de usuario logueado
            List<HighScoreEntry> userHighScoreEntries = new List<HighScoreEntry>();
            foreach (HighScoreEntry entry in allHighScoreEntries)
            {
                if (entry.userId == userId)
                {
                    userHighScoreEntries.Add(entry);
                }
            }

            // Rellenar la tabla con los datos obtenidos
            FillHighScoreTable(userHighScoreEntries);
        }
    }

    private void FillHighScoreTable(List<HighScoreEntry> highScoreEntries)
    {
        float templateHeight = 30f;

        for (int i = 0; i < highScoreEntries.Count; i++)
        {
            HighScoreEntry scoreEntry = highScoreEntries[i];
            Transform entryTransform = Instantiate(entryTemplate, entryContainer);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * i);
            entryTransform.gameObject.SetActive(true);

            entryTransform.Find("ITEMScore_IDText").GetComponent<Text>().text = scoreEntry.scoreId.ToString();
            entryTransform.Find("ITEMNameUserText").GetComponent<Text>().text = PlayerPrefs.GetString("nameUser");
            entryTransform.Find("ITEMScoreUserText").GetComponent<Text>().text = scoreEntry.score.ToString();

            // Parsear la fecha en formato ISO 8601
            DateTime date;
            if (DateTime.TryParse(scoreEntry.dateRegistrer, null, System.Globalization.DateTimeStyles.RoundtripKind, out date))
            {
                entryTransform.Find("ITEMFechaText").GetComponent<Text>().text = date.ToString("yyyy-MM-dd");
            }
            else
            {
                entryTransform.Find("ITEMFechaText").GetComponent<Text>().text = "Fecha inválida";
            }

            entryTransform.Find("ITEMNumPatidaText").GetComponent<Text>().text = scoreEntry.game.ToString();
        }
    }

    public void SaliraMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }



    [System.Serializable]
    public class HighScoreEntry
    {
        public int scoreId;
        public int userId;
        public int score;
        public string dateRegistrer; // Mantener como string para el parseo
        public int game;
    }

    [System.Serializable]
    public class HighScoreEntryList
    {
        public List<HighScoreEntry> highScoreEntries;
    }
}
