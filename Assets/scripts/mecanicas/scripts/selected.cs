using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements;
using System.Runtime.Serialization.Formatters;

public class selected : MonoBehaviour

{
    LayerMask mask;

    public float distancia = 8;

    public Texture2D puntero;
    public GameObject TextDetect;
    GameObject ultimoreconocido = null;
    
    public GameObject Panel_Guayaba_infect;
    
    public GameObject Panel_selected;
    public GameObject panel;


    // Start is called before the first frame update
    void Start()
    {
        mask = LayerMask.GetMask("raycast detect");
        
        


    }

    // Update is called once per frame
    void Update()
    {
        //raycast
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distancia, mask))
        {
            //aqui se agregan los tipos de colores que se quieran que tengan lso objetos con sus tag
            Deselected();
           if(hit.collider.tag == "guayaba_infect")
            {
               
                SelectGuayaba_I(hit.transform);
               

            }
           



            if (hit.collider.tag == "DoorOfice")
            {
                if (Input.GetKeyDown(KeyCode.E))
                {

                    hit.collider.transform.GetComponent<ScriptDoor2>().ChangeDoorState();
                }
            }
            if (hit.collider.tag == "doorstore")
            {
                if (Input.GetKeyDown(KeyCode.E))
                {

                    hit.collider.transform.GetComponent<scriptdoorstore>().ChangeDoorState();
                }
            }
            if (hit.collider.tag == "DoorOfice1")
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.collider.transform.GetComponent<ScriptDoorOfice>().ChangeDoorState();
                }
            }
            if (hit.collider.tag == "object")
            {
               
                SelectedObject(hit.transform);
                

            }
            if (hit.collider.tag == "Door")
            {
                
                if (Input.GetKeyDown(KeyCode.E))
                {

                    SelectedObject(hit.transform);
                    hit.collider.transform.GetComponent<ScriptDoor>().ChangeDoorState();
                }
            }
            if (hit.collider.tag == "Table")
            {
                SelectedObjectNT(hit.transform);
                
                if (Input.GetKeyDown(KeyCode.E))
                {

                    hit.collider.transform.GetComponent<ObjectInt>().ActivateObject();
                    
                    

                }
            }
            
            if (hit.collider.tag == "interact")
            {
             SelectedObjectNT(hit.transform);
                
                if (Input.GetKeyDown(KeyCode.E)) 
                {
                    
                    hit.collider.transform.GetComponent<ObjectInt>().ActivateObject();
                    

                }
            }
            if (hit.collider.tag == "pruebas")
            {
                SelectedObjectNT(hit.transform);

                if (Input.GetKeyDown(KeyCode.E))
                {

                    hit.collider.transform.GetComponent<ObjectInt>().ActivateObject();


                }
            }


        }
        else
        {
            Deselected();
        }
        

    }
    //first mision
    

    private void SelectGuayaba_I(Transform transform)
    {
        Panel_Guayaba_infect.SetActive(true);
        transform.GetComponent<MeshRenderer>().material.color = Color.red;
        ultimoreconocido = transform.gameObject;
    }
    
    private void SelectedObjectNT(Transform transform)
    {
        Panel_selected.SetActive(true);
        transform.GetComponent<MeshRenderer>().material.color = Color.red;
        ultimoreconocido = transform.gameObject;
    }
    private void SelectedObject(Transform transform) 
    {
        
        transform.GetComponent<MeshRenderer>().material.color = Color.green;
        ultimoreconocido = transform.gameObject;


    }
    void Deselected()
    {
        if (ultimoreconocido)
        {
            ultimoreconocido.GetComponent<Renderer>().material.color = Color.white;
            ultimoreconocido = null;
            Panel_Guayaba_infect.SetActive(false); 
            Panel_selected.SetActive(false);
        }
    }
    private void OnGUI()
    {
        Rect rect = new Rect(Screen.width / 2, Screen.height / 2, puntero.width, puntero.height);
        GUI.DrawTexture(rect, puntero); 
        
        if (ultimoreconocido)
        {
            TextDetect.SetActive(true);
        }
        else
        {
            TextDetect.SetActive(false);
           
        }

    }


}
