using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement1 : MonoBehaviour
{
    private CharacterController character;
    private Animator animator;
    private Vector3 input;

    private float speed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        input.Set(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        character.Move(input * Time.deltaTime * speed);
        character.Move(Vector3.down * Time.deltaTime);

        if(input != Vector3.zero) 
        {
            animator.SetBool("walking", true);
            transform.forward = Vector3.Slerp(transform.forward, input, Time.deltaTime * 10);
        

        }
        else
        {
            animator.SetBool("walking", false);

        }
    }
}
