using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class recolector : MonoBehaviour
{
    public secondmision second;
   
    // Start is called before the first frame update
    void Start()
    {
        second = GameObject.FindGameObjectWithTag("Enemigo1").GetComponent<secondmision>();
        
    }

    // Update is called once per frame
    void Update()
    {
     
    }
    public void ActivateObject()
    {
        
        Destroy(gameObject);
    }
    public void OnTriggerEnter(Collider col)
    {
        if(col.tag == "recolector")
        {

            
            ActivateObject();
            
            
           
            
            
        }
    }
    
}
