using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class npc1 : MonoBehaviour
{
    private MainMenu _mainMenu;
    public int NumNpc;

    public GameObject Simbolayuda;
    public GameObject Npc;
    public GameObject panelpregunta;
    public TextMeshProUGUI respuestatext;
    public GameObject Panelrespuesta;
    public TextMeshProUGUI TextoPlayer;
    public GameObject PanelNpc1;
    public TextMeshProUGUI TextoNpc1;
    public playercontroller jugador;
    public GameObject PanelMission2;
    public TextMeshProUGUI textmission2;
    public bool jugadorcerca;
    public bool encontrar;
    public bool ayuda;
    public bool text1;
    public bool text2;
    public bool key;
    public bool final1;
    public bool final2;
    public bool Finalmap;
    GameManager _gameManager;
    TimerAndScore _timerandscore;


    // Método donde la misión se completa
    // Método donde la misión se completa
 

    void Start()
    {
      
        jugador = GameObject.FindGameObjectWithTag("Player").GetComponent<playercontroller>();

      


        if (final1 == false)
        {
            panelpregunta.SetActive(false);
            PanelNpc1.SetActive(false);
            Panelrespuesta.SetActive(false);

            encontrar = false;
            ayuda = false;
            Npc.SetActive(false);
            text2 = false;
            text1 = false;
            key = false;
        }

        PanelMission2.SetActive(false);

        NumNpc = GameObject.FindGameObjectsWithTag("test").Length;
        textmission2.text = "consulta con la gente de los alrededores sobre el problema con las guayabas" +
                "\n restantes: " + NumNpc;
    }

    void CompleteMission()
    {
        LevelManager.instance.mission1Completed = true;
        // Otras lógicas relacionadas con completar la misión
    }

    void Update()
    {
        if (key == false)
        {
            if (ayuda == true)
            {
                Npc.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.E) && ayuda == false && jugadorcerca == true)
            {
                Vector3 posicionJugador = new Vector3(transform.position.x, jugador.gameObject.transform.position.y, transform.position.z);
                jugador.gameObject.transform.LookAt(posicionJugador);

                jugador.anim.SetFloat("X", 0);
                jugador.anim.SetFloat("Y", 0);
                jugador.enabled = false;
                panelpregunta.SetActive(false);
                PanelNpc1.SetActive(true);
                Panelrespuesta.SetActive(true);
            }
            if (PanelNpc1 == true && Input.GetKey(KeyCode.Y) && text1 == false && text2 == false)
            {
                TextoNpc1.text = "si señor, tenemos un nuevo caso de un virus en guayabas";
                TextoPlayer.text = "presiona 'T' -de guayabas?, y donde? \n preiona 'C'- ah juemadre va a tocar hacer una investigacion, sabes donde es?";

                text1 = true;
            }
            if (PanelNpc1 == true && Input.GetKeyDown(KeyCode.X) && text1 == false && text2 == false)
            {
                TextoNpc1.text = "le informo tenemos un nuevo caso";
                TextoPlayer.text = "presiona 'T' -y ahora donde es el caso? \n preiona 'C'- okey va a tocar hacer una investigacion, sabes donde es?";

                text1 = true;
            }
            if (PanelNpc1 == true && Input.GetKeyDown(KeyCode.T) && text1 == true && text2 == false)
            {
                TextoNpc1.text = "es en una finca, toca preguntar con los vecinos";
                TextoPlayer.text = "presiona 'Y' - listo ya voy, has visto mis llaves?  \n Presiona 'X' - voy consultar unos pocos";

                text2 = true;
            }
            if (PanelNpc1 == true && Input.GetKeyDown(KeyCode.C) && text1 == true && text2 == false)
            {
                TextoNpc1.text = "es en una finca, toca consultar en las calles";
                TextoPlayer.text = "presiona 'Y' - listo ya voy, has visto mis llaves?  \n Presiona 'X' - voy consultar ";
                text2 = true;
            }
            if (PanelNpc1 == true && Input.GetKeyDown(KeyCode.X) && text1 == true && text2 == true)
            {
                respuestatext.text = "presiona 'E' para preguntar por las llaves";
                TextoNpc1.text = "si señor?";
                TextoPlayer.text = "presiona 'Y'- has visto mis llaves?";
                PanelNpc1.SetActive(false);
                Panelrespuesta.SetActive(false);
                panelpregunta.SetActive(true);
                ayuda = false;
                encontrar = false;
                jugador.enabled = true;
            }
            if (PanelNpc1 == true && Input.GetKeyDown(KeyCode.Y) && text1 == true && text2 == true)
            {
                Npc.SetActive(true);
                ayuda = true;
                jugador.enabled = true;
                TextoNpc1.text = "las vi en su escritorio";
                TextoPlayer.text = "muchas gracias";
                Simbolayuda.SetActive(false);
                key = true;
                final1 = true;
                PanelMission2.SetActive(true);
            }
            if (jugadorcerca == false)
            {
                PanelNpc1.SetActive(false);
                Panelrespuesta.SetActive(false);
            }
        }
        if (key == true)
        {
            if (Finalmap == false)
            {
                NumNpc = GameObject.FindGameObjectsWithTag("test").Length;
                textmission2.text = "consulta con la gente de los alrededores sobre el problema con las guayabas" +
                        "\n restantes: " + NumNpc;
            }
        }
        if (NumNpc == 0)
        {
            Finalmap = true;
        }
        if (Finalmap == true)
        {
            textmission2.text = "bien hecho con esta informacion nos dirigimos a la granja \n presiona 'ESC' y luego dirigete a la granja";
            CompleteMission();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            jugadorcerca = true;
            if (ayuda == false)
            {
                panelpregunta.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (encontrar == false)
        {
            if (other.tag == "Player")
            {
                jugadorcerca = false;
                if (ayuda == true)
                {
                    Simbolayuda.SetActive(false);
                    Npc.SetActive(true);
                }
                panelpregunta.SetActive(false);
                PanelNpc1.SetActive(false);
                Panelrespuesta.SetActive(false);
            }
        }
    }
}
