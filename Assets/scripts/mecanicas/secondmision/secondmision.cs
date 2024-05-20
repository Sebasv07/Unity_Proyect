using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class secondmision : MonoBehaviour
{
    public Pause pause;
    public GameObject panel;
    public GameObject SimbolMision;
    public playercontroller jugador;
    public GameObject Panel_Interacion;
    public GameObject PanelDialog;
    public GameObject PanelMision2;

    public TextMeshProUGUI TextMision2;
    public bool JugadorCerca;
    public bool aceptarmision;
    public GameObject[] objetivos;
    public int NumDeObjetivos;
    public GameObject boton;

    void CompleteMission()
    {
        LevelManager.instance.mission2Completed = true;
        // Otras lógicas relacionadas con completar la misión
    }


    // Start is called before the first frame update
    void Start()
    {
        NumDeObjetivos = GameObject.FindGameObjectsWithTag("guayaba_infect").Length;
        TextMision2.text = "ayudame a recoger las guayabas infectadas" +
                           "\n Restantes: " + NumDeObjetivos;
        jugador = GameObject.FindGameObjectWithTag("Player").GetComponent<playercontroller>();
        SimbolMision.SetActive(true);
        Panel_Interacion.SetActive(false);



    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)&& aceptarmision == false && JugadorCerca==true)
        {
            Vector3 posicionJugador = new Vector3(transform.position.x, jugador.gameObject.transform.position.y, transform.position.z);
            jugador.gameObject.transform.LookAt(posicionJugador);



            jugador.anim.SetFloat("X", 0);
            jugador.anim.SetFloat("Y", 0);
            jugador.enabled = false;
            Panel_Interacion.SetActive(false);
            PanelDialog.SetActive(true);
            
        }
        if (PanelDialog == true && Input.GetKeyDown(KeyCode.Y) && JugadorCerca == true)
        {
            Si();
            panel.SetActive(false);

        }
        if (PanelDialog == true && Input.GetKeyDown(KeyCode.X) && JugadorCerca == true)
        {
            No();
        }
        NumDeObjetivos = GameObject.FindGameObjectsWithTag("guayaba_infect").Length;
        TextMision2.text = "ayudame a recoger las guayabas infectadas" +
                           "\n Restantes: " + NumDeObjetivos;
        if (NumDeObjetivos <= 0)
        {
            TextMision2.text = "Bien hecho ahora a la tienda para buscar mas pruebas \n presiona ESC para moverte a ella";
            CompleteMission();

            if (NumDeObjetivos <= 0 && Input.GetKeyDown(KeyCode.T))
            {
                PanelMision2.SetActive(false);
                pause.IsPaused = true;
                

                



            }


        }
      
        
    }
   
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            
            JugadorCerca = true;
            if (aceptarmision == false)
            {
                Panel_Interacion.SetActive(true);
            }
            panel.SetActive(false);
        }
        



    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            
            JugadorCerca = false;
            if (aceptarmision == false) { panel.SetActive(true); }
            if(aceptarmision == true)
            {
                panel.SetActive(false);
            }
        
            Panel_Interacion.SetActive(false);
        }
    }
    public void No()
    {
        jugador.enabled = true;

        PanelDialog.SetActive(false);

        Panel_Interacion.SetActive(true);

    }

    public void Si()
    {
        jugador.enabled = true;
        aceptarmision= true;

        
        JugadorCerca = false;
        SimbolMision.SetActive(false);
        Panel_Interacion.SetActive(false);
        PanelDialog.SetActive(false);
        PanelMision2.SetActive(true);
    }





}
