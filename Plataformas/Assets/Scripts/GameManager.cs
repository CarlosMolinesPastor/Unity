using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//karlinux - Carlos Molines Pastor
public class GameManager : MonoBehaviour
{
    //Creamos una instancia de la clase GameManager, publica y estatica 
    //para poder acceder desde cualquier sitio
    public static GameManager instance;
    public int puntos = 0;
    public int vidas = 3;
    public int nivelActual = 1;
    public int nivelMasAlto = 2;
    
    SceneController sceneController;
    
    //Creamos una variable publica para saber si el juego esta en progreso
    public bool gameInProgress;

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
        //Iniciamos el juego, con los pickups a 0, las vidas a 3 y el juego en progreso
        sceneController = FindObjectOfType<SceneController>();
        puntos = 0;
        vidas = 3;

        gameInProgress = true;
    }

    // Creamos un metodo para acabar el juego
    public void EndGame()
    {
        gameInProgress = false;
    }

    public void PerderVida()
    {
        Player player;

        vidas--;
        player = FindObjectOfType<Player>();
        if (vidas >0)
        {
            player.Recolocar();
            Debug.Log("Quedan " + vidas + " vidas.");
            sceneController.StartCoroutine("Mensaje");
        }
        else
        {
            //Indicamos que la partida ha terminado gameInProgress = false
            EndGame();
            Debug.Log("Partida terminada");
            sceneController.StartCoroutine("VolverAlMenuPrincipal");
        }
    }



}
