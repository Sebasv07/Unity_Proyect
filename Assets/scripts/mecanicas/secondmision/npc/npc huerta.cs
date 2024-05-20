using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class npchuerta : MonoBehaviour
{
    public GameObject objeto;
    public GameObject panel1;
    public GameObject panel2;
    public TextMeshProUGUI texto1;
    public TextMeshProUGUI texto2;
    public GameObject panelinteraccion;
    public playercontroller jugador;
    public bool jugadorcerca1;
    public bool informacion;




    // Start is called before the first frame update
    void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player").GetComponent<playercontroller>();
        objeto.SetActive(true);
        informacion = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (informacion == true)
        {
            objeto.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.E) && informacion == false && jugadorcerca1 == true)
        {
            Vector3 posicionJugador = new Vector3(transform.position.x, jugador.gameObject.transform.position.y, transform.position.z);
            jugador.gameObject.transform.LookAt(posicionJugador);



            jugador.anim.SetFloat("X", 0);
            jugador.anim.SetFloat("Y", 0);
            jugador.enabled = false;
            panelinteraccion.SetActive(false);
            panel1.SetActive(true);
            panel2.SetActive(true);
            texto1.text = "buen dia, necesita algo?";
            texto2.text = "presiona 'Y'- perdone sabe usted algo sobre las gauyabas infectadas en el mercado?" +
                "\n presiona 'X'- no que pena me equivoque";
        }
        if (panel1 == true && panel2 == true && Input.GetKeyDown(KeyCode.Y) && jugadorcerca1 == true)
        {
            texto1.text = "¡Hola! Bueno, sé un poquito sobre esas guayabas. Escuché a mi vecino, que es dueño de una tienda de frutas, comentar sobre eso el otro día. Dijo que las guayabas tenían como unas manchas raras, así como un olor extraño, ¿sabes? Dijo algo sobre un hongo que afecta a las frutas, creo que se llama **Botrytis cinerea** o algo así. Parece ser un problema para los agricultores. Nunca he tenido mucha suerte con las plantas, pero suena bastante desagradable.";
            texto2.text = "vale muchas gracias por su informacion";
            informacion = true;
            jugador.enabled = true;

        }
        if (panel1 == true && panel2 == true && Input.GetKeyDown(KeyCode.X) && jugadorcerca1 == true)
        {
            informacion = false;
            panel1.SetActive(false);
            panel2.SetActive(false);
            jugador.enabled = true;
            panelinteraccion.SetActive(true);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (informacion == false)
            {
                jugadorcerca1 = true;
                panelinteraccion.SetActive(true);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (informacion == false)
        {
            if (other.tag == "Player")
            {

                jugadorcerca1 = false;
                panelinteraccion.SetActive(false);
            }
        }

        if (informacion == true)
        {
            if (other.tag == "Player")
            {
                panel1.SetActive(false);
                panel2.SetActive(false);
                panelinteraccion.SetActive(false);
                jugadorcerca1 = false;
            }
        }
    }

}
