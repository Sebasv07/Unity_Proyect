using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FirstMision : MonoBehaviour
{
    public int NumObjetivos;
    public GameObject panel;
    public TextMeshProUGUI TextMision;
    public playercontroller jugador;
    public GameObject SimbolMision;
    public GameObject Panel_Interacion1;
    public GameObject PanelDialog1;
    public bool JugadorCerca;
    public bool aceptarmision;
    public bool pasarmision;
    public bool mision;

    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(false);
        NumObjetivos= GameObject.FindGameObjectsWithTag("Table").Length;
        TextMision.text = "encuentra las tablas que estan flojas" +
            "\n restantes: " + NumObjetivos;
        
        jugador = GameObject.FindGameObjectWithTag("Player").GetComponent<playercontroller>();
        SimbolMision.SetActive(true);
        Panel_Interacion1.SetActive(false);
        pasarmision = false;

    }

    // Update is called once per frame
    void Update()
    {
        if(mision == true)
        {
            SimbolMision.SetActive(false);

        }
        if (Input.GetKeyDown(KeyCode.E) && aceptarmision == false && JugadorCerca == true &&mision == false)
        {
            Vector3 posicionJugador = new Vector3(transform.position.x, jugador.gameObject.transform.position.y, transform.position.z);
            jugador.gameObject.transform.LookAt(posicionJugador);



            jugador.anim.SetFloat("X", 0);
            jugador.anim.SetFloat("Y", 0);
            jugador.enabled = false;
            Panel_Interacion1.SetActive(false);
            PanelDialog1.SetActive(true);

        }
        if (PanelDialog1 == true && Input.GetKeyDown(KeyCode.Y) && JugadorCerca == true)
        {
            Si();
            panel.SetActive(false);

        }
        if (PanelDialog1 == true && Input.GetKeyDown(KeyCode.X) && JugadorCerca == true)
        {
            No();
        }
        if (pasarmision == false)
        {
            NumObjetivos = GameObject.FindGameObjectsWithTag("Table").Length;
            TextMision.text = "encuentra las tablas que estan flojas" +
                "\n restantes: " + NumObjetivos;
        }
        if (NumObjetivos <= 1)
        {
            if (pasarmision == false)
            {
                TextMision.text = "bien hecho ahora presiona ´T´ para la siguiente mision";
            }
                if (Input.GetKeyDown(KeyCode.T))    
            {


                pasarmision = true;
                TextMision.text = "busca al granjero";
                mision = true;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (mision == false)
            {
                JugadorCerca = true;
                if (aceptarmision == false)
                {
                    Panel_Interacion1.SetActive(true);
                }
            }
        }




    }
    private void OnTriggerExit(Collider other)
    {
        if (mision == false)
        {
            if (other.tag == "Player")
            {

                JugadorCerca = false;
                if (aceptarmision == true)
                {
                    panel.SetActive(true);
                }

                Panel_Interacion1.SetActive(false);
            }
        }
    }
    public void No()
    {
        jugador.enabled = true;

        PanelDialog1.SetActive(false);

        Panel_Interacion1.SetActive(true);

    }

    public void Si()
    {
        jugador.enabled = true;
        aceptarmision = true;


        JugadorCerca = false;
        SimbolMision.SetActive(false);
        Panel_Interacion1.SetActive(false);
        PanelDialog1.SetActive(false);
        panel.SetActive(true);
    }





}

    

