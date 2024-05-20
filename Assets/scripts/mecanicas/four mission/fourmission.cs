using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class fourmission : MonoBehaviour
{
    
    public int contador;
    public GameObject panelmission;
    public TextMeshProUGUI textomission;
    public GameObject panel1;
    public GameObject panelinteraccion;
    public GameObject panelinteraccion2;

    public TextMeshProUGUI texto1;
    Animator anim;
    public GameObject Handpoint;
    private GameObject pickedObject = null;
    public bool mision;
    public GameObject GetScore;
    private TimerAndScore timerAndScore;
    private ScoreSender scoreSender;




    void Start()
    {

        timerAndScore = FindObjectOfType<TimerAndScore>();
        scoreSender = FindObjectOfType<ScoreSender>();

        timerAndScore = FindObjectOfType<TimerAndScore>();
        scoreSender = FindObjectOfType<ScoreSender>();

        // Obtener el valor actual del puntaje



        contador = GameObject.FindGameObjectsWithTag("examinarcilindro").Length + GameObject.FindGameObjectsWithTag("examinarguayabagrande").Length + GameObject.FindGameObjectsWithTag("examinarguayaba").Length + GameObject.FindGameObjectsWithTag("examinarguayaba2").Length + GameObject.FindGameObjectsWithTag("examinarcajas").Length;
       textomission.text = "examina todos lo recolectado en el laboratorio \n restantes: "+ contador;
        anim = GetComponentInChildren<Animator>();
        panelinteraccion2.SetActive(false);
        mision = false;
        panelmission.SetActive(true);
       
    }
    // Update is called once per frame

    void CompleteMission()
    {
        LevelManager.instance.mission4Completed = true;
        LevelManager.instance.Juego_Finalizado = true;
        // Otras lógicas relacionadas con completar la misión
    }

    private void LoadNextScene()
    {
        // Redirigir a la escena "GameFind"
        SceneManager.LoadScene("GameFind");
    }



    void Update()
    {
        Soltar();
        if(panelinteraccion2 == true && Input.GetKeyDown(KeyCode.Y))
        {
            pickedObject.GetComponent<ObjectInt>().ActivateObject();
            panelinteraccion.SetActive(false);
            panel1.SetActive(false);
            panelinteraccion2.SetActive(false);
        }
        if(pickedObject == null)
        {
            panelinteraccion2.SetActive(false);
        }
        if(mision == false)
        {
            contador = GameObject.FindGameObjectsWithTag("examinarcilindro").Length + GameObject.FindGameObjectsWithTag("examinarguayabagrande").Length + GameObject.FindGameObjectsWithTag("examinarguayaba").Length + GameObject.FindGameObjectsWithTag("examinarguayaba2").Length + GameObject.FindGameObjectsWithTag("examinarcajas").Length;
            textomission.text = "examina todos lo recolectado en el laboratorio \n restantes: " + contador;

        }
        if(contador == 0)
        {
            ///////////////////////////////////////////
            int score = (int)timerAndScore.GetScore();
            ////////////////////////////// Llamar al método para enviar los datos a la API
            scoreSender.AddScoreData(score);
            scoreSender.FinishGameAndSendScores();
            // Pausar el tiempo y el puntaje
            timerAndScore.PauseTimerAndScore();
            CompleteMission();

            // Actualizar el texto de la misión
            textomission.text = "perfecto, con esto ya tenemos para detener esta placa, \n muchas gracias por tu ayuda, \n hasta el próximo caso";
            /////////////////////////////////////////////////////////////

            // Esperar 5 segundos antes de redirigir a la siguiente escena
            Invoke("LoadNextScene", 5f);

            if (Input.GetKeyDown(KeyCode.E))
            {
                panelmission.SetActive(false);
                mision = true;
            }
        }

    }

    public void Soltar()
    {
        if (pickedObject != null)
        {
            if (Input.GetKey("r"))
            {
                
                pickedObject.GetComponent<Rigidbody>().useGravity = true;
                pickedObject.GetComponent<Rigidbody>().isKinematic = false;
                pickedObject.GetComponent<Rigidbody>().freezeRotation = false;
                pickedObject.GetComponent<Rigidbody>().position = Vector3.zero;
                pickedObject.gameObject.transform.SetParent(null);
                pickedObject = null;


            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("examinarcilindro"))
        {
            panelinteraccion.SetActive(true);
            if (Input.GetKey(KeyCode.E) && pickedObject == null)
            {
                panelinteraccion.SetActive(false);

                other.GetComponent<Rigidbody>().useGravity = false;
                other.GetComponent<Rigidbody>().isKinematic = true;

                other.transform.position = Handpoint.transform.position;
                other.gameObject.transform.SetParent(Handpoint.gameObject.transform);

                pickedObject = other.gameObject;
                panel1.SetActive(true);
                texto1.text = "Aunque el hongo se puede dar naturalmente, mucho fertilizante nitrogenado como este potencia el moho gris... ";
                panelinteraccion2.SetActive(true);
            }

        }
        if (other.gameObject.CompareTag("examinarguayabagrande"))
        {
            panelinteraccion.SetActive(true);
            if (Input.GetKey(KeyCode.E) && pickedObject == null)
            {
                panelinteraccion.SetActive(false);

                other.GetComponent<Rigidbody>().useGravity = false;
                other.GetComponent<Rigidbody>().isKinematic = true;

                other.transform.position = Handpoint.transform.position;
                other.gameObject.transform.SetParent(Handpoint.gameObject.transform);

                pickedObject = other.gameObject;
                panel1.SetActive(true);
                texto1.text = "Esta guayaba esta muy grande, y se ve normal por fuera pero por dentro esta infectada, tal y como dijo el señor de la ciudad...";
                panelinteraccion2.SetActive(true);
            }

        }
        if (other.gameObject.CompareTag("examinarguayaba"))
        {
            panelinteraccion.SetActive(true);
            if (Input.GetKey(KeyCode.E) && pickedObject == null)
            {
                panelinteraccion.SetActive(false);

                other.GetComponent<Rigidbody>().useGravity = false;
                other.GetComponent<Rigidbody>().isKinematic = true;

                other.transform.position = Handpoint.transform.position;
                other.gameObject.transform.SetParent(Handpoint.gameObject.transform);

                pickedObject = other.gameObject;
                panel1.SetActive(true);
                texto1.text = "Parece que esta guayaba tiene una enfermedad muy comun llamada Botritis cinerea...";
                panelinteraccion2.SetActive(true);
            }

        }
        if (other.gameObject.CompareTag("examinarguayaba2"))
        {
            panelinteraccion.SetActive(true);
            if (Input.GetKey(KeyCode.E) && pickedObject == null)
            {
                panelinteraccion.SetActive(false);

                other.GetComponent<Rigidbody>().useGravity = false;
                other.GetComponent<Rigidbody>().isKinematic = true;

                other.transform.position = Handpoint.transform.position;
                other.gameObject.transform.SetParent(Handpoint.gameObject.transform);

                pickedObject = other.gameObject;
                panel1.SetActive(true);
                texto1.text = "Parece ser que esta fue la primer infectada, ya que la enfermedad esta mas avanzada...";
                panelinteraccion2.SetActive(true);
            }

        }
        if (other.gameObject.CompareTag("examinarcajas"))
        {
            panelinteraccion.SetActive(true);
            if (Input.GetKey(KeyCode.E) && pickedObject == null)
            {
                panelinteraccion.SetActive(false);

                other.GetComponent<Rigidbody>().useGravity = false;
                other.GetComponent<Rigidbody>().isKinematic = true;

                other.transform.position = Handpoint.transform.position;
                other.gameObject.transform.SetParent(Handpoint.gameObject.transform);

                pickedObject = other.gameObject;
                panel1.SetActive(true);
                texto1.text = "Uh parece ser que debido a las condiciones en que se encuentra esta caja es que ela hongo se anida, debemos cambiar esas condiciones en el cultivo...";
                panelinteraccion2.SetActive(true);
            }

        }






    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("examinarcilindro"))
        {
            panelinteraccion.SetActive(false);
            

        }
        if (other.gameObject.CompareTag("examinarguayabagrande"))
        {
            panelinteraccion.SetActive(false);


        }
        if (other.gameObject.CompareTag("examinarguayaba"))
        {
            panelinteraccion.SetActive(false);


        }
        if (other.gameObject.CompareTag("examinarguayaba2"))
        {
            panelinteraccion.SetActive(false);


        }
        if (other.gameObject.CompareTag("examinarcajas"))
        {
            panelinteraccion.SetActive(false);


        }
    }

}
