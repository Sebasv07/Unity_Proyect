using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor.Experimental.GraphView;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    public Text UserLogin;

    [SerializeField]
    private Text TextMensaje;

    [SerializeField]
    private Button ButtonAtrasMenu;

    [SerializeField]
    private Button ButtonIniciar;

    [SerializeField]
    private Button ButtonScore;



    GameManager gameManager;
    void Start()
    {
        gameManager = GameManager.Instance;
        // Obtener los datos del usuario logueado
        string username = PlayerPrefs.GetString("nameUser");
        int userId = PlayerPrefs.GetInt("userId");
        

        // Actualizar el texto con los datos del usuario
        UserLogin.text = $"Usuario: {username}\n\nID: {userId}";
 
    }

    public void IniJuego()
    {
        SceneManager.LoadScene("OfficeScene");
        LevelManager.instance.mission1Completed = false;
        LevelManager.instance.mission2Completed = false;
        LevelManager.instance.mission3Completed = false;
        LevelManager.instance.mission4Completed = false;
        LevelManager.instance.Juego_Finalizado = false;
        gameManager.ResetStartTime();
        gameManager.ResetScore();

        
    }




    public void CargarNivel(string NombreNivel)
    {
        if (NombreNivel == "OfficeScene")
        {
            if (!LevelManager.instance.mission1Completed)
            {
                SceneManager.LoadScene("OfficeScene");
            }
            else
            {
                TextMensaje.text = "Ya has completado la misión en la Oficina.";
            }
        }
        else if (NombreNivel == "Farm")
        {
            if (LevelManager.instance.mission1Completed && !LevelManager.instance.mission2Completed)
            {
                SceneManager.LoadScene("Farm");
            }
            else if (!LevelManager.instance.mission1Completed)
            {
                TextMensaje.text = "Debes completar la misión en la Oficina primero.";
            }
            else
            {
                TextMensaje.text = "Ya has completado la misión en la Granja.";
            }
        }
        else if (NombreNivel == "Store")
        {
            if (LevelManager.instance.mission2Completed && !LevelManager.instance.mission3Completed)
            {
                SceneManager.LoadScene("Store");
            }
            else if (!LevelManager.instance.mission2Completed)
            {
                TextMensaje.text = "Debes completar la misión en la Granja primero.";
            }
            else
            {
                TextMensaje.text = "Ya has completado la misión en la Tienda.";
            }
        }
        else if (NombreNivel == "laboratory")
        {
            if (LevelManager.instance.mission3Completed && !LevelManager.instance.mission4Completed)
            {
                SceneManager.LoadScene("laboratory");
            }
            else if (!LevelManager.instance.mission3Completed)
            {
                TextMensaje.text = "Debes completar la misión en la Tienda primero.";
            }
            else
            {
                TextMensaje.text = "Ya has completado la misión en el Laboratorio.";
            }
        }
        else
        {
            TextMensaje.text = "Error al cargar Mundo.";
        }
    }


    public void Salir()
    {
        // Reiniciar los valores de las preferencias del jugador al cerrar la aplicación
        PlayerPrefs.DeleteKey("nameUser");
        PlayerPrefs.DeleteKey("userId");
        PlayerPrefs.Save();
        Application.Quit();
    }

    public void Score()
    {
        SceneManager.LoadScene("ShowScore");
    }

}
