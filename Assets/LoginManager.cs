using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Text;
using System;

public class LoginManager : MonoBehaviour
{
    public InputField usernameInput;
    public InputField passwordInput;
    public Button loginButton;
    public Button registerButton;
    public Text messageText;

    private string baseUrl = "http://www.guayabagame.somee.com/api/";

    void Start()
    {
        //PlayerPrefs.DeleteKey("nameUser");
        //PlayerPrefs.DeleteKey("userId");
        //PlayerPrefs.Save();

        loginButton.onClick.AddListener(OnLoginButtonClick);
        registerButton.onClick.AddListener(OnRegisterButtonClick);
    }

    void OnLoginButtonClick()
    {
        StartCoroutine(Login(usernameInput.text, passwordInput.text));
    }

    void OnRegisterButtonClick()
    {
        // Implementar lógica de registro si es necesario
    }

    IEnumerator Login(string username, string password)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            messageText.text = "Por favor, ingresa el nombre de usuario y la contraseña.";
            yield break;
        }

        string url = $"{baseUrl}Users?nameUser={username}";
        Debug.Log("Requesting URL: " + url);

        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            messageText.text = "Error al consultar la API: " + request.error;
            Debug.LogError($"Error al consultar la API: {request.error} | URL: {url}");
            yield break;
        }

        if (request.responseCode == 404)
        {
            messageText.text = "Usuario no encontrado.";
            Debug.LogError($"Usuario no encontrado en la URL: {url}");
            yield break;
        }

        string jsonResponse = request.downloadHandler.text;
        Debug.Log("Response: " + jsonResponse);

        List<UserViewModel> users = JsonConvert.DeserializeObject<List<UserViewModel>>(jsonResponse);
        var user = users?.Find(u => u.NameUser.Equals(username, StringComparison.OrdinalIgnoreCase));

        if (user == null)
        {
            messageText.text = "El nombre de usuario no existe. ¿Deseas registrarte?";
            yield break;
        }

        if (!user.Active)
        {
            messageText.text = "Usuario inactivo. Por favor, contacte con el administrador.";
            yield break;
        }

        string encryptedPassword = EncryptPassword(password);

        if (user.Password != encryptedPassword)
        {
            messageText.text = "Usuario o contraseña incorrecta.";
            yield break;
        }

        // Aquí puedes guardar los datos del usuario en PlayerPrefs
        PlayerPrefs.SetString("nameUser", user.NameUser);
        PlayerPrefs.SetInt("userId", user.UserId); // Assuming UserId is an int
        bool isAdmin = user.NameUser.Equals("admin", StringComparison.OrdinalIgnoreCase);
        PlayerPrefs.SetString("IsAdmin", isAdmin.ToString());

        messageText.text = "Login exitoso";
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        // Aquí puedes cargar la siguiente escena o hacer algo más
    }

    private string EncryptPassword(string password)
    {
        // Esta es una implementación básica de encriptación, se recomienda mejorarla para un entorno de producción
        using (var sha256 = System.Security.Cryptography.SHA256.Create())
        {
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }

    [System.Serializable]
    public class UserViewModel
    {
        public int UserId; // Added UserId field
        public string NameUser;
        public string Password;
        public bool Active;
    }
}
