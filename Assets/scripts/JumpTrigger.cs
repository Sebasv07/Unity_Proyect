using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    //player
    playercontroller player;
    void Update()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<playercontroller>();
    }
    private void OnTriggerEnter(Collider other)
    {
       
    }
    private void OnTriggerExit(Collider other)
    {
        
        
    }

}
