using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject ObjetoMenuPausa;
    public bool Pausa = false;
    public bool IsPaused = false;
    public TimerAndScore Time_Score;
    void Start()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(IsPaused == true)
        {
           // Time_Score.PauseTimer();

            if (Pausa == false)
            {
                ObjetoMenuPausa.SetActive(true);
                Pausa = true;
                Time.timeScale = 0;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                //Time_Score.ResumeTimer();
            }
            else if (Pausa)
            {
                Resumir();

            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Pausa == false)
            {
                ObjetoMenuPausa.SetActive(true);
                Pausa = true;
                Time.timeScale = 0;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else if(Pausa)
            {
                Resumir();
            }
        }
    }

    public void Resumir()
    {
        ObjetoMenuPausa.SetActive(false);
        Pausa = false;
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void IrAlMenu(string NombreMenu)
    {
        SceneManager.LoadScene(NombreMenu);
    }

    public void SalirDelJuego()
    {
        // Reiniciar los valores de las preferencias del jugador al cerrar la aplicación
        PlayerPrefs.DeleteKey("nameUser");
        PlayerPrefs.DeleteKey("userId");
        PlayerPrefs.Save();
        Application.Quit();
    }

    public void IrAlMapa(string Mapa)
    {
        SceneManager.LoadScene(Mapa);
    }
}
