using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputController))]
public class Blockcontroller : MonoBehaviour
{
    [SerializeField] float _speed = 0f;
    [SerializeField] float _rotationSpeed = 0f;

    float velocidadInicial;
    float velocidadAgachado;
    float velocidadsaltando;

    InputController _inputController = null;
    Animator anim;
    Rigidbody rb;
    CapsuleCollider cc;
    public LogicaCabeza logicaCabeza;
    public GameObject cabeza;
    public float movementSpeed = 0f;
    public float jumpForce = 8f;
    bool canJump = false;
    public float gravedad;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        cc = GetComponent<CapsuleCollider>();
        velocidadInicial = _speed;
        velocidadAgachado = _speed * 0.3f;
        velocidadsaltando = _speed * 0.5f;
    }

    
    private void Awake()
    {
        _inputController = GetComponent<InputController>();
    }
    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector2 input = _inputController.MoveInput();
        movementSpeed = Mathf.Clamp01(_inputController.MoveInput().magnitude);
        float x = input.x;
        float y = input.y;
        anim.SetFloat("SpeedX", x);
        anim.SetFloat("SpeedY", y);
        transform.position += transform.forward * y * _speed * Time.deltaTime;
        transform.position += transform.right * x * _speed * Time.deltaTime;

        //transform.Rotate(Vector3.up * input.x * _rotationSpeed * Time.deltaTime);
        if (Input.GetButtonDown("Jump") && canJump)
        {
            rb.velocity += (Vector3.up * jumpForce);
            rb.velocity -= (Vector3.down * gravedad);
            anim.SetBool("isJumpB", true);
            anim.SetBool("Crouch", false);
            transform.position += transform.forward * y * _speed * Time.deltaTime;
            transform.position += transform.right * x * _speed * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            anim.SetBool("Crouch", true);
            _speed = velocidadAgachado;
            cc.height = 1.0f;
            cc.center = new Vector3(0, -0.5f);
            cabeza.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            if (logicaCabeza.contadorcolision <= 0)
            {
                anim.SetBool("Crouch", false);
                _speed = velocidadInicial;
                cc.height = 2.0f;
                cc.center = new Vector3(0, 0);
                cabeza.SetActive(false);
            }
            
        }
        


    }

    private void OnCollisionEnter(Collision collision)
    {
         if(collision.transform.tag == "Ground")
        {
            canJump = true;
            anim.SetBool("isJumpB", false);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Ground")
        {
            canJump = false;
            anim.SetBool("isJumpB", true);
        }
    }

}
