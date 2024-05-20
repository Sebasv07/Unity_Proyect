using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptDoor2 : MonoBehaviour
{

    public bool OpenDoor = false;  //si esta abierta o cerrada
    public float DoorOpenAngle = 95f;  //apertura
    public float DoorCloseAngle = 0.0f;  //cerrar
    public float Smooth = 3.0f; //velocidad

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
            Quaternion targetRotation = Quaternion.Euler(0, DoorOpenAngle, -270);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, Smooth * Time.deltaTime);
        }
        else
        {
            Quaternion targetRotation2 = Quaternion.Euler(0, -180, DoorCloseAngle);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation2, Smooth * Time.deltaTime);
        }
    }
    
}
