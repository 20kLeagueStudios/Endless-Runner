using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float runSpeed = 3f;
    [SerializeField] float jumpHeight=2;

    float gravity = -50;
    CharacterController characterController;
    Vector3 velocity;

    public bool isGrounded;

    float horizontalInput;


    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = 1;

        //verso dove guarda il player
        transform.forward = new Vector3(horizontalInput, 0 , Mathf.Abs(horizontalInput)-1); //mathf.abs = valore assoluto, cioè senza segno (i valori negativi diventano positivi)

        //alternativa al raycast per verificare se tocchiamo il suolo
        isGrounded = Physics.CheckSphere(transform.position, 0.1f, groundLayer, QueryTriggerInteraction.Ignore); //ignore per ignorare i trigger dei collider

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = 0;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        if(isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        characterController.Move(new Vector3(0, 0, horizontalInput * runSpeed) * Time.deltaTime);
     
        //velocità verticale
        characterController.Move(velocity * Time.deltaTime);
    }

}
