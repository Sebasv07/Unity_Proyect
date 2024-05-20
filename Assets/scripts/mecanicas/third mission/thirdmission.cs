using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class thirdmission : MonoBehaviour
{
    public bool mission_3; 
    public GameObject panelmission;
    public TextMeshProUGUI textomission;
    public GameObject panel1;
    public TextMeshProUGUI texto1;
    public GameObject panel2;
    public TextMeshProUGUI texto2;
    public GameObject panelinteraccion;
    public int numobjetos;
    public GameObject simbolnpc;
    public playercontroller jugador;
    public bool jugadorcerca;
    public bool aceptarmission;
    public GameObject objetos;
    // Start is called before the first frame update


    void CompleteMission()
    {
        LevelManager.instance.mission3Completed = true;
        // Otras lógicas relacionadas con completar la misión
    }

    void Start()
    {
        panelmission.SetActive(false);
        simbolnpc.SetActive(true);
        numobjetos = GameObject.FindGameObjectsWithTag("pruebas").Length;
        textomission.text = "encuentre y agarre los objetos que se ven de dudosa procedencia"+
            "\n restantes: " + numobjetos;
        jugador = GameObject.FindGameObjectWithTag("Player").GetComponent<playercontroller>();
        mission_3 = false;
        aceptarmission = false;



    }

    // Update is called once per frame
    void Update()
    {
        if(aceptarmission == true)
        {
            simbolnpc.SetActive (false);
        }
        if(aceptarmission == false && Input.GetKeyDown(KeyCode.E) && mission_3 == false && jugadorcerca == true )
        {
            Vector3 posicionJugador = new Vector3(transform.position.x, jugador.gameObject.transform.position.y, transform.position.z);
            jugador.gameObject.transform.LookAt(posicionJugador);



            jugador.anim.SetFloat("X", 0);
            jugador.anim.SetFloat("Y", 0);
            jugador.enabled = false;
            panelinteraccion.SetActive(false);
            panel1.SetActive(true);
            panel2.SetActive(true);
            texto1.text = "esta listo para encontrar los objetos sospechosos?";
            texto2.text = "presiona 'Y'- claro vamos a encontrar de donde vienen esas guayabas \n presiona 'X'-dame un momento ";

        }
        if(panel1 == true &&panel2 == true && jugadorcerca == true &&Input.GetKeyDown(KeyCode.Y))
        {
            panel2.SetActive (false);
            panel1.SetActive (false);
            panelmission.SetActive(true);
            jugador.enabled = true;
            jugadorcerca = false;
            panelinteraccion.SetActive (false);
            aceptarmission = true;
        }
        if (panel1 == true && panel2 == true && jugadorcerca == true && Input.GetKeyDown(KeyCode.X))
        {
            panel2.SetActive(false);
            panel1.SetActive(false);
            jugador.enabled = true;
            jugadorcerca = false;
            panelinteraccion.SetActive(true); 
            aceptarmission = false;
        }
        if (mission_3 == false)
        {
            numobjetos = GameObject.FindGameObjectsWithTag("pruebas").Length;
            textomission.text = "encuentre y agarre los objetos que se ven de dudosa procedencia" +
                "\n restantes: " + numobjetos;
        }
        if(numobjetos == 0)
        {
            
            mission_3 = true;
            textomission.text = "bien hecho con esto tenemos lo suficiente para detener esa plaga en las guayabas \n presiona 'ESC' y dirigete al laboratorio en el mapa ";
            CompleteMission();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(mission_3 == false)
            {
                jugadorcerca = true;
                if(aceptarmission == false)
                {
                    panelinteraccion.SetActive(true);
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (mission_3 == false)
        {
            if (other.tag == "Player")
            {
                jugadorcerca = false;
                if(aceptarmission == true)
                {
                    objetos.SetActive(true);
                }
                panelinteraccion.SetActive(false);
            }
        }
    }
}
