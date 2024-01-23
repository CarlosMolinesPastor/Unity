using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

//Karlinux -  Carlos Molines Pastor
public class Player : MonoBehaviour
{
    //Variable publica para la velocidad del jugador
    public float speed = 10f;
    //Creamos los textos
    public Text countText;
    //Creamos el texto de ganar

    public Text livesText;
    //Creamos el texto para el tiempo
    public Text timeText;
    private int tiempoRestante = 30;
    private float contadorSegundos = 0;
    //Variable publica para la fuerza del salto
    public float fuerzaSalto = 10f;
    //Variable publica para el rigidbody del jugador
    private Rigidbody rb;
    //Creamos los audios en un array
    private AudioSource[] audios;
    //Creamos un array de materiales con un indice que nos 
    //ayuda a llevar la cuenta de ese array
    public Material[] materiales;
    private int indiceMaterial = 0;
    // Definimos los límites de tamaño
    float minSize = 0.5f;
    float maxSize = 1.4f;
    // Start is called before the first frame update
    void Start()
    {
        
        //Recogemos el rigidbody del jugador
        rb = GetComponent<Rigidbody>();
        //Iniciamos el audio
        audios = GetComponents<AudioSource>();
        //Si la escena es la escena final el tiempo restante es 45 segundos ya que es mas dificil
        if(SceneManager.GetActiveScene().name == "Final"){
            tiempoRestante = 45;
        }
        //Textos a 0
        SetCountText();
        SetLivesText();
    }

    // Update is called once per frame
    void Update()
    {
        // Si la posicion del jugador es -1 en y desaparece
        if(transform.position.y < -1){
            //Reproducimos el audio de caida al vacio
            audios[1].Play();
            Debug.Log("Caida al vacio");
            //Desactivamos el jugador
            GetComponent<Renderer>().enabled = false;
            //Poner al jugador en el centro
            transform.position = new Vector3(0, 0.4f, 0);
            //Activamos el jugador
            GetComponent<Renderer>().enabled = true;
            //Restar una vida
            GameManager.instance.playerLives--;

        }
        //Si pulsamos espacio y la velocidad en y es menor que 0.1 es decir que no estamos saltando
        if(Input.GetButtonDown("Jump") && Mathf.Abs(rb.velocity.y) < 0.1f){
            //Saltamos
            rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
        }
        //Metodo para controlar el tiempoi por segundos, como vamos en el update, es decir por cada frame lo
        //multiplicamos por el tiempo que tarda en ejecutarse cada frame y si es mayor que 1 segundo, al tiempo restante
        //que es un entero le restamos 1 y ponemos el contador de segundos a 0
        contadorSegundos += Time.deltaTime;
        if(contadorSegundos >= 1){
            tiempoRestante--;
            contadorSegundos = 0;
        }
        //Actualizamos el texto del tiempo
        timeText.text = "Tiempo: " + tiempoRestante.ToString();
        if(tiempoRestante <= 0 && GameManager.instance.gameInProgress == true){
            tiempoRestante = 0;
            GameManager.instance.playerLives = 0;

        }
        //Actualizamos el texto de las vidas
        SetLivesText();
        //Actualizamos el texto del contador de pickups
        SetCountText();
        //Vemos la posicion en cada momento
        Debug.Log(transform.position);
        //Vemos si esta activo
        Debug.Log(gameObject.activeSelf);
    }
    // Los movimientos físicos se hacen el FixedUpdate, con intervalos fijos.
    // No se pueden hacer en el Update.
    // El intervalo es 0.02 segundos definido en Edit->Project Settings->Time
    void FixedUpdate()
    {
        //Recogemos los valores de los ejes X y Z
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        //Creamos un vector para el movimiento
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        //Añadimos una fuerza al jugador
        rb.AddForce(movement * speed);
    }
    //Cuando colisiona con un trigger si es un pickup lo desactivamos y sumamos uno al contador
    void OnTriggerEnter (Collider other) {
        if(other.tag == "pickup"){
            other.gameObject.SetActive(false);
            GameManager.instance.pickupsCount++;
        }
    
    }
    //Cuando colisiona con un muro cambiamos el material del jugador y reproducimos el audio de choque
    void OnCollisionEnter(Collision collision){
        if(collision.gameObject.tag == "muro" || collision.gameObject.tag == "MuroInterior"){
            audios[0].Play();
            //Cambiamos el material del jugador, añadimos el indice y lo dividimos
            //entre el numero de materiales, con lo que conseguimos que el indice
             //vuelva a 0 cuando llegue al numero de materiales. Asi creamos un bucle
            GetComponent<Renderer>().material= materiales[indiceMaterial++];
            indiceMaterial %= materiales.Length;

        }
        if (collision.gameObject.tag == "muro")
        {
            collision.gameObject.transform.localScale -= new Vector3(0, 2f, 0);
            if (collision.gameObject.transform.localScale.y <= 0)
            {
                collision.gameObject.SetActive(false);
            }
        }
        if (collision.gameObject.name == "MuroFondo" || collision.gameObject.name == "MuroDelantero1" 
        || collision.gameObject.name == "MuroDelantero2"){
            // Reducimos el tamaño del jugador
            transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
            // Nos aseguramos de que el jugador no sea más pequeño que el tamaño mínimo
            if (transform.localScale.x < minSize)
            {
                transform.localScale = new Vector3(minSize, minSize, minSize);
            }
        } else if (collision.gameObject.name == "MuroDerecha" || collision.gameObject.name == "MuroIzq")
        {
            // Aumentamos el tamaño del jugador
            transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
            // Nos aseguramos de que el jugador no sea más grande que el tamaño máximo
            if (transform.localScale.x > maxSize)
            {
                transform.localScale = new Vector3(maxSize, maxSize, maxSize);
            }
        }
        if (collision.gameObject.tag == "Enemigo")
        {
            //Restamos una vida
            GameManager.instance.playerLives--;
        }
    }
    void SetCountText()
    {
        // Actualiza el texto contador
        countText.text = "Count: " + GameManager.instance.pickupsCount.ToString();
        // Si hemos recogido 12 pick-ups ganamos, desactivamos el jugador.
        if (GameManager.instance.pickupsCount == 12)
        {
            gameObject.SetActive(false);
        }
    }

    void SetLivesText()
    {
        // Actualiza el texto contador
        livesText.text = "Vidas: " + GameManager.instance.playerLives.ToString();

        //Si las vidas es 0, desactivamos el jugador y llamamos al metodo EndGame
        if (GameManager.instance.playerLives == 0)
        {
            GameManager.instance.EndGame();
            gameObject.SetActive(false);
        }
    }
}
