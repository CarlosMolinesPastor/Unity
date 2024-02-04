using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//karlinux - Carlos Molines Pastor
public class Player : MonoBehaviour
{
    //Variable de velocidad
    public float speed = 4;
    //Variable de velocidad inicial
    public float initialSpeed = 4;
    //Variable de referencia al componente Rigidbody2D
    private Rigidbody2D rb;
    //Variable de referencia al componente Animator
    private Animator anim;
    //Variable de referencia al componente SpriteRenderer
    private SpriteRenderer sprite;
    //Variable de movimiento
    private float moveInput;
    //Variable de fuerza en el salto
    public float jumpForce = 4;
    //Variable boolenaa de si esta en el suelo
    public bool isGrounded;
    //Variable de tiempo de salto
    public float jumpTime = 0.3f;
    //Variable de tiempo de salto
    private float jumpTimeCounter;
    //Variable de si esta saltando
    public bool isJumping;
    //Variable de posiciones iniciales
    private float xInicial, yInicial;
    //Variable para el controlador de escena
    private SceneController sceneController;

  


    // Use this for initialization
    void Start()
    {
        //Recogemos el controlador de escena
        sceneController = FindObjectOfType<SceneController>();
        //Recogemos el componente Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
        //Recogemos el componente Animator
        anim = GetComponent<Animator>();
        //Recogemos el componente SpriteRenderer
        sprite = GetComponent<SpriteRenderer>();
        //Le decimos que la velocidad inicial es la velocidad
        initialSpeed = speed;
        //Indicamos las posiciones iniciales.
        xInicial = transform.position.x;
        yInicial = transform.position.y;

    }

    //Fixed Update se ejecuta cada cierto tiempo mas habitualmente que update por ello se usa para fisicas
    void FixedUpdate()
    {
        //Guardamos en la variable moveInput el valor del eje horizontal
        moveInput = Input.GetAxis("Horizontal");
        //Si moveInput es mayor que 0 es que nos movemos a la derecha
        if (moveInput > 0)
        {
            //Indicamos que no esta volteado
            sprite.flipX = false;
            //Indicamos que esta corriendo y que active la animacion
            anim.SetBool("Run", true);
        }
        //Si moveInput es menor que 0 es que nos movemos a la izquierda
        else if (moveInput < 0)                             //moving towards left side
        {
            //Indicamos que esta volteado
            sprite.flipX = true;
            //Indicamos que esta corriendo y que active la animacion
            anim.SetBool("Run", true);
        }
        //Si moveInput es igual que 0 es que no nos movemos y por lo tanto no corremos.
        else
            anim.SetBool("Run", false);

        //Indicamos que la velocidad del rigidbody es igual a la velocidad por el eje horizontal
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    }

    private void Update()
    {
        //Aqui se hace el salto primero chequeamos si estamos en el suelo mediante un rayo sobre el suelo
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(0, -1), Mathf.Infinity, LayerMask.GetMask("Ground"));
        //Chequeamos que el rayo no sea nulo
        if (hit.collider != null)
        {
            //Recogemos la altura del personaje
            float alturaPlayer = GetComponent<Collider2D>().bounds.size.y;
            //Calculamos la distancia entre el personaje y el suelo
            float distanciaAlSuelo = hit.distance;
            //Si la distancia es menor que la altura del personaje, es que esta en el suelo
            isGrounded = distanciaAlSuelo < alturaPlayer;
        }else
        {
            isGrounded = false;
        }

        //Una vez que sabemos si esta en el suelo o no, hacemos el salto
        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            //Indicamos que esta saltando y que active la animacion
            isJumping = true;
            //Indicamos que el contador deltiempo de salto es el tiempo de salto
            jumpTimeCounter = jumpTime;
            //Indicamos que la velocidad del rigidbody es igual al vector hacia arriba por la fuerza del salto
            rb.velocity = Vector2.up * jumpForce;
            //Indicamos que esta saltando y que active la animacion
            anim.SetBool("Jump", true);
        }
        //Si no le indicamos que no esta saltando y que no active la animacion
        else
        {
            anim.SetBool("Jump", false);
        }

        //Si esta saltando y mantenemos la tecla de salto pulsada limita la duración del salto
        if (Input.GetKey(KeyCode.Space) && isJumping == true)
        {
            //Si el contador del tiempo de salto es mayor que 0
            if (jumpTimeCounter > 0)
            {
                //Indicamos que la velocidad del rigidbody es igual al vector hacia arriba por la fuerza del salto
                rb.velocity = Vector2.up * jumpForce;
                //Y vamos reduciendoel tiempo del salto desde el ultimo frame
                jumpTimeCounter -= Time.deltaTime;
            }
            else                                        
            {
                isJumping = false;                      
            }
        }
        //Si no pulsamos la tecla de salto indicamos que no esta saltando
        if (Input.GetKeyUp(KeyCode.Space))              
        {
            isJumping = false;                          
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Si colisiona el jugador con el suelo le indicamos que la velocidad es la velocidad inicial,
        //pues ocurria que cuando chocaba con el suelo la velocidad volaba
        if (collision.gameObject.tag == "ground")
        {
            speed = initialSpeed;
            return;
        }


         
        //Devolvemos la velocidad a 0 cuando choca contra los laterales del suelo, eso era porque se quedaba enganchado
        // por las fuerzas aplicadcas.
        if (collision.contacts[0].normal != Vector2.up && !isGrounded)
        {
            speed = 0;
        }
        else
        {
            speed = initialSpeed;
        }
        //Si colisiona contra el enemigo perdemos la vida instanciamos a gamemanager
        if (collision.gameObject.tag == "enemigo")
        {
            GameManager.instance.PerderVida();
            Debug.Log("Enemigo");
        }

    }
    //Cuando salimos de las colisiones igualamos la velocidfad a la veloicidad inicial 
    private void OnCollisionExit2D(Collision2D collision)
    {
        speed = initialSpeed;

    }

    //Funcion que se llama cuando el player choca contra enemigo y poerdemos vida o la llave
    // que llalamos al ControlPartida del sceneController
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "enemigo")
        {
            GameManager.instance.PerderVida();
        }
        if (other.gameObject.tag == "llave")
        {
            sceneController.ControlPartida();
        }
    }
    //Funcion para recolocar al jugador
    public void Recolocar()
    {
        transform.position = new Vector3(xInicial, yInicial, 0);
    }
}
