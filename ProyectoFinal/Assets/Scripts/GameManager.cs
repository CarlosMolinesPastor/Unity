using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
// Karlinux -  Carlos Molines Pastor
public class GameManager : MonoBehaviour
{

    //Instancia de GmaeManager
    public static GameManager instance;
    //Vida del player
    public float vidaPlayer = 100f;

    //Juego en progreso
    public bool gameInProgress;
    //Controlador de escena
    SceneController sceneController;

    //Danio al player
    public int cantidadDanio = 10;
    public float tiempoDanio = 0.225f;
    float tiempoActualDanio;

    //Puntos 
    public int puntos = 0;


    // Muy importante para que no se destruya el objeto al cambiar de escena
    void Awake()
    {
        //Si la instancia es nula
        //La instancia es esta
        if (instance == null)
        {
            instance = this;
            //Imopotante para que no se destruya el objeto al cambiar de escena
            DontDestroyOnLoad(gameObject);
        }
        //Si no destruimos el objeto
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    public void Start()
    {
        //Iniciamos el juego, con los puntos a 0,  el juego en progreso y instanciando el controlador de escena
        sceneController = FindObjectOfType<SceneController>();
        //Iniciamos el juego
        gameInProgress = true;
        //Ponemos los puntos a 0
        puntos = 0;
        //Vida del player
        vidaPlayer = 100f;
        cantidadDanio = 10;
        tiempoDanio = 0.225f;
        
    }

    // Creamos un metodo para indicar que el juego ha terminado
    public void EndGame()
    {
        //Acaba el juego
        gameInProgress = false;
    }


    // Update is called once per frame
    void Update()
    {
        //Para que no pase de 100 de vida, es decir vida infinita
        vidaPlayer = Mathf.Clamp(vidaPlayer, 0f, 100f);
        // Si la escena actual es la escena del menú
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            // Desbloquea el cursor para que pueda moverse libremente
            Cursor.lockState = CursorLockMode.None;
            // Hace que el cursor sea visible
            Cursor.visible = true;
        }
        else
        {
            // Bloquea el cursor para que no pueda moverse libremente
            Cursor.lockState = CursorLockMode.Locked;
            // Hace que el cursor no sea visible
            Cursor.visible = false;
        }
    }
    //Metodo para restar la vida
    public void restarVida()
    {
        //Restamos la vida al player al tiempo actual de danio le vamos sumando el tiempo actual
        tiempoActualDanio += Time.deltaTime;
        // Si el tiempo actual de danio es mayor o igual que el tiempo de danio le quitamos la vida
        // y reiniciamos el tiempo
        if (tiempoActualDanio >= tiempoDanio)
        {
            vidaPlayer -= cantidadDanio;
            tiempoActualDanio = 0f;
        }
    }
}
