using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterMovement : MonoBehaviour
{
    private CharacterController characterController;
    private Animator animator;
    private Rigidbody rb;
    private Vector3 move = Vector3.zero;
    public new Transform camera;
    public LogicaCabezaOr logicaCabezaOr;
    public GameObject cabeza;

    public float velocidad = 10.0f;
    public float velocidadInicial;
    public float velocidadAgachado;
    public float gravedad;
    bool isJump = false;
    public float jumpForce = 20.0f;
    bool floorDetect = false;
    public float height;
    public Vector3 center;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        characterController.height = 1.86f;
        characterController.center = new Vector3(0,1);
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        velocidadInicial = velocidad;
        velocidadAgachado = velocidadInicial * 0.3f;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        Vector3 movimiento = Vector3.zero;
        float movimientoSpeed = 0;

        if (hor != 0 || ver != 0)
        {
            Vector3 forward = camera.forward;
            forward.y = 0;
            forward.Normalize();

            Vector3 right = camera.right;
            right.y = 0;
            right.Normalize();

            Vector3 direction = forward * ver + right * hor;
            movimientoSpeed = Mathf.Clamp01(direction.magnitude);
            direction.Normalize();

            movimiento = direction * velocidad * movimientoSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.2f);

        }

        movimiento.y -= gravedad * Time.deltaTime;

        characterController.Move(movimiento);
        animator.SetFloat("Speed", movimientoSpeed);

        Vector3 floor = characterController.transform.TransformDirection(Vector3.down);

        if (characterController.isGrounded)
        {
            
            move = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));

            if (Input.GetButtonDown("Jump") && animator.GetBool("isCrouch") == false)
            {
                move.y = jumpForce;
                animator.SetBool("isJump", true);
            }
            else
            {
                animator.SetBool("isJump", false);
            }

            if (Input.GetKey(KeyCode.LeftControl))
            {
                animator.SetBool("isCrouch", true);
                velocidad = velocidadAgachado;
                characterController.height = 1.0f;
                characterController.center = new Vector3(0, 0.5f);
                cabeza.SetActive(true);
                
            }
            if(Input.GetKeyUp(KeyCode.LeftControl))
            {
                if(logicaCabezaOr.contadorcolision <= 0)
                {
                    animator.SetBool("isCrouch", false);
                    velocidad = velocidadInicial;
                    characterController.height = 1.86f;
                    characterController.center = new Vector3(0, 1);
                    cabeza.SetActive(false);
                }
                
            }

        }
        move.y -= gravedad * Time.deltaTime;

        characterController.Move(move * Time.deltaTime);
    }


}
