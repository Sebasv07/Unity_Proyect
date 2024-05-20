using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Object1 : MonoBehaviour
{
    

    public GameObject DoorRFalse;
    public GameObject DoorR;
    public GameObject DoorLFalse;
    public GameObject DoorL;
    public GameObject Handpoint;
    private GameObject pickedObject = null;
    public ScriptDoorOfice prueba;
    public GameObject interaccion;
    public GameObject mano;
   
    public GameObject soltar;

    void Start()
    {
        
        interaccion.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        Soltar();
        
        if(pickedObject == null)
        {
            mano.SetActive(false);
        }
        if(pickedObject != null)
        {
            mano.SetActive(true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("key"))
        {
            mano.SetActive(true);
            interaccion.SetActive(true);
            
        }
        if (other.gameObject.CompareTag("Door"))
        {
            mano.SetActive(true);
            interaccion.SetActive(true);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("key"))
        {
            interaccion.SetActive(false);
            
        }
        if (other.gameObject.CompareTag("Door"))
        {
            mano.SetActive(false);
            interaccion.SetActive(false);

        }
    }

    public void Soltar()
    {
        if(pickedObject != null)
        {
            soltar.SetActive(true);
            if (Input.GetKey("r"))
            {
                pickedObject.GetComponent<Rigidbody>().useGravity = true;
                pickedObject.GetComponent<Rigidbody>().isKinematic = false;
                pickedObject.GetComponent<Rigidbody>().freezeRotation = false;
                pickedObject.GetComponent<Rigidbody>().position = Vector3.zero;
                pickedObject.gameObject.transform.SetParent(null);
                pickedObject = null;
                mano.SetActive(false);


            }
        }
        if(pickedObject == null)
        {
            soltar.SetActive(false) ;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("guayaba_infect"))
            {
            if (Input.GetKey(KeyCode.E) && pickedObject == null)
            {

                other.GetComponent<Rigidbody>().useGravity = false;
                other.GetComponent<Rigidbody>().isKinematic = true;

                other.transform.position = Handpoint.transform.position;
                other.gameObject.transform.SetParent(Handpoint.gameObject.transform);

                pickedObject = other.gameObject;






            }

        }
        if (other.gameObject.CompareTag("object"))
            {

            if (Input.GetKey(KeyCode.E) && pickedObject==null)
            {
                
                other.GetComponent<Rigidbody>().useGravity = false;
                other.GetComponent <Rigidbody>().isKinematic = true;

                other.transform.position = Handpoint.transform.position;
                other.gameObject.transform.SetParent(Handpoint.gameObject.transform);

                pickedObject = other.gameObject;

                
                  
                
                

            }
        }
        if (other.gameObject.CompareTag("key"))
        {
          

            if (Input.GetKey(KeyCode.E) && pickedObject == null)
            {

                other.GetComponent<Rigidbody>().useGravity = false;
                other.GetComponent<Rigidbody>().isKinematic = true;

                other.transform.position = Handpoint.transform.position;
                other.gameObject.transform.SetParent(Handpoint.gameObject.transform);

                pickedObject = other.gameObject;
                DoorRFalse.SetActive(false);
                DoorR.SetActive(true);
                DoorLFalse.SetActive(false);
                DoorL.SetActive(true);





            }

        }
        

         
    }
    
}
