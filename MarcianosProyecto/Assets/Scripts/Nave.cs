using System.Collections;
//using System.Collections.Generic;
//using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Carlos Molines Pastor: karlinux

public class Nave : MonoBehaviour
{
    // Variablesariable float velocidad
    [SerializeField] float velocidad = 2f;
    //Disparo
    [SerializeField] GameObject prefabDisparoNAve;
    //Explosion
    [SerializeField] GameObject prefabExplosion;

    //Vamos a controlar el tiempo entre disparos ya que dispara muy seguido
    [SerializeField] float tiempoEntreDisparos = 0.3f;
    private float tiempoDesdeElEltimoDisparo = 3f;

    //Texto de Game Over
    [SerializeField] Text textoGameOver;

    //Sonido
    [SerializeField] AudioClip sonidoJuego; //Lo establezco en la nave asi al ser destruida desaparece el sonido del juego

    //AudioSource
    private AudioSource audioSource;

    //Vidas
    [SerializeField] Text textoVidas;
    //Puntos
    [SerializeField] Text textoPuntos;

    // Start is called before the first frame update
    void Start()
    {
        //Inicializamos el sonido
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = sonidoJuego;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //#############Movimiento#############
        // Calcula la entrada del usuario
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        // Calcula la dirección del movimiento
        Vector3 movimiento = new Vector3(horizontalInput, verticalInput, 0f);
        // Normaliza la dirección del movimiento para mantener una velocidad constante
        movimiento.Normalize();
        // Actualiza la posición de la nave
        transform.Translate(movimiento * velocidad * Time.deltaTime);

        //#############Limites#############
        // Calcula la nueva posición de la nave, con Mathf.Clamp para que no se salga de la pantalla
        float newX = Mathf.Clamp(transform.position.x + Input.GetAxis("Horizontal") * velocidad * Time.deltaTime, -8.5f, 8.5f);
        float newY = Mathf.Clamp(transform.position.y + Input.GetAxis("Vertical") * velocidad * Time.deltaTime, -4, 4);
        // Actualiza la posición de la nave
        transform.position = new Vector3(newX, newY, transform.position.z);

        //#############Disparo#############
        //Disparo: Controlamos el tiempo desde el último disparo lo incrementamos por Time.deltaTime cada frame
        // y si se presiona el botón de disparo y el tiempo desde el último disparo es mayor que el tiempo entre disparos
        // Instanciamos el disparo y  reestablecemos el tiempo desde el último disparo a 0
        tiempoDesdeElEltimoDisparo += Time.deltaTime;
        //Si se pulsa el boton de disparo y el tiempo desde el ultimo disparo es mayor que el tiempo entre disparos y 
        //el juego esta en progreso
        if (Input.GetButton("Fire1") && tiempoDesdeElEltimoDisparo > tiempoEntreDisparos && GameManager.instance.gameInProgress)
        {
            //Instanciamos el disparo
            Instantiate(prefabDisparoNAve, transform.position, Quaternion.identity);
            //Reestablecemos el tiempo desde el ultimo disparo a 0
            tiempoDesdeElEltimoDisparo = 0f;
        }
        //#############Vidas#############
        //Actualizamos el texto de las vidas
        textoVidas.text = "Vidas: " + GameManager.instance.naveLifes;
        //#############Puntos#############
        //Actualizamos el texto de los puntos
        textoPuntos.text = ScoreManager.instance.score.ToString();

    }
    //Funcion de colision
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Si colisiona con un asteroide o una nave enemiga entonces se elimina la nave y el asteroide o la nave enemiga 
        if (other.tag == "Asteroide" || other.tag == "NaveEnemiga" || other.tag == "Jefe")
        {
            //EXplosion. Instanciamos a la explosion
            Transform explosion = Instantiate(prefabExplosion, other.transform.position, Quaternion.identity).transform;
            // El otro objeto, es decir el asteroide no lo destruimos Destroy(other.gameObject);
            Destroy(explosion.gameObject, 1f);
            //Destruimos el objeto con el que colisiona
            Destroy(other.gameObject);
            //Ponemos las vidas a 0
            GameManager.instance.naveLifes = 0;
            //Llamamos a la corutina MainMenu
            StartCoroutine(MainMenu());

        }
        //Si colisiona con un disparo enemigo o con un disparo del jefe
        if (other.tag == "DisparoEnemigo" || other.tag == "DisparoJefe")
        {
            //Restamos una vida del GameManager
            GameManager.instance.naveLifes--;
            //Si las vidas son mayores que 0
            if (GameManager.instance.naveLifes > 0)
            {
                //Destruimos el disparo enemigo
                Destroy(other.gameObject);
            }
            //Si no destruimos la nave
            else
            {
                //Instanciamos la explosion
                Transform explosion = Instantiate(prefabExplosion, transform.position, Quaternion.identity).transform;
                //Destruimos la explosion
                Destroy(explosion.gameObject, 1f);
                //Destruimos el disparo enemigo
                Destroy(other.gameObject);
                //Ponemos las vidas a 0
                GameManager.instance.naveLifes = 0;
                //Llamamos a la corutina MainMenu
                StartCoroutine(MainMenu());
            }
        }
    }
    //Funcion de corutina para el Game Over
    IEnumerator MainMenu()
    {
        //Texto de Game Over
        textoGameOver.text = "GAME OVER";
        //Indicamos que el juego ha terminado para gameInProgress sea false
        GameManager.instance.EndGame();
        //Desactivamos el collider y el renderer de la nave para que no colisione y no se vea
        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        //Esperamos 4 segundos y destruimos la nave
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
        //Cargamos la escena Menu
        SceneManager.LoadScene("Menu");
    }
}
