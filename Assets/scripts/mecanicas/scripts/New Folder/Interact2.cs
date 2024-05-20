using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Interact2 : MonoBehaviour
{
    LayerMask mask;
    public float distancia = 10f;

    public Texture2D puntero;
    public GameObject TextDetectFalse;
    GameObject ultimoreconocido = null;
    public GameObject Panel_Guayaba_nomadurada;



    // Start is called before the first frame update
    void Start()
    {
        mask = LayerMask.GetMask("raycast detect");
        TextDetectFalse.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        //raycast
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distancia, mask))
        {
            //aqui se agregan los tipos de colores que se quieran que tengan lso objetos con sus tag
            Deselected();
            if (hit.collider.tag == "no_guayaba")
            {
                Panel_Guayaba_nomadurada.SetActive(true);
                SelectedObjectNG(hit.transform);
            }
           

        }
        else
        {
            Deselected();
        }


    }
    private void SelectedObjectNG(Transform transform)
    {
        transform.GetComponent<MeshRenderer>().material.color = Color.red;
        ultimoreconocido = transform.gameObject;
    }
    
    void Deselected()
    {
        if (ultimoreconocido)
        {
            ultimoreconocido.GetComponent<Renderer>().material.color = Color.white;
            ultimoreconocido = null;
            Panel_Guayaba_nomadurada.SetActive(false);
}
    }
    
    


}