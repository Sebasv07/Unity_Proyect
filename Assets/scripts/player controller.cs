using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class playercontroller : MonoBehaviour
{
    //probe mano
    



    //player
    Transform tr;
    Rigidbody rb;

    public float walkSpeed = 350;
   
    

    bool isJump = false;
    public float JumpForce = 5;

    bool floorDetected = false;

    //correr
    public int VelRun;
    public float x, y;

    //camara 3D
    public Transform cameraShoulder;  //eje de la camara
    public Transform cameraHolder; //posicion y rotacion de camara segun el personaje
    private Transform cam;

    private float rotY = 0f;

    public float RotationSpeed = 200;
    public float minAngle = -49;
    public float maxAngle = 30;
    public float cameraSpeed = 200;

    //animations

    public Animator anim;

    private Vector2 animSpeed;

    private GameObject pickedObject = null;



    // Start is called before the first frame update
    void Start()
    {
        tr = this.transform;
        rb = GetComponent<Rigidbody>();

        anim = GetComponentInChildren<Animator>();

        cam = Camera.main.transform;

    }

    // Update is called once per frame
    void Update()
    {
        MoveControl();
        CameraControl();
        AnimControl();

        ActionsControl();
        
        

    }
   
    public void ActionsControl()
    {
        Vector3 floor = transform.TransformDirection(Vector3.down);

        
        if (Input.GetKey(KeyCode.R) && pickedObject != null)
        {
            anim.SetBool("Agarrar", false);
            pickedObject = null;
        }
        if (Physics.Raycast(transform.position, floor, 0.5f))
        {
            floorDetected = true;
            print("contacto con el suelo");
        }
        else
        {
            floorDetected= false;
            print("no hay contacto con el suelo");
        }

        isJump = Input.GetButtonDown("Jump");
        if (isJump && floorDetected)
        {
            rb.AddForce(new Vector3(0, JumpForce, 0), ForceMode.Impulse);

        }
        

        if (Input.GetKey(KeyCode.LeftShift))
        {
            walkSpeed = VelRun;
            cameraSpeed = VelRun;

            
                anim.SetBool("run", true);
            
            
            
             
            
        }
        else
        {
            anim.SetBool("run", false);
            walkSpeed = 350;
            cameraSpeed = 350;

        }
    }

    public void MoveControl()
    {
        float deltaX = Input.GetAxis("Horizontal");
        float deltaZ = Input.GetAxis("Vertical");
        float deltaT = Time.deltaTime;

        animSpeed = new Vector2(deltaX, deltaZ);
        Vector3 side = walkSpeed * deltaX * deltaT * tr.right;
        Vector3 forward = walkSpeed * deltaZ * deltaT * tr.forward;

        Vector3 direction = side + forward;
        direction.y = rb.velocity.y;
        rb.velocity = direction;


    }
    public void CameraControl()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        float deltaT = Time.deltaTime;

        rotY += mouseY * RotationSpeed * deltaT;

        float rotX = mouseX * RotationSpeed * deltaT;

        tr.Rotate(0, rotX, 0);

        rotY = Mathf.Clamp(rotY, minAngle, maxAngle);

        Quaternion localRotation = Quaternion.Euler(-rotY, 0, 0);
        cameraShoulder.localRotation = localRotation;

        cam.SetPositionAndRotation(Vector3.Lerp(cam.position, cameraHolder.position, cameraSpeed * deltaT), Quaternion.Lerp(cam.rotation, cameraHolder.rotation, cameraSpeed * deltaT));

   
    }
    public void AnimControl()
    {
        anim.SetBool("floor", floorDetected);

        anim.SetFloat("X", animSpeed.x);
        anim.SetFloat("Y", animSpeed.y);

    }
   
}
