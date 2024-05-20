using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class probe : MonoBehaviour
{

    public float movementSpeed = 5f;
    public float rotationSpeed = 3f;
    public float jumpForce = 5f;
    public Transform cameraShoulder;
    public Transform cameraHolder;

    private Rigidbody rb;
    private Animator anim;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // Movimiento del personaje
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        if (movementDirection != Vector3.zero)
        {
            // Rotar hacia la dirección de movimiento
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Mover en la dirección de movimiento
            Vector3 targetVelocity = transform.forward * movementSpeed;
            targetVelocity.y = rb.velocity.y; // Mantener la velocidad vertical
            rb.velocity = targetVelocity;

            // Animación de movimiento
            anim.SetFloat("Speed", movementDirection.magnitude);
        }
        else
        {
            // Detener la animación si no hay movimiento
            anim.SetFloat("Speed", 0f);
        }

        // Salto
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        // Verificar si el personaje está en el suelo
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.1f);
    }

    void LateUpdate()
    {
        // Control de la cámara
        cameraShoulder.localRotation = Quaternion.identity; // Mantener el hombro de la cámara fijo
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f); // Mantener la rotación del personaje solo en Y
        cameraHolder.rotation = Quaternion.Euler(0f, cameraHolder.rotation.eulerAngles.y, 0f); // Mantener la rotación del holder solo en Y
    }
}
