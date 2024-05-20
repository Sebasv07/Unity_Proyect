using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class npcsaludo : MonoBehaviour
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
            texto1.text = "buen dia detective juanito que le puedo ayudar?";
            texto2.text = "presiona 'Y'- perdone sabe usted algo sobre las gauyabas infectadas en el mercado?" +
                "\n presiona 'X'- no que pena me equivoque";
        }
        if (panel1 == true && panel2 == true && Input.GetKeyDown(KeyCode.Y) && jugadorcerca1 == true)
        {
            texto1.text = "¡Hola, Detective Juanito! Interesante pregunta. Bueno, sí, compré algunas guayabas la semana pasada en el mercado local. ¡Pero vaya que fue una sorpresa! Cuando llegué a casa y las inspeccioné, noté que algunas tenían manchas marrones oscuras y una textura un poco viscosa. No parecían en buen estado, así que las tuve que desechar. ¿Crees que podría haber algún problema con ellas?";
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
