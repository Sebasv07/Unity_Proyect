using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class miss : MonoBehaviour
{
    public bool mission_3_1;

    public GameObject panel1_1;
    public TextMeshProUGUI texto1;
    public GameObject panel2;
    public TextMeshProUGUI texto2;
    public GameObject panelinteraccion;
    public GameObject simbolnpc;
    public playercontroller jugador;
    public bool jugadorcerca;
    public bool info;
    public bool tec;
    public GameObject panelmis;
    public TextMeshProUGUI textmis;
    // Start is called before the first frame update
    void Start()
    {
        
        simbolnpc.SetActive(true);
        
        jugador = GameObject.FindGameObjectWithTag("Player").GetComponent<playercontroller>();
       
        info = false;
        tec = false;


    }

    // Update is called once per frame
    void Update()
    {
       
        
        if ( tec == false && info == false && Input.GetKeyDown(KeyCode.E) && jugadorcerca == true)
        {
            Vector3 posicionJugador = new Vector3(transform.position.x, jugador.gameObject.transform.position.y, transform.position.z);
            jugador.gameObject.transform.LookAt(posicionJugador);



            jugador.anim.SetFloat("X", 0);
            jugador.anim.SetFloat("Y", 0);
            jugador.enabled = false;
            panelinteraccion.SetActive(false);
            panel1_1.SetActive(true);
            panel2.SetActive(true);
            texto1.text = "¿Que me has traido el dia de hoy?";
            texto2.text = "presiona 'Y'- Un extraño caso de guayabas dañadas y quiero saber que tienen, miralas \n presiona 'X'-dame un momento ";

        }
        if (tec == false && info == false &&panel1_1 == true && panel2 == true && jugadorcerca == true && Input.GetKeyDown(KeyCode.Y))
        {
            texto1.text = "Mmmm nomas por como se ven fisicamente puedo decir que es algo muy común llamado Moho gris.";
            texto2.text = "Presiona 'T'- Mmm voy a revisarlas mas a fondo, muchas gracias.";
            tec = true;
            
            
        }
        if (tec  = true &&info == false && panel1_1 == true && panel2 == true && jugadorcerca == true && Input.GetKeyDown(KeyCode.T))
        {
            texto1.text = "Mmmm nomas por como se ven fisicamente puedo decir que es algo muy común llamado Moho gris.";
            texto2.text = "Presiona 'T'- Mmm voy a revisarlas mas a fondo, muchas gracias.";

            info = true;
            simbolnpc.SetActive(false);
            jugador.enabled = true;
            jugadorcerca = false;

        }
        if ( tec  == false && info == false && panel1_1 == true && panel2 == true && jugadorcerca == true && Input.GetKeyDown(KeyCode.X))
        {
            panel2.SetActive(false);
            panel1_1.SetActive(false);
            jugador.enabled = true;
            jugadorcerca = false;
            panelinteraccion.SetActive(true);
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (info == false)
            {
                panelmis.SetActive(false);
                jugadorcerca = true;
                
                    panelinteraccion.SetActive(true);
                
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            if (info == true)
            {
                panel1_1.SetActive(false);
                panel2.SetActive(false);
                panelmis.SetActive(true);
                jugadorcerca = false;
                panelinteraccion.SetActive(false);
            }
        }
        if (info == false) 
        {
            if (other.tag == "Player")
            {
                jugadorcerca = false;
                panel1_1.SetActive(false);
                panel2.SetActive(false);
                panelmis.SetActive(true);
                panelinteraccion.SetActive(false);
            }
        }
    }
}
