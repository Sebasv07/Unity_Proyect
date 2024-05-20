using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptDoor : MonoBehaviour
{
    public bool OpenDoor = false;  //si esta abierta o cerrada
    public float DoorOpenAngle = 95f;  //apertura
    public float DoorCloseAngle = 0.0f;  //cerrar
    public float Smooth = 3.0f; //velocidad

    public AudioClip DoorOpen;
    public AudioClip DoorClose;
    public void ChangeDoorState()
    {
        OpenDoor = !OpenDoor;
    }

    void Start()
    {
        
    }
    void Update()
    {
        if (OpenDoor)
        {
            Quaternion targetRotation = Quaternion.Euler(270, DoorOpenAngle, -90);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, Smooth * Time.deltaTime);
        }
        else
        {
            Quaternion targetRotation2 = Quaternion.Euler(270, -0, DoorCloseAngle);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation2, Smooth * Time.deltaTime);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag== "TriggerDoor")
        {
            AudioSource.PlayClipAtPoint(DoorClose, transform.position, 2);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "TriggerDoor")
        {
            AudioSource.PlayClipAtPoint(DoorOpen, transform.position, 2);
        }
    }
}
